<script setup lang="ts">
import type { AppBadgeTone } from './types'

withDefaults(
  defineProps<{
    tone?: AppBadgeTone
    size?: 'sm' | 'md'
    rounded?: boolean
    dot?: boolean
    loading?: boolean
    disabled?: boolean
  }>(),
  { tone: 'neutral', size: 'md', rounded: true },
)
</script>

<template>
  <span
    class="app-badge"
    :class="[`app-badge--${tone}`, `app-badge--${size}`, { 'app-badge--rounded': rounded, 'app-badge--disabled': disabled }]"
    :aria-busy="loading || undefined"
  >
    <span v-if="loading" class="app-spinner app-badge__spinner" aria-hidden="true" />
    <span v-else-if="dot" class="app-badge__dot" aria-hidden="true" />
    <slot />
  </span>
</template>

<style scoped>
.app-badge { display: inline-flex; width: fit-content; align-items: center; gap: var(--space-1); color: var(--color-text-secondary); background: var(--color-surface-muted); font-weight: var(--font-weight-bold); line-height: 1; white-space: nowrap; }
.app-badge--sm { min-height: 1.5rem; padding-inline: var(--space-2); font-size: 0.6875rem; }
.app-badge--md { min-height: 1.75rem; padding-inline: var(--space-3); font-size: var(--font-size-xs); }
.app-badge--rounded { border-radius: var(--radius-full); }
.app-badge--success { color: var(--color-success); background: var(--color-success-soft); }
.app-badge--warning { color: var(--color-warning); background: var(--color-warning-soft); }
.app-badge--danger { color: var(--color-danger); background: var(--color-danger-soft); }
.app-badge--info { color: var(--color-info); background: var(--color-info-soft); }
.app-badge--disabled { opacity: 0.55; }
.app-badge__dot { width: 0.45rem; height: 0.45rem; border-radius: 50%; background: currentColor; }
.app-badge__spinner { width: 0.8rem; height: 0.8rem; border-width: 1.5px; }
</style>
