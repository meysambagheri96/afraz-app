import { http } from '../../../services/http'

export interface GoogleLoginResult {
  user: {
    userId: number
    firstName: string
    lastName: string
    email: string | null
  }
}

export async function exchangeGoogleAuthorizationCode(authorizationCode: string) {
  const response = await http.post<GoogleLoginResult>('/api/auth/google', { authorizationCode })
  return response.data
}
