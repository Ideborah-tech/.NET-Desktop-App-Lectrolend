import { useCallback, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { api } from '../../api/index.js';
import { dayNumber, fmtDateTime, fmtRange, monthShort } from '../../utils/format.js';
import { Badge, Empty, Loading, PageHeader, useConfirm, useToast } from '../../components/ui/index.js';

const LIVE = ['BOOKED', 'ISSUED', 'OVERDUE'];

/** Booked -> Issued -> Returned progress, as in the reservation state diagram. */
function Steps({ status }) {
  const rank = { BOOKED: 0, ISSUED: 1, OVERDUE: 1, RETURNED: 2 }[status];
  if (rank === undefined) return null;
  const steps = ['Booked', status === 'OVERDUE' ? 'Overdue' : 'With you', 'Returned'];
  return (
    <div className="steps" aria-label={`Progress: ${status.toLowerCase()}`}>
      {steps.map((s, i) => {
        const state = i < rank || status === 'RETURNED' ? 'done' : i === rank ? (status === 'OVERDUE' ? 'late' : 'now') : '';
        return (
          <span key={s} style={{ display: 'contents' }}>
            {i > 0 && <span className={`step-line ${i <= rank ? 'done' : ''}`} />}
            <span className={`step ${state}`}><i>{state === 'done' ? '✓' : ''}</i><span>{s}</span></span>
          </span>
        );
      })}
    </div>
  );
}

export default function MyReservations() {
  const toast = useToast();
  const confirm = useConfirm();
  const [list, setList] = useState(null);
  const [tab, setTab] = useState('active');

  const load = useCallback(() => api.reservations.mine().then(setList).catch((e) => toast(e.message, 'error')), [toast]);
  useEffect(() => { load(); }, [load]);

  const cancel = async (r) => {
    const yes = await confirm({ title: 'Cancel this booking?', message: `Your reservation for ${r.equipment.name} will be released for others.`, confirmText: 'Cancel booking', cancelText: 'Keep booking' });
    if (!yes) return;
    try {
      await api.reservations.cancel(r.id);
      toast('Reservation cancelled');
      load();
    } catch (e) { toast(e.message, 'error'); }
  };

  const shown = (list || []).filter((r) => (tab === 'active') === LIVE.includes(r.status));
  const activeCount = list ? list.filter((r) => LIVE.includes(r.status)).length : 0;

  const cls = (s) => (s === 'RETURNED' ? 'pass-done' : s === 'CANCELLED' || s === 'EXPIRED' ? 'pass-dead' : s === 'OVERDUE' ? 'pass-late' : '');

  return (
    <>
      <PageHeader eyebrow="Bookings" title="My bookings">
        <Link to="/catalogue" className="btn btn-primary">Reserve something</Link>
      </PageHeader>

      <div className="chips">
        <button className={`chip ${tab === 'active' ? 'on' : ''}`} onClick={() => setTab('active')}>Active ({activeCount})</button>
        <button className={`chip ${tab === 'history' ? 'on' : ''}`} onClick={() => setTab('history')}>History</button>
      </div>

      {!list ? <Loading /> : shown.length === 0 ? (
        <Empty title={tab === 'active' ? 'No active bookings' : 'No history yet'}>
          {tab === 'active' ? 'Head to the catalogue to reserve something.' : 'Returned and cancelled bookings appear here.'}
        </Empty>
      ) : (
        <ul className="passes">
          {shown.map((r) => (
            <li key={r.id} className={`pass ${cls(r.status)}`}>
              <div className="pass-date"><b>{dayNumber(r.startTime)}</b><span>{monthShort(r.startTime)}</span></div>
              <div className="pass-main">
                <div className="pass-title">
                  <h3>{r.equipment.name}</h3>
                  <span className="tag">{r.equipment.assetTag}</span>
                  <Badge value={r.status} />
                </div>
                <p className="pass-meta">
                  {fmtRange(r.startTime, r.endTime)}{r.room ? ` · Room ${r.room.roomCode}` : ''} · #{r.id}
                </p>
                <Steps status={r.status} />
                {r.status === 'BOOKED' && (
                  <p className="muted small" style={{ marginTop: 10 }}>
                    Collect at the IT office before {fmtDateTime(new Date(new Date(r.startTime).getTime() + 30 * 60000))}.
                  </p>
                )}
                {r.status === 'OVERDUE' && <p className="danger small" style={{ marginTop: 10 }}>This item is overdue. Please return it to the IT office.</p>}
                {r.status === 'EXPIRED' && <p className="muted small">Not collected within 30 minutes, so the booking expired.</p>}
                {r.returnInfo && (
                  <p className="muted small" style={{ marginTop: 10 }}>
                    Returned {fmtDateTime(r.returnInfo.returnedAt)}{r.returnInfo.lateReturn ? ' (late)' : ''} &middot; {r.returnInfo.conditionOnReturn}
                  </p>
                )}
              </div>
              {r.status === 'BOOKED' && (
                <div className="pass-actions"><button className="btn btn-danger btn-sm" onClick={() => cancel(r)}>Cancel</button></div>
              )}
            </li>
          ))}
        </ul>
      )}
    </>
  );
}
