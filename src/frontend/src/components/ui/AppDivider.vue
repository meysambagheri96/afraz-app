<script setup lang="ts">
withDefaults(defineProps<{ orientation?: 'horizontal' | 'vertical'; label?: string }>(), {
  orientation: 'horizontal',
})
</script>

<template>
  <div class="app-divider" :class="`app-divider--${orientation}`" role="separator" :aria-orientation="orientation">
    <span v-if="label && orientation === 'horizontal'" class="app-divider__line" />
    <span v-if="label && orientation === 'horizontal'" class="app-divider__label">{{ label }}</span>
    <span class="app-divider__line" />
  </div>
</template>

<style scoped>
.app-divider { color: var(--color-text-secondary); }
.app-divider--horizontal { display: flex; width: 100%; align-items: center; gap: var(--space-3); }
.app-divider--vertical { align-self: stretch; width: 1px; min-height: 1.5rem; background: var(--color-border); }
.app-divider__line { flex: 1; height: 1px; background: var(--color-border); }
.app-divider__label { flex: none; font-size: var(--font-size-xs); }
</style>
