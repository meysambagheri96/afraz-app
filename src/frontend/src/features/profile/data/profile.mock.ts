import {
  BookOpen,
  CalendarDays,
  CircleUserRound,
  Headphones,
  Image,
  Info,
  LogOut,
  MapPin,
  Package,
} from '@lucide/vue'
import avatarUrl from '../../../assets/images/profile/sara-mohammadi.webp'
import type { CustomerProfile, ProfileMenuItem } from '../profile.types'

export const mockCustomerProfile: CustomerProfile = {
  name: 'سارا محمدی',
  mobile: '۰۹۱۲ ۱۲۳ ۴۵۶۷',
  membership: 'عضو ویژه',
  avatarUrl,
  avatarAlt: 'تصویر پروفایل سارا محمدی',
  isAdmin: false,
}

export const profileMenuItems: ProfileMenuItem[] = [
  { id: 'bookings', title: 'رزروهای من', subtitle: 'مشاهده و مدیریت رزروهای آتلیه', icon: CalendarDays },
  { id: 'orders', title: 'سفارش‌های من', subtitle: 'مشاهده سفارش‌ها و وضعیت پرداخت', icon: Package },
  { id: 'photos', title: 'عکس‌های من', subtitle: 'مشاهده و انتخاب عکس‌های آماده شده', icon: Image },
  { id: 'albums', title: 'آلبوم‌های من', subtitle: 'آلبوم‌های خریداری شده و در حال ساخت', icon: BookOpen },
  { id: 'addresses', title: 'آدرس‌های من', subtitle: 'مدیریت آدرس‌های ارسال سفارش‌ها', icon: MapPin },
  { id: 'account', title: 'اطلاعات حساب', subtitle: 'مشاهده و ویرایش اطلاعات کاربری', icon: CircleUserRound },
  { id: 'support', title: 'پشتیبانی', subtitle: 'تماس با پشتیبانی و ثبت درخواست', icon: Headphones },
  { id: 'about', title: 'درباره آتلیه افراز', subtitle: 'با ما و خدمات ما بیشتر آشنا شوید', icon: Info },
  { id: 'logout', title: 'خروج از حساب کاربری', icon: LogOut, destructive: true },
]

export const adminMenuItems: ProfileMenuItem[] = [
  { id: 'admin-content', title: 'مدیریت محتوا', icon: Image },
  { id: 'admin-orders', title: 'مدیریت سفارشات', icon: Package },
  { id: 'admin-bookings', title: 'مدیریت رزروها', icon: CalendarDays },
  { id: 'admin-banners', title: 'مدیریت بنرها', icon: BookOpen },
]
