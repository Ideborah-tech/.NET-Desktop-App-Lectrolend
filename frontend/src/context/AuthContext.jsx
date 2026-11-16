import { createContext, useCallback, useContext, useEffect, useMemo, useState } from 'react';
import { api, setUnauthorizedHandler, tokenStore } from '../api/index.js';

const AuthContext = createContext(null);
export const useAuth = () => useContext(AuthContext);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(Boolean(tokenStore.get()));

  const logout = useCallback(() => {
    tokenStore.clear();
    setUser(null);
  }, []);

  useEffect(() => {
    setUnauthorizedHandler(logout);
    if (!tokenStore.get()) return;
    api.me().then(setUser).catch(() => tokenStore.clear()).finally(() => setLoading(false));
  }, [logout]);

  const accept = useCallback((res) => {
    tokenStore.set(res.token);
    setUser(res.user);
    return res.user;
  }, []);

  const value = useMemo(() => ({
    user,
    loading,
    isAdmin: user?.role === 'ADMIN',
    login: async (email, password) => accept(await api.login(email, password)),
    register: async (payload) => accept(await api.register(payload)),
    updateUser: setUser,
    logout,
  }), [user, loading, logout, accept]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
