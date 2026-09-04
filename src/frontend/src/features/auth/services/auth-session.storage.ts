import type { AuthSession } from '../types/auth.types'

const storageKey = 'afraz.auth.session'
let memorySession: AuthSession | null = null

function isAuthSession(value: unknown): value is AuthSession {
  if (!value || typeof value !== 'object') return false

  const session = value as Partial<AuthSession>
  return (
    typeof session.accessToken === 'string' &&
    typeof session.accessTokenExpiresAt === 'string' &&
    typeof session.refreshToken === 'string' &&
    typeof session.refreshTokenExpiresAt === 'string' &&
    Boolean(session.user && typeof session.user.userId === 'number')
  )
}

export function readAuthSession(): AuthSession | null {
  if (typeof window === 'undefined') return memorySession

  try {
    const serialized = window.localStorage.getItem(storageKey)
    if (!serialized) return memorySession
    const session: unknown = JSON.parse(serialized)
    if (isAuthSession(session)) {
      memorySession = session
      return session
    }
  } catch {
    return memorySession
  }

  clearAuthSession()
  return null
}

export function writeAuthSession(session: AuthSession) {
  memorySession = session
  try {
    window.localStorage.setItem(storageKey, JSON.stringify(session))
  } catch {
    // Keep the session in memory when persistent browser storage is unavailable.
  }
}

export function clearAuthSession() {
  memorySession = null
  if (typeof window === 'undefined') return
  try {
    window.localStorage.removeItem(storageKey)
  } catch {
    // The in-memory session has already been cleared.
  }
}
