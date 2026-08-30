import bannerUrl from '../../assets/images/photography/home/banner.png'
import sampleOneUrl from '../../assets/images/photography/home/sample-1.png'
import sampleTwoUrl from '../../assets/images/photography/home/sample-2.png'
import sampleThreeUrl from '../../assets/images/photography/home/sample-3.png'
import type {
  AlbumPreviewItem,
  HomeAction,
  LastOrder,
  PhotographyCategory,
  PortfolioItem,
} from './home.types'

export const homeHeroImageUrl = bannerUrl

export const quickActions: readonly HomeAction[] = [
  { id: 'booking', label: 'رزرو نوبت', icon: 'booking', to: { name: 'booking' } },
  { id: 'orders', label: 'سفارش‌های من', icon: 'orders', to: { name: 'orders' } },
  {
    id: 'photo-selection',
    label: 'انتخاب عکس برای چاپ',
    icon: 'photo-selection',
    to: { name: 'orders', query: { action: 'photo-selection' } },
  },
  { id: 'album', label: 'ساخت آلبوم', icon: 'album', to: { name: 'albums' } },
]

export const featuredPortfolio: readonly PortfolioItem[] = [
  { id: 'newborn', imageUrl: sampleOneUrl, alt: 'عکاسی نوزاد در دکور روشن' },
  { id: 'child', imageUrl: sampleTwoUrl, alt: 'عکاسی کودک در فضای باز' },
  { id: 'girl', imageUrl: sampleThreeUrl, alt: 'پرتره کودک کنار گل‌های سفید' },
]

export const photographyCategories: readonly PhotographyCategory[] = [
  { id: 'newborn', label: 'نوزاد', icon: 'newborn', to: { name: 'portfolio-category', params: { category: 'newborn' } } },
  { id: 'child', label: 'کودک', icon: 'child', to: { name: 'portfolio-category', params: { category: 'child' } } },
  { id: 'birthday', label: 'تولد', icon: 'birthday', to: { name: 'portfolio-category', params: { category: 'birthday' } } },
  { id: 'family', label: 'خانوادگی', icon: 'family', to: { name: 'portfolio-category', params: { category: 'family' } } },
]

export const latestOrder: LastOrder = {
  id: 'AFR-1404-0012',
  studioName: 'آتلیه نوزاد',
  dateLabel: '۱۲ فروردین ۱۴۰۴',
  statusLabel: 'در حال ویرایش',
  newPhotoCount: 235,
  thumbnailUrl: sampleOneUrl,
  to: { name: 'orders', query: { order: 'AFR-1404-0012' } },
}

export const albumPreviews: readonly AlbumPreviewItem[] = [
  { id: 'newborn-album', title: 'آلبوم نوزاد', caption: 'ثبت اولین خاطره‌ها', accent: 'mint', to: { name: 'store', query: { category: 'newborn-albums' } } },
  { id: 'child-album', title: 'آلبوم کودک', caption: 'چاپ ماندگار لحظه‌ها', accent: 'pink', to: { name: 'store', query: { category: 'child-albums' } } },
  { id: 'custom-album', title: 'آلبوم اختصاصی', caption: 'ساخته‌شده برای شما', accent: 'lilac', to: { name: 'albums' } },
]
