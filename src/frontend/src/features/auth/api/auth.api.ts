import { http } from '../../../services/http'
import type { AuthSession, RequestOtpResponse } from '../types/auth.types'

export async function requestOtp(phone: string) {
  const response = await http.post<RequestOtpResponse>(
    '/api/auth/otp/request',
    {
      phone,
      dialingCode: '+98',
    },
    { timeout: 30_000 },
  )
  return response.data
}

export async function verifyOtp(phone: string, code: string) {
  const response = await http.post<AuthSession>(
    '/api/auth/otp/verify',
    {
      phone,
      code,
      dialingCode: '+98',
    },
    { timeout: 30_000 },
  )
  return response.data
}
