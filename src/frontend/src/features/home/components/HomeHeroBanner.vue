<script setup lang="ts">
import { useRouter } from 'vue-router'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'

defineProps<{ imageUrl: string }>()
const router = useRouter()
</script>

<template>
  <section class="home-hero" aria-labelledby="home-hero-title">
    <div class="home-hero__photo" aria-hidden="true">
      <img :src="imageUrl" alt="" width="828" height="412" fetchpriority="high" />
    </div>
    <div class="home-hero__content">
      <p class="home-hero__eyebrow">ثبت لحظه‌های</p>
      <h2 id="home-hero-title" class="home-hero__title text-display">شیرین کودکی</h2>
      <p class="home-hero__description">خاطراتی که ماندگار می‌مانند...</p>
      <AppButton class="home-hero__cta" variant="primary" size="lg" @click="router.push({ name: 'booking' })">
        رزرو نوبت
        <template #trailing><AppIcon name="chevron-back" size="sm" /></template>
      </AppButton>
    </div>
  </section>
</template>

<style scoped>
.home-hero {
  position: relative;
  min-block-size: clamp(12rem, 50vw, 14rem);
  overflow: hidden;
  border-radius: var(--radius-xl);
  background: var(--color-warning-soft);
  isolation: isolate;
}

.home-hero__photo {
  position: absolute;
  inset-block: 0;
  inset-inline-start: 0;
  inline-size: 58%;
  overflow: hidden;
}

.home-hero__photo::after {
  position: absolute;
  inset: 0;
  background: linear-gradient(to left, transparent 68%, var(--color-warning-soft));
  content: '';
}

.home-hero__photo img {
  position: absolute;
  inset-block: 0;
  inset-inline-start: 0;
  inline-size: 172%;
  block-size: 100%;
  max-inline-size: none;
  object-fit: cover;
  object-position: right center;
}

.home-hero__content {
  position: relative;
  z-index: var(--z-content);
  display: flex;
  inline-size: 52%;
  min-block-size: inherit;
  margin-inline-start: auto;
  align-items: flex-start;
  justify-content: center;
  flex-direction: column;
  padding: clamp(var(--space-4), 5vw, var(--space-6));
}

.home-hero__eyebrow,
.home-hero__description { margin: 0; }

.home-hero__eyebrow {
  color: var(--color-text-strong);
  font-size: var(--font-size-lg);
  font-weight: var(--font-weight-semibold);
}

.home-hero__title {
  margin-block: var(--space-1) var(--space-2);
  color: var(--color-text-strong);
  white-space: nowrap;
}

.home-hero__description {
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
  white-space: nowrap;
}

.home-hero__cta {
  min-inline-size: 0;
  margin-block-start: var(--space-4);
  padding-inline: var(--space-5);
  border-radius: var(--radius-full);
  white-space: nowrap;
}

@media (max-width: 22.5rem) {
  .home-hero__eyebrow { font-size: var(--font-size-sm); }
  .home-hero__title { font-size: var(--font-size-2xl); }
  .home-hero__description { font-size: var(--font-size-xs); }
  .home-hero__cta { min-block-size: var(--control-height-md); padding-inline: var(--space-3); font-size: var(--font-size-sm); }
}
</style>
