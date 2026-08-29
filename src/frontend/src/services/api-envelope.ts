export interface ApiErrorEntry {
  key: string
  errorCode: number | null
  errors: string[]
}

export interface ApiMetadata {
  code: number
  errorMessage: string | null
  errors: ApiErrorEntry[] | null
}

export interface PaginationInfo {
  total: number
}

export interface Envelop<T> {
  meta: ApiMetadata
  data: T | null
  pagination: PaginationInfo | null
}

export class ApiResponseError extends Error {
  readonly statusCode: number
  readonly errors: ApiErrorEntry[]

  constructor(message: string, statusCode: number, errors: ApiErrorEntry[] = []) {
    super(message)
    this.name = 'ApiResponseError'
    this.statusCode = statusCode
    this.errors = errors
  }
}

export function isEnvelop(value: unknown): value is Envelop<unknown> {
  if (typeof value !== 'object' || value === null || !('meta' in value)) {
    return false
  }

  const meta = value.meta
  return typeof meta === 'object' && meta !== null && 'code' in meta && typeof meta.code === 'number'
}

export function unwrapEnvelop<T>(envelop: Envelop<T>): T {
  if (envelop.meta.code < 200 || envelop.meta.code >= 300) {
    throw createApiResponseError(envelop)
  }

  return envelop.data as T
}

export function createApiResponseError(envelop: Envelop<unknown>): ApiResponseError {
  const errors = envelop.meta.errors ?? []
  const message =
    envelop.meta.errorMessage ?? errors.flatMap((entry) => entry.errors)[0] ?? 'خطایی رخ داده است.'

  return new ApiResponseError(message, envelop.meta.code, errors)
}
