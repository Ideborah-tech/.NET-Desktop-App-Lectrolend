import { useCallback, useEffect, useState } from 'react';
import { api } from '../../api/index.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { Badge, ErrorNote, Field, Loading, Modal, PageHeader, useToast } from '../../components/ui/index.js';

export default function UsersAdmin() {
  const toast = useToast();
  const { user: me } = useAuth();
  const [users, setUsers] = useState(null);
  const [adding, setAdding] = useState(false);

  const load = useCallback(() => api.users.list().then(setUsers).catch((e) => toast(e.message, 'error')), [toast]);
  useEffect(() => { load(); }, [load]);

  const toggle = async (u) => {
    try { await api.users.setActive(u.id, !u.active); toast(u.active ? 'Account deactivated' : 'Account reactivated'); } catch (e) { toast(e.message, 'error'); }
    load();
  };

  return (
    <>
      <PageHeader eyebrow="Admin" title="Users">
        <button className="btn btn-primary" onClick={() => setAdding(true)}>Add user</button>
      </PageHeader>
      {!users ? <Loading /> : (
        <div className="card table-wrap">
          <table className="table">
            <thead><tr><th>Name</th><th>Role</th><th>ID</th><th>Status</th><th aria-label="Actions" /></tr></thead>
            <tbody>
              {users.map((u) => (
                <tr key={u.id}>
                  <td><strong>{u.fullName}</strong><span className="sub">{u.email}</span></td>
                  <td><Badge value={u.role} kind="role" /></td>
                  <td className="mono">{u.registrationNo || u.staffNo}<span className="sub">{u.faculty || u.department || ''}</span></td>
                  <td><Badge value={u.active ? 'AVAILABLE' : 'RETIRED'} /></td>
                  <td className="actions">
                    <button className="btn btn-sm" disabled={u.id === me.id} onClick={() => toggle(u)}>{u.active ? 'Deactivate' : 'Reactivate'}</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      {adding && <AddUser onClose={() => setAdding(false)} onDone={() => { setAdding(false); toast('User added'); load(); }} />}
    </>
  );
}

function AddUser({ onClose, onDone }) {
  const [form, setForm] = useState({ fullName: '', email: '', password: '', phone: '', role: 'STUDENT', registrationNo: '', faculty: '', program: '', staffNo: '', department: '' });
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setError(''); setFields({});
    try { await api.users.create(form); onDone(); } catch (err) { setError(err.message); setFields(err.fields || {}); }
  };

  return (
    <Modal title="Add user" onClose={onClose}
      footer={(<><button className="btn" type="button" onClick={onClose}>Cancel</button><button className="btn btn-primary" form="user-form">Create</button></>)}>
      <form id="user-form" onSubmit={submit}>
        <ErrorNote error={error} />
        <Field label="Full name" error={fields.fullName}><input value={form.fullName} onChange={set('fullName')} required /></Field>
        <div className="row2">
          <Field label="Email" error={fields.email}><input type="email" value={form.email} onChange={set('email')} required /></Field>
          <Field label="Temporary password" error={fields.password} hint="At least 8 characters"><input type="password" value={form.password} onChange={set('password')} required autoComplete="new-password" /></Field>
        </div>
        <Field label="Role">
          <select value={form.role} onChange={set('role')}>
            <option value="STUDENT">Student</option><option value="LECTURER">Lecturer</option><option value="ADMIN">IT administrator</option>
          </select>
        </Field>
        {form.role === 'STUDENT' ? (
          <div className="row2">
            <Field label="Registration number"><input value={form.registrationNo} onChange={set('registrationNo')} /></Field>
            <Field label="Faculty"><input value={form.faculty} onChange={set('faculty')} /></Field>
          </div>
        ) : (
          <div className="row2">
            <Field label="Staff number"><input value={form.staffNo} onChange={set('staffNo')} /></Field>
            {form.role === 'LECTURER' && <Field label="Department"><input value={form.department} onChange={set('department')} /></Field>}
          </div>
        )}
      </form>
    </Modal>
  );
}
