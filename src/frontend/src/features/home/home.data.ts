import bannerUrl from '../../assets/images/photography/home/banner.jpg'
import sampleOneUrl from '../../assets/images/photography/home/sample-1.png'
import sampleTwoUrl from '../../assets/images/photography/home/sample-2.png'
import sampleThreeUrl from '../../assets/images/photography/home/sample-3.png'
import type {
  AlbumPreviewItem,
  HomeAction,
  LastOrder,
  HomeStory,
  PhotographyCategory,
  PortfolioItem,
} from './home.types'

export const homeHeroImageUrl = bannerUrl

export const homeStories: readonly HomeStory[] = [
  { id: 'family', label: 'خانوادگی', imageUrl: sampleThreeUrl, imageAlt: 'نمونه عکاسی خانوادگی کودک' },
  { id: 'birthday', label: 'تولد', icon: 'birthday' },
  { id: 'pregnancy', label: 'بارداری', imageUrl: sampleOneUrl, imageAlt: 'نمونه عکاسی بارداری' },
  { id: 'child', label: 'کودک', imageUrl: sampleTwoUrl, imageAlt: 'نمونه عکاسی کودک' },
  { id: 'newborn', label: 'نوزاد', imageUrl: sampleOneUrl, imageAlt: 'نمونه عکاسی نوزاد' },
  { id: 'create', label: 'ثبت لحظه‌ها', create: true },
]

export const quickActions: readonly HomeAction[] = [
  { id: 'album', label: 'ساخت آلبوم', subtitle: 'شخصی‌سازی', icon: 'album', to: { name: 'albums' } },
  {
    id: 'photo-selection',
    label: 'انتخاب عکس',
    subtitle: 'برای چاپ',
    icon: 'photo-selection',
    to: { name: 'orders', query: { action: 'photo-selection' } },
  },
  { id: 'orders', label: 'سفارش‌های من', subtitle: 'پیگیری سفارش‌ها', icon: 'orders', to: { name: 'orders' } },
  { id: 'booking', label: 'رزرو نوبت', subtitle: 'عکاسی', icon: 'booking', to: { name: 'booking' } },
]

export const featuredPortfolio: readonly PortfolioItem[] = [
  { id: 'girl', imageUrl: sampleThreeUrl, alt: 'پرتره کودک کنار گل‌های سفید' },
  { id: 'child', imageUrl: sampleTwoUrl, alt: 'عکاسی کودک در فضای باز' },
  { id: 'birthday', imageUrl: sampleThreeUrl, alt: 'عکاسی تولد کودک' },
  { id: 'outdoor', imageUrl: sampleTwoUrl, alt: 'عکاسی کودک همراه خرس عروسکی' },
  { id: 'newborn', imageUrl: sampleOneUrl, alt: 'عکاسی نوزاد در دکور روشن' },
]

export const photographyCategories: readonly PhotographyCategory[] = [
  { id: 'outdoor', label: 'فضای باز', icon: 'outdoor', to: { name: 'portfolio-category', params: { category: 'outdoor' } } },
  { id: 'family', label: 'خانوادگی', icon: 'family', to: { name: 'portfolio-category', params: { category: 'family' } } },
  { id: 'pregnancy', label: 'بارداری', icon: 'pregnancy', to: { name: 'portfolio-category', params: { category: 'pregnancy' } } },
  { id: 'birthday', label: 'تولد', icon: 'birthday', to: { name: 'portfolio-category', params: { category: 'birthday' } } },
  { id: 'child', label: 'کودک', icon: 'child', to: { name: 'portfolio-category', params: { category: 'child' } } },
  { id: 'newborn', label: 'نوزاد', icon: 'newborn', to: { name: 'portfolio-category', params: { category: 'newborn' } } },
]

export const latestOrder: LastOrder = {
  id: 'AFR-1404-0012',
  studioName: 'آتلیه کودک',
  dateLabel: '۱۲ فروردین ۱۴۰۴',
  statusLabel: 'آماده انتخاب',
  newPhotoCount: 235,
  thumbnailUrl: sampleOneUrl,
  to: { name: 'orders', query: { order: 'AFR-1404-0012' } },
}

export const albumPreviews: readonly AlbumPreviewItem[] = [
  { id: 'luxury-album', title: 'آلبوم لوکس', caption: 'مشاهده محصولات', accent: 'cream', to: { name: 'store', query: { category: 'luxury-albums' } } },
  { id: 'child-album', title: 'آلبوم کودک', caption: 'مشاهده محصولات', accent: 'pink', to: { name: 'store', query: { category: 'child-albums' } } },
  { id: 'newborn-album', title: 'آلبوم نوزاد', caption: 'مشاهده محصولات', accent: 'mint', to: { name: 'store', query: { category: 'newborn-albums' } } },
]
