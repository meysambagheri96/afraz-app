<script setup lang="ts">
import type { RouteLocationRaw } from 'vue-router'

defineProps<{
  items: readonly {
    label: string
    to?: RouteLocationRaw
  }[]
}>()
</script>

<template>
  <nav class="app-breadcrumb" aria-label="مسیر صفحه">
    <template v-for="(item, index) in items" :key="item.label">
      <span v-if="index" class="app-breadcrumb__separator" aria-hidden="true">‹</span>
      <RouterLink v-if="item.to" :to="item.to">{{ item.label }}</RouterLink>
      <span v-else class="app-breadcrumb__current" aria-current="page">{{ item.label }}</span>
    </template>
  </nav>
</template>

<style scoped>
.app-breadcrumb {
  display: flex;
  min-inline-size: 0;
  align-items: center;
  gap: var(--space-2);
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  white-space: nowrap;
}

.app-breadcrumb a {
  color: var(--color-text-secondary);
  text-decoration: underline;
  text-decoration-color: var(--color-border-strong);
  text-underline-offset: 4px;
}

.app-breadcrumb a:focus-visible {
  border-radius: var(--radius-xs);
  outline: none;
  box-shadow: var(--focus-ring);
}

.app-breadcrumb__separator { color: var(--color-brand-primary); }

.app-breadcrumb__current {
  overflow: hidden;
  min-inline-size: 0;
  color: var(--color-text-primary);
  text-overflow: ellipsis;
}
</style>
