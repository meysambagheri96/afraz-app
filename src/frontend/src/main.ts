import { createPinia } from 'pinia'
import { createApp } from 'vue'
import App from './App.vue'
import { router } from './router'
import { registerServiceWorker } from './services/pwa/register-service-worker'
import './styles/main.css'

registerServiceWorker()

createApp(App).use(createPinia()).use(router).mount('#app')
