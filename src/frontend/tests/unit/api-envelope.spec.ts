import { describe, expect, it } from 'vitest'
import { ApiResponseError, unwrapEnvelop, type Envelop } from '../../src/services/api-envelope'

describe('API envelope', () => {
  it('unwraps successful response data', () => {
    const response: Envelop<{ status: string }> = {
      meta: { code: 200, errorMessage: null, errors: null },
      data: { status: 'ready' },
      pagination: null,
    }

    expect(unwrapEnvelop(response)).toEqual({ status: 'ready' })
  })

  it('throws a typed error for unsuccessful envelopes', () => {
    const response: Envelop<null> = {
      meta: {
        code: 400,
        errorMessage: 'Validation failed.',
        errors: [{ key: 'name', errorCode: 400, errors: ['Required'] }],
      },
      data: null,
      pagination: null,
    }

    expect(() => unwrapEnvelop(response)).toThrow(ApiResponseError)
  })
})
