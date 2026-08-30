/// <reference types="vitest/config" />

import { fileURLToPath, URL } from 'node:url'
import tailwindcss from '@tailwindcss/vite'
import vue from '@vitejs/plugin-vue'
import { defineConfig, loadEnv } from 'vite'
import { VitePWA } from 'vite-plugin-pwa'

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '')

  return {
    base: '/afraz-app/',
    plugins: [
      vue(),
      tailwindcss(),
      VitePWA({
        registerType: 'autoUpdate',
        injectRegister: 'auto',
        manifest: false,
        workbox: {
          globPatterns: [
            'assets/**/*.{js,css,ico,png,svg,jpg,jpeg,webp,avif,woff,woff2}',
          ],
          manifestTransforms: [
            (entries) => ({
              manifest: entries.filter(({ url }) => url.startsWith('assets/')),
              warnings: [],
            }),
          ],
          cleanupOutdatedCaches: true,
          clientsClaim: true,
          skipWaiting: true,
          navigateFallback: null,
          runtimeCaching: [
            {
              urlPattern: ({ request }) => request.mode === 'navigate',
              handler: 'NetworkOnly',
              options: { fetchOptions: { cache: 'no-store' } },
            },
            {
              urlPattern: ({ url }) => url.pathname.endsWith('/manifest.webmanifest'),
              handler: 'NetworkOnly',
              options: { fetchOptions: { cache: 'no-store' } },
            },
            {
              urlPattern: ({ url }) => url.pathname.startsWith('/api/'),
              handler: 'NetworkOnly',
              options: { fetchOptions: { cache: 'no-store' } },
            },
          ],
        },
      }),
    ],
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url)),
      },
    },
    build: {
      outDir: 'dist',
      emptyOutDir: true,
    },
    server: {
      port: 5173,
      proxy: {
        '/api': {
          target: env.VITE_DEV_API_TARGET || 'http://localhost:5080',
          changeOrigin: true,
          secure: false,
        },
        '/health': {
          target: env.VITE_DEV_API_TARGET || 'http://localhost:5080',
          changeOrigin: true,
          secure: false,
        },
      },
    },
    test: {
      environment: 'jsdom',
      setupFiles: './tests/setup.ts',
      include: ['./tests/unit/**/*.spec.ts'],
    },
  }
})
