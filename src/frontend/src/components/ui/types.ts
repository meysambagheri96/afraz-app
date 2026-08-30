export type AppControlSize = 'sm' | 'md' | 'lg'

export type AppButtonVariant = 'primary' | 'secondary' | 'ghost' | 'danger' | 'glass'

export type AppBadgeTone = 'neutral' | 'success' | 'warning' | 'danger' | 'info'

export type AppSelectValue = string | number

export interface AppSelectOption {
  label: string
  value: AppSelectValue
  disabled?: boolean
}
