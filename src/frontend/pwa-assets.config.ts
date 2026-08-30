import { defineConfig } from '@vite-pwa/assets-generator/config'

export default defineConfig({
  preset: {
    transparent: {
      sizes: [192, 512],
      padding: 0,
      favicons: [[48, 'favicon.ico']],
      resizeOptions: {
        fit: 'contain',
        background: { r: 0, g: 0, b: 0, alpha: 0 },
      },
    },
    maskable: {
      sizes: [192, 512],
      padding: 0.18,
      resizeOptions: {
        fit: 'contain',
        background: '#075d69',
      },
    },
    apple: {
      sizes: [180],
      padding: 0.12,
      resizeOptions: {
        fit: 'contain',
        background: '#fcfbf9',
      },
    },
  },
  images: ['public/icons/afraz-logo.svg'],
})
