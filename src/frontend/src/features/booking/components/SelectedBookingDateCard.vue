<script setup lang="ts">
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'

defineProps<{
  dateLabel: string
}>()

defineEmits<{ change: [] }>()
</script>

<template>
  <section
    class="selected-date-card app-surface-card"
    aria-labelledby="selected-date-title"
  >
    <span
      class="selected-date-card__icon"
      aria-hidden="true"
    >
      <AppIcon
        name="booking"
        size="xl"
      />
    </span>

    <div class="selected-date-card__copy">
      <p
        id="selected-date-title"
        class="text-label"
      >
        تاریخ انتخاب شده
      </p>
      <p class="selected-date-card__date">
        {{ dateLabel }}
      </p>
    </div>

    <AppButton
      class="selected-date-card__change"
      variant="secondary"
      size="sm"
      @click="$emit('change')"
    >
      <template #leading>
        <AppIcon
          name="booking"
          size="sm"
        />
      </template>
      تغییر تاریخ
    </AppButton>
  </section>
</template>

<style scoped>
.selected-date-card {
  display: grid;
  grid-template-columns: 3.5rem minmax(0, 1fr) auto;
  align-items: center;
  gap: var(--space-3);
  padding: var(--space-4);
  border-radius: var(--radius-xl);
}

.selected-date-card__icon {
  display: grid;
  inline-size: 3.5rem;
  block-size: 3.5rem;
  place-items: center;
  border-radius: var(--radius-full);
  color: var(--color-surface);
  background: var(--color-brand-primary);
  box-shadow: 0 0.65rem 1.4rem rgb(7 93 105 / 18%);
}

.selected-date-card__copy {
  min-inline-size: 0;
}

.selected-date-card__copy > * {
  margin: 0;
}

.selected-date-card__copy > :first-child {
  color: var(--color-text-secondary);
}

.selected-date-card__date {
  margin-block: var(--space-1);
  color: var(--color-text-primary);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-bold);
  line-height: var(--line-height-heading);
  white-space: nowrap;
}

.selected-date-card__change {
  min-inline-size: auto;
  padding-inline: var(--space-2);
  white-space: nowrap;
}

@media (max-width: 22.5rem) {
  .selected-date-card {
    grid-template-columns: 3.5rem minmax(0, 1fr);
    gap: var(--space-3);
    padding: var(--space-4);
  }

  .selected-date-card__icon {
    inline-size: 3.5rem;
    block-size: 3.5rem;
  }

  .selected-date-card__change {
    grid-column: 1 / -1;
    inline-size: 100%;
  }
}
</style>
