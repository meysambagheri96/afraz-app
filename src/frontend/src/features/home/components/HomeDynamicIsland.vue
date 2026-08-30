<script setup lang="ts">
import { Capacitor } from '@capacitor/core'

const showWebPreview = !Capacitor.isNativePlatform()
</script>

<template>
  <div v-if="showWebPreview" class="dynamic-island-preview" aria-hidden="true" />
</template>

<style scoped>
.dynamic-island-preview {
  inline-size: clamp(6.5rem, 28vw, 7.75rem);
  block-size: 2rem;
  margin: 0 auto var(--space-4);
  border-radius: var(--radius-full);
  background: var(--color-icon);
}

@media (min-width: 31rem) {
  .dynamic-island-preview { display: none; }
}
</style>
