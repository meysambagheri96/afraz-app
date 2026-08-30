<script setup lang="ts">
import type { RouteLocationRaw } from 'vue-router'
import AppIcon from '../ui/AppIcon.vue'

defineProps<{
  id?: string
  title: string
  to?: RouteLocationRaw
  actionLabel?: string
  decorated?: boolean
}>()
</script>

<template>
  <header class="section-header">
    <h2 :id="id" class="section-header__title text-section-title">
      <span v-if="decorated" class="section-header__sparkle" aria-hidden="true">✦</span>
      {{ title }}
    </h2>
    <RouterLink v-if="to" class="section-header__action" :to="to">
      {{ actionLabel ?? 'مشاهده همه' }}
      <AppIcon name="chevron-back" size="xs" />
    </RouterLink>
  </header>
</template>

<style scoped>
.section-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-4);
  margin-block-end: var(--space-3);
}

.section-header__title {
  display: inline-flex;
  min-inline-size: 0;
  align-items: center;
  gap: var(--space-2);
  color: var(--color-text-strong);
}

.section-header__sparkle { color: var(--color-accent-yellow); }

.section-header__action {
  display: inline-flex;
  min-block-size: var(--touch-target);
  flex: none;
  align-items: center;
  gap: var(--space-1);
  color: var(--color-brand-primary);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-semibold);
  text-decoration: none;
}
</style>
