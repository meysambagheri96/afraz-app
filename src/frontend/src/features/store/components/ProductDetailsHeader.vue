<script setup lang="ts">
import { computed } from 'vue'
import { Share2, ShoppingCart, X } from '@lucide/vue'
import { useRouter } from 'vue-router'
import AppBreadcrumb from '../../../components/shared/AppBreadcrumb.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'

const props = defineProps<{ cartCount: number; category: string; categoryId: string; title: string }>()
defineEmits<{ share: [] }>()

const router = useRouter()
const breadcrumbItems = computed(() => [
  { label: 'فروشگاه', to: { name: 'store' } },
  { label: props.category, to: { name: 'store', query: { category: props.categoryId } } },
  { label: props.title },
])
</script>

<template>
  <header class="product-details-header">
    <div class="product-details-header__toolbar">
      <AppIconButton class="product-details-header__close" label="بستن صفحه محصول" @click="router.push({ name: 'store' })">
        <X :stroke-width="1.8" />
      </AppIconButton>
      <div class="product-details-header__actions">
        <AppIconButton label="اشتراک‌گذاری محصول" @click="$emit('share')"><Share2 :stroke-width="1.8" /></AppIconButton>
        <AppIconButton label="سبد خرید"><ShoppingCart :stroke-width="1.8" /></AppIconButton>
        <span v-if="cartCount" class="product-details-header__count">{{ cartCount.toLocaleString('fa-IR') }}</span>
      </div>
    </div>
    <AppBreadcrumb :items="breadcrumbItems" />
  </header>
</template>

<style scoped>
.product-details-header { display: grid; padding-block: var(--space-2) var(--space-3); gap: var(--space-1); }
.product-details-header__toolbar { display: flex; min-block-size: 48px; align-items: center; justify-content: space-between; gap: var(--space-2); }
.product-details-header__close { flex: none; border-radius: var(--radius-full); }
.product-details-header__toolbar :deep(.app-icon-button) { color: var(--color-icon); }
.product-details-header__toolbar :deep(.app-icon-button svg) {
  inline-size: var(--icon-size-header-action);
  block-size: var(--icon-size-header-action);
}
.product-details-header__actions { position: relative; display: flex; justify-content: flex-end; }
.product-details-header__count { position: absolute; inset-block-start: 2px; inset-inline-end: 2px; display: grid; min-inline-size: 20px; block-size: 20px; place-items: center; padding-inline: 4px; border: 2px solid var(--color-background); border-radius: var(--radius-full); color: white; background: var(--color-brand-primary); font-size: 10px; font-weight: var(--font-weight-bold); }
</style>
