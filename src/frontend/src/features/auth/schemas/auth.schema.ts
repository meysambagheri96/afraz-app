import { z } from 'zod'

export const mobileSchema = z
  .string()
  .trim()
  .regex(/^(?:9|۹)[0-9۰-۹]{9}$/, 'شماره موبایل باید ۱۰ رقم و بدون صفر ابتدایی باشد.')

export function normalizeMobileDigits(value: string) {
  return value.trim().replace(/[۰-۹]/g, (digit) => String('۰۱۲۳۴۵۶۷۸۹'.indexOf(digit)))
}

export const otpSchema = z
  .string()
  .regex(/^[0-9۰-۹]{5}$/, 'کد تأیید باید ۵ رقم باشد.')
