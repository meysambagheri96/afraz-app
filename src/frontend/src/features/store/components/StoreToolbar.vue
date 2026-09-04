<script setup lang="ts">
import { ArrowDownUp, Search } from '@lucide/vue'
import { computed, ref } from 'vue'
import AppBottomSheet from '../../../components/ui/AppBottomSheet.vue'
import AppInput from '../../../components/ui/AppInput.vue'
import type { StoreSort } from '../store.types'

const search = defineModel<string>('search', { default: '' })
const sort = defineModel<StoreSort>('sort', { default: 'newest' })
const isSortOpen = ref(false)

const sortOptions: readonly { id: StoreSort; label: string }[] = [
  { id: 'newest', label: 'جدیدترین' },
  { id: 'bestselling', label: 'پرفروش‌ترین' },
  { id: 'price-asc', label: 'ارزان‌ترین' },
  { id: 'price-desc', label: 'گران‌ترین' },
]

const selectedSortLabel = computed(
  () => sortOptions.find((option) => option.id === sort.value)?.label ?? 'مرتب‌سازی',
)

function chooseSort(value: StoreSort) {
  sort.value = value
  isSortOpen.value = false
}
</script>

<template>
  <div class="store-toolbar">
    <AppInput
      v-model="search"
      class="store-toolbar__search"
      type="search"
      inputmode="search"
      aria-label="جستجو در محصولات"
      placeholder="جستجو در محصولات..."
      autocomplete="off"
    >
      <template #leading><Search :size="25" :stroke-width="1.8" /></template>
    </AppInput>

    <button
      class="store-toolbar__sort"
      type="button"
      aria-haspopup="dialog"
      :aria-expanded="isSortOpen"
      :aria-label="`مرتب‌سازی: ${selectedSortLabel}`"
      @click="isSortOpen = true"
    >
      <ArrowDownUp :size="21" :stroke-width="1.8" aria-hidden="true" />
      <span>مرتب‌سازی</span>
    </button>
  </div>

  <AppBottomSheet v-model="isSortOpen" title="مرتب‌سازی محصولات" flush-bottom>
    <div class="store-sort-options" role="radiogroup" aria-label="روش مرتب‌سازی محصولات">
      <button
        v-for="option in sortOptions"
        :key="option.id"
        class="store-sort-options__item"
        :class="{ 'store-sort-options__item--selected': sort === option.id }"
        type="button"
        role="radio"
        :aria-checked="sort === option.id"
        @click="chooseSort(option.id)"
      >
        <span>{{ option.label }}</span>
        <span class="store-sort-options__indicator" aria-hidden="true">
          <span v-if="sort === option.id" />
        </span>
      </button>
    </div>
  </AppBottomSheet>
</template>

<style scoped>
.store-toolbar {
  display: flex;
  min-block-size: 50px;
  align-items: stretch;
  gap: var(--space-2);
  margin-block-start: var(--space-3);
}

.store-toolbar__search {
  min-inline-size: 0;
  flex: 1;
}

.store-toolbar__search :deep(.app-field__control) {
  min-block-size: 50px;
  padding-inline: var(--space-3);
  border-radius: var(--radius-md);
  background: var(--color-surface);
  box-shadow: 0 3px 12px rgb(16 24 40 / 2%);
}

.store-toolbar__search :deep(.app-field__input) {
  font-size: 13px;
}

.store-toolbar__sort {
  display: inline-flex;
  min-inline-size: 108px;
  min-block-size: var(--touch-target);
  align-items: center;
  justify-content: center;
  gap: 6px;
  flex: none;
  padding-inline: 10px;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  color: var(--color-text-primary);
  background: var(--color-surface);
  box-shadow: 0 3px 12px rgb(16 24 40 / 2%);
  font: inherit;
  font-size: 12px;
  font-weight: var(--font-weight-medium);
  cursor: pointer;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.store-toolbar__sort:active { transform: scale(.98); }

.store-sort-options { display: grid; gap: var(--space-1); }

.store-sort-options__item {
  display: flex;
  min-block-size: 52px;
  align-items: center;
  justify-content: space-between;
  padding-inline: var(--space-3);
  border: 0;
  border-radius: var(--radius-control);
  color: var(--color-text-primary);
  background: transparent;
  font: inherit;
  cursor: pointer;
}

.store-sort-options__item--selected {
  color: var(--color-brand-primary);
  background: var(--color-brand-soft);
  font-weight: var(--font-weight-bold);
}

.store-sort-options__indicator {
  display: grid;
  inline-size: 20px;
  block-size: 20px;
  place-items: center;
  border: 1.5px solid currentcolor;
  border-radius: 50%;
}

.store-sort-options__indicator span {
  inline-size: 10px;
  block-size: 10px;
  border-radius: 50%;
  background: currentcolor;
}

@media (max-width: 22.5rem) {
  .store-toolbar { gap: 6px; }
  .store-toolbar__sort { min-inline-size: 96px; padding-inline: 7px; }
}
</style>
