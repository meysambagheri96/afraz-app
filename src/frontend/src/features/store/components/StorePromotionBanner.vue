<script setup lang="ts">
import { ChevronLeft } from '@lucide/vue'
import keepsakeBoxUrl from '../../../assets/images/store/keepsake-box.webp'
</script>

<template>
  <section class="store-promotion" aria-label="پیشنهاد ویژه فروشگاه">
    <img
      class="store-promotion__image"
      :src="keepsakeBoxUrl"
      alt="باکس هدیه ویژه آتلیه افراز"
      width="900"
      height="675"
      loading="eager"
      decoding="async"
    />
    <div class="store-promotion__veil" aria-hidden="true" />
    <div class="store-promotion__content">
      <span class="store-promotion__discount">۲۰٪ تخفیف</span>
      <h2 class="store-promotion__title text-section-title">هدیه‌ای ماندگار برای عزیزان</h2>
      <p class="store-promotion__subtitle text-caption">روی آلبوم‌ها و باکس‌های هدیه</p>
      <button class="store-promotion__action" type="button">
        <span>مشاهده محصولات</span>
        <ChevronLeft :size="17" :stroke-width="2" aria-hidden="true" />
      </button>
    </div>
  </section>
</template>

<style scoped>
.store-promotion {
  position: relative;
  min-block-size: 132px;
  margin-block-start: var(--space-4);
  overflow: hidden;
  border: 1px solid rgb(201 140 83 / 18%);
  border-radius: var(--radius-lg);
  background: #faeee2;
}

.store-promotion__image {
  position: absolute;
  inset-block: 0;
  inset-inline-start: 0;
  inline-size: 58%;
  block-size: 100%;
  object-fit: cover;
  object-position: center;
}

.store-promotion__veil {
  position: absolute;
  inset: 0;
  background: linear-gradient(90deg, #faeee2 18%, rgb(250 238 226 / 82%) 51%, transparent 100%);
}

.store-promotion__content {
  position: relative;
  z-index: var(--z-content);
  display: grid;
  inline-size: 66%;
  min-block-size: 132px;
  margin-inline-start: auto;
  align-content: center;
  justify-items: start;
  padding: var(--space-3) var(--space-4);
}

.store-promotion__discount {
  position: absolute;
  inset-block-start: var(--space-3);
  inset-inline-end: var(--space-3);
  padding: 5px 10px;
  border-radius: var(--radius-full);
  color: #8d3146;
  background: #ffd8df;
  font-size: 10px;
  font-weight: var(--font-weight-bold);
}

.store-promotion__title {
  max-inline-size: 100%;
  margin-block-start: var(--space-2);
  color: var(--color-text-primary);
  font-size: 14px;
}

.store-promotion__subtitle {
  margin-block-start: 1px;
  color: var(--color-text-secondary);
  font-size: 10px;
}

.store-promotion__action {
  display: inline-flex;
  min-block-size: 34px;
  align-items: center;
  gap: var(--space-1);
  margin-block-start: 7px;
  padding-inline: 12px;
  border: 0;
  border-radius: var(--radius-full);
  color: var(--color-surface);
  background: #102f3c;
  font: inherit;
  font-size: 10px;
  font-weight: var(--font-weight-bold);
  cursor: pointer;
}

@media (max-width: 22.5rem) {
  .store-promotion__content { padding-inline: var(--space-3); }
  .store-promotion__title { font-size: 13px; }
}
</style>
