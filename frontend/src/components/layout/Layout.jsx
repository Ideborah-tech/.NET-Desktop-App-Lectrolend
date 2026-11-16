import { useCallback, useEffect, useState } from 'react';
import { Link, NavLink, Outlet, useLocation, useNavigate } from 'react-router-dom';
import { api } from '../../api/index.js';
import { useAuth } from '../../context/AuthContext.jsx';
import { label } from '../../utils/format.js';

const BORROWER_NAV = [
  { to: '/home', text: 'Home' },
  { to: '/catalogue', text: 'Borrow' },
  { to: '/my-reservations', text: 'My bookings' },
  { to: '/profile', text: 'Profile' },
];
const ADMIN_NAV = [
  { to: '/admin', text: 'Dashboard', end: true },
  { to: '/admin/reservations', text: 'Bookings' },
  { to: '/admin/equipment', text: 'Equipment' },
  { to: '/admin/rooms', text: 'Rooms' },
  { to: '/admin/users', text: 'Users' },
  { to: '/profile', text: 'Profile' },
];

/** Slim header on top, floating pill "dock" with the navigation at the bottom of the screen. */
export default function Layout() {
  const { user, isAdmin, logout } = useAuth();
  const navigate = useNavigate();
  const nav = isAdmin ? ADMIN_NAV : BORROWER_NAV;
  const { pathname } = useLocation();
  const [active, setActive] = useState(0);

  // badge on "My bookings": refresh on navigation and whenever a booking is made or changed
  const refresh = useCallback(() => {
    if (isAdmin) return;
    api.reservations.mine()
      .then((l) => setActive(l.filter((r) => ['BOOKED', 'ISSUED', 'OVERDUE'].includes(r.status)).length))
      .catch(() => {});
  }, [isAdmin]);
  useEffect(() => { refresh(); }, [refresh, pathname]);
  useEffect(() => {
    window.addEventListener('bookings:changed', refresh);
    return () => window.removeEventListener('bookings:changed', refresh);
  }, [refresh]);

  const signOut = () => {
    logout();
    navigate('/login', { replace: true });
  };

  return (
    <div className="app">
      <header className="topbar">
        <Link to={isAdmin ? '/admin' : '/home'} className="wordmark" aria-label="LectroLend home">
          Lectro<em>Lend</em>
        </Link>
        <div className="topbar-right">
          <Link to="/profile" className="who" aria-label="Your profile">
            <span className="avatar" aria-hidden="true">{user.fullName.charAt(0)}</span>
            <span className="who-text">
              <strong>{user.fullName}</strong>
              <small>{label(user.role)}</small>
            </span>
          </Link>
          <button className="btn btn-sm" onClick={signOut}>Sign out</button>
        </div>
      </header>

      <main className="page">
        <Outlet />
      </main>

      <nav className="dock" aria-label="Main">
        {nav.map((n) => (
          <NavLink key={n.to} to={n.to} end={n.end} className={({ isActive }) => `dock-link ${isActive ? 'active' : ''}`}>
            {n.text}
            {n.to === '/my-reservations' && active > 0 && <em className="dock-badge">{active}</em>}
          </NavLink>
        ))}
      </nav>
    </div>
  );
}
