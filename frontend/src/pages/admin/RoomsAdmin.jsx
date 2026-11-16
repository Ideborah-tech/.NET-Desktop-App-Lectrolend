import { useCallback, useEffect, useState } from 'react';
import { api } from '../../api/index.js';
import { Empty, ErrorNote, Field, Loading, Modal, PageHeader, useConfirm, useToast } from '../../components/ui/index.js';

export default function RoomsAdmin() {
  const toast = useToast();
  const confirm = useConfirm();
  const [rooms, setRooms] = useState(null);
  const [editing, setEditing] = useState(null);

  const load = useCallback(() => api.rooms.list().then(setRooms).catch((e) => toast(e.message, 'error')), [toast]);
  useEffect(() => { load(); }, [load]);

  const remove = async (r) => {
    const yes = await confirm({ title: `Delete room ${r.roomCode}?`, message: 'This cannot be undone.', confirmText: 'Delete room' });
    if (!yes) return;
    try { await api.rooms.remove(r.id); toast('Room deleted'); } catch (e) { toast(e.message, 'error'); }
    load();
  };

  return (
    <>
      <PageHeader eyebrow="Admin" title="Rooms">
        <button className="btn btn-primary" onClick={() => setEditing({})}>Add room</button>
      </PageHeader>
      {!rooms ? <Loading /> : rooms.length === 0 ? <Empty title="No rooms yet" /> : (
        <div className="card table-wrap">
          <table className="table">
            <thead><tr><th>Code</th><th>Building</th><th>Capacity</th><th aria-label="Actions" /></tr></thead>
            <tbody>
              {rooms.map((r) => (
                <tr key={r.id}>
                  <td className="mono"><strong>{r.roomCode}</strong></td><td>{r.building}</td><td>{r.capacity}</td>
                  <td className="actions">
                    <button className="btn btn-sm" onClick={() => setEditing(r)}>Edit</button>
                    <button className="btn btn-sm btn-danger" onClick={() => remove(r)}>Delete</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      {editing && <RoomModal room={editing} onClose={() => setEditing(null)} onDone={() => { setEditing(null); toast('Saved'); load(); }} />}
    </>
  );
}

function RoomModal({ room, onClose, onDone }) {
  const isNew = !room.id;
  const [form, setForm] = useState({ roomCode: room.roomCode || '', building: room.building || '', capacity: room.capacity || 30 });
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setError(''); setFields({});
    const payload = { ...form, capacity: Number(form.capacity) };
    try { await (isNew ? api.rooms.create(payload) : api.rooms.update(room.id, payload)); onDone(); } catch (err) { setError(err.message); setFields(err.fields || {}); }
  };

  return (
    <Modal title={isNew ? 'Add room' : 'Edit room'} onClose={onClose}
      footer={(<><button className="btn" type="button" onClick={onClose}>Cancel</button><button className="btn btn-primary" form="room-form">Save</button></>)}>
      <form id="room-form" onSubmit={submit}>
        <ErrorNote error={error} />
        <div className="row2">
          <Field label="Room code" error={fields.roomCode}><input value={form.roomCode} onChange={set('roomCode')} required /></Field>
          <Field label="Capacity" error={fields.capacity}><input type="number" min="1" value={form.capacity} onChange={set('capacity')} required /></Field>
        </div>
        <Field label="Building" error={fields.building}><input value={form.building} onChange={set('building')} required /></Field>
      </form>
    </Modal>
  );
}
