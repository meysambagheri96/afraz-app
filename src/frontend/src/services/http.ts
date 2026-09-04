import axios, { type AxiosError, type InternalAxiosRequestConfig } from 'axios'
import {
  clearAuthSession,
  readAuthSession,
  writeAuthSession,
} from '../features/auth/services/auth-session.storage'
import type { AuthSession } from '../features/auth/types/auth.types'
import { createApiResponseError, isEnvelop, unwrapEnvelop, type Envelop } from './api-envelope'

export const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || undefined,
  headers: {
    Accept: 'application/json',
  },
  timeout: 15_000,
})

const refreshHttp = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || undefined,
  headers: { Accept: 'application/json' },
  timeout: 15_000,
})

interface RetryableRequestConfig extends InternalAxiosRequestConfig {
  _authRetry?: boolean
}

let refreshPromise: Promise<AuthSession> | null = null

http.interceptors.request.use((config) => {
  const token = readAuthSession()?.accessToken
  if (token) config.headers.Authorization = `Bearer ${token}`
  return config
})

async function refreshSession(): Promise<AuthSession> {
  const session = readAuthSession()
  if (!session?.refreshToken) throw new Error('No refresh token is available.')

  const response = await refreshHttp.post<Envelop<AuthSession>>('/api/auth/refresh', {
    refreshToken: session.refreshToken,
  })
  const refreshed = unwrapEnvelop(response.data)
  writeAuthSession(refreshed)
  return refreshed
}

http.interceptors.response.use(
  (response) => {
    if (isEnvelop(response.data)) {
      response.data = unwrapEnvelop(response.data)
    }

    return response
  },
  async (error: AxiosError) => {
    const request = error.config as RetryableRequestConfig | undefined
    const isAuthEndpoint = request?.url?.startsWith('/api/auth/') ?? false

    if (error.response?.status === 401 && request && !request._authRetry && !isAuthEndpoint) {
      request._authRetry = true
      refreshPromise ??= refreshSession().finally(() => {
        refreshPromise = null
      })

      try {
        const refreshed = await refreshPromise
        request.headers.Authorization = `Bearer ${refreshed.accessToken}`
        return await http(request)
      } catch {
        clearAuthSession()
      }
    }

    if (isEnvelop(error.response?.data)) {
      return Promise.reject(createApiResponseError(error.response.data as Envelop<unknown>))
    }

    return Promise.reject(error)
  },
)
