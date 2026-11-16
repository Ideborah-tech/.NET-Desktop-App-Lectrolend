import { request, qs } from './http.js';

export { ApiError, tokenStore, setUnauthorizedHandler, downloadFile } from './http.js';

export const api = {
  login: (email, password) => request('POST', '/auth/login', { email, password }),
  register: (payload) => request('POST', '/auth/register', payload),
  me: () => request('GET', '/auth/me'),
  updateProfile: (payload) => request('PUT', '/auth/me', payload),
  changePassword: (currentPassword, newPassword) => request('POST', '/auth/change-password', { currentPassword, newPassword }),

  equipment: {
    list: (params) => request('GET', `/equipment${qs(params)}`),
    availability: (id, start, end) => request('GET', `/equipment/${id}/availability${qs({ start, end })}`),
    create: (payload) => request('POST', '/equipment', payload),
    update: (id, payload) => request('PUT', `/equipment/${id}`, payload),
    setStatus: (id, status) => request('PATCH', `/equipment/${id}/status`, { status }),
    remove: (id) => request('DELETE', `/equipment/${id}`),
  },
  rooms: {
    list: () => request('GET', '/rooms'),
    create: (payload) => request('POST', '/rooms', payload),
    update: (id, payload) => request('PUT', `/rooms/${id}`, payload),
    remove: (id) => request('DELETE', `/rooms/${id}`),
  },
  reservations: {
    create: (payload) => request('POST', '/reservations', payload),
    createBatch: (payload) => request('POST', '/reservations/batch', payload),
    mine: () => request('GET', '/reservations/mine'),
    all: (status) => request('GET', `/reservations${qs({ status })}`),
    cancel: (id) => request('POST', `/reservations/${id}/cancel`),
    issue: (id) => request('POST', `/reservations/${id}/issue`),
    confirmReturn: (id, payload) => request('POST', `/reservations/${id}/return`, payload),
  },
  users: {
    list: () => request('GET', '/users'),
    create: (payload) => request('POST', '/users', payload),
    setActive: (id, active) => request('PATCH', `/users/${id}/active`, { active }),
  },
  reports: {
    summary: () => request('GET', '/reports/summary'),
    download: () => request('GET', '/reports/download'),
  },
};
