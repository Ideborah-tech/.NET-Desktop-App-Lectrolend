import { useCallback, useEffect, useState } from 'react';
import { api } from '../../api/index.js';
import { fmtDateTime, fmtRange } from '../../utils/format.js';
import { Badge, Empty, ErrorNote, Field, Loading, Modal, PageHeader, useConfirm, useToast } from '../../components/ui/index.js';

const FILTERS = ['ALL', 'BOOKED', 'ISSUED', 'OVERDUE', 'RETURNED', 'CANCELLED', 'EXPIRED'];

export default function ReservationsAdmin() {
  const toast = useToast();
  const confirm = useConfirm();
  const [list, setList] = useState(null);
  const [filter, setFilter] = useState('ALL');
  const [returning, setReturning] = useState(null);

  const load = useCallback(() => api.reservations.all().then(setList).catch((e) => toast(e.message, 'error')), [toast]);
  useEffect(() => { load(); }, [load]);

  const askCancel = async (r) => {
    const yes = await confirm({ title: 'Cancel this reservation?', message: `${r.equipment.name} booked by ${r.borrower.fullName} will be released.`, confirmText: 'Cancel reservation', cancelText: 'Keep it' });
    if (yes) act(() => api.reservations.cancel(r.id), 'Reservation cancelled');
  };

  const act = async (fn, okMsg) => {
    try { await fn(); toast(okMsg); load(); } catch (e) { toast(e.message, 'error'); load(); }
  };

  const shown = (list || []).filter((r) => filter === 'ALL' || r.status === filter);

  // one group per borrower per day, so a student's items booked together sit together
  const groups = [];
  const byKey = new Map();
  shown.forEach((r) => {
    const day = r.startTime.slice(0, 10);
    const key = `${r.borrower.email}|${day}`;
    let g = byKey.get(key);
    if (!g) { g = { key, day, borrower: r.borrower, rows: [] }; byKey.set(key, g); groups.push(g); }
    g.rows.push(r);
  });
  groups.forEach((g) => g.rows.sort((a, b) => new Date(a.startTime) - new Date(b.startTime)));

  const handOverAll = async (g) => {
    const todo = g.rows.filter((r) => r.status === 'BOOKED');
    try {
      for (const r of todo) await api.reservations.issue(r.id);
      toast(`${todo.length} ${todo.length === 1 ? 'item' : 'items'} handed over to ${g.borrower.fullName}`);
    } catch (e) { toast(e.message, 'error'); }
    load();
  };

  return (
    <>
      <PageHeader eyebrow="Bookings" title="Bookings" />
      <div className="chips">
        {FILTERS.map((f) => (
          <button key={f} className={`chip ${filter === f ? 'on' : ''}`} onClick={() => setFilter(f)}>
            {f === 'ALL' ? 'All' : f.charAt(0) + f.slice(1).toLowerCase()}
            {list && f !== 'ALL' ? ` (${list.filter((r) => r.status === f).length})` : ''}
          </button>
        ))}
      </div>

      {!list ? <Loading /> : groups.length === 0 ? <Empty title="No reservations here" /> : (
        <div className="groups">
          {groups.map((g) => {
            const waiting = g.rows.filter((r) => r.status === 'BOOKED').length;
            return (
              <section key={g.key} className="group">
                <header className="group-head">
                  <span className="avatar" aria-hidden="true">{g.borrower.fullName.charAt(0)}</span>
                  <div className="group-who">
                    <h2>{g.borrower.fullName}</h2>
                    <span>{g.borrower.email}</span>
                  </div>
                  <div className="group-day">
                    <b>{new Date(g.day).toLocaleDateString('en-GB', { weekday: 'long', day: 'numeric', month: 'long' })}</b>
                    <span>{g.rows.length} {g.rows.length === 1 ? 'item' : 'items'}</span>
                  </div>
                  {waiting > 1 && <button className="btn btn-primary btn-sm" onClick={() => handOverAll(g)}>Hand over all {waiting}</button>}
                </header>
                <ul className="group-rows">
                  {g.rows.map((r) => (
                    <li key={r.id} className="group-row">
                      <div className="g-item">
                        <strong>{r.equipment.name}</strong>
                        <span className="sub mono">#{r.id} · {r.equipment.assetTag}{r.room ? ` · ${r.room.roomCode}` : ''}</span>
                      </div>
                      <div className="g-when">
                        {fmtRange(r.startTime, r.endTime)}
                        {r.returnInfo && <span className="sub">Returned {fmtDateTime(r.returnInfo.returnedAt)}{r.returnInfo.lateReturn ? ' (late)' : ''} · {r.returnInfo.conditionOnReturn}</span>}
                      </div>
                      <Badge value={r.status} />
                      <div className="actions">
                        {r.status === 'BOOKED' && (
                          <>
                            <button className="btn btn-primary btn-sm" onClick={() => act(() => api.reservations.issue(r.id), 'Handed over')}>Hand over</button>
                            <button className="btn btn-sm" onClick={() => askCancel(r)}>Cancel</button>
                          </>
                        )}
                        {(r.status === 'ISSUED' || r.status === 'OVERDUE') && (
                          <button className="btn btn-primary btn-sm" onClick={() => setReturning(r)}>Mark returned</button>
                        )}
                      </div>
                    </li>
                  ))}
                </ul>
              </section>
            );
          })}
        </div>
      )}

      {returning && (
        <ReturnModal reservation={returning} onClose={() => setReturning(null)}
          onDone={() => { setReturning(null); toast('Return recorded'); load(); }} />
      )}
    </>
  );
}

function ReturnModal({ reservation: r, onClose, onDone }) {
  const [damaged, setDamaged] = useState(false);
  const [condition, setCondition] = useState('');
  const [remarks, setRemarks] = useState('');
  const [error, setError] = useState('');
  const [busy, setBusy] = useState(false);
  const late = new Date() > new Date(r.endTime);

  const submit = async (e) => {
    e.preventDefault();
    setBusy(true);
    setError('');
    try {
      await api.reservations.confirmReturn(r.id, { damaged, conditionOnReturn: condition, remarks });
      onDone();
    } catch (err) { setError(err.message); } finally { setBusy(false); }
  };

  return (
    <Modal title="Mark as returned" onClose={onClose}
      footer={(
        <>
          <button className="btn" type="button" onClick={onClose}>Cancel</button>
          <button className="btn btn-primary" form="return-form" disabled={busy}>{busy ? 'Saving...' : 'Confirm'}</button>
        </>
      )}>
      <form id="return-form" onSubmit={submit}>
        <p><strong>{r.equipment.name}</strong> <span className="tag">{r.equipment.assetTag}</span><br />
          <span className="muted">Borrowed by {r.borrower.fullName}, due {fmtDateTime(r.endTime)}</span></p>
        {late && <div className="form-error">This item is being returned late. It will be flagged on the record.</div>}
        <ErrorNote error={error} />
        <label className="check">
          <input type="checkbox" checked={damaged} onChange={(e) => setDamaged(e.target.checked)} />
          Item is damaged (send to maintenance)
        </label>
        <Field label="Condition on return" hint={damaged ? 'Describe the damage' : 'Leave blank for "Good"'}>
          <input value={condition} maxLength={100} onChange={(e) => setCondition(e.target.value)} />
        </Field>
        <Field label="Remarks (optional)"><textarea rows={3} value={remarks} onChange={(e) => setRemarks(e.target.value)} /></Field>
      </form>
    </Modal>
  );
}
