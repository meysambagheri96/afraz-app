import axios from 'axios'

export const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || undefined,
  headers: {
    Accept: 'application/json',
  },
  timeout: 15_000,
})

