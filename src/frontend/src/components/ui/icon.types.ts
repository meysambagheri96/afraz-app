export const appIconNames = [
  'home',
  'booking',
  'orders',
  'store',
  'profile',
  'photo-selection',
  'album',
  'newborn',
  'child',
  'birthday',
  'pregnancy',
  'family',
  'outdoor',
  'search',
  'notification',
  'chevron-back',
  'photo-stack',
  'location',
  'phone',
  'clock',
  'shield',
] as const

export type AppIconName = (typeof appIconNames)[number]
export type AppIconSize = 'xs' | 'sm' | 'md' | 'lg' | 'xl'
export type AppIconTone = 'inherit' | 'default' | 'brand' | 'muted' | 'accent'
