import { useCallback, useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { api } from '../../api/index.js';
import { tokenStore } from '../../api/http.js';
import { fmtDateTime, fmtRange, greeting, statusText } from '../../utils/format.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { Badge, Loading, useToast } from '../../components/ui/index.js';

const SLICES = [
  { key: 'AVAILABLE', text: 'Available', color: '#2c74b3' },
  { key: 'IN_USE', text: 'Borrowed', color: '#03346e' },
  { key: 'UNDER_MAINTENANCE', text: 'In repair', color: '#ffc93c' },
  { key: 'RETIRED', text: 'Retired', color: '#bcd9f0' },
];

/** Solid-colour ring chart made of SVG arcs (no gradients). */
function Ring({ slices, total, size = 150, stroke = 26, children }) {
  const r = (size - stroke) / 2;
  const c = 2 * Math.PI * r;
  let used = 0;
  return (
    <div className="ring" style={{ width: size, height: size }}>
      <svg viewBox={`0 0 ${size} ${size}`} width={size} height={size} role="img"
        aria-label={slices.map((s) => `${s.value} ${s.text}`).join(', ')}>
        <circle cx={size / 2} cy={size / 2} r={r} fill="none" stroke="#eef5fb" strokeWidth={stroke} />
        {slices.filter((s) => s.value > 0).map((s) => {
          const len = (s.value / total) * c;
          const el = (
            <circle key={s.key} cx={size / 2} cy={size / 2} r={r} fill="none" stroke={s.color} strokeWidth={stroke}
              strokeDasharray={`${len} ${c - len}`} strokeDashoffset={-used} transform={`rotate(-90 ${size / 2} ${size / 2})`} />
          );
          used += len;
          return el;
        })}
      </svg>
      <div className="ring-center">{children}</div>
    </div>
  );
}

export default function Dashboard() {
  const toast = useToast();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [report, setReport] = useState(null);
  const [reservations, setReservations] = useState([]);
  const [dlBusy, setDlBusy] = useState(false);

  const downloadReport = async () => {
    setDlBusy(true);
    try {
      const res = await fetch('/api/reports/download', {
        headers: { Authorization: `Bearer ${tokenStore.get()}` },
      });
      if (!res.ok) throw new Error(`Download failed (${res.status})`);
      const blob = await res.blob();
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `lectrolend-report-${new Date().toISOString().slice(0, 10)}.csv`;
      a.click();
      URL.revokeObjectURL(url);
    } catch (e) { toast(e.message, 'error'); }
    finally { setDlBusy(false); }
  };

  const load = useCallback(() => {
    Promise.all([api.reports.summary(), api.reservations.all()])
      .then(([r, res]) => { setReport(r); setReservations(res); })
      .catch((e) => toast(e.message, 'error'));
  }, [toast]);
  useEffect(() => { load(); }, [load]);

  if (!report) return <Loading />;

  const eq = report.equipmentByStatus;
  const rs = report.reservationsByStatus;
  const outNow = (rs.ISSUED || 0) + (rs.OVERDUE || 0);
  const byStart = (a, b) => new Date(a.startTime) - new Date(b.startTime);
  const toCollect = reservations.filter((r) => r.status === 'BOOKED').sort(byStart);
  const out = reservations.filter((r) => r.status === 'ISSUED' || r.status === 'OVERDUE')
    .sort((a, b) => (a.status === b.status ? byStart(a, b) : a.status === 'OVERDUE' ? -1 : 1));
  const today = new Date().toLocaleDateString('en-GB', { weekday: 'long', day: 'numeric', month: 'long' });
  const availableNow = eq.AVAILABLE || 0;
  const latest = [...reservations].sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt)).slice(0, 6);
  const max = Math.max(1, ...report.mostBorrowed.map((p) => p.count));
  const total = Math.max(1, report.totalEquipment);
  const slices = SLICES.map((s) => ({ ...s, value: eq[s.key] || 0 }));

  const handOver = async (r) => {
    try { await api.reservations.issue(r.id); toast('Handed over'); } catch (e) { toast(e.message, 'error'); }
    load();
  };

  const Lane = ({ title, hint, list, empty, action }) => (
    <section className="lane">
      <header className="lane-head"><h2>{title}</h2><span className="lane-count">{list.length}</span><p>{hint}</p></header>
      {list.length === 0 ? <p className="lane-empty">{empty}</p> : (
        <ul className="lane-list">
          {list.slice(0, 5).map((r) => (
            <li key={r.id} className={`lane-card ${r.status === 'OVERDUE' ? 'late' : ''}`}>
              <div className="lane-top">
                <strong>{r.equipment.name}</strong>
                <span className="tag">{r.equipment.assetTag}</span>
              </div>
              <p>{r.borrower.fullName}</p>
              <p className="muted small">{fmtRange(r.startTime, r.endTime)}</p>
              <div className="lane-foot">
                <Badge value={r.status} />
                {action(r)}
              </div>
            </li>
          ))}
        </ul>
      )}
    </section>
  );

  return (
    <>
      <section className="hero">
        <div className="hero-main">
          <p className="eyebrow">{today}</p>
          <h1>{greeting()}, {user.fullName.split(' ')[0]}.</h1>
          <p className="hero-sub">
            {toCollect.length === 0 ? 'Nothing is waiting at the desk.' : `${toCollect.length} ${toCollect.length === 1 ? 'booking is' : 'bookings are'} waiting to be collected.`}
            {(rs.OVERDUE || 0) > 0 && ` ${rs.OVERDUE} overdue.`}
          </p>
          <div className="hero-actions">
            <Link to="/admin/reservations" className="btn btn-sun">All bookings</Link>
            <Link to="/admin/equipment" className="btn btn-light">Equipment</Link>
            <button className="btn btn-light" onClick={downloadReport} disabled={dlBusy}>
              {dlBusy ? 'Preparing…' : '↓ Download report'}
            </button>
          </div>
        </div>
        <div className="hero-big"><b>{outNow}</b><span>out now</span></div>
        <ul className="hero-strip">
          <li><b>{rs.BOOKED || 0}</b><span>To collect</span></li>
          <li className={(rs.OVERDUE || 0) > 0 ? 'warn' : ''}><b>{rs.OVERDUE || 0}</b><span>Overdue</span></li>
          <li><b>{availableNow}</b><span>Free items</span></li>
          <li><b>{report.lateReturns}</b><span>Late returns</span></li>
        </ul>
      </section>

      <div className="lanes">
        <Lane title="To collect" hint="Waiting at the desk" list={toCollect} empty="Nothing to hand over."
          action={(r) => <button className="btn btn-primary btn-sm" onClick={() => handOver(r)}>Hand over</button>} />
        <Lane title="Out now" hint="Borrowed, not yet back" list={out} empty="Everything is back."
          action={() => <button className="btn btn-sm" onClick={() => navigate('/admin/reservations')}>Check in</button>} />
      </div>

      <div className="insights">
        <section className="tile">
          <header className="tile-head"><h2>Equipment</h2><Link to="/admin/equipment">Manage</Link></header>
          <div className="ring-card">
            <Ring slices={slices} total={total}><b>{report.totalEquipment}</b><span>items</span></Ring>
            <ul className="legend">
              {slices.map((s) => (
                <li key={s.key}><i style={{ background: s.color }} />{s.text}<b>{s.value}</b></li>
              ))}
            </ul>
          </div>
        </section>

        <section className="tile">
          <header className="tile-head"><h2>Popular items</h2></header>
          {report.mostBorrowed.length === 0 ? <p className="muted pad">Nothing borrowed yet.</p> : (
            <ul className="bars">
              {report.mostBorrowed.map((p) => (
                <li key={p.name}>
                  <span>{p.name}</span>
                  <div className="bar"><i style={{ width: `${(p.count / max) * 100}%` }} /></div>
                  <b>{p.count}</b>
                </li>
              ))}
            </ul>
          )}
          <p className="muted small pad">
            {report.totalUsers} users &middot;{' '}
            {Object.entries(rs).filter(([, v]) => v > 0).map(([k, v]) => `${v} ${statusText(k).toLowerCase()}`).join(', ') || 'no bookings yet'}
          </p>
        </section>
      </div>

      <section className="tile latest">
        <header className="tile-head"><h2>Latest bookings</h2><Link to="/admin/reservations">View all</Link></header>
        {latest.length === 0 ? <p className="muted pad">No bookings yet.</p> : (
          <div className="table-wrap">
            <table className="table">
              <thead><tr><th>Borrower</th><th>Equipment</th><th>Made</th><th>Status</th></tr></thead>
              <tbody>
                {latest.map((r) => (
                  <tr key={r.id}>
                    <td><strong>{r.borrower.fullName}</strong></td>
                    <td>{r.equipment.name}<span className="sub mono">{r.equipment.assetTag}</span></td>
                    <td className="muted">{fmtDateTime(r.createdAt)}</td>
                    <td><Badge value={r.status} /></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </>
  );
}
