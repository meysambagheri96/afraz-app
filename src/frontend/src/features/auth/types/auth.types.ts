export interface AuthUser {
  userId: number
  firstName: string
  lastName: string
  phone: string
  dialingCode: string
  email: string | null
  avatar: string | null
  nationalCode: string | null
  shebaNumber: string | null
  cardNumber: string | null
  accountNumber: string | null
  gender: number | null
  birthDate: string | null
  isActive: boolean
}

export interface AuthSession {
  accessToken: string
  accessTokenExpiresAt: string
  refreshToken: string
  refreshTokenExpiresAt: string
  user: AuthUser
}

export interface RequestOtpResponse {
  expiresAt: string
}
