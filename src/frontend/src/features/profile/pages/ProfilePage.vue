<script setup lang="ts">
import { Bell, Settings } from '@lucide/vue'
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import AppIconButton from '../../../components/ui/AppIconButton.vue'
import ProfileMenu from '../components/ProfileMenu.vue'
import ProfileSummaryCard from '../components/ProfileSummaryCard.vue'
import { adminMenuItems, mockCustomerProfile, profileMenuItems } from '../data/profile.mock'
import type { ProfileMenuItem } from '../profile.types'

const router = useRouter()
const announcement = ref('')
const visibleAdminItems = computed(() => mockCustomerProfile.isAdmin ? adminMenuItems : [])

function announce(message: string) {
  announcement.value = message
}

function handleMenuSelect(item: ProfileMenuItem) {
  if (item.id === 'bookings') void router.push({ name: 'bookings' })
  else if (item.id === 'orders') void router.push({ name: 'orders' })
  else if (item.id === 'about') void router.push({ name: 'studio' })
  else if (item.id === 'support') void router.push({ name: 'contact' })
  else announce(`${item.title} در نسخه نمایشی انتخاب شد.`)
}
</script>

<template>
  <div class="profile-page">
    <header class="profile-header">
      <AppIconButton label="تنظیمات" size="lg" @click="announce('تنظیمات در نسخه نمایشی انتخاب شد.')">
        <Settings :size="26" :stroke-width="1.8" />
      </AppIconButton>
      <h1 class="profile-header__title text-page-title">پروفایل</h1>
      <AppIconButton class="profile-header__notification" label="اعلان‌ها" size="lg" @click="router.push({ name: 'notifications' })">
        <Bell :size="26" :stroke-width="1.8" />
        <span class="profile-header__notification-dot" aria-hidden="true" />
      </AppIconButton>
    </header>

    <ProfileSummaryCard
      :profile="mockCustomerProfile"
      @edit="announce('ویرایش اطلاعات در نسخه نمایشی انتخاب شد.')"
      @avatar="announce('تغییر تصویر پروفایل در نسخه نمایشی انتخاب شد.')"
    />

    <ProfileMenu
      class="profile-page__menu"
      :items="profileMenuItems"
      label="امکانات پروفایل"
      @select="handleMenuSelect"
    />

    <section v-if="visibleAdminItems.length" class="profile-page__admin" aria-labelledby="admin-menu-title">
      <h2 id="admin-menu-title" class="text-section-title">مدیریت آتلیه</h2>
      <ProfileMenu :items="visibleAdminItems" label="امکانات مدیریت آتلیه" @select="handleMenuSelect" />
    </section>

    <p class="sr-only" role="status" aria-live="polite">{{ announcement }}</p>
  </div>
</template>

<style scoped>
.profile-page {
  display: grid;
  gap: var(--space-5);
  padding-block-end: var(--space-6);
}

.profile-header {
  display: grid;
  min-block-size: 74px;
  grid-template-columns: var(--control-height-lg) minmax(0, 1fr) var(--control-height-lg);
  align-items: center;
  gap: var(--space-2);
}

.profile-header__title { color: var(--color-text-primary); text-align: center; }
.profile-header :deep(.app-icon-button) { border-radius: 50%; }
.profile-header__notification { position: relative; }

.profile-header__notification-dot {
  position: absolute;
  inset-block-start: 10px;
  inset-inline-start: 9px;
  inline-size: 7px;
  block-size: 7px;
  border: 2px solid var(--color-background);
  border-radius: 50%;
  background: var(--color-danger);
}

.profile-page__admin { display: grid; gap: var(--space-3); }

@media (max-width: 23rem) {
  .profile-page { gap: var(--space-4); }
}
</style>
