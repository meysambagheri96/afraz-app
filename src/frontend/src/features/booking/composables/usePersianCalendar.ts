import { computed, ref, watch } from 'vue'
import type { CalendarDay, CalendarDayState, PersianDateParts } from '../booking.types'

const dayInMilliseconds = 86_400_000
const persianMonthNames = [
  'فروردین',
  'اردیبهشت',
  'خرداد',
  'تیر',
  'مرداد',
  'شهریور',
  'مهر',
  'آبان',
  'آذر',
  'دی',
  'بهمن',
  'اسفند',
] as const
const persianWeekdayNames = ['شنبه', 'یکشنبه', 'دوشنبه', 'سه‌شنبه', 'چهارشنبه', 'پنجشنبه', 'جمعه'] as const
const shortWeekdayNames = ['ش', 'ی', 'د', 'س', 'چ', 'پ', 'ج'] as const

const persianPartsFormatter = new Intl.DateTimeFormat('en-US-u-ca-persian-nu-latn', {
  timeZone: 'Asia/Tehran',
  year: 'numeric',
  month: 'numeric',
  day: 'numeric',
})
const islamicPartsFormatter = new Intl.DateTimeFormat('fa-IR-u-ca-islamic', {
  timeZone: 'Asia/Tehran',
  year: 'numeric',
  month: 'long',
})

function readCalendarParts(formatter: Intl.DateTimeFormat, date: Date) {
  return Object.fromEntries(
    formatter
      .formatToParts(date)
      .filter(({ type }) => type !== 'literal')
      .map(({ type, value }) => [type, value]),
  )
}

function getPersianParts(date: Date): PersianDateParts {
  const parts = readCalendarParts(persianPartsFormatter, date)
  return {
    year: Number(parts.year),
    month: Number(parts.month),
    day: Number(parts.day),
  }
}

function findGregorianDate({ year, month, day }: PersianDateParts) {
  const approximateDate = Date.UTC(year + 621, month + 1, 15, 12)

  for (let offset = -50; offset <= 50; offset += 1) {
    const candidate = new Date(approximateDate + offset * dayInMilliseconds)
    const parts = getPersianParts(candidate)
    if (parts.year === year && parts.month === month && parts.day === day) return candidate
  }

  throw new Error(`Unable to resolve Persian date ${year}/${month}/${day}`)
}

function addMonths(year: number, month: number, amount: number) {
  const absoluteMonth = year * 12 + month - 1 + amount
  return {
    year: Math.floor(absoluteMonth / 12),
    month: ((absoluteMonth % 12) + 12) % 12 + 1,
  }
}

function toDateNumber({ year, month, day }: PersianDateParts) {
  return year * 10_000 + month * 100 + day
}

function toDateKey({ year, month, day }: PersianDateParts) {
  return `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`
}

function getBaseState(
  parts: PersianDateParts,
  isCurrentMonth: boolean,
  todayNumber: number,
): Exclude<CalendarDayState, 'selected'> {
  if (!isCurrentMonth || toDateNumber(parts) < todayNumber) return 'disabled'
  if ([4, 13, 29].includes(parts.day)) return 'holiday'
  if ([8, 21].includes(parts.day)) return 'full'
  if ([11, 23].includes(parts.day)) return 'disabled'
  return 'available'
}

function getIslamicMonthLabel(firstDate: Date, lastDate: Date) {
  const first = readCalendarParts(islamicPartsFormatter, firstDate)
  const last = readCalendarParts(islamicPartsFormatter, lastDate)
  if (first.month === last.month) return `${first.month} ${last.year}`
  return `${first.month} - ${last.month} ${last.year}`
}

export function usePersianCalendar() {
  const todayParts = getPersianParts(new Date())
  const displayedYear = ref(todayParts.year)
  const displayedMonth = ref(todayParts.month)
  const selectedDateKey = ref<string | null>(null)

  const calendarData = computed(() => {
    const firstDate = findGregorianDate({
      year: displayedYear.value,
      month: displayedMonth.value,
      day: 1,
    })
    const nextMonth = addMonths(displayedYear.value, displayedMonth.value, 1)
    const nextMonthFirstDate = findGregorianDate({ ...nextMonth, day: 1 })
    const monthLength = Math.round(
      (nextMonthFirstDate.getTime() - firstDate.getTime()) / dayInMilliseconds,
    )
    const firstWeekdayOffset = (firstDate.getUTCDay() + 1) % 7
    const gridStart = new Date(firstDate.getTime() - firstWeekdayOffset * dayInMilliseconds)
    const todayNumber = toDateNumber(todayParts)

    const days: CalendarDay[] = Array.from({ length: 42 }, (_, index) => {
      const gregorianDate = new Date(gridStart.getTime() + index * dayInMilliseconds)
      const parts = getPersianParts(gregorianDate)
      const isCurrentMonth =
        parts.year === displayedYear.value && parts.month === displayedMonth.value
      const baseState = getBaseState(parts, isCurrentMonth, todayNumber)
      const dateKey = toDateKey(parts)
      const state =
        baseState === 'available' && selectedDateKey.value === dateKey ? 'selected' : baseState
      const weekdayIndex = (gregorianDate.getUTCDay() + 1) % 7

      return {
        dateKey,
        day: parts.day,
        dayLabel: parts.day.toLocaleString('fa-IR', { useGrouping: false }),
        fullLabel: `${persianWeekdayNames[weekdayIndex]} ${parts.day.toLocaleString('fa-IR')} ${persianMonthNames[parts.month - 1]} ${parts.year.toLocaleString('fa-IR', { useGrouping: false })}`,
        isCurrentMonth,
        isToday: toDateNumber(parts) === todayNumber,
        baseState,
        state,
      }
    })

    return {
      days,
      monthLength,
      islamicMonthLabel: getIslamicMonthLabel(
        firstDate,
        new Date(firstDate.getTime() + (monthLength - 1) * dayInMilliseconds),
      ),
    }
  })

  watch(
    () => calendarData.value.days,
    (days) => {
      if (selectedDateKey.value !== null) return

      selectedDateKey.value = days.find(
        ({ baseState, isCurrentMonth }) => isCurrentMonth && baseState === 'available',
      )?.dateKey ?? null
    },
    { immediate: true },
  )

  const monthLabel = computed(
    () =>
      `${persianMonthNames[displayedMonth.value - 1]} ${displayedYear.value.toLocaleString('fa-IR', { useGrouping: false })}`,
  )
  const selectedDay = computed(
    () => calendarData.value.days.find(({ dateKey }) => dateKey === selectedDateKey.value) ?? null,
  )

  function moveMonth(amount: number) {
    const next = addMonths(displayedYear.value, displayedMonth.value, amount)
    displayedYear.value = next.year
    displayedMonth.value = next.month
  }

  function selectDay(day: CalendarDay) {
    if (day.baseState !== 'available') return
    selectedDateKey.value = day.dateKey
  }

  return {
    days: computed(() => calendarData.value.days),
    islamicMonthLabel: computed(() => calendarData.value.islamicMonthLabel),
    monthLabel,
    selectedDay,
    shortWeekdayNames,
    moveMonth,
    selectDay,
  }
}
