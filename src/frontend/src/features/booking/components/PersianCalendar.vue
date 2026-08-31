<script setup lang="ts">
import { watch } from 'vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'
import { usePersianCalendar } from '../composables/usePersianCalendar'

const emit = defineEmits<{ select: [dateKey: string | null, label: string | null] }>()

const {
  days,
  monthLabel,
  selectedDay,
  shortWeekdayNames,
  moveMonth,
  selectDay,
} = usePersianCalendar()

watch(
  selectedDay,
  (day) => emit('select', day?.dateKey ?? null, day?.fullLabel ?? null),
  { immediate: true },
)
</script>

<template>
  <section class="persian-calendar app-surface-card" aria-labelledby="booking-calendar-month">
    <header class="persian-calendar__header">
      <AppIconButton
        class="persian-calendar__nav persian-calendar__nav--next"
        label="ماه بعد"
        variant="ghost"
        @click="moveMonth(1)"
      >
        <AppIcon name="chevron-back" size="lg" />
      </AppIconButton>

      <div class="persian-calendar__heading">
        <h2 id="booking-calendar-month" class="text-section-title">{{ monthLabel }}</h2>
      </div>

      <AppIconButton
        class="persian-calendar__nav"
        label="ماه قبل"
        variant="ghost"
        @click="moveMonth(-1)"
      >
        <AppIcon name="chevron-back" size="lg" />
      </AppIconButton>
    </header>

    <div class="persian-calendar__weekdays" aria-hidden="true">
      <span v-for="weekday in shortWeekdayNames" :key="weekday">{{ weekday }}</span>
    </div>

    <div class="persian-calendar__days" role="grid" :aria-label="`تقویم ${monthLabel}`">
      <button
        v-for="day in days"
        :key="day.dateKey"
        type="button"
        class="persian-calendar__day"
        :class="[
          `persian-calendar__day--${day.state}`,
          { 'persian-calendar__day--adjacent': !day.isCurrentMonth, 'persian-calendar__day--today': day.isToday },
        ]"
        role="gridcell"
        :aria-label="day.fullLabel"
        :aria-selected="day.state === 'selected'"
        :aria-disabled="day.baseState !== 'available'"
        :disabled="day.baseState !== 'available'"
        @click="selectDay(day)"
      >
        <span>{{ day.dayLabel }}</span>
      </button>
    </div>
  </section>
</template>

<style scoped>
.persian-calendar {
  padding: var(--space-5) var(--space-4) var(--space-6);
  border-radius: var(--radius-xl);
  box-shadow: 0 0.5rem 1.75rem rgb(16 24 40 / 6%);
}

.persian-calendar__header {
  display: grid;
  grid-template-columns: var(--touch-target) minmax(0, 1fr) var(--touch-target);
  align-items: center;
  gap: var(--space-2);
}

.persian-calendar__heading {
  text-align: center;
}

.persian-calendar__heading h2 {
  color: var(--color-text-primary);
  font-size: var(--font-size-xl);
}

.persian-calendar__nav {
  color: var(--color-text-primary);
  border-radius: var(--radius-full);
}

.persian-calendar__nav--next :deep(svg) {
  transform: rotate(180deg);
}

.persian-calendar__weekdays,
.persian-calendar__days {
  display: grid;
  grid-template-columns: repeat(7, minmax(0, 1fr));
}

.persian-calendar__weekdays {
  margin-block: var(--space-6) var(--space-2);
  color: var(--color-text-primary);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-semibold);
  text-align: center;
}

.persian-calendar__days {
  grid-auto-rows: 3.25rem;
  align-items: center;
}

.persian-calendar__day {
  position: relative;
  display: grid;
  inline-size: var(--touch-target);
  block-size: var(--touch-target);
  margin: auto;
  place-items: center;
  padding: 0;
  border: 0;
  border-radius: var(--radius-full);
  color: var(--color-text-primary);
  background: transparent;
  font-size: var(--font-size-base);
  font-weight: var(--font-weight-semibold);
  transition:
    transform var(--motion-fast) var(--ease-standard),
    color var(--motion-fast) var(--ease-standard),
    background-color var(--motion-fast) var(--ease-standard);
}

.persian-calendar__day:not(:disabled) {
  cursor: pointer;
}

.persian-calendar__day:not(:disabled):active {
  transform: scale(0.94);
}

.persian-calendar__day--available::after,
.persian-calendar__day--selected::after {
  position: absolute;
  inset-block-end: -0.125rem;
  inline-size: 0.375rem;
  block-size: 0.375rem;
  border-radius: var(--radius-full);
  background: var(--color-success);
  content: '';
}

.persian-calendar__day--selected {
  color: var(--color-surface);
  background: var(--color-brand-primary);
  box-shadow: 0 0.45rem 1rem rgb(7 93 105 / 20%);
}

.persian-calendar__day--selected::after {
  inline-size: 0.625rem;
  block-size: 0.125rem;
  background: var(--color-brand-primary);
}

.persian-calendar__day--full {
  color: var(--color-warning);
  background: var(--color-warning-soft);
}

.persian-calendar__day--holiday {
  color: var(--color-danger);
  background: var(--color-danger-soft);
}

.persian-calendar__day--disabled {
  color: var(--color-disabled);
}

.persian-calendar__day--adjacent {
  opacity: 0.48;
}

.persian-calendar__day--today:not(.persian-calendar__day--selected) {
  box-shadow: inset 0 0 0 1px var(--color-border-strong);
}

@media (max-width: 22.5rem) {
  .persian-calendar { padding-inline: var(--space-2); }
  .persian-calendar__days { grid-auto-rows: 3rem; }
}

@media (prefers-reduced-motion: reduce) {
  .persian-calendar__day { transition: none; }
}
</style>
