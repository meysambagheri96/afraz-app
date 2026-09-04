<script setup lang="ts">
import type { RouteLocationRaw } from 'vue-router'
import AppIcon from '../ui/AppIcon.vue'
import AppIconButton from '../ui/AppIconButton.vue'
import AppBackButton from './AppBackButton.vue'
import AppBreadcrumb from './AppBreadcrumb.vue'

withDefaults(
  defineProps<{
    title: string
    subtitle?: string
    backTo?: RouteLocationRaw
    showHelp?: boolean
    helpLabel?: string
    breadcrumbs?: readonly { label: string; to?: RouteLocationRaw }[]
  }>(),
  { showHelp: false, helpLabel: 'راهنما' },
)

defineEmits<{ help: [] }>()
</script>

<template>
  <header class="app-page-header">
    <div class="app-page-header__main">
      <AppBackButton :to="backTo" />
      <div class="app-page-header__copy">
        <h1 class="app-page-header__title text-page-title">{{ title }}</h1>
        <p v-if="subtitle" class="app-page-header__subtitle text-label">{{ subtitle }}</p>
      </div>
      <AppIconButton
        v-if="showHelp"
        class="app-page-header__help"
        :label="helpLabel"
        variant="ghost"
        @click="$emit('help')"
      >
        <AppIcon name="help" size="lg" />
      </AppIconButton>
      <span v-else aria-hidden="true" />
    </div>
    <AppBreadcrumb v-if="breadcrumbs?.length" :items="breadcrumbs" />
  </header>
</template>

<style scoped>
.app-page-header {
  display: grid;
  gap: var(--space-1);
  padding-block-end: var(--space-3);
}

.app-page-header__main {
  display: grid;
  grid-template-columns: var(--touch-target) minmax(0, 1fr) var(--touch-target);
  align-items: center;
  gap: var(--space-2);
  min-block-size: 4.5rem;
}

.app-page-header__copy {
  min-inline-size: 0;
  text-align: center;
}

.app-page-header__title {
  color: var(--color-text-primary);
  font-size: var(--font-size-xl);
  font-weight: var(--font-weight-bold);
  line-height: var(--line-height-tight);
}

.app-page-header__subtitle {
  margin-block-start: var(--space-1);
  color: var(--color-text-secondary);
}

.app-page-header__help {
  color: var(--color-text-primary);
  border-radius: var(--radius-full);
}

.app-page-header__help :deep(svg) {
  inline-size: 1.625rem;
  block-size: 1.625rem;
}
</style>
