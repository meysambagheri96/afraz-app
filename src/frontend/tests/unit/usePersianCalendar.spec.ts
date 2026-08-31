import { nextTick } from 'vue'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import { usePersianCalendar } from '../../src/features/booking/composables/usePersianCalendar'

describe('usePersianCalendar', () => {
  beforeEach(() => {
    vi.useFakeTimers()
    vi.setSystemTime(new Date('2026-08-31T08:00:00+03:30'))
  })

  afterEach(() => {
    vi.useRealTimers()
  })

  it('preserves the selected date after navigating away from its month and back', async () => {
    const calendar = usePersianCalendar()
    await nextTick()

    const availableDay = calendar.days.value.find(
      (day) => day.baseState === 'available' && day.dateKey !== calendar.selectedDay.value?.dateKey,
    )

    expect(availableDay).toBeDefined()
    calendar.selectDay(availableDay!)
    await nextTick()

    const selectedDateKey = calendar.selectedDay.value?.dateKey
    expect(selectedDateKey).toBe(availableDay?.dateKey)

    calendar.moveMonth(1)
    await nextTick()
    expect(calendar.selectedDay.value).toBeNull()

    calendar.moveMonth(-1)
    await nextTick()
    expect(calendar.selectedDay.value?.dateKey).toBe(selectedDateKey)
  })
})
