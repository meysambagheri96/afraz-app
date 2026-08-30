import { registerSW } from 'virtual:pwa-register'

const updateIntervalMs = 5 * 60 * 1000

export function registerServiceWorker() {
  registerSW({
    immediate: true,
    onRegisteredSW: (_serviceWorkerUrl, registration) => {
      if (!registration) return

      const checkForUpdate = () => {
        void registration.update().catch(() => undefined)
      }

      checkForUpdate()
      window.setInterval(checkForUpdate, updateIntervalMs)
      window.addEventListener('online', checkForUpdate)
      document.addEventListener('visibilitychange', () => {
        if (document.visibilityState === 'visible') checkForUpdate()
      })
    },
  })
}
