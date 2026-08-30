export type NavigationItemId = 'home' | 'booking' | 'orders' | 'store' | 'profile'

export interface NavigationItem {
  id: NavigationItemId
  label: string
  routeName: NavigationItemId
}

export const primaryNavigationItems: readonly NavigationItem[] = [
  { id: 'home', label: 'خانه', routeName: 'home' },
  { id: 'booking', label: 'رزرو', routeName: 'booking' },
  { id: 'orders', label: 'سفارش‌ها', routeName: 'orders' },
  { id: 'store', label: 'فروشگاه', routeName: 'store' },
  { id: 'profile', label: 'پروفایل', routeName: 'profile' },
]
