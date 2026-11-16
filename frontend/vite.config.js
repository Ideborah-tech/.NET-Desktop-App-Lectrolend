import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// The API runs on 8081 by default; override with VITE_PROXY_TARGET if you changed SERVER_PORT.
const target = process.env.VITE_PROXY_TARGET || 'http://localhost:8082';

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: { '/api': { target, changeOrigin: true } },
  },
});
