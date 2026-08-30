<script setup lang="ts">
import { computed } from 'vue'

const props = withDefaults(
  defineProps<{
    width?: string
    height?: string
    shape?: 'text' | 'circle' | 'rect'
  }>(),
  { shape: 'rect' },
)

const skeletonStyle = computed(() => ({ width: props.width, height: props.height }))
</script>

<template>
  <span class="app-skeleton" :class="`app-skeleton--${shape}`" :style="skeletonStyle" aria-hidden="true" />
</template>

<style scoped>
.app-skeleton { display: block; width: 100%; min-height: 1rem; overflow: hidden; background: linear-gradient(100deg, var(--color-surface-muted) 30%, white 50%, var(--color-surface-muted) 70%); background-size: 200% 100%; animation: app-shimmer 1.4s infinite; }
.app-skeleton--text { height: 0.85em; border-radius: var(--radius-sm); }
.app-skeleton--circle { aspect-ratio: 1; border-radius: 50%; }
.app-skeleton--rect { border-radius: var(--radius-md); }
@media (prefers-reduced-motion: reduce) { .app-skeleton { animation: none; } }
</style>
