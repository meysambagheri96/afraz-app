<script setup lang="ts">
import type { Component } from 'vue'
import { Baby, CakeSlice, PersonStanding, Smile, TreePine, UsersRound } from '@lucide/vue'
import type { ExploreCategory, ExploreCategoryOption } from '../explore.types'

defineProps<{ items: readonly ExploreCategoryOption[] }>()
const model = defineModel<ExploreCategory>({ required: true })

const categoryIcons: Readonly<Record<Exclude<ExploreCategory, 'all'>, Component>> = {
  outdoor: TreePine,
  family: UsersRound,
  pregnancy: PersonStanding,
  birthday: CakeSlice,
  child: Smile,
  newborn: Baby,
}
</script>

<template>
  <div class="explore-categories" role="group" aria-label="دسته‌بندی عکس‌ها">
    <button
      v-for="item in items"
      :key="item.id"
      class="explore-category"
      :class="{ 'explore-category--selected': model === item.id }"
      type="button"
      :aria-pressed="model === item.id"
      @click="model = item.id"
    >
      <span class="explore-category__icon" aria-hidden="true">
        <svg v-if="item.id === 'all'" viewBox="0 0 24 24" fill="none">
          <rect x="4" y="4" width="6" height="6" rx="1" fill="currentColor" />
          <rect x="14" y="4" width="6" height="6" rx="1" fill="currentColor" />
          <rect x="4" y="14" width="6" height="6" rx="1" fill="currentColor" />
          <rect x="14" y="14" width="6" height="6" rx="1" fill="currentColor" />
        </svg>
        <component
          :is="categoryIcons[item.icon]"
          v-else-if="item.icon"
          :stroke-width="1.8"
        />
      </span>
      <span class="explore-category__label">{{ item.label }}</span>
    </button>
  </div>
</template>

<style scoped>
.explore-categories {
  display: flex;
  justify-content: space-between;
  gap: 4px;
  margin-block-start: 13px;
  margin-inline: -10px;
  padding-inline: 10px;
  overflow-x: auto;
  overscroll-behavior-inline: contain;
  scrollbar-width: none;
  scroll-snap-type: inline proximity;
}
.explore-categories::-webkit-scrollbar { display: none; }
.explore-category { display: grid; min-inline-size: 48px; min-block-size: 80px; place-items: start center; gap: 5px; flex: none; padding: 0; border: 0; color: var(--color-text-primary); background: transparent; font-family: var(--font-family-sans); cursor: pointer; scroll-snap-align: start; }
.explore-category__icon { display: grid; inline-size: 45px; block-size: 45px; place-items: center; border: 1px solid var(--color-border-subtle); border-radius: 50%; color: var(--color-icon); background: var(--color-surface); transition: color var(--motion-fast) var(--ease-standard), background var(--motion-fast) var(--ease-standard), transform var(--motion-fast) var(--ease-standard); }
.explore-category__icon svg { inline-size: 27px; block-size: 27px; }
.explore-category__label { font-size: 12px; font-weight: var(--font-weight-medium); line-height: 1.3; white-space: nowrap; }
.explore-category--selected .explore-category__icon { color: var(--color-surface); border-color: var(--color-brand-primary); background: var(--color-brand-primary); box-shadow: 0 7px 16px rgb(7 93 105 / 16%); }
.explore-category--selected .explore-category__label { font-weight: 600; }
.explore-category:active .explore-category__icon { transform: scale(.96); }
.explore-category:focus-visible { outline: none; }
.explore-category:focus-visible .explore-category__icon { box-shadow: var(--focus-ring); }

@media (max-width: 22.5rem) {
  .explore-categories { justify-content: flex-start; gap: 7px; }
}
</style>
