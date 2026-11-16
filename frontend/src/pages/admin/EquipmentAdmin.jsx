import { useCallback, useEffect, useState } from 'react';
import { api } from '../../api/index.js';
import { EQUIPMENT_TYPES, TYPE_LABELS, statusText } from '../../utils/format.js';
import { Badge, Empty, ErrorNote, Field, Loading, Modal, PageHeader, useConfirm, useToast } from '../../components/ui/index.js';

const BLANK = { name: '', type: 'PROJECTOR', assetTag: '', description: '' };
const SETTABLE = ['AVAILABLE', 'UNDER_MAINTENANCE', 'RETIRED'];

export default function EquipmentAdmin() {
  const toast = useToast();
  const confirm = useConfirm();
  const [items, setItems] = useState(null);
  const [editing, setEditing] = useState(null); // null | {} (new) | item

  const load = useCallback(() => api.equipment.list().then(setItems).catch((e) => toast(e.message, 'error')), [toast]);
  useEffect(() => { load(); }, [load]);

  const changeStatus = async (item, status) => {
    try { await api.equipment.setStatus(item.id, status); toast('Status updated'); } catch (e) { toast(e.message, 'error'); }
    load();
  };

  const remove = async (item) => {
    const yes = await confirm({ title: `Remove ${item.name}?`, message: `${item.assetTag} will be deleted. Items with borrowing history are retired instead of deleted.`, confirmText: 'Remove item' });
    if (!yes) return;
    try {
      const res = await api.equipment.remove(item.id);
      toast(res ? 'Item has history, so it was retired' : 'Item removed');
    } catch (e) { toast(e.message, 'error'); }
    load();
  };

  return (
    <>
      <PageHeader eyebrow="Admin" title="Equipment">
        <button className="btn btn-primary" onClick={() => setEditing({})}>Add equipment</button>
      </PageHeader>

      {!items ? <Loading /> : items.length === 0 ? <Empty title="No equipment yet" /> : (
        <div className="card table-wrap">
          <table className="table">
            <thead><tr><th>Asset tag</th><th>Name</th><th>Type</th><th>Status</th><th aria-label="Actions" /></tr></thead>
            <tbody>
              {items.map((e) => (
                <tr key={e.id}>
                  <td className="mono">{e.assetTag}</td>
                  <td><strong>{e.name}</strong>{e.description && <span className="sub">{e.description}</span>}</td>
                  <td>{TYPE_LABELS[e.type]}</td>
                  <td>
                    {e.status === 'IN_USE' ? <Badge value="IN_USE" /> : (
                      <select className="select-sm" value={e.status} onChange={(ev) => changeStatus(e, ev.target.value)} aria-label={`Status of ${e.assetTag}`}>
                        {SETTABLE.map((s) => <option key={s} value={s}>{statusText(s)}</option>)}
                      </select>
                    )}
                  </td>
                  <td className="actions">
                    <button className="btn btn-sm" onClick={() => setEditing(e)}>Edit</button>
                    <button className="btn btn-sm btn-danger" onClick={() => remove(e)}>Remove</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {editing && (
        <EquipmentModal item={editing} onClose={() => setEditing(null)}
          onDone={() => { setEditing(null); toast('Saved'); load(); }} />
      )}
    </>
  );
}

function EquipmentModal({ item, onClose, onDone }) {
  const isNew = !item.id;
  const [form, setForm] = useState(isNew ? BLANK : { name: item.name, type: item.type, assetTag: item.assetTag, description: item.description || '' });
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const [busy, setBusy] = useState(false);
  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setBusy(true); setError(''); setFields({});
    try {
      await (isNew ? api.equipment.create(form) : api.equipment.update(item.id, form));
      onDone();
    } catch (err) { setError(err.message); setFields(err.fields || {}); } finally { setBusy(false); }
  };

  return (
    <Modal title={isNew ? 'Add equipment' : 'Edit equipment'} onClose={onClose}
      footer={(
        <>
          <button className="btn" type="button" onClick={onClose}>Cancel</button>
          <button className="btn btn-primary" form="eq-form" disabled={busy}>{busy ? 'Saving...' : 'Save'}</button>
        </>
      )}>
      <form id="eq-form" onSubmit={submit}>
        <ErrorNote error={error} />
        <Field label="Name" error={fields.name}><input value={form.name} onChange={set('name')} required /></Field>
        <div className="row2">
          <Field label="Type">
            <select value={form.type} onChange={set('type')}>
              {EQUIPMENT_TYPES.map((t) => <option key={t} value={t}>{TYPE_LABELS[t]}</option>)}
            </select>
          </Field>
          <Field label="Asset tag" error={fields.assetTag}><input value={form.assetTag} onChange={set('assetTag')} required /></Field>
        </div>
        <Field label="Description"><textarea rows={3} value={form.description} onChange={set('description')} /></Field>
      </form>
    </Modal>
  );
}
