<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { LastOrder } from '../home.types'

defineProps<{ order: LastOrder }>()
</script>

<template>
  <section class="last-order app-surface-card" aria-labelledby="last-order-title">
    <h2 id="last-order-title" class="last-order__title text-section-title">آخرین سفارش شما</h2>
    <div class="last-order__content">
      <img
        class="last-order__thumbnail"
        :src="order.thumbnailUrl"
        alt="تصویر سفارش عکاسی نوزاد"
        width="258"
        height="278"
        loading="lazy"
      />
      <div class="last-order__identity">
        <h3 class="text-card-title">{{ order.studioName }}</h3>
        <p>{{ order.dateLabel }}</p>
        <span class="last-order__status">{{ order.statusLabel }}</span>
      </div>
      <RouterLink class="last-order__summary" :to="order.to">
        <span class="last-order__photos" aria-hidden="true">
          <AppIcon name="photo-stack" size="lg" tone="brand" />
        </span>
        <span class="last-order__count">{{ order.newPhotoCount.toLocaleString('fa-IR') }}</span>
        <span class="last-order__caption">عکس جدید</span>
        <span class="last-order__action">
          مشاهده و انتخاب
          <AppIcon name="chevron-back" size="xs" />
        </span>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.last-order {
  padding: var(--space-4);
  border-radius: var(--radius-xl);
}

.last-order__title {
  margin-block-end: var(--space-3);
  color: var(--color-text-strong);
}

.last-order__content {
  display: grid;
  grid-template-columns: 4.75rem minmax(0, 1fr) auto;
  align-items: center;
  gap: var(--space-4);
}

.last-order__thumbnail {
  inline-size: 4.75rem;
  block-size: 4.75rem;
  border-radius: var(--radius-sm);
  object-fit: cover;
}

.last-order__identity { min-inline-size: 0; }
.last-order__identity h3,
.last-order__identity p { margin: 0; }
.last-order__identity p { margin-block: var(--space-1) var(--space-2); color: var(--color-text-secondary); font-size: var(--font-size-xs); }

.last-order__status {
  display: inline-flex;
  padding: var(--space-1) var(--space-2);
  border-radius: var(--radius-full);
  color: var(--color-warning);
  background: var(--color-warning-soft);
  font-size: var(--font-size-xs);
  white-space: nowrap;
}

.last-order__summary {
  display: grid;
  grid-template-columns: auto auto;
  align-items: center;
  gap: 0 var(--space-2);
  color: var(--color-text-primary);
  text-decoration: none;
}

.last-order__photos {
  grid-row: span 2;
  display: grid;
  inline-size: 3.5rem;
  block-size: 3.5rem;
  place-items: center;
  border-radius: var(--radius-md);
  background: var(--color-brand-soft);
}

.last-order__count { font-size: var(--font-size-xl); font-weight: var(--font-weight-bold); }
.last-order__caption { color: var(--color-text-secondary); font-size: var(--font-size-xs); }
.last-order__action {
  grid-column: 1 / -1;
  display: inline-flex;
  align-items: center;
  gap: var(--space-1);
  margin-block-start: var(--space-2);
  color: var(--color-brand-primary);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-semibold);
  white-space: nowrap;
}

@media (max-width: 22.5rem) {
  .last-order__content { grid-template-columns: 4rem minmax(0, 1fr); gap: var(--space-3); }
  .last-order__thumbnail { inline-size: 4rem; block-size: 4rem; }
  .last-order__summary { grid-column: 1 / -1; grid-template-columns: auto 1fr; padding-block-start: var(--space-3); border-block-start: 1px solid var(--color-border-subtle); }
  .last-order__action { justify-content: flex-end; }
}
</style>
