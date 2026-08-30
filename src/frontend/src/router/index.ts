import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import AppShell from '../app/AppShell.vue'

const FoundationView = () => import('./views/FoundationView.vue')

const shellRoutes: RouteRecordRaw[] = [
  {
    path: '',
    name: 'home',
    component: FoundationView,
    props: {
      title: 'خانه',
      description: 'خانه افراز برای نمایش خدمات و تازه‌ترین لحظه‌های ثبت‌شده آماده می‌شود.',
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
