<script setup lang="ts">
import { Share2, ShoppingCart } from '@lucide/vue'
import AppBrandLogo from '../../../components/shared/AppBrandLogo.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'
import AppBackButton from '../../../components/shared/AppBackButton.vue'

defineProps<{ cartCount: number }>()
defineEmits<{ share: [] }>()
</script>

<template>
  <header class="product-details-header">
    <div class="product-details-header__brand">
      <AppBackButton :to="{ name: 'store' }" />
      <AppBrandLogo size="md" />
      <div class="product-details-header__copy">
        <strong>آتلیه افراز قم</strong>
        <span>ثبت خاطره‌های شیرین کودک شما</span>
      </div>
    </div>
    <div class="product-details-header__actions">
      <AppIconButton label="اشتراک‌گذاری محصول" @click="$emit('share')"><Share2 :size="24" :stroke-width="1.8" /></AppIconButton>
      <AppIconButton label="سبد خرید"><ShoppingCart :size="24" :stroke-width="1.8" /></AppIconButton>
      <span v-if="cartCount" class="product-details-header__count">{{ cartCount.toLocaleString('fa-IR') }}</span>
    </div>
  </header>
</template>

<style scoped>
.product-details-header { display: flex; min-block-size: 74px; align-items: center; justify-content: space-between; gap: var(--space-2); }
.product-details-header__brand { display: flex; min-inline-size: 0; align-items: center; gap: var(--space-2); line-height: 1.45; text-align: start; }
.product-details-header__copy { display: grid; min-inline-size: 0; justify-items: start; }
.product-details-header__brand strong { font-size: var(--font-size-sm); white-space: nowrap; }
.product-details-header__brand span { overflow: hidden; max-inline-size: 138px; color: var(--color-text-secondary); font-size: 9px; text-overflow: ellipsis; white-space: nowrap; }
.product-details-header__actions { position: relative; display: flex; justify-content: flex-end; }
.product-details-header__count { position: absolute; inset-block-start: 2px; inset-inline-end: 2px; display: grid; min-inline-size: 20px; block-size: 20px; place-items: center; padding-inline: 4px; border: 2px solid var(--color-background); border-radius: var(--radius-full); color: white; background: var(--color-brand-primary); font-size: 10px; font-weight: var(--font-weight-bold); }
@media (max-width: 22.5rem) { .product-details-header__brand span, .product-details-header__brand strong { display: none; } }
</style>
