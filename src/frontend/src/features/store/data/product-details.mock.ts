import accordionAlbumUrl from '../../../assets/images/store/accordion-album.webp'
import classicAlbumUrl from '../../../assets/images/store/classic-album.webp'
import giftSetUrl from '../../../assets/images/store/gift-set.webp'
import keepsakeBoxUrl from '../../../assets/images/store/keepsake-box.webp'
import woodenFrameUrl from '../../../assets/images/store/wooden-frame.webp'
import { storeProducts } from './products.mock'
import type { StoreProductDetails } from '../store.types'

const classicAlbum = storeProducts.find((product) => product.id === 'classic-newborn-album')!

export const mockProductDetails: StoreProductDetails = {
  product: classicAlbum,
  categoryLabel: 'آلبوم عکس',
  tagline: 'ثبت خاطرات ماندگار با طراحی ساده و شیک',
  price: 890_000,
  introduction: 'آلبوم پارچه‌ای کلاسیک با طراحی ساده و شیک، انتخابی ماندگار برای نگهداری خاطرات شیرین شماست. این آلبوم با ظرفیت ۴۰ عکس و کاغذ عکس باکیفیت، مناسب ثبت لحظات خاص کودکان، نوزادان و خانواده‌ها طراحی شده است.',
  overview: 'جلد پارچه‌ای لطیف، صحافی محکم و چاپ حرفه‌ای باعث می‌شود عکس‌ها مرتب، درخشان و برای سال‌ها سالم باقی بمانند. فرم مینیمال این آلبوم با دکور خانه و اتاق کودک هماهنگ است.',
  gallery: [
    { src: classicAlbumUrl, alt: 'نمای روبه‌روی آلبوم پارچه‌ای کلاسیک' },
    { src: accordionAlbumUrl, alt: 'نمای صفحات داخلی آلبوم' },
    { src: keepsakeBoxUrl, alt: 'بسته‌بندی آلبوم پارچه‌ای' },
    { src: woodenFrameUrl, alt: 'جزئیات چاپ عکس آلبوم' },
    { src: giftSetUrl, alt: 'آلبوم در چیدمان هدیه کودک' },
  ],
  specifications: [
    { label: 'ابعاد', value: '۲۸×۲۳×۴ سانتی‌متر' },
    { label: 'سایز عکس قابل استفاده', value: '۲۱×۲۱ سانتی‌متر' },
    { label: 'تعداد عکس قابل استفاده', value: '۴۰ عدد' },
    { label: 'تعداد عکس در هر برگ', value: '۲ عدد' },
    { label: 'جنس جلد', value: 'پارچه‌ای' },
    { label: 'فرم صحافی', value: 'عمودی' },
    { label: 'مناسب', value: 'کودک، نوزاد، خانوادگی' },
    { label: 'نوع چاپ', value: 'مات / براق' },
    { label: 'رنگ‌های موجود', value: 'کرم، خاکستری، سرمه‌ای، زرشکی' },
    { label: 'زمان آماده‌سازی', value: '۳ تا ۵ روز' },
  ],
  benefits: ['جلد پارچه‌ای باکیفیت', 'صحافی محکم و ماندگار', 'چاپ مات یا براق', 'بسته‌بندی ایمن', 'آماده‌سازی سریع'],
  rating: 4.8,
  reviewCount: 1166,
  ratingDistribution: [
    { stars: 5, percent: 87 },
    { stars: 4, percent: 10 },
    { stars: 3, percent: 2 },
    { stars: 2, percent: 0.7 },
    { stars: 1, percent: 0.3 },
  ],
  reviews: [
    { id: 'r1', author: 'کاربر دیجی‌کالا', date: '۳ شهریور ۱۴۰۵', rating: 5, body: 'کیفیتش به اندازه قیمتش نبود؛ حتی از انتظارم بهتر بود. عکس‌ها روی کاغذ خیلی شفاف و مرتب چاپ شده‌اند.', likes: 0, dislikes: 0, variant: 'رنگ جلد: کرم  |  سایز: ۲۰×۲۵' },
    { id: 'r2', author: 'سارا محمدی', date: '۱۲ مرداد ۱۴۰۵', rating: 5, title: 'آلبوم زرشکی', body: 'آلبوم از لحاظ زیبایی و سادگی خیلی خوب است و صحافی محکمی دارد. بسته‌بندی هم عالی بود.', likes: 16, dislikes: 3, variant: 'رنگ جلد: کرم  |  سایز: ۲۰×۲۵' },
  ],
}
