import { useCallback, useEffect, useMemo, useState } from 'react';
import { api } from '../../api/index.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { TypeIcon } from '../../components/icons/index.jsx';
import { EQUIPMENT_TYPES, TYPE_LABELS, greeting, toInputValue } from '../../utils/format.js';
import { Badge, Empty, ErrorNote, Field, Loading, Modal, PageHeader, useToast } from '../../components/ui/index.js';

const LIVE = ['BOOKED', 'ISSUED', 'OVERDUE'];
const limitFor = (user) => (user.role === 'LECTURER' ? 5 : 3);

export default function Catalogue() {
  const { user } = useAuth();
  const toast = useToast();
  const [items, setItems] = useState(null);
  const [mine, setMine] = useState([]);
  const [q, setQ] = useState('');
  const [type, setType] = useState('ALL');
  const [freeOnly, setFreeOnly] = useState(false);
  const [picked, setPicked] = useState([]);
  const [booking, setBooking] = useState(false);

  const load = useCallback(() => {
    api.equipment.list().then(setItems).catch((e) => toast(e.message, 'error'));
    api.reservations.mine().then(setMine).catch(() => {});
  }, [toast]);
  useEffect(() => { load(); }, [load]);

  const counts = useMemo(() => {
    const c = { ALL: items?.length || 0 };
    EQUIPMENT_TYPES.forEach((t) => { c[t] = (items || []).filter((e) => e.type === t).length; });
    return c;
  }, [items]);

  const shown = useMemo(() => {
    const needle = q.trim().toLowerCase();
    return (items || []).filter((e) =>
      (type === 'ALL' || e.type === type)
      && (!freeOnly || e.status === 'AVAILABLE')
      && (!needle || e.name.toLowerCase().includes(needle) || e.assetTag.toLowerCase().includes(needle)));
  }, [items, q, type, freeOnly]);

  const availableNow = (items || []).filter((e) => e.status === 'AVAILABLE').length;
  const active = mine.filter((r) => LIVE.includes(r.status)).length;

  const room = Math.max(0, limitFor(user) - active);
  const shelves = EQUIPMENT_TYPES.map((t) => ({ type: t, list: shown.filter((e) => e.type === t) })).filter((g) => g.list.length > 0);
  const isPicked = (e) => picked.some((x) => x.id === e.id);
  const toggle = (e) => {
    if (isPicked(e)) { setPicked(picked.filter((x) => x.id !== e.id)); return; }
    if (picked.length >= room) {
      toast(room === 0 ? `You already hold ${active} active bookings, which is your limit.` : `You can book ${room} more ${room === 1 ? 'item' : 'items'} right now.`, 'info');
      return;
    }
    setPicked([...picked, e]);
  };

  return (
    <>
      <PageHeader eyebrow={`${greeting()}, ${user.fullName.split(' ')[0]}`} title="What do you need today?" />

      <div className="borrow-layout">
      <div className="borrow-main">
      <section className="finder">
        <input className="finder-search" type="search" placeholder="Search a projector, cable, remote or asset tag" value={q}
          onChange={(e) => setQ(e.target.value)} aria-label="Search equipment" />
        <div className="finder-row">
          <div className="pill-chips" role="group" aria-label="Filter by type">
            {['ALL', ...EQUIPMENT_TYPES].map((t) => (
              <button key={t} className={`pill-chip ${type === t ? 'on' : ''}`} onClick={() => setType(t)} aria-pressed={type === t}>
                <TypeIcon type={t} size={20} />
                {t === 'ALL' ? 'Everything' : TYPE_LABELS[t]}
                <em>{counts[t]}</em>
              </button>
            ))}
          </div>
          <label className="check finder-check">
            <input type="checkbox" checked={freeOnly} onChange={(e) => setFreeOnly(e.target.checked)} />
            Available now only
          </label>
        </div>
        <p className="finder-note">
          <b>{availableNow}</b> items are free right now. You hold <b>{active}</b> of <b>{limitFor(user)}</b> allowed bookings.
          Add everything you need, then book it all in one go.
        </p>
      </section>

      {!items ? <Loading text="Loading equipment" /> : shelves.length === 0 ? (
        <Empty title="Nothing matches">Try a different search or clear the filters.</Empty>
      ) : shelves.map((g) => (
        <section key={g.type} className="shelf">
          <header className="shelf-head">
            <h2>{TYPE_LABELS[g.type]}s</h2>
            <span>{g.list.length} {g.list.length === 1 ? 'item' : 'items'}</span>
          </header>
          <div className="shelf-row">
            {g.list.map((e) => (
              <article key={e.id} className={`item t-${e.type} ${e.reservable ? '' : 'item-off'} ${isPicked(e) ? 'item-picked' : ''}`}>
                <div className="item-top">
                  <span className="tag">{e.assetTag}</span>
                  <Badge value={e.status} />
                </div>
                <div className="item-sticker"><TypeIcon type={e.type} size={44} /></div>
                <div className="item-body">
                  <h3>{e.name}</h3>
                  <p>{e.description || TYPE_LABELS[e.type]}</p>
                  <button className={`btn btn-block-sm ${isPicked(e) ? 'btn-sun' : 'btn-primary'}`} disabled={!e.reservable} onClick={() => toggle(e)}>
                    {!e.reservable ? 'Unavailable' : isPicked(e) ? 'Added. Tap to remove' : 'Add to booking'}
                  </button>
                </div>
              </article>
            ))}
          </div>
        </section>
      ))}

      </div>

      <aside className="booking-panel" aria-label="Your booking">
        <BookingSummary items={picked} limit={limitFor(user)} active={active}
          onRemove={(id) => setPicked(picked.filter((x) => x.id !== id))}
          onClear={() => setPicked([])} onOpen={() => setBooking(true)} />
      </aside>
      </div>

      {booking && picked.length > 0 && (
        <BookingDialog items={picked} limit={limitFor(user)} active={active}
          onRemove={(id) => setPicked(picked.filter((x) => x.id !== id))}
          onClose={() => setBooking(false)}
          onDone={(n) => {
            setBooking(false); setPicked([]); load();
            window.dispatchEvent(new Event('bookings:changed'));
            toast(n === 1 ? 'Booked. Collect it at the IT office.' : `${n} items booked. Collect them together at the IT office.`);
          }} />
      )}
    </>
  );
}

// ---- booking summary (right-hand side) and booking dialog ---------------------------------------------------------------------

const DAY_START = 7;
const DAY_END = 21;

/** Visual strip of one day showing booked slots and the slot being chosen. */
function DayBar({ startStr, endStr, busy, ok }) {
  const start = new Date(startStr);
  if (Number.isNaN(start.getTime())) return null;
  const end = new Date(endStr);
  const from = new Date(start); from.setHours(DAY_START, 0, 0, 0);
  const to = new Date(start); to.setHours(DAY_END, 0, 0, 0);
  const span = to - from;
  const pct = (d) => Math.min(100, Math.max(0, ((d - from) / span) * 100));
  const seg = (s, e) => ({ left: `${pct(s)}%`, width: `${Math.max(0, pct(e) - pct(s))}%` });
  const dayLabel = start.toLocaleDateString('en-GB', { weekday: 'long', day: 'numeric', month: 'long' });

  return (
    <div className="daybar">
      <div className="daybar-title"><span>{dayLabel}</span><span>{String(DAY_START).padStart(2, '0')}:00 to {DAY_END}:00</span></div>
      <div className="daybar-track">
        {busy.filter((b) => new Date(b.end) > from && new Date(b.start) < to)
          .map((b, i) => <div key={i} className="daybar-seg" style={seg(new Date(b.start), new Date(b.end))} title="Booked" />)}
        {end > start && <div className={`daybar-sel ${ok === false ? 'bad' : ''}`} style={seg(start, end)} />}
      </div>
      <div className="daybar-ticks">{[7, 10, 13, 16, 19, 21].map((h) => <span key={h}>{String(h).padStart(2, '0')}</span>)}</div>
      <div className="daybar-legend"><span><i />Already booked</span><span><i className="sel" />Your slot</span></div>
    </div>
  );
}

const pad = (n) => String(n).padStart(2, '0');
const dayString = (d) => `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
/** Next quarter hour from now, as { day, time } strings for the date and time inputs. */
function soonest() {
  const d = new Date(Math.ceil((Date.now() + 60000) / 900000) * 900000);
  return { day: dayString(d), time: `${pad(d.getHours())}:${pad(d.getMinutes())}` };
}
/** One hour after a HH:mm time, capped at the end of the day. */
function plusHour(t) {
  const [h, m] = t.split(':').map(Number);
  return h >= 23 ? '23:45' : `${pad(h + 1)}:${pad(m)}`;
}

/** Small card beside the shelves: what you picked so far and a button that opens the booking form. */
function BookingSummary({ items, limit, active, onRemove, onClear, onOpen }) {
  if (items.length === 0) {
    return (
      <div className="bs bs-empty">
        <h2>Your booking</h2>
        <p>Press <b>Add to booking</b> on an item. You hold {active} of {limit} bookings.</p>
      </div>
    );
  }
  return (
    <div className="bs">
      <header className="bs-head">
        <h2>{items.length} {items.length === 1 ? 'item' : 'items'} picked</h2>
        <button type="button" className="bp-clear" onClick={onClear}>Clear</button>
      </header>
      <div className="bp-chips">
        {items.map((i) => (
          <span key={i.id} className="bp-chip">
            {i.name}
            <button type="button" onClick={() => onRemove(i.id)} aria-label={`Remove ${i.name}`}>&times;</button>
          </span>
        ))}
      </div>
      <button className="btn btn-sun btn-block" onClick={onOpen}>Choose time and book<span>&rarr;</span></button>
    </div>
  );
}

function BookingDialog({ items, limit, active, onRemove, onClose, onDone }) {
  const [day, setDay] = useState(() => soonest().day);
  const [time, setTime] = useState(() => soonest().time);
  const [endTime, setEndTime] = useState(() => plusHour(soonest().time));
  const [roomId, setRoomId] = useState('');
  const [rooms, setRooms] = useState([]);
  const [avail, setAvail] = useState({});   // equipment id -> availability result
  const [error, setError] = useState('');
  const [busy, setBusy] = useState(false);

  useEffect(() => { api.rooms.list().then(setRooms).catch(() => {}); }, []);

  const start = `${day}T${time}`;
  const startDate = new Date(start);
  const end = !Number.isNaN(startDate.getTime()) && endTime > time ? `${day}T${endTime}` : '';
  const dayLabel = (d) => (d === dayString(new Date()) ? 'Today' : d === dayString(new Date(Date.now() + 86400000)) ? 'Tomorrow'
    : new Date(`${d}T00:00`).toLocaleDateString('en-GB', { weekday: 'short', day: 'numeric', month: 'short' }));
  const clock = (v) => new Date(v).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' });

  // Live availability check for every selected item, debounced while the user edits the times.
  const key = items.map((i) => i.id).join(',');
  useEffect(() => {
    if (!start || !end || Number.isNaN(startDate.getTime()) || items.length === 0) return undefined;
    let live = true;
    const t = setTimeout(() => {
      Promise.all(items.map((i) => api.equipment.availability(i.id, start, end).then((a) => [i.id, a]).catch(() => [i.id, null])))
        .then((pairs) => live && setAvail(Object.fromEntries(pairs)));
    }, 300);
    return () => { live = false; clearTimeout(t); };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [key, start, end]);

  const blocked = items.filter((i) => avail[i.id] && !avail[i.id].available);
  const busySlots = items.flatMap((i) => avail[i.id]?.busySlots || []);
  const minutes = Math.round((new Date(end) - new Date(start)) / 60000);
  const length = Number.isFinite(minutes) && minutes > 0
    ? (minutes >= 60 ? `${Math.floor(minutes / 60)} h${minutes % 60 ? ` ${minutes % 60} min` : ''}` : `${minutes} min`)
    : null;

  const submit = async (e) => {
    e.preventDefault();
    setError('');
    setBusy(true);
    try {
      const made = await api.reservations.createBatch({
        equipmentIds: items.map((i) => i.id), startTime: start, endTime: end, roomId: roomId ? Number(roomId) : null,
      });
      onDone(made.length);
    } catch (err) {
      setError(err.message);
    } finally {
      setBusy(false);
    }
  };

  const week = Array.from({ length: 7 }, (_, i) => new Date(Date.now() + i * 86400000));

  return (
    <Modal title="Choose time and book" onClose={onClose} wide className="modal-book"
      footer={(
        <>
          <p className="bk-used">{active + items.length} of {limit} bookings used</p>
          <button className="btn" type="button" onClick={onClose}>Back</button>
          <button className="btn btn-primary" form="book-form" disabled={busy || blocked.length > 0 || !end}>
            {busy ? 'Booking...' : `Book ${items.length} ${items.length === 1 ? 'item' : 'items'}`}
          </button>
        </>
      )}>
      <form id="book-form" className="bk" onSubmit={submit}>
        <div className="bp-chips bk-items">
          {items.map((i) => {
            const a = avail[i.id];
            const bad = a && !a.available;
            return (
              <span key={i.id} className={`bp-chip ${bad ? 'bad' : ''}`} title={bad ? (a.reason || 'Not free then') : i.assetTag}>
                {i.name}
                <button type="button" onClick={() => onRemove(i.id)} aria-label={`Remove ${i.name}`}>&times;</button>
              </span>
            );
          })}
        </div>
        <ErrorNote error={error} />

        <div className="bk-cols">
          <div className="bk-col">
            <h3>Day</h3>
            <div className="bp-days" role="group" aria-label="Pick a day">
              {week.map((d) => {
                const v = dayString(d);
                return (
                  <button type="button" key={v} className={`bp-day ${day === v ? 'on' : ''}`} onClick={() => setDay(v)} aria-pressed={day === v}>
                    <span>{d.toLocaleDateString('en-GB', { weekday: 'short' })}</span>
                    <b>{d.getDate()}</b>
                  </button>
                );
              })}
            </div>
            <label className="bp-other">Another date
              <input type="date" value={day} min={dayString(new Date())} onChange={(e) => setDay(e.target.value)} required />
            </label>

            <h3>Time</h3>
            <div className="bp-stub">
              <label>
                <span>Pick up</span>
                <input type="time" value={time} step="900" onChange={(e) => setTime(e.target.value)} required />
              </label>
              <i aria-hidden="true" />
              <label>
                <span>Return</span>
                <input type="time" value={endTime} step="900" onChange={(e) => setEndTime(e.target.value)} required />
              </label>
            </div>
            {!end
              ? <p className="field-error">Return time must be after the pick up time.</p>
              : <p className="bp-when">{dayLabel(day)}, <b>{clock(start)}</b> to <b>{clock(end)}</b>{length ? ` (${length})` : ''}</p>}
          </div>

          <div className="bk-col">
            {Object.keys(avail).length > 0 && (
              <div className={`avail ${blocked.length === 0 ? 'avail-ok' : 'avail-no'}`} role="status">
                <div>
                  <strong>{blocked.length === 0
                    ? `${items.length === 1 ? 'Item is' : `All ${items.length} items are`} free for that time`
                    : `${blocked.map((b) => b.name).join(', ')} ${blocked.length === 1 ? 'is' : 'are'} not free then`}</strong>
                  {blocked.length > 0 && <div>Remove {blocked.length === 1 ? 'it' : 'them'} or choose another time.</div>}
                </div>
              </div>
            )}
            <DayBar startStr={start} endStr={end || start} busy={busySlots} ok={Object.keys(avail).length ? blocked.length === 0 : undefined} />
            <h3>Room <small>optional</small></h3>
            <select value={roomId} onChange={(e) => setRoomId(e.target.value)} aria-label="Room">
              <option value="">No room, I will collect only</option>
              {rooms.map((r) => <option key={r.id} value={r.id}>{r.details}</option>)}
            </select>
            <p className="bk-hint">Collect within 30 minutes of the start. Longest booking: 7 days.</p>
          </div>
        </div>
      </form>
    </Modal>
  );
}
