<script setup lang="ts">
import { BookOpen, Frame, Gift, Grid2X2, Image } from '@lucide/vue'
import type { Component } from 'vue'
import type { StoreCategory, StoreCategoryOption } from '../store.types'

defineProps<{ items: readonly StoreCategoryOption[] }>()
const model = defineModel<StoreCategory>({ required: true })

const icons: Readonly<Record<StoreCategoryOption['icon'], Component>> = {
  all: Grid2X2,
  album: BookOpen,
  frame: Frame,
  print: Image,
  gift: Gift,
}
</script>

<template>
  <div class="product-categories" role="group" aria-label="دسته‌بندی محصولات">
    <button
      v-for="item in items"
      :key="item.id"
      class="product-category"
      :class="{ 'product-category--selected': model === item.id }"
      type="button"
      :aria-pressed="model === item.id"
      @click="model = item.id"
    >
      <component :is="icons[item.icon]" :size="20" :stroke-width="1.8" aria-hidden="true" />
      <span>{{ item.label }}</span>
    </button>
  </div>
</template>

<style scoped>
.product-categories {
  display: flex;
  gap: var(--space-2);
  margin-block-start: var(--space-3);
  margin-inline: -10px;
  padding-inline: 10px;
  overflow-x: auto;
  overscroll-behavior-inline: contain;
  scrollbar-width: none;
  scroll-snap-type: inline proximity;
}

.product-categories::-webkit-scrollbar { display: none; }

.product-category {
  display: inline-flex;
  min-block-size: var(--touch-target);
  align-items: center;
  justify-content: center;
  gap: 7px;
  flex: none;
  padding-inline: var(--space-4);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-full);
  color: var(--color-text-primary);
  background: var(--color-surface);
  font: inherit;
  font-size: 12px;
  font-weight: var(--font-weight-medium);
  white-space: nowrap;
  cursor: pointer;
  scroll-snap-align: start;
  transition:
    color var(--motion-fast) var(--ease-standard),
    background var(--motion-fast) var(--ease-standard),
    transform var(--motion-fast) var(--ease-standard);
}

.product-category--selected {
  color: var(--color-surface);
  border-color: var(--color-brand-primary);
  background: var(--color-brand-primary);
  box-shadow: 0 7px 16px rgb(7 93 105 / 14%);
}

.product-category:active { transform: scale(.97); }
</style>
