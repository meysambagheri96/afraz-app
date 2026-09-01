import { createRouter, createWebHashHistory, type RouteRecordRaw } from 'vue-router'
import AppShell from '../app/AppShell.vue'

const FoundationView = () => import('./views/FoundationView.vue')
const HomeView = () => import('../features/home/pages/HomeView.vue')
const BookingDatePage = () => import('../features/booking/pages/BookingDatePage.vue')
const BookingCustomerInfoPage = () => import('../features/booking/pages/BookingCustomerInfoPage.vue')
const BookingSuccessPage = () => import('../features/booking/pages/BookingSuccessPage.vue')
const AuthFallbackPage = () => import('../features/auth/pages/AuthFallbackPage.vue')

const shellRoutes: RouteRecordRaw[] = [
  {
    path: '',
    name: 'home',
    component: HomeView,
    meta: { navigation: 'home' },
  },
  {
    path: 'search',
    name: 'search',
    component: FoundationView,
    props: {
      title: 'جستجو',
      description: 'جستجو میان نمونه‌کارها، آلبوم‌ها و محصولات افراز.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'notifications',
    name: 'notifications',
    component: FoundationView,
    props: {
      title: 'اعلان‌ها',
      description: 'خبرهای رزرو، سفارش و آماده‌شدن عکس‌ها در این بخش نمایش داده می‌شود.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'portfolio',
    name: 'portfolio',
    component: FoundationView,
    props: {
      title: 'نمونه‌کارها',
      description: 'گالری عمومی عکس‌های منتخب آتلیه افراز.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'portfolio/:category',
    name: 'portfolio-category',
    component: FoundationView,
    props: {
      title: 'دسته‌بندی نمونه‌کارها',
      description: 'نمونه‌کارهای منتخب این دسته در این بخش نمایش داده می‌شوند.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'albums/create',
    name: 'albums',
    component: FoundationView,
    props: {
      title: 'ساخت آلبوم',
      description: 'آلبوم اختصاصی خود را با عکس‌های دلخواه بسازید.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'studio',
    name: 'studio',
    component: FoundationView,
    props: {
      title: 'درباره آتلیه',
      description: 'اطلاعات تماس، نشانی، ساعات کاری و مجوزهای آتلیه افراز.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'articles',
    name: 'articles',
    component: FoundationView,
    props: {
      title: 'مقالات',
      description: 'راهنماها و نوشته‌های آتلیه افراز درباره عکاسی کودک و ثبت خاطره‌ها.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'contact',
    name: 'contact',
    component: FoundationView,
    props: {
      title: 'تماس با ما',
      description: 'راه‌های ارتباط با آتلیه افراز، نشانی و ساعات پاسخ‌گویی.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'privacy',
    name: 'privacy',
    component: FoundationView,
    props: {
      title: 'حریم خصوصی',
      description: 'شیوه نگهداری و حفاظت از اطلاعات و تصاویر مشتریان آتلیه افراز.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'terms',
    name: 'terms',
    component: FoundationView,
    props: {
      title: 'قوانین و مقررات',
      description: 'قوانین استفاده از خدمات، رزرو، سفارش و پرداخت در آتلیه افراز.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'faq',
    name: 'faq',
    component: FoundationView,
    props: {
      title: 'سوالات متداول',
      description: 'پاسخ پرسش‌های رایج درباره رزرو، عکاسی، انتخاب عکس و سفارش‌ها.',
    },
    meta: { navigation: 'home' },
  },
  {
    path: 'booking',
    name: 'booking',
    component: BookingDatePage,
    meta: { navigation: 'booking', focusedFlow: true },
  },
  {
    path: 'booking/customer-info',
    name: 'booking-create',
    component: BookingCustomerInfoPage,
    meta: { navigation: 'booking', focusedFlow: true },
  },
  {
    path: 'booking/success',
    name: 'booking-success',
    component: BookingSuccessPage,
    meta: { navigation: 'booking', focusedFlow: true },
  },
  {
    path: 'bookings',
    name: 'bookings',
    component: FoundationView,
    props: {
      title: 'نوبت‌های من',
      description: 'نوبت‌های ثبت‌شده شما در این بخش نمایش داده می‌شوند.',
    },
    meta: { navigation: 'booking' },
  },
  {
    path: 'orders',
    name: 'orders',
    component: FoundationView,
    props: {
      title: 'سفارش‌ها',
      description: 'وضعیت سفارش‌ها، گالری‌ها و مراحل آماده‌سازی عکس‌ها در این بخش قرار می‌گیرد.',
    },
    meta: { navigation: 'orders' },
  },
  {
    path: 'store',
    name: 'store',
    component: FoundationView,
    props: {
      title: 'فروشگاه',
      description: 'محصولات چاپی، قاب‌ها و آلبوم‌های افراز از این بخش در دسترس خواهند بود.',
    },
    meta: { navigation: 'store' },
  },
  {
    path: 'profile',
    name: 'profile',
    component: FoundationView,
    props: {
      title: 'پروفایل',
      description: 'اطلاعات حساب، آدرس‌ها و فعالیت‌های شما در این بخش مدیریت می‌شود.',
    },
    meta: { navigation: 'profile' },
  },
]

export const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: '/auth',
      name: 'auth',
      component: AuthFallbackPage,
    },
    { path: '/', component: AppShell, children: shellRoutes },
    { path: '/:pathMatch(.*)*', redirect: { name: 'home' } },
  ],
  scrollBehavior: () => ({ top: 0 }),
})
