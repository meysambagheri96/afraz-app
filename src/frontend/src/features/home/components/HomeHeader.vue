<script setup lang="ts">
import { useRouter } from 'vue-router'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'

const router = useRouter()
</script>

<template>
  <header class="home-header">
    <div class="home-header__brand">
      <div class="home-header__logo" aria-label="نشان آتلیه افراز">
        <span class="home-header__logo-text">افراز</span>
        <span class="home-header__logo-sun" aria-hidden="true" />
      </div>
      <div class="home-header__copy">
        <h1 class="home-header__title text-page-title">آتلیه افراز قم</h1>
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
        variant="glass"
        @click="router.push({ name: 'notifications' })"
      >
        <AppIcon name="notification" size="lg" />
        <span class="home-header__notification-dot" aria-hidden="true" />
      </AppIconButton>
      <AppIconButton
        class="home-header__action"
        label="جستجو"
        variant="glass"
        @click="router.push({ name: 'search' })"
      >
        <AppIcon name="search" size="lg" />
      </AppIconButton>
    </div>
  </header>
</template>

<style scoped>
.home-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-3);
}

.home-header__brand {
  display: flex;
  min-inline-size: 0;
  align-items: center;
  gap: var(--space-3);
}

.home-header__logo {
  position: relative;
  display: grid;
  inline-size: clamp(3rem, 14vw, 4rem);
  block-size: clamp(3rem, 14vw, 4rem);
  flex: none;
  place-items: center;
  border: 3px solid var(--color-surface);
  border-radius: 50%;
  color: var(--color-accent-yellow);
  background: var(--color-brand-primary);
  box-shadow: var(--shadow-card);
}

.home-header__logo-text {
  font-size: clamp(0.9rem, 4vw, 1.2rem);
  font-weight: var(--font-weight-bold);
  line-height: 1;
}

.home-header__logo-sun {
  position: absolute;
  inset-block-start: 0.35rem;
  inset-inline-start: 50%;
  inline-size: var(--space-2);
  block-size: var(--space-2);
  border-radius: 50%;
  background: var(--color-accent-yellow);
  transform: translateX(50%);
}

.home-header__copy { min-inline-size: 0; }

.home-header__title {
  overflow: hidden;
  color: var(--color-text-strong);
  font-size: clamp(var(--font-size-lg), 5vw, var(--font-size-2xl));
  text-overflow: ellipsis;
  white-space: nowrap;
}

.home-header__subtitle {
  display: flex;
  align-items: center;
  gap: var(--space-1);
  margin: var(--space-1) 0 0;
  color: var(--color-text-secondary);
  font-size: clamp(var(--font-size-xs), 2.8vw, var(--font-size-sm));
  white-space: nowrap;
}

.home-header__subtitle span { color: var(--color-accent-yellow); }

.home-header__actions {
  display: flex;
  flex: none;
  gap: var(--space-2);
}

.home-header__action {
  position: relative;
  inline-size: clamp(var(--touch-target), 12vw, 3.5rem);
  block-size: clamp(var(--touch-target), 12vw, 3.5rem);
  border-radius: var(--radius-lg);
  color: var(--color-icon);
  background: color-mix(in srgb, var(--color-surface) 90%, transparent);
}

.home-header__notification-dot {
  position: absolute;
  inset-block-start: var(--space-2);
  inset-inline-start: var(--space-2);
  inline-size: var(--space-2);
  block-size: var(--space-2);
  border: 2px solid var(--color-surface);
  border-radius: 50%;
  background: var(--color-accent-yellow);
}

@media (max-width: 22.5rem) {
  .home-header__brand { gap: var(--space-2); }
  .home-header__actions { gap: var(--space-1); }
  .home-header__subtitle { display: none; }
}
</style>
