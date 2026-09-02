<script setup lang="ts">
import { ref } from 'vue'
import AppBottomSheet from '../../../components/ui/AppBottomSheet.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppInput from '../../../components/ui/AppInput.vue'
import type { ExploreSort } from '../explore.types'

const search = defineModel<string>('search', { default: '' })
const sort = defineModel<ExploreSort>('sort', { default: 'newest' })
const isSortOpen = ref(false)
const sortOptions: readonly { id: ExploreSort; label: string }[] = [
  { id: 'newest', label: 'جدیدترین' },
  { id: 'oldest', label: 'قدیمی‌ترین' },
  { id: 'popular', label: 'محبوب‌ترین' },
]

function chooseSort(value: ExploreSort) {
  sort.value = value
  isSortOpen.value = false
}
</script>

<template>
  <div class="explore-toolbar">
    <AppInput
      v-model="search"
      class="explore-toolbar__search"
      type="search"
      inputmode="search"
      aria-label="جستجو در عکس‌ها"
      placeholder="جستجو در عکس‌ها..."
      autocomplete="off"
    >
      <template #leading><AppIcon name="search" size="lg" /></template>
    </AppInput>
    <span class="explore-toolbar__divider" aria-hidden="true" />
    <button
      class="explore-toolbar__sort"
      type="button"
      aria-haspopup="dialog"
      :aria-expanded="isSortOpen"
      aria-label="مرتب‌سازی عکس‌ها"
      @click="isSortOpen = true"
    >
      <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
        <path d="M4 7h3M11 7h9M4 12h9M17 12h3M4 17h5M13 17h7" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" />
        <circle cx="9" cy="7" r="1.5" stroke="currentColor" stroke-width="1.8" />
        <circle cx="15" cy="12" r="1.5" stroke="currentColor" stroke-width="1.8" />
        <circle cx="11" cy="17" r="1.5" stroke="currentColor" stroke-width="1.8" />
      </svg>
      <span>مرتب‌سازی</span>
    </button>
  </div>

  <AppBottomSheet v-model="isSortOpen" title="مرتب‌سازی عکس‌ها">
    <div class="sort-options" role="radiogroup" aria-label="روش مرتب‌سازی">
      <button
        v-for="option in sortOptions"
        :key="option.id"
        class="sort-options__item"
        :class="{ 'sort-options__item--selected': sort === option.id }"
        type="button"
        role="radio"
        :aria-checked="sort === option.id"
        @click="chooseSort(option.id)"
      >
        <span>{{ option.label }}</span>
        <span class="sort-options__indicator" aria-hidden="true">
          <span v-if="sort === option.id" />
        </span>
      </button>
    </div>
  </AppBottomSheet>
</template>

<style scoped>
.explore-toolbar {
  display: flex;
  min-block-size: 48px;
  align-items: center;
  margin-block-start: 12px;
  overflow: hidden;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  background: var(--color-surface);
  box-shadow: 0 3px 12px rgb(16 24 40 / 2%);
}
.explore-toolbar__search { min-inline-size: 0; flex: 1; }
.explore-toolbar__search :deep(.app-field__control) { min-block-size: 46px; padding-inline: 12px 8px; border: 0; border-radius: 0; box-shadow: none; }
.explore-toolbar__search :deep(.app-field__input) { font-family: var(--font-family-sans); font-size: 13px; }
.explore-toolbar__search :deep(.app-field__adornment) { color: var(--color-icon); }
.explore-toolbar__search :deep(svg) { inline-size: 24px; block-size: 24px; }
.explore-toolbar__divider { inline-size: 1px; block-size: 28px; flex: none; background: var(--color-border-subtle); }
.explore-toolbar__sort { display: inline-flex; min-inline-size: 114px; min-block-size: var(--touch-target); align-items: center; justify-content: center; gap: 8px; padding-inline: 10px 13px; border: 0; color: var(--color-text-primary); background: transparent; font-family: var(--font-family-sans); font-size: 13px; font-weight: var(--font-weight-medium); cursor: pointer; }
.explore-toolbar__sort svg { inline-size: 23px; block-size: 23px; flex: none; }
.explore-toolbar__sort:active { transform: scale(.98); }
.explore-toolbar__sort:focus-visible { outline: none; box-shadow: inset var(--focus-ring); }
.sort-options { display: grid; gap: 4px; }
.sort-options__item { display: flex; min-block-size: 52px; align-items: center; justify-content: space-between; padding-inline: 12px; border: 0; border-radius: var(--radius-control); color: var(--color-text-primary); background: transparent; font: inherit; cursor: pointer; }
.sort-options__item--selected { color: var(--color-brand-primary); background: var(--color-brand-soft); font-weight: 700; }
.sort-options__indicator { display: grid; inline-size: 20px; block-size: 20px; place-items: center; border: 1.5px solid currentcolor; border-radius: 50%; }
.sort-options__indicator span { inline-size: 10px; block-size: 10px; border-radius: 50%; background: currentcolor; }
:global(.app-bottom-sheet:has(.sort-options)) { padding-block-end: 0; }

@media (max-width: 22.5rem) {
  .explore-toolbar__sort { min-inline-size: 102px; padding-inline: 8px; font-size: 12px; }
}
</style>
