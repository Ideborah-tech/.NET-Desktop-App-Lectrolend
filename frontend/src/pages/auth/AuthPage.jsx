import { useEffect, useRef, useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext.jsx';
import { Field, ErrorNote } from '../../components/ui/index.js';
import Scene3D from '../../components/three/Scene3D.jsx';

const EMPTY = {
  fullName: '', email: '', password: '', phone: '', role: 'STUDENT',
  registrationNo: '', faculty: '', program: '', staffNo: '', department: '',
};

const DEMO = import.meta.env.DEV ? [
  { label: 'Student', email: 'student@auca.ac.rw', password: 'Student@123' },
  { label: 'Lecturer', email: 'lecturer@auca.ac.rw', password: 'Lecturer@123' },
  { label: 'IT admin', email: 'admin@auca.ac.rw', password: 'Admin@123' },
] : [];

const LEDE = {
  login: {
    eyebrow: 'AUCA IT office',
    title: <>Borrow what<br />you need<i>.</i></>,
    items: [
      ['Browse', 'Projectors, HDMI cables, remotes and more, with live availability.'],
      ['Reserve', 'Pick a time slot and, if you like, a room. Conflicts are caught for you.'],
      ['Collect and return', 'Pick it up at the IT office, bring it back when you are done.'],
    ],
  },
  register: {
    eyebrow: 'New account',
    title: <>Make an<br />account<i>.</i></>,
    items: [
      ['Students', 'Hold up to 3 active bookings at a time.'],
      ['Lecturers', 'Hold up to 5 active bookings at a time.'],
      ['Every booking', 'Lasts up to 7 days. Collect within 30 minutes of the start.'],
    ],
  },
};

function PasswordInput({ value, onChange, autoComplete }) {
  const [show, setShow] = useState(false);
  return (
    <div className="pw">
      <input type={show ? 'text' : 'password'} value={value} onChange={onChange} autoComplete={autoComplete} required />
      <button type="button" onClick={() => setShow((s) => !s)} aria-label={show ? 'Hide password' : 'Show password'}>
        {show ? 'Hide' : 'Show'}
      </button>
    </div>
  );
}

export default function AuthPage({ mode }) {
  const isLogin = mode === 'login';
  const { user, login, register } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState(EMPTY);
  const [error, setError] = useState('');
  const [fields, setFields] = useState({});
  const [busy, setBusy] = useState(false);

  // animMode drives all visuals and lags behind mode during the transition
  const [animMode, setAnimMode] = useState(mode);
  const [phase, setPhase] = useState('idle'); // 'idle' | 'out' | 'in'
  const prevMode = useRef(mode);

  useEffect(() => {
    if (mode === prevMode.current) return;
    prevMode.current = mode;
    setPhase('out');
    const t1 = setTimeout(() => { setAnimMode(mode); setPhase('in'); }, 300);
    const t2 = setTimeout(() => setPhase('idle'), 620);
    return () => { clearTimeout(t1); clearTimeout(t2); };
  }, [mode]);

  const isAnimLogin = animMode === 'login';

  if (user) return <Navigate to={user.role === 'ADMIN' ? '/admin' : '/home'} replace />;

  const set = (k) => (e) => setForm((f) => ({ ...f, [k]: e.target.value }));

  const submit = async (e) => {
    e.preventDefault();
    setError('');
    setFields({});
    setBusy(true);
    try {
      const u = isLogin ? await login(form.email, form.password) : await register(form);
      navigate(u.role === 'ADMIN' ? '/admin' : '/home', { replace: true });
    } catch (err) {
      setError(err.message);
      setFields(err.fields || {});
    } finally {
      setBusy(false);
    }
  };

  // Both lede-content and card-content get the same animation class so they
  // move in perfect sync. The 3D scene sits outside auth-content so it never
  // moves or remounts during the transition.
  const contentCls = `auth-content${phase !== 'idle' ? ` auth-content-${phase}` : ''}`;

  return (
    <div className={`auth ${isAnimLogin ? '' : 'flip'}`}>
      <header className="auth-top">
        <Link to="/login" className="wordmark">LectroLend<i>.</i></Link>
        <div className="auth-alt">
          <span className="auth-alt-text">{isLogin ? 'No account yet?' : 'Already registered?'}</span>
          <Link to={isLogin ? '/register' : '/login'}>{isLogin ? 'Create account' : 'Sign in'}</Link>
        </div>
      </header>

      <div className="auth-shell">
        <section className="auth-lede">
          {/* Scene3D lives inside auth-lede but outside auth-content so it never animates */}
          <div className="auth-scene"><Scene3D minWidth={961} /></div>
          <div className={contentCls}>
            <p className="eyebrow">{LEDE[isAnimLogin ? 'login' : 'register'].eyebrow}</p>
            <h1>{LEDE[isAnimLogin ? 'login' : 'register'].title}</h1>
            <ul className="lede-points">
              {LEDE[isAnimLogin ? 'login' : 'register'].items.map(([title, text]) => (
                <li key={title}><i aria-hidden="true" /><div><strong>{title}</strong><span>{text}</span></div></li>
              ))}
            </ul>
          </div>
        </section>

        <section className="auth-card">
          <div className={contentCls}>
            <h2>{isAnimLogin ? 'Sign in' : 'Create account'}</h2>
            <p className="lead">{isAnimLogin ? 'Welcome back. Use your AUCA email.' : 'For AUCA students and lecturers.'}</p>

            <form onSubmit={submit} noValidate>
              <ErrorNote error={error} />

              {isLogin ? (
                <>
                  <Field label="Email" error={fields.email}>
                    <input type="email" value={form.email} onChange={set('email')} autoComplete="email" placeholder="you@auca.ac.rw" required />
                  </Field>
                  <Field label="Password" error={fields.password}>
                    <PasswordInput value={form.password} onChange={set('password')} autoComplete="current-password" />
                  </Field>
                </>
              ) : (
                <>
                  <div className="seg" role="radiogroup" aria-label="I am a">
                    {['STUDENT', 'LECTURER'].map((r) => (
                      <button type="button" key={r} role="radio" aria-checked={form.role === r}
                        className={form.role === r ? 'on' : ''} onClick={() => setForm((f) => ({ ...f, role: r }))}>
                        {r === 'STUDENT' ? 'Student' : 'Lecturer'}
                      </button>
                    ))}
                  </div>
                  <div className="auth-grid">
                    <Field label="Full name" error={fields.fullName}>
                      <input value={form.fullName} onChange={set('fullName')} autoComplete="name" required />
                    </Field>
                    <Field label="Email" error={fields.email}>
                      <input type="email" value={form.email} onChange={set('email')} autoComplete="email" placeholder="you@auca.ac.rw" required />
                    </Field>
                    <Field label="Password" error={fields.password} hint="At least 8 characters">
                      <PasswordInput value={form.password} onChange={set('password')} autoComplete="new-password" />
                    </Field>
                    <Field label="Phone (optional)">
                      <input value={form.phone} onChange={set('phone')} autoComplete="tel" />
                    </Field>
                    {form.role === 'STUDENT' ? (
                      <>
                        <Field label="Registration number"><input value={form.registrationNo} onChange={set('registrationNo')} /></Field>
                        <Field label="Faculty"><input value={form.faculty} onChange={set('faculty')} /></Field>
                        <div className="span2"><Field label="Programme"><input value={form.program} onChange={set('program')} /></Field></div>
                      </>
                    ) : (
                      <>
                        <Field label="Staff number"><input value={form.staffNo} onChange={set('staffNo')} /></Field>
                        <Field label="Department"><input value={form.department} onChange={set('department')} /></Field>
                      </>
                    )}
                  </div>
                </>
              )}

              <button className="btn btn-primary btn-block" disabled={busy}>
                {busy ? 'Please wait...' : isLogin ? 'Sign in' : 'Create account'}
                {!busy && <span aria-hidden="true">&rarr;</span>}
              </button>
            </form>

            <p className="auth-foot">
              {isAnimLogin ? <>Don&apos;t have an account? <Link to="/register">Create one</Link></>
                : <>Already have an account? <Link to="/login">Sign in</Link></>}
            </p>

            {isAnimLogin && DEMO.length > 0 && (
              <div className="demo">
                <p>Demo (dev only)</p>
                {DEMO.map((d) => (
                  <button key={d.label} type="button"
                    onClick={() => setForm((f) => ({ ...f, email: d.email, password: d.password }))}>
                    {d.label}
                  </button>
                ))}
              </div>
            )}
          </div>
        </section>
      </div>
    </div>
  );
}
