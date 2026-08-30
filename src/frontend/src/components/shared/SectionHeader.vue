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
  min-block-size: 28px;
  margin-block-end: 7px;
}

.section-header__title {
  display: inline-flex;
  min-inline-size: 0;
  align-items: center;
  gap: var(--space-2);
  color: #111820;
  font-size: 17px;
  font-weight: 900;
}

.section-header__sparkle { color: var(--color-accent-yellow); }

.section-header__action {
  display: inline-flex;
  min-block-size: var(--touch-target);
  flex: none;
  align-items: center;
  gap: var(--space-1);
  color: #111820;
  font-size: 12px;
  font-weight: 600;
  text-decoration: none;
}
</style>
