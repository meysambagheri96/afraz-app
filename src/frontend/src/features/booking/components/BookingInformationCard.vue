<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { AppIconName } from '../../../components/ui/icon.types'

defineProps<{
  rows: readonly {
    label: string
    value: string
    icon: AppIconName
    secondary?: string
    direction?: 'rtl' | 'ltr'
    tone?: 'default' | 'warning'
  }[]
}>()
</script>

<template>
  <div class="booking-information-card app-surface-card">
    <div
      v-for="row in rows"
      :key="row.label"
      class="booking-information-card__row"
      :class="{ 'booking-information-card__row--warning': row.tone === 'warning' }"
    >
      <div class="booking-information-card__copy">
        <span class="booking-information-card__label">{{ row.label }}</span>
        <span class="booking-information-card__value">
          <bdi :dir="row.direction ?? 'rtl'">{{ row.value }}</bdi>
          <small v-if="row.secondary">{{ row.secondary }}</small>
        </span>
      </div>
      <span
        class="booking-information-card__icon"
        aria-hidden="true"
      >
        <AppIcon
          :name="row.icon"
          size="md"
        />
      </span>
    </div>
    <slot name="footer" />
  </div>
</template>

<style scoped>
.booking-information-card {
  padding: var(--space-2) var(--space-3);
  border-radius: var(--radius-lg);
}

.booking-information-card__row {
  display: grid;
  grid-template-columns: minmax(0, 1fr) 2.75rem;
  align-items: center;
  gap: var(--space-3);
  min-block-size: 2.75rem;
  border-block-end: 1px dashed var(--color-border-subtle);
}

.booking-information-card__row--warning {
  margin: var(--space-1) calc(var(--space-2) * -1);
  padding-inline: var(--space-2);
  border: 1px solid color-mix(in srgb, var(--color-accent-yellow) 28%, transparent);
  border-radius: var(--radius-md);
  color: var(--color-warning);
  background: color-mix(in srgb, var(--color-warning-soft) 60%, var(--color-surface));
}

.booking-information-card__copy {
  display: grid;
  grid-template-columns: minmax(6.5rem, 0.9fr) minmax(0, 1.25fr);
  align-items: center;
  gap: var(--space-3);
}

.booking-information-card__label {
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
}

.booking-information-card__value {
  display: grid;
  gap: var(--space-1);
  color: var(--color-text-primary);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-semibold);
  text-align: start;
}

.booking-information-card__row--warning .booking-information-card__label,
.booking-information-card__row--warning .booking-information-card__value {
  color: var(--color-warning);
  font-weight: var(--font-weight-bold);
}

.booking-information-card__value small {
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  font-weight: var(--font-weight-regular);
}

.booking-information-card__icon {
  display: grid;
  inline-size: 2.25rem;
  block-size: 2.25rem;
  place-items: center;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  color: var(--color-text-secondary);
  background: var(--color-surface-muted);
}

.booking-information-card__row--warning .booking-information-card__icon {
  color: var(--color-warning);
  border-color: color-mix(in srgb, var(--color-accent-yellow) 28%, transparent);
  background: var(--color-warning-soft);
}

@media (max-width: 22.5rem) {
  .booking-information-card__copy {
    grid-template-columns: minmax(5.5rem, 0.85fr) minmax(0, 1.15fr);
    gap: var(--space-2);
  }

  .booking-information-card__label,
  .booking-information-card__value {
    font-size: var(--font-size-xs);
  }
}
</style>
