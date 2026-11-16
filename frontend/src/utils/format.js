const dateTime = new Intl.DateTimeFormat('en-GB', {
  weekday: 'short', day: 'numeric', month: 'short', hour: '2-digit', minute: '2-digit', hour12: false,
});
const dateOnly = new Intl.DateTimeFormat('en-GB', { weekday: 'short', day: 'numeric', month: 'short' });
const timeOnly = new Intl.DateTimeFormat('en-GB', { hour: '2-digit', minute: '2-digit', hour12: false });

export const fmtDateTime = (iso) => (iso ? dateTime.format(new Date(iso)).replace(/,/g, '') : '-');

export function fmtRange(startIso, endIso) {
  const s = new Date(startIso);
  const e = new Date(endIso);
  if (s.toDateString() === e.toDateString()) {
    return `${dateOnly.format(s).replace(/,/g, '')}, ${timeOnly.format(s)} to ${timeOnly.format(e)}`;
  }
  return `${fmtDateTime(startIso)} to ${fmtDateTime(endIso)}`;
}

const pad = (n) => String(n).padStart(2, '0');

/** Value for <input type="datetime-local"> in local time. */
export const toInputValue = (d) =>
  `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;

export const label = (s) => (s ? s.replace(/_/g, ' ').toLowerCase().replace(/^\w/, (c) => c.toUpperCase()) : '');

export const TYPE_LABELS = {
  PROJECTOR: 'Projector',
  HDMI_CABLE: 'HDMI cable',
  REMOTE_CONTROL: 'Remote control',
  OTHER: 'Other',
};
export const EQUIPMENT_TYPES = Object.keys(TYPE_LABELS);

export function greeting(now = new Date()) {
  const h = now.getHours();
  return h < 12 ? 'Good morning' : h < 18 ? 'Good afternoon' : 'Good evening';
}

export const dayNumber = (iso) => new Date(iso).getDate();
export const monthShort = (iso) => new Date(iso).toLocaleString('en-GB', { month: 'short' });

const STATUS_TEXT = {
  AVAILABLE: 'Available', IN_USE: 'Borrowed', UNDER_MAINTENANCE: 'In repair', RETIRED: 'Retired',
  BOOKED: 'Booked', ISSUED: 'Collected', OVERDUE: 'Overdue', RETURNED: 'Returned', CANCELLED: 'Cancelled', EXPIRED: 'Expired',
};
/** Plain-language name for a status or role value. */
export const statusText = (s) => STATUS_TEXT[s] || label(s);
