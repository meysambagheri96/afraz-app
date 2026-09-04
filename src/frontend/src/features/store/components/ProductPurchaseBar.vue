<script setup lang="ts">
import { ShoppingBag } from '@lucide/vue'
import AppButton from '../../../components/ui/AppButton.vue'
import BookingStickyAction from '../../booking/components/BookingStickyAction.vue'

defineProps<{ price: number; added: boolean }>()
defineEmits<{ add: [] }>()
const formatter = new Intl.NumberFormat('fa-IR')
</script>

<template>
  <BookingStickyAction class="product-purchase-action">
    <div class="product-purchase-action__inner">
      <AppButton size="lg" block @click="$emit('add')"><template #trailing><ShoppingBag :size="23" :stroke-width="1.8" /></template>{{ added ? 'به سبد خرید اضافه شد' : 'افزودن به سبد خرید' }}</AppButton>
      <p><bdi>{{ formatter.format(price) }}</bdi><span>تومان</span></p>
    </div>
  </BookingStickyAction>
</template>

<style scoped>
.product-purchase-action {
  border-block-start: 1px solid var(--color-border-subtle);
  background: var(--color-surface);
}

.product-purchase-action__inner {
  display: grid;
  inline-size: 100%;
  grid-template-columns: minmax(0, 1fr) auto;
  align-items: center;
  gap: var(--space-3);
}

.product-purchase-action__inner :deep(.app-button__label) { white-space: nowrap; }
.product-purchase-action__inner p { display: flex; align-items: baseline; gap: 5px; margin: 0; white-space: nowrap; }
.product-purchase-action__inner bdi { font-size: var(--font-size-xl); font-weight: var(--font-weight-bold); }
.product-purchase-action__inner span { font-size: var(--font-size-xs); }

@media (max-width: 22.5rem) {
  .product-purchase-action__inner { gap: var(--space-2); }
  .product-purchase-action__inner bdi { font-size: var(--font-size-base); }
  .product-purchase-action__inner :deep(.app-button) { font-size: var(--font-size-sm); }
}
</style>
