<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { LastOrder } from '../home.types'

defineProps<{ order: LastOrder }>()
</script>

<template>
  <section class="photo-ready" aria-labelledby="photo-ready-title">
    <div class="photo-ready__thumb">
      <img :src="order.thumbnailUrl" alt="تصویر مجموعه عکس آماده انتخاب" width="90" height="90" loading="lazy" />
      <span>{{ order.newPhotoCount.toLocaleString('fa-IR') }}</span>
    </div>
    <div class="photo-ready__copy">
      <h2 id="photo-ready-title">عکس‌های شما آماده انتخاب است</h2>
      <p>{{ order.dateLabel }} - {{ order.studioName }}</p>
      <RouterLink :to="order.to">مشاهده و انتخاب عکس‌ها <AppIcon name="chevron-back" size="xs" /></RouterLink>
    </div>
    <div class="photo-ready__art" aria-hidden="true">
      <AppIcon name="photo-stack" size="xl" />
      <span>♡</span>
    </div>
  </section>
</template>

<style scoped>
.photo-ready {
  display: grid;
  grid-template-columns: 82px minmax(0, 1fr) 77px;
  min-block-size: 81px;
  align-items: center;
  overflow: hidden;
  border: 1px solid #f0f1f2;
  border-radius: 14px;
  background: #fafcfc;
}

.photo-ready__thumb { position: relative; align-self: stretch; }
.photo-ready__thumb img { display: block; inline-size: 100%; block-size: 100%; object-fit: cover; }
.photo-ready__thumb span { position: absolute; inset-block-start: 6px; inset-inline-start: 5px; padding: 2px 6px; border-radius: 6px; color: #fff; background: #2d3033; font-size: 11px; font-weight: 600; }
.photo-ready__copy { min-inline-size: 0; padding: 7px 10px; }
.photo-ready__copy h2,
.photo-ready__copy p { margin: 0; }
.photo-ready__copy h2 { overflow: hidden; color: #111820; font-size: 13px; font-weight: 600; text-overflow: ellipsis; white-space: nowrap; }
.photo-ready__copy p { margin-block: 2px 4px; color: #8a8e92; font-size: 10px; }
.photo-ready__copy a { display: inline-flex; align-items: center; gap: 3px; color: #111820; font-size: 11px; font-weight: 500; text-decoration: none; white-space: nowrap; }
.photo-ready__art { position: relative; display: grid; block-size: 100%; place-items: center; color: #92dce5; opacity: .75; }
.photo-ready__art :deep(svg) { inline-size: 43px; block-size: 43px; stroke-width: 1.2; }
.photo-ready__art span { position: absolute; inset-block-start: 8px; inset-inline-start: 11px; font-size: 18px; }

@media (max-width: 375px) {
  .photo-ready { grid-template-columns: 72px minmax(0, 1fr) 54px; }
  .photo-ready__copy { padding-inline: 7px; }
  .photo-ready__copy h2 { font-size: 12px; }
}
</style>
