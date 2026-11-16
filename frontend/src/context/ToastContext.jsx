import { createContext, useCallback, useContext, useEffect, useRef, useState } from 'react';

// ---- toasts and confirmation dialog --------------------------------------------------------
// useToast()   -> toast(message, kind)  kind: 'ok' | 'error' | 'info'
// useConfirm() -> confirm({ title, message, confirmText, tone }) returns a Promise<boolean>
const ToastContext = createContext(() => {});
const ConfirmContext = createContext(() => Promise.resolve(false));
export const useToast = () => useContext(ToastContext);
export const useConfirm = () => useContext(ConfirmContext);

const TITLES = { ok: 'Done', error: 'Something went wrong', info: 'Note' };

function ConfirmDialog({ ask, onAnswer }) {
  const cancelRef = useRef(null);
  useEffect(() => {
    cancelRef.current?.focus();
    const onKey = (e) => e.key === 'Escape' && onAnswer(false);
    document.addEventListener('keydown', onKey);
    return () => document.removeEventListener('keydown', onKey);
  }, [onAnswer]);

  const danger = ask.tone !== 'neutral';
  return (
    <div className="modal-backdrop" onMouseDown={(e) => e.target === e.currentTarget && onAnswer(false)}>
      <div className="confirm" role="alertdialog" aria-modal="true" aria-label={ask.title}>
        <span className={`confirm-mark ${danger ? 'danger' : ''}`} aria-hidden="true">!</span>
        <h2>{ask.title}</h2>
        <p>{ask.message}</p>
        <div className="confirm-actions">
          <button ref={cancelRef} className="btn" onClick={() => onAnswer(false)}>{ask.cancelText}</button>
          <button className={`btn ${danger ? 'btn-danger-solid' : 'btn-primary'}`} onClick={() => onAnswer(true)}>{ask.confirmText}</button>
        </div>
      </div>
    </div>
  );
}

export function ToastProvider({ children }) {
  const [toasts, setToasts] = useState([]);
  const [ask, setAsk] = useState(null);
  const nextId = useRef(1);
  const resolver = useRef(null);

  const dismiss = useCallback((id) => setToasts((t) => t.filter((x) => x.id !== id)), []);
  const push = useCallback((message, kind = 'ok') => {
    const id = nextId.current++;
    setToasts((t) => [...t.slice(-3), { id, message, kind }]);
    setTimeout(() => dismiss(id), kind === 'error' ? 7000 : 4500);
  }, [dismiss]);

  const confirm = useCallback((opts) => new Promise((resolve) => {
    resolver.current = resolve;
    setAsk({ title: 'Are you sure?', confirmText: 'Confirm', cancelText: 'Keep', tone: 'danger', ...opts });
  }), []);
  const answer = useCallback((yes) => {
    resolver.current?.(yes);
    resolver.current = null;
    setAsk(null);
  }, []);

  return (
    <ToastContext.Provider value={push}>
      <ConfirmContext.Provider value={confirm}>
        {children}
        <div className="toasts" role="status" aria-live="polite">
          {toasts.map((t) => (
            <div key={t.id} className={`toast toast-${t.kind}`}>
              <div className="toast-text">
                <strong>{TITLES[t.kind] || TITLES.info}</strong>
                <span>{t.message}</span>
              </div>
              <button className="toast-x" onClick={() => dismiss(t.id)} aria-label="Dismiss">&times;</button>
            </div>
          ))}
        </div>
        {ask && <ConfirmDialog ask={ask} onAnswer={answer} />}
      </ConfirmContext.Provider>
    </ToastContext.Provider>
  );
}
