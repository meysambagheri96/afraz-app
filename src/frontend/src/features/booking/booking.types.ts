export type CalendarDayState = 'available' | 'selected' | 'full' | 'disabled' | 'holiday'

export interface PersianDateParts {
  year: number
  month: number
  day: number
}

export interface CalendarDay {
  dateKey: string
  day: number
  dayLabel: string
  fullLabel: string
  isCurrentMonth: boolean
  isToday: boolean
  baseState: Exclude<CalendarDayState, 'selected'>
  state: CalendarDayState
}
