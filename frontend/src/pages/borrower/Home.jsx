import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { api } from '../../api/index.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { dayNumber, fmtRange, greeting, monthShort } from '../../utils/format.js';
import { Badge, Loading, PageHeader, useToast } from '../../components/ui/index.js';

const LIVE = ['BOOKED', 'ISSUED', 'OVERDUE'];
const HOUR = 3600_000;
const clock = (d) => new Date(d).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' });

/** Builds the notification list from the user's own reservations (most urgent first). */
function notificationsFor(list, now = Date.now()) {
  const out = [];
  list.forEach((r) => {
    const name = r.equipment.name;
    const start = new Date(r.startTime).getTime();
    const end = new Date(r.endTime).getTime();
    if (r.status === 'OVERDUE') {
      out.push({ id: `o${r.id}`, tone: 'late', rank: 0, title: `${name} is overdue`, text: 'Please return it to the IT office as soon as you can.' });
    } else if (r.status === 'ISSUED' && end - now < 2 * HOUR) {
      out.push({ id: `d${r.id}`, tone: 'warn', rank: 1, title: `${name} is due back at ${clock(end)}`, text: 'Return it to the IT office on time.' });
    } else if (r.status === 'BOOKED' && now >= start - 30 * 60000 && now <= start + 30 * 60000) {
      out.push({ id: `c${r.id}`, tone: 'warn', rank: 1, title: `Collect ${name} now`, text: `Pick-up closes at ${clock(start + 30 * 60000)}, then the booking expires.` });
    } else if (r.status === 'BOOKED' && start > now && start - now < 24 * HOUR) {
      out.push({ id: `u${r.id}`, tone: 'info', rank: 2, title: `${name} is booked for ${fmtRange(r.startTime, r.endTime)}`, text: 'Collect it at the IT office.' });
    } else if (r.status === 'EXPIRED' && now - end < 3 * 24 * HOUR) {
      out.push({ id: `x${r.id}`, tone: 'late', rank: 3, title: `${name} booking expired`, text: 'It was not collected within 30 minutes of the start.' });
    } else if (r.status === 'RETURNED' && r.returnInfo && now - new Date(r.returnInfo.returnedAt).getTime() < 3 * 24 * HOUR) {
      out.push({ id: `r${r.id}`, tone: 'ok', rank: 4, title: `${name} returned`, text: r.returnInfo.lateReturn ? 'Returned late. Please be on time next time.' : 'Thank you for returning it on time.' });
    }
  });
  return out.sort((a, b) => a.rank - b.rank).slice(0, 6);
}

export default function Home() {
  const { user } = useAuth();
  const toast = useToast();
  const [list, setList] = useState(null);

  useEffect(() => { api.reservations.mine().then(setList).catch((e) => toast(e.message, 'error')); }, [toast]);

  if (!list) return <Loading />;

  const notes = notificationsFor(list);
  const recent = [...list].sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt)).slice(0, 5);
  const active = list.filter((r) => LIVE.includes(r.status)).length;

  return (
    <>
      <PageHeader eyebrow={`${greeting()}, ${user.fullName.split(' ')[0]}`} title="Your activity">
        <Link to="/catalogue" className="btn btn-sun">Borrow equipment</Link>
      </PageHeader>

      <div className="home-grid">
        <section className="tile">
          <header className="tile-head"><h2>Notifications</h2>{notes.length > 0 && <span className="lane-count">{notes.length}</span>}</header>
          {notes.length === 0 ? (
            <p className="muted pad">You are all caught up. Nothing needs your attention.</p>
          ) : (
            <ul className="notes">
              {notes.map((n) => (
                <li key={n.id} className={`note note-${n.tone}`}>
                  <strong>{n.title}</strong>
                  <span>{n.text}</span>
                </li>
              ))}
            </ul>
          )}
        </section>

        <section className="tile">
          <header className="tile-head">
            <h2>Recent bookings</h2>
            <Link to="/my-reservations">View all{active > 0 ? ` (${active} active)` : ''}</Link>
          </header>
          {recent.length === 0 ? (
            <p className="muted pad">No bookings yet. <Link to="/catalogue">Borrow something</Link> to get started.</p>
          ) : (
            <ul className="recent">
              {recent.map((r) => (
                <li key={r.id}>
                  <div className="q-when"><b>{dayNumber(r.startTime)}</b><span>{monthShort(r.startTime)}</span></div>
                  <div className="q-main">
                    <strong>{r.equipment.name}</strong>
                    <span className="muted small">{fmtRange(r.startTime, r.endTime)}</span>
                  </div>
                  <Badge value={r.status} />
                </li>
              ))}
            </ul>
          )}
        </section>
      </div>
    </>
  );
}
