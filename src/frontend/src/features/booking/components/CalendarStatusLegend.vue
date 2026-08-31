<script setup lang="ts">
import type { CalendarDayState } from '../booking.types'

const statuses: ReadonlyArray<{ state: CalendarDayState; label: string }> = [
  { state: 'available', label: 'قابل رزرو' },
  { state: 'selected', label: 'انتخاب شده' },
  { state: 'full', label: 'تکمیل ظرفیت' },
  { state: 'disabled', label: 'غیر فعال' },
  { state: 'holiday', label: 'تعطیل رسمی' },
]
</script>

<template>
  <section class="calendar-legend app-surface-card" aria-labelledby="calendar-legend-title">
    <h2 id="calendar-legend-title" class="calendar-legend__title text-card-title">
      راهنمای وضعیت روزها
    </h2>
    <ul class="calendar-legend__items">
      <li v-for="status in statuses" :key="status.state">
        <span class="calendar-legend__indicator" :class="`calendar-legend__indicator--${status.state}`" aria-hidden="true" />
        <span>{{ status.label }}</span>
      </li>
    </ul>
  </section>
</template>

<style scoped>
.calendar-legend {
  padding: var(--space-4) var(--space-3) var(--space-5);
  border-radius: var(--radius-xl);
}

.calendar-legend__title {
  color: var(--color-text-primary);
  text-align: start;
}

.calendar-legend__items {
  display: grid;
  grid-template-columns: repeat(5, max-content);
  justify-content: space-between;
  gap: 0;
  margin: var(--space-4) 0 0;
  padding: 0;
  color: var(--color-text-primary);
  font-family: var(--font-family-sans);
  font-size: 0.6875rem;
  font-weight: var(--font-weight-medium);
  list-style: none;
}

.calendar-legend__items li {
  display: inline-flex;
  align-items: center;
  gap: 0.1875rem;
  white-space: nowrap;
}

.calendar-legend__indicator {
  inline-size: 0.5rem;
  block-size: 0.5rem;
  flex: none;
  border-radius: var(--radius-full);
}

.calendar-legend__indicator--available { background: var(--color-success); }
.calendar-legend__indicator--selected { background: var(--color-brand-primary); }
.calendar-legend__indicator--full {
  background: color-mix(in srgb, var(--color-accent-yellow) 68%, var(--color-surface));
}
.calendar-legend__indicator--disabled { background: var(--color-disabled); }
.calendar-legend__indicator--holiday { background: var(--color-accent-pink); }

@media (max-width: 25rem) {
  .calendar-legend__items {
    grid-template-columns: repeat(2, minmax(0, 1fr));
    row-gap: var(--space-2);
    column-gap: var(--space-3);
  }
}
</style>
