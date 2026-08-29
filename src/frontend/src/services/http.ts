import axios, { type AxiosError } from 'axios'
import {
  createApiResponseError,
  isEnvelop,
  unwrapEnvelop,
  type Envelop,
} from './api-envelope'

export const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || undefined,
  headers: {
    Accept: 'application/json',
  },
  timeout: 15_000,
})

http.interceptors.response.use(
  (response) => {
    if (isEnvelop(response.data)) {
      response.data = unwrapEnvelop(response.data)
    }

    return response
  },
  (error: AxiosError) => {
    if (isEnvelop(error.response?.data)) {
      return Promise.reject(createApiResponseError(error.response.data as Envelop<unknown>))
    }

    return Promise.reject(error)
  },
)
