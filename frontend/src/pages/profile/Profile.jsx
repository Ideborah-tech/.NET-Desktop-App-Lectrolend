import { useEffect, useState } from 'react';
import { api } from '../../api/index.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { label } from '../../utils/format.js';
import { ErrorNote, Field, PageHeader, useToast } from '../../components/ui/index.js';

const LIVE = ['BOOKED', 'ISSUED', 'OVERDUE'];

export default function Profile() {
  const { user, isAdmin, updateUser } = useAuth();
  const toast = useToast();
  const [stats, setStats] = useState(null);

  useEffect(() => {
    if (isAdmin) return;
    api.reservations.mine().then((list) => setStats({
      total: list.length,
      active: list.filter((r) => LIVE.includes(r.status)).length,
      returned: list.filter((r) => r.status === 'RETURNED').length,
    })).catch(() => {});
  }, [isAdmin]);

  const idLabel = user.role === 'STUDENT' ? 'Registration no.' : 'Staff no.';
  const idValue = user.registrationNo || user.staffNo;

  return (
    <>
      <PageHeader eyebrow="Account" title="Profile" />

      <div className="profile-grid">
        <section className="card card-rail profile-card">
          <div className="profile-avatar" aria-hidden="true">{user.fullName.charAt(0)}</div>
          <h2>{user.fullName}</h2>
          <p><span className="tag">{label(user.role)}</span></p>
          <dl className="facts">
            <div><dt>Email</dt><dd>{user.email}</dd></div>
            {idValue && <div><dt>{idLabel}</dt><dd className="mono">{idValue}</dd></div>}
            {user.faculty && <div><dt>Faculty</dt><dd>{user.faculty}</dd></div>}
            {user.program && <div><dt>Programme</dt><dd>{user.program}</dd></div>}
            {user.department && <div><dt>Department</dt><dd>{user.department}</dd></div>}
            {user.phone && <div><dt>Phone</dt><dd>{user.phone}</dd></div>}
          </dl>
          {stats && (
            <div className="mini-stats">
              <div><b>{stats.active}</b><span>Active</span></div>
              <div><b>{stats.returned}</b><span>Returned</span></div>
              <div><b>{stats.total}</b><span>All bookings</span></div>
            </div>
          )}
        </section>

        <div className="profile-forms">
          <DetailsForm user={user} onSaved={(u) => { updateUser(u); toast('Profile saved'); }} />
          <PasswordForm onSaved={() => toast('Password changed')} />
        </div>
      </div>
    </>
  );
}

function DetailsForm({ user, onSaved }) {
  const [form, setForm] = useState({
    fullName: user.fullName || '', phone: user.phone || '',
    faculty: user.faculty || '', program: user.program || '', department: user.department || '',
  });
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const [busy, setBusy] = useState(false);
  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setError(''); setFields({}); setBusy(true);
    try { onSaved(await api.updateProfile(form)); } catch (err) { setError(err.message); setFields(err.fields || {}); } finally { setBusy(false); }
  };

  return (
    <form className="card card-rail form-card" onSubmit={submit}>
      <h2>Your details</h2>
      <p className="muted small">Your email and ID number are set by the IT office and cannot be changed here.</p>
      <ErrorNote error={error} />
      <div className="row2">
        <Field label="Full name" error={fields.fullName}><input value={form.fullName} onChange={set('fullName')} required /></Field>
        <Field label="Phone" error={fields.phone}><input value={form.phone} onChange={set('phone')} autoComplete="tel" /></Field>
      </div>
      {user.role === 'STUDENT' && (
        <div className="row2">
          <Field label="Faculty"><input value={form.faculty} onChange={set('faculty')} /></Field>
          <Field label="Programme"><input value={form.program} onChange={set('program')} /></Field>
        </div>
      )}
      {user.role === 'LECTURER' && (
        <Field label="Department"><input value={form.department} onChange={set('department')} /></Field>
      )}
      <button className="btn btn-primary" disabled={busy}>{busy ? 'Saving...' : 'Save changes'}</button>
    </form>
  );
}

function PasswordForm({ onSaved }) {
  const [form, setForm] = useState({ current: '', next: '', confirm: '' });
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const [busy, setBusy] = useState(false);
  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setError(''); setFields({});
    if (form.next !== form.confirm) { setError('The new passwords do not match'); return; }
    setBusy(true);
    try {
      await api.changePassword(form.current, form.next);
      setForm({ current: '', next: '', confirm: '' });
      onSaved();
    } catch (err) { setError(err.message); setFields(err.fields || {}); } finally { setBusy(false); }
  };

  return (
    <form className="card card-rail form-card" onSubmit={submit}>
      <h2>Change password</h2>
      <ErrorNote error={error} />
      <Field label="Current password" error={fields.currentPassword}>
        <input type="password" value={form.current} onChange={set('current')} autoComplete="current-password" required />
      </Field>
      <div className="row2">
        <Field label="New password" error={fields.newPassword} hint="At least 8 characters">
          <input type="password" value={form.next} onChange={set('next')} autoComplete="new-password" required />
        </Field>
        <Field label="Confirm new password">
          <input type="password" value={form.confirm} onChange={set('confirm')} autoComplete="new-password" required />
        </Field>
      </div>
      <button className="btn" disabled={busy}>{busy ? 'Updating...' : 'Update password'}</button>
    </form>
  );
}
