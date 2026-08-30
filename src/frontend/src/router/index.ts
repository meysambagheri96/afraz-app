import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import AppShell from '../app/AppShell.vue'

const FoundationView = () => import('./views/FoundationView.vue')
const HomeView = () => import('../features/home/pages/HomeView.vue')

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
    path: 'booking',
    name: 'booking',
    component: FoundationView,
    props: {
      title: 'رزرو نوبت',
      description: 'از این بخش می‌توانید زمان مناسب عکاسی را انتخاب و رزرو کنید.',
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
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', component: AppShell, children: shellRoutes },
    { path: '/:pathMatch(.*)*', redirect: { name: 'home' } },
  ],
  scrollBehavior: () => ({ top: 0 }),
})
