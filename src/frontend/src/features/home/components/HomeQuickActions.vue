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
        <strong>{{ action.label }}</strong>
        <small>{{ action.subtitle }}</small>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.quick-actions {
  display: grid;
  grid-template-columns: repeat(4, minmax(0, 1fr));
  gap: 0;
  min-block-size: 93px;
}

.quick-action {
  display: flex;
  min-inline-size: 0;
  position: relative;
  min-block-size: 93px;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  gap: 1px;
  padding: 8px 2px 4px;
  border: 0;
  border-radius: 0;
  color: var(--color-text-primary);
  background: transparent;
  box-shadow: none;
  font-size: 13px;
  line-height: 1.4;
  text-align: center;
  text-decoration: none;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.quick-action:not(:last-child)::after { position: absolute; inset-block: 14px; inset-inline-end: 0; inline-size: 1px; background: #ececec; content: ''; }
.quick-action :deep(svg) { inline-size: 27px; block-size: 27px; margin-block-end: 7px; }
.quick-action strong { font-weight: 700; white-space: nowrap; }
.quick-action small { color: #969a9f; font-size: 11px; font-weight: 400; white-space: nowrap; }

.quick-action:active { transform: scale(0.98); }

@media (max-width: 22.5rem) {
  .quick-action { min-block-size: 88px; font-size: 12px; }
  .quick-action small { font-size: 10px; }
}

@media (prefers-reduced-motion: reduce) {
  .quick-action { transition: none; }
}
</style>
