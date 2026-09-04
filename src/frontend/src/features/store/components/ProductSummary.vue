<script setup lang="ts">
import { Heart } from '@lucide/vue'

defineProps<{ category: string; title: string; tagline: string; favorite: boolean }>()
defineEmits<{ favorite: [] }>()
</script>

<template>
  <section class="product-summary">
    <p class="product-summary__category text-label">{{ category }}</p>
    <h1 class="product-summary__title text-page-title">{{ title }}</h1>
    <p class="product-summary__tagline text-body">{{ tagline }}</p>
    <button class="product-summary__favorite" type="button" :aria-pressed="favorite" @click="$emit('favorite')">
      <Heart :size="24" :fill="favorite ? 'currentColor' : 'none'" :stroke-width="1.8" />
      {{ favorite ? 'حذف از علاقه‌مندی‌ها' : 'افزودن به علاقه‌مندی‌ها' }}
    </button>
  </section>
</template>

<style scoped>
.product-summary { display: grid; justify-items: start; padding-block: var(--space-3) var(--space-5); }
.product-summary__category { color: var(--color-text-secondary); }
.product-summary__title { margin-block-start: var(--space-1); color: var(--color-text-primary); }
.product-summary__tagline { color: var(--color-text-secondary); font-size: var(--font-size-sm); }
.product-summary__favorite { display: inline-flex; min-block-size: var(--touch-target); align-items: center; gap: var(--space-2); margin-block-start: var(--space-4); padding-inline: var(--space-4); border: 0; border-radius: var(--radius-full); color: var(--color-brand-primary); background: var(--color-brand-soft); font: inherit; cursor: pointer; }
.product-summary__favorite:focus-visible { outline: none; box-shadow: var(--focus-ring); }
</style>
