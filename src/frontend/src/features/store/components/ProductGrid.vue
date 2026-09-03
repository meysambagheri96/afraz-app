<script setup lang="ts">
import ProductCard from './ProductCard.vue'
import ProductCardSkeleton from './ProductCardSkeleton.vue'
import type { StoreProduct } from '../store.types'

withDefaults(
  defineProps<{
    products: readonly StoreProduct[]
    loading?: boolean
    favoriteIds?: ReadonlySet<string>
    addedIds?: ReadonlySet<string>
  }>(),
  { loading: false, favoriteIds: () => new Set<string>(), addedIds: () => new Set<string>() },
)

defineEmits<{
  favorite: [product: StoreProduct]
  add: [product: StoreProduct]
  select: [product: StoreProduct]
}>()
</script>

<template>
  <div class="product-grid" :aria-busy="loading">
    <template v-if="loading">
      <ProductCardSkeleton v-for="index in 6" :key="index" />
    </template>
    <template v-else>
      <ProductCard
        v-for="product in products"
        :key="product.id"
        :product="product"
        :favorite="favoriteIds.has(product.id)"
        :added="addedIds.has(product.id)"
        @favorite="$emit('favorite', $event)"
        @add="$emit('add', $event)"
        @select="$emit('select', $event)"
      />
    </template>
  </div>
</template>

<style scoped>
.product-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: var(--space-3);
  margin-block-start: var(--space-3);
}

@media (max-width: 22.5rem) {
  .product-grid { gap: var(--space-2); }
}

@media (min-width: 40rem) {
  .product-grid { grid-template-columns: repeat(3, minmax(0, 1fr)); }
}
</style>
