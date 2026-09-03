<script setup lang="ts">
import { useRouter } from 'vue-router'
import AppBrandLogo from '../../../components/shared/AppBrandLogo.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'

const router = useRouter()
</script>

<template>
  <header class="home-header">
    <div class="home-header__brand">
      <AppBrandLogo />
      <div class="home-header__copy">
        <h1 class="home-header__title text-page-title">آتلیه افراز قم</h1>
        <AppIcon class="home-header__chevron" name="chevron-down" size="xs" />
        <p class="home-header__subtitle">
          <span aria-hidden="true">✦</span>
          ثبت خاطره‌های شیرین کودک شما
        </p>
      </div>
    </div>

    <div class="home-header__actions">
      <AppIconButton
        class="home-header__action"
        label="اعلان‌ها"
        variant="ghost"
        @click="router.push({ name: 'notifications' })"
      >
        <AppIcon name="notification" size="lg" />
        <span class="home-header__notification-dot" aria-hidden="true" />
      </AppIconButton>
      <AppIconButton
        class="home-header__action"
        label="جستجو"
        variant="ghost"
        @click="router.push({ name: 'search' })"
      >
        <AppIcon name="send" size="lg" />
      </AppIconButton>
    </div>
  </header>
</template>

<style scoped>
.home-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  min-block-size: 59px;
  gap: 10px;
}

.home-header__brand {
  display: flex;
  min-inline-size: 0;
  align-items: center;
  gap: 10px;
}

.home-header__copy { position: relative; min-inline-size: 0; }

.home-header__title {
  overflow: hidden;
  color: #101820;
  font-size: 19px;
  font-weight: 700;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.home-header__chevron {
  position: absolute;
  inset-block-start: 7px;
  inset-inline-end: -20px;
  color: #17202a;
}

.home-header__subtitle {
  display: flex;
  align-items: center;
  gap: var(--space-1);
  margin: 1px 0 0;
  color: #777b80;
  font-size: 11px;
  white-space: nowrap;
}

.home-header__subtitle span { color: var(--color-accent-yellow); }

.home-header__actions {
  display: flex;
  flex: none;
  gap: 4px;
}

.home-header__action {
  position: relative;
  inline-size: 42px;
  block-size: 44px;
  border-radius: 50%;
  color: var(--color-icon);
  background: transparent;
}

.home-header__action :deep(svg) { inline-size: 27px; block-size: 27px; }

.home-header__notification-dot {
  position: absolute;
  inset-block-start: 7px;
  inset-inline-start: 7px;
  inline-size: 7px;
  block-size: 7px;
  border: 2px solid var(--color-surface);
  border-radius: 50%;
  background: #ff3347;
}

@media (max-width: 22.5rem) {
  .home-header__brand { gap: var(--space-2); }
  .home-header__actions { gap: var(--space-1); }
  .home-header__subtitle { display: none; }
}
</style>
