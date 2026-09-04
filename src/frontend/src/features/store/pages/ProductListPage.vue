<script setup lang="ts">
import { PackageSearch, SlidersHorizontal } from '@lucide/vue'
import { computed, onBeforeUnmount, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import AppBottomSheet from '../../../components/ui/AppBottomSheet.vue'
import AppEmptyState from '../../../components/ui/AppEmptyState.vue'
import AppSwitch from '../../../components/ui/AppSwitch.vue'
import ProductCategoryTabs from '../components/ProductCategoryTabs.vue'
import ProductGrid from '../components/ProductGrid.vue'
import StoreHeader from '../components/StoreHeader.vue'
import StorePromotionBanner from '../components/StorePromotionBanner.vue'
import StoreToolbar from '../components/StoreToolbar.vue'
import { storeCategories, storeProducts } from '../data/products.mock'
import type { StoreCategory, StoreProduct, StoreSort } from '../store.types'

const route = useRoute()
const router = useRouter()
const search = ref('')
const sort = ref<StoreSort>('newest')
const selectedCategory = ref<StoreCategory>('all')
const favoriteIds = ref<ReadonlySet<string>>(new Set())
const addedIds = ref<ReadonlySet<string>>(new Set())
const availableOnly = ref(false)
const isFilterOpen = ref(false)
const isLoading = ref(true)
const announcement = ref('')
let loadingTimer: ReturnType<typeof window.setTimeout> | undefined

const knownCategories = new Set<StoreCategory>(storeCategories.map((category) => category.id))

function syncCategoryFromRoute(value: unknown) {
  selectedCategory.value = typeof value === 'string' && knownCategories.has(value as StoreCategory)
    ? value as StoreCategory
    : 'all'
}

watch(() => route.query.category, syncCategoryFromRoute, { immediate: true })

const filteredProducts = computed(() => {
  const normalizedSearch = search.value.trim().toLocaleLowerCase('fa')
  const products = storeProducts.filter((product) => {
    const categoryMatches = selectedCategory.value === 'all'
      || product.category === selectedCategory.value
    const searchMatches = !normalizedSearch
      || `${product.title} ${product.subtitle}`.toLocaleLowerCase('fa').includes(normalizedSearch)
    const availabilityMatches = !availableOnly.value || product.available
    return categoryMatches && searchMatches && availabilityMatches
  })

  return [...products].sort((first, second) => {
    if (sort.value === 'bestselling') return second.sales - first.sales
    if (sort.value === 'price-asc') return first.price - second.price
    if (sort.value === 'price-desc') return second.price - first.price
    return second.createdAt.localeCompare(first.createdAt)
  })
})

const resultLabel = computed(() => `${filteredProducts.value.length.toLocaleString('fa-IR')} محصول`)

function toggleInSet(source: ReadonlySet<string>, id: string) {
  const next = new Set(source)
  if (next.has(id)) next.delete(id)
  else next.add(id)
  return next
}

function toggleFavorite(product: StoreProduct) {
  favoriteIds.value = toggleInSet(favoriteIds.value, product.id)
}

function toggleAdded(product: StoreProduct) {
  if (!product.available) return
  addedIds.value = toggleInSet(addedIds.value, product.id)
  announcement.value = addedIds.value.has(product.id)
    ? `${product.title} به سبد خرید اضافه شد.`
    : `${product.title} از سبد خرید حذف شد.`
}

function previewProduct(product: StoreProduct) {
  void router.push({ name: 'store-product', params: { productId: product.id } })
}

onMounted(() => {
  loadingTimer = window.setTimeout(() => { isLoading.value = false }, 550)
})

onBeforeUnmount(() => {
  if (loadingTimer) window.clearTimeout(loadingTimer)
})
</script>

<template>
  <div class="store-page">
    <StoreHeader :cart-count="addedIds.size" />
    <StoreToolbar v-model:search="search" v-model:sort="sort" />
    <ProductCategoryTabs v-model="selectedCategory" :items="storeCategories" />
    <StorePromotionBanner />

    <div class="store-results-bar">
      <p class="store-results-bar__count text-label">{{ resultLabel }}</p>
      <button
        class="store-results-bar__filter"
        :class="{ 'store-results-bar__filter--active': availableOnly }"
        type="button"
        aria-haspopup="dialog"
        :aria-expanded="isFilterOpen"
        @click="isFilterOpen = true"
      >
        <SlidersHorizontal :size="21" :stroke-width="1.8" aria-hidden="true" />
        <span>فیلتر</span>
        <span v-if="availableOnly" class="store-results-bar__filter-dot" aria-hidden="true" />
      </button>
    </div>

    <ProductGrid
      v-if="isLoading || filteredProducts.length"
      :products="filteredProducts"
      :loading="isLoading"
      :favorite-ids="favoriteIds"
      :added-ids="addedIds"
      @favorite="toggleFavorite"
      @add="toggleAdded"
      @select="previewProduct"
    />

    <AppEmptyState
      v-else
      title="محصولی پیدا نشد"
      description="فیلتر یا عبارت جستجو را تغییر دهید."
    >
      <template #icon><PackageSearch :stroke-width="1.5" /></template>
    </AppEmptyState>

    <p class="visually-hidden" aria-live="polite">{{ announcement }}</p>

    <AppBottomSheet
      v-model="isFilterOpen"
      title="فیلتر محصولات"
      description="نتایج نمایش‌داده‌شده را محدود کنید."
      flush-bottom
    >
      <AppSwitch
        v-model="availableOnly"
        label="فقط کالاهای موجود"
        description="محصولات ناموجود نمایش داده نشوند."
      />
    </AppBottomSheet>
  </div>
</template>

<style scoped>
.store-page {
  min-inline-size: 0;
  overflow-x: clip;
  padding-block-end: calc(var(--bottom-nav-height) + var(--space-4));
  color: var(--color-text-primary);
}

.store-results-bar {
  display: flex;
  min-block-size: var(--touch-target);
  align-items: center;
  justify-content: space-between;
  gap: var(--space-3);
  margin-block-start: var(--space-3);
}

.store-results-bar__count {
  color: var(--color-text-secondary);
  font-size: 12px;
}

.store-results-bar__filter {
  position: relative;
  display: inline-flex;
  min-block-size: var(--touch-target);
  align-items: center;
  gap: 7px;
  padding-inline: var(--space-2);
  border: 0;
  border-radius: var(--radius-control);
  color: var(--color-text-primary);
  background: transparent;
  font: inherit;
  font-size: 12px;
  font-weight: var(--font-weight-medium);
  cursor: pointer;
}

.store-results-bar__filter--active { color: var(--color-brand-primary); }

.store-results-bar__filter-dot {
  inline-size: 6px;
  block-size: 6px;
  border-radius: 50%;
  background: var(--color-accent-pink);
}
</style>
