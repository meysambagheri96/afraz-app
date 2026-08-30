<script setup lang="ts">
import { useRouter } from 'vue-router'
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
      <div class="home-hero__title-row">
        <h2 id="home-hero-title" class="home-hero__title">شیرین کودکی</h2>
        <svg class="home-hero__heart" viewBox="0 0 32 32" aria-hidden="true"><path d="M5 17C1 9 9 5 14 12c4-8 14-4 12 4-1 5-7 9-13 12" /></svg>
      </div>
      <p class="home-hero__description">خاطراتی که ماندگار می‌مانند...</p>
      <button class="home-hero__cta" type="button" @click="router.push({ name: 'booking' })">
        رزرو نوبت عکاسی
        <AppIcon name="booking" size="sm" />
      </button>
    </div>
    <div class="home-hero__pagination" aria-hidden="true"><span class="is-active" /><span /><span /><span /></div>
  </section>
</template>

<style scoped>
.home-hero {
  position: relative;
  inline-size: 100vw;
  block-size: clamp(202px, 50vw, 218px);
  margin-inline: calc(50% - 50vw);
  overflow: hidden;
  border-radius: 0;
  background: #f5eadf;
  isolation: isolate;
}

.home-hero__photo {
  position: absolute;
  inset-block: 0;
  inset-inline-start: 0;
  inline-size: 61%;
  overflow: hidden;
  border-radius: 0;
}

.home-hero__photo::after {
  position: absolute;
  inset: 0;
  background: linear-gradient(to left, transparent 70%, #f7ede4 100%);
  content: '';
}

.home-hero__photo img {
  position: absolute;
  inset-block: 0;
  inset-inline-start: 0;
  inline-size: 184%;
  block-size: 100%;
  max-inline-size: none;
  object-fit: cover;
  object-position: right center;
  border-radius: 0;
}

.home-hero__content {
  position: relative;
  z-index: var(--z-content);
  display: flex;
  inline-size: 53%;
  min-block-size: inherit;
  margin-inline-start: auto;
  align-items: flex-start;
  justify-content: center;
  flex-direction: column;
  padding: 18px 18px 24px 8px;
}

.home-hero__eyebrow,
.home-hero__description { margin: 0; }

.home-hero__eyebrow {
  color: #121b24;
  font-size: 22px;
  font-weight: 500;
  line-height: 1.3;
}

.home-hero__title-row { display: flex; align-items: center; gap: 5px; }
.home-hero__title {
  margin: 0;
  color: #121b24;
  font-size: 28px;
  font-weight: 800;
  line-height: 1.3;
  white-space: nowrap;
}

.home-hero__heart { inline-size: 25px; block-size: 25px; fill: none; stroke: #ff91a5; stroke-linecap: round; stroke-linejoin: round; stroke-width: 2; transform: rotate(-8deg); }

.home-hero__description {
  margin-block-start: 7px;
  color: #5c6064;
  font-size: 14px;
  white-space: nowrap;
}

.home-hero__cta {
  display: inline-flex;
  min-block-size: 43px;
  margin-block-start: 15px;
  align-items: center;
  justify-content: center;
  gap: 10px;
  padding-inline: 17px;
  border: 0;
  border-radius: 999px;
  color: #fff;
  background: #152b39;
  font-size: 14px;
  font-weight: 700;
  white-space: nowrap;
  cursor: pointer;
}

.home-hero__pagination { position: absolute; inset-block-end: 7px; inset-inline: 0; z-index: 2; display: flex; justify-content: center; gap: 7px; direction: ltr; }
.home-hero__pagination span { inline-size: 8px; block-size: 6px; border-radius: 999px; background: #d8d8d7; }
.home-hero__pagination .is-active { inline-size: 18px; background: #152b39; }

@media (max-width: 22.5rem) {
  .home-hero__content { padding-inline-start: 12px; }
  .home-hero__eyebrow { font-size: 19px; }
  .home-hero__title { font-size: 25px; }
  .home-hero__description { font-size: 12px; }
  .home-hero__cta { min-block-size: 40px; padding-inline: 12px; font-size: 12px; }
}
</style>
