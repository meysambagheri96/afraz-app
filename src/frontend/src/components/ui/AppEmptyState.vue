<script setup lang="ts">
withDefaults(
  defineProps<{
    title: string
    description?: string
  }>(),
  { description: '' },
)
</script>

<template>
  <section class="app-empty-state" aria-live="polite">
    <span v-if="$slots.icon" class="app-empty-state__icon" aria-hidden="true">
      <slot name="icon" />
    </span>
    <h2 class="app-empty-state__title text-section-title">{{ title }}</h2>
    <p v-if="description" class="app-empty-state__description text-label">{{ description }}</p>
  </section>
</template>

<style scoped>
.app-empty-state {
  display: grid;
  min-block-size: 17.5rem;
  place-items: center;
  align-content: center;
  gap: var(--space-2);
  padding: var(--space-6);
  color: var(--color-text-secondary);
  text-align: center;
}

.app-empty-state__icon {
  display: grid;
  inline-size: 4rem;
  block-size: 4rem;
  margin-block-end: var(--space-1);
  place-items: center;
  color: var(--color-disabled);
}

.app-empty-state__icon :deep(svg) {
  inline-size: 100%;
  block-size: 100%;
}

.app-empty-state__title {
  color: var(--color-text-primary);
  font-size: var(--font-size-lg);
}

.app-empty-state__description { margin: 0; }
</style>
