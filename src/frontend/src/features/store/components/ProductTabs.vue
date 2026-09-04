<script setup lang="ts">
import type { ProductTabId } from '../store.types'

const model = defineModel<ProductTabId>({ required: true })
const emit = defineEmits<{ select: [tab: ProductTabId] }>()
const tabs: Array<{ id: ProductTabId; label: string }> = [
  { id: 'specifications', label: 'مشخصات' },
  { id: 'overview', label: 'بررسی محصول' },
  { id: 'reviews', label: 'دیدگاه‌ها' },
]
</script>

<template>
  <div class="product-tabs" role="tablist" aria-label="اطلاعات محصول">
    <button v-for="tab in tabs" :id="`tab-${tab.id}`" :key="tab.id" type="button" role="tab" :aria-selected="model === tab.id" :class="{ active: model === tab.id }" @click="emit('select', tab.id)">{{ tab.label }}</button>
  </div>
</template>

<style scoped>
.product-tabs { position: sticky; inset-block-start: 0; z-index: var(--z-sticky); display: grid; grid-template-columns: repeat(3, 1fr); border-block: 1px solid var(--color-border-subtle); background: color-mix(in srgb, var(--color-background) 94%, transparent); backdrop-filter: blur(14px); }
.product-tabs button { position: relative; min-block-size: 52px; padding: 0; border: 0; color: var(--color-text-secondary); background: transparent; font: inherit; font-size: var(--font-size-sm); cursor: pointer; }
.product-tabs button.active { color: var(--color-brand-primary); font-weight: var(--font-weight-bold); }
.product-tabs button.active::after { position: absolute; inset-inline: 12%; inset-block-end: 0; block-size: 3px; border-radius: var(--radius-full); background: var(--color-brand-primary); content: ''; }
</style>
