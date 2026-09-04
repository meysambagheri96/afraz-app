import { computed, ref } from 'vue'
import { defineStore } from 'pinia'
import {
  clearAuthSession,
  readAuthSession,
  writeAuthSession,
} from '../services/auth-session.storage'
import type { AuthSession } from '../types/auth.types'

export const useAuthStore = defineStore('auth', () => {
  const session = ref<AuthSession | null>(readAuthSession())
  const isAuthenticated = computed(
    () =>
      Boolean(session.value?.accessToken) &&
      Boolean(session.value?.user.isActive) &&
      Date.parse(session.value?.refreshTokenExpiresAt ?? '') > Date.now(),
  )

  function setSession(value: AuthSession) {
    session.value = value
    writeAuthSession(value)
  }

  function clearSession() {
    session.value = null
    clearAuthSession()
  }

  return { session, isAuthenticated, setSession, clearSession }
})
