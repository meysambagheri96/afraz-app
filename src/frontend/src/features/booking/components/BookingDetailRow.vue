<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { AppIconName } from '../../../components/ui/icon.types'

defineProps<{
  label: string
  value: string
  dot?: boolean
  icon?: AppIconName
}>()
</script>

<template>
  <div class="booking-detail-row">
    <span class="booking-detail-row__label">{{ label }}</span>
    <span class="booking-detail-row__value">
      <span
        v-if="dot"
        class="booking-detail-row__dot"
        aria-hidden="true"
      />
      <AppIcon
        v-if="icon"
        :name="icon"
        size="sm"
        tone="muted"
      />
      <span>{{ value }}</span>
    </span>
  </div>
</template>

<style scoped>
.booking-detail-row {
  display: flex;
  min-block-size: 2.25rem;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-4);
  color: var(--color-text-primary);
  font-size: var(--font-size-sm);
}

.booking-detail-row__label {
  flex: none;
  font-weight: var(--font-weight-semibold);
}

.booking-detail-row__value {
  display: inline-flex;
  min-inline-size: 0;
  align-items: center;
  justify-content: flex-end;
  gap: var(--space-2);
  color: var(--color-text-primary);
  font-weight: var(--font-weight-medium);
  text-align: end;
}

.booking-detail-row__dot {
  inline-size: 0.625rem;
  block-size: 0.625rem;
  flex: none;
  border-radius: var(--radius-full);
  background: color-mix(in srgb, var(--color-accent-blue) 70%, var(--color-accent-mint));
}

@media (max-width: 22.5rem) {
  .booking-detail-row {
    align-items: flex-start;
    font-size: var(--font-size-xs);
  }
}
</style>
