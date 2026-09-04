const googleAuthorizationEndpoint = 'https://accounts.google.com/o/oauth2/v2/auth'
const defaultRedirectUri = 'https://afrazstudioqom.ir/signin-google'
const oauthStateStorageKey = 'afraz.google-oauth.state'

interface GoogleOAuthRequestContext {
  state: string
  returnPath: string
}

export interface ConsumedGoogleOAuthState {
  isValid: boolean | null
  returnPath: string
}

function createOAuthState() {
  const bytes = crypto.getRandomValues(new Uint8Array(24))
  return Array.from(bytes, (byte) => byte.toString(16).padStart(2, '0')).join('')
}

export function startGoogleOAuthRedirect() {
  const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID?.trim()
  const redirectUri = import.meta.env.VITE_GOOGLE_REDIRECT_URI?.trim() || defaultRedirectUri

  if (!clientId) {
    throw new Error('Google OAuth client ID is not configured.')
  }

  const state = createOAuthState()
  const requestContext: GoogleOAuthRequestContext = {
    state,
    returnPath: `${window.location.pathname}${window.location.search}${window.location.hash}`,
  }
  sessionStorage.setItem(oauthStateStorageKey, JSON.stringify(requestContext))

  const query = new URLSearchParams({
    client_id: clientId,
    redirect_uri: redirectUri,
    response_type: 'code',
    scope: 'openid email profile',
    state,
    include_granted_scopes: 'true',
    prompt: 'select_account',
  })

  window.location.assign(`${googleAuthorizationEndpoint}?${query.toString()}`)
}

export function consumeGoogleOAuthState(returnedState: string | null): ConsumedGoogleOAuthState {
  const storedContext = sessionStorage.getItem(oauthStateStorageKey)
  sessionStorage.removeItem(oauthStateStorageKey)

  if (!storedContext) return { isValid: null, returnPath: '/' }

  try {
    const context = JSON.parse(storedContext) as GoogleOAuthRequestContext
    return {
      isValid: context.state === returnedState,
      returnPath: context.returnPath.startsWith('/') ? context.returnPath : '/',
    }
  } catch {
    return { isValid: storedContext === returnedState, returnPath: '/' }
  }
}
