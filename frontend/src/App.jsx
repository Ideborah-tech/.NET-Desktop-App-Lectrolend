import { Navigate, Outlet, Route, Routes } from 'react-router-dom';
import { useAuth } from './context/AuthContext.jsx';
import Layout from './components/layout/Layout.jsx';
import { Loading } from './components/ui/index.js';
import AuthPage from './pages/auth/AuthPage.jsx';
import Home from './pages/borrower/Home.jsx';
import Catalogue from './pages/borrower/Catalogue.jsx';
import MyReservations from './pages/borrower/MyReservations.jsx';
import Dashboard from './pages/admin/Dashboard.jsx';
import EquipmentAdmin from './pages/admin/EquipmentAdmin.jsx';
import ReservationsAdmin from './pages/admin/ReservationsAdmin.jsx';
import RoomsAdmin from './pages/admin/RoomsAdmin.jsx';
import UsersAdmin from './pages/admin/UsersAdmin.jsx';
import Profile from './pages/profile/Profile.jsx';

const home = (user) => (user.role === 'ADMIN' ? '/admin' : '/home');

/** Gate: must be logged in, optionally with a specific kind of role. */
function Protected({ admin, borrower }) {
  const { user, loading } = useAuth();
  if (loading) return <div className="boot"><Loading text="Signing you in" /></div>;
  if (!user) return <Navigate to="/login" replace />;
  if (admin && user.role !== 'ADMIN') return <Navigate to={home(user)} replace />;
  if (borrower && user.role === 'ADMIN') return <Navigate to={home(user)} replace />;
  return <Outlet />;
}

function Landing() {
  const { user, loading } = useAuth();
  if (loading) return <div className="boot"><Loading /></div>;
  return <Navigate to={user ? home(user) : '/login'} replace />;
}

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<AuthPage mode="login" />} />
      <Route path="/register" element={<AuthPage mode="register" />} />
      <Route path="/" element={<Landing />} />

      <Route element={<Protected borrower />}>
        <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/catalogue" element={<Catalogue />} />
          <Route path="/my-reservations" element={<MyReservations />} />
        </Route>
      </Route>

      <Route element={<Protected admin />}>
        <Route element={<Layout />}>
          <Route path="/admin" element={<Dashboard />} />
          <Route path="/admin/equipment" element={<EquipmentAdmin />} />
          <Route path="/admin/reservations" element={<ReservationsAdmin />} />
          <Route path="/admin/rooms" element={<RoomsAdmin />} />
          <Route path="/admin/users" element={<UsersAdmin />} />
        </Route>
      </Route>

      <Route element={<Protected />}>
        <Route element={<Layout />}>
          <Route path="/profile" element={<Profile />} />
        </Route>
      </Route>

      <Route path="*" element={<Landing />} />
    </Routes>
  );
}
