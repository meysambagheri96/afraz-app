<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { HomeAction } from '../home.types'

defineProps<{ actions: readonly HomeAction[] }>()
</script>

<template>
  <section aria-label="دسترسی سریع">
    <div class="quick-actions">
      <RouterLink v-for="action in actions" :key="action.id" class="quick-action" :to="action.to">
        <AppIcon :name="action.icon" size="xl" tone="default" />
        <span>{{ action.label }}</span>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.quick-actions {
  display: grid;
  grid-template-columns: repeat(4, minmax(0, 1fr));
  gap: clamp(var(--space-2), 2.5vw, var(--space-3));
}

.quick-action {
  display: flex;
  min-inline-size: 0;
  min-block-size: clamp(7rem, 28vw, 8rem);
  align-items: center;
  justify-content: center;
  flex-direction: column;
  gap: var(--space-3);
  padding: var(--space-3) var(--space-1);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-xl);
  color: var(--color-text-primary);
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
  font-size: clamp(var(--font-size-xs), 3.2vw, var(--font-size-base));
  font-weight: var(--font-weight-medium);
  line-height: var(--line-height-heading);
  text-align: center;
  text-decoration: none;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.quick-action:active { transform: scale(0.98); }

@media (max-width: 22.5rem) {
  .quick-action { min-block-size: 6.5rem; }
}

@media (prefers-reduced-motion: reduce) {
  .quick-action { transition: none; }
}
</style>
