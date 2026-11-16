import { statusText } from '../../utils/format.js';

// ---- small pieces --------------------------------------------------------------------------
export function Badge({ value, kind }) {
  return <span className={`badge badge-${(kind || value || '').toLowerCase()}`}><i />{statusText(value)}</span>;
}

export function Field({ label: text, error, hint, children }) {
  return (
    <label className={`field ${error ? 'has-error' : ''}`}>
      <span className="field-label">{text}</span>
      {children}
      {hint && !error && <span className="field-hint">{hint}</span>}
      {error && <span className="field-error">{error}</span>}
    </label>
  );
}

/** Page title row: eyebrow, big title and optional actions. */
export function PageHeader({ eyebrow, title, children }) {
  return (
    <header className="page-head">
      <div>
        {eyebrow && <p className="eyebrow">{eyebrow}</p>}
        <h1>{title}</h1>
      </div>
      {children && <div className="page-head-actions">{children}</div>}
    </header>
  );
}

export function Loading({ text = 'Loading' }) {
  return <div className="loading"><span className="spinner" />{text}</div>;
}

export function Empty({ title, children }) {
  return (
    <div className="empty">
      <h3>{title}</h3>
      {children && <p>{children}</p>}
    </div>
  );
}

export function ErrorNote({ error }) {
  return error ? <div className="form-error" role="alert">{error}</div> : null;
}
