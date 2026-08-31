<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
</script>

<template>
  <aside id="booking-calendar-help" class="booking-info-card" aria-labelledby="booking-info-title">
    <span class="booking-info-card__icon" aria-hidden="true">
      <AppIcon name="calendar-info" size="xl" />
    </span>
    <div>
      <h2 id="booking-info-title" class="text-card-title">توجه</h2>
      <p class="text-label">
        پس از انتخاب روز، در مرحله بعد ساعت مورد نظر خود را انتخاب خواهید کرد.
      </p>
    </div>
  </aside>
</template>

<style scoped>
.booking-info-card {
  display: grid;
  grid-template-columns: 4.25rem minmax(0, 1fr);
  align-items: center;
  gap: var(--space-4);
  padding: var(--space-5);
  border: 1px solid color-mix(in srgb, var(--color-accent-blue) 16%, transparent);
  border-radius: var(--radius-xl);
  color: var(--color-text-primary);
  background: linear-gradient(110deg, color-mix(in srgb, var(--color-brand-soft) 72%, white), color-mix(in srgb, var(--color-accent-blue) 14%, white));
}

.booking-info-card__icon {
  display: grid;
  inline-size: 4.25rem;
  block-size: 4.25rem;
  place-items: center;
  border: 1px solid color-mix(in srgb, var(--color-brand-primary) 12%, transparent);
  border-radius: var(--radius-full);
  color: var(--color-brand-primary);
  background: color-mix(in srgb, var(--color-surface) 58%, transparent);
}

.booking-info-card p {
  margin: var(--space-2) 0 0;
  color: var(--color-text-primary);
  line-height: var(--line-height-body);
}

@media (max-width: 22.5rem) {
  .booking-info-card {
    grid-template-columns: 3.5rem minmax(0, 1fr);
    padding: var(--space-4);
  }

  .booking-info-card__icon {
    inline-size: 3.5rem;
    block-size: 3.5rem;
  }
}
</style>
