import { http } from '../../../services/http'
import type { AuthSession } from '../types/auth.types'

export async function exchangeGoogleAuthorizationCode(authorizationCode: string) {
  const response = await http.post<AuthSession>('/api/auth/google', { authorizationCode })
  return response.data
}
