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
        includeAssets: [
          'icons/favicon.ico',
          'icons/apple-touch-icon-180x180.png',
          'icons/afraz-logo.svg',
        ],
        manifest: {
          id: '/afraz-app/',
          name: 'آتلیه افراز قم',
          short_name: 'افراز',
          description: 'اپلیکیشن آتلیه افراز قم',
          lang: 'fa',
          dir: 'rtl',
          start_url: '.',
          scope: '.',
          display: 'standalone',
          orientation: 'portrait-primary',
          theme_color: '#075d69',
          background_color: '#fcfbf9',
          icons: [
            {
              src: 'icons/pwa-192x192.png',
              sizes: '192x192',
              type: 'image/png',
              purpose: 'any',
            },
            {
              src: 'icons/pwa-512x512.png',
              sizes: '512x512',
              type: 'image/png',
              purpose: 'any',
            },
            {
              src: 'icons/maskable-icon-192x192.png',
              sizes: '192x192',
              type: 'image/png',
              purpose: 'maskable',
            },
            {
              src: 'icons/maskable-icon-512x512.png',
              sizes: '512x512',
              type: 'image/png',
              purpose: 'maskable',
            },
          ],
        },
        workbox: {
          globPatterns: ['**/*.{js,css,html,ico,png,svg,jpg,jpeg,webp,woff2}'],
          cleanupOutdatedCaches: true,
          clientsClaim: true,
          skipWaiting: true,
          navigateFallback: 'index.html',
          runtimeCaching: [],
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
