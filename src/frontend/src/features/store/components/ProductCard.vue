<script setup lang="ts">
import { Heart } from '@lucide/vue'
import { computed } from 'vue'
import AppBadge from '../../../components/ui/AppBadge.vue'
import AppButton from '../../../components/ui/AppButton.vue'
import type { StoreProduct } from '../store.types'

const props = defineProps<{
  product: StoreProduct
  favorite?: boolean
  added?: boolean
}>()

defineEmits<{
  favorite: [product: StoreProduct]
  add: [product: StoreProduct]
  select: [product: StoreProduct]
}>()

const priceFormatter = new Intl.NumberFormat('fa-IR')
const discountPercent = computed(() => {
  if (!props.product.oldPrice) return 0
  return Math.round((1 - props.product.price / props.product.oldPrice) * 100)
})
</script>

<template>
  <article class="product-card">
    <div class="product-card__media">
      <button
        class="product-card__image-button"
        type="button"
        :aria-label="`مشاهده ${product.title}`"
        @click="$emit('select', product)"
      >
        <img
          :src="product.imageUrl"
          :alt="product.imageAlt"
          width="900"
          height="675"
          loading="lazy"
          decoding="async"
        />
      </button>
      <button
        class="product-card__favorite"
        :class="{ 'product-card__favorite--active': favorite }"
        type="button"
        :aria-label="favorite ? `حذف ${product.title} از علاقه‌مندی‌ها` : `افزودن ${product.title} به علاقه‌مندی‌ها`"
        :aria-pressed="favorite"
        @click="$emit('favorite', product)"
      >
        <Heart :size="22" :stroke-width="1.8" :fill="favorite ? 'currentColor' : 'none'" />
      </button>
      <AppBadge
        v-if="discountPercent"
        class="product-card__discount"
        tone="danger"
        size="sm"
      >{{ discountPercent.toLocaleString('fa-IR') }}٪</AppBadge>
      <AppBadge
        v-if="!product.available"
        class="product-card__availability"
        tone="neutral"
        size="sm"
      >ناموجود</AppBadge>
    </div>

    <div class="product-card__body">
      <button class="product-card__copy" type="button" @click="$emit('select', product)">
        <h2 class="product-card__title text-card-title">{{ product.title }}</h2>
        <p class="product-card__subtitle text-caption">{{ product.subtitle }}</p>
      </button>

      <div class="product-card__footer">
        <div class="product-card__price">
          <del v-if="product.oldPrice">{{ priceFormatter.format(product.oldPrice) }}</del>
          <span><bdi>{{ priceFormatter.format(product.price) }}</bdi> تومان</span>
        </div>
        <AppButton
          class="product-card__add"
          variant="secondary"
          size="sm"
          :disabled="!product.available"
          @click="$emit('add', product)"
        >
          {{ added ? 'افزوده شد' : 'افزودن' }}
        </AppButton>
      </div>
    </div>
  </article>
</template>

<style scoped>
.product-card {
  min-inline-size: 0;
  overflow: hidden;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-card);
  background: var(--color-surface);
  box-shadow: 0 5px 18px rgb(16 24 40 / 4%);
}

.product-card__media {
  position: relative;
  aspect-ratio: 4 / 3;
  overflow: hidden;
  background: var(--color-surface-muted);
}

.product-card__image-button {
  display: block;
  inline-size: 100%;
  block-size: 100%;
  padding: 0;
  border: 0;
  background: transparent;
  cursor: pointer;
}

.product-card__image-button img {
  display: block;
  inline-size: 100%;
  block-size: 100%;
  object-fit: cover;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.product-card__image-button:active img { transform: scale(.985); }

.product-card__favorite {
  position: absolute;
  inset-block-start: var(--space-2);
  inset-inline-start: var(--space-2);
  display: grid;
  inline-size: 36px;
  block-size: 36px;
  place-items: center;
  padding: 0;
  border: 1px solid rgb(255 255 255 / 72%);
  border-radius: 50%;
  color: var(--color-text-primary);
  background: rgb(255 255 255 / 78%);
  box-shadow: 0 4px 12px rgb(16 24 40 / 7%);
  backdrop-filter: blur(10px);
  -webkit-backdrop-filter: blur(10px);
  cursor: pointer;
}

.product-card__favorite--active { color: var(--color-danger); }

.product-card__discount {
  position: absolute;
  inset-block-start: var(--space-2);
  inset-inline-end: var(--space-2);
}

.product-card__availability {
  position: absolute;
  inset-block-end: var(--space-2);
  inset-inline-end: var(--space-2);
}

.product-card__body {
  display: grid;
  min-block-size: 126px;
  align-content: space-between;
  gap: var(--space-2);
  padding: 10px;
}

.product-card__copy {
  min-inline-size: 0;
  padding: 0;
  border: 0;
  color: inherit;
  background: transparent;
  text-align: start;
  cursor: pointer;
}

.product-card__title {
  overflow: hidden;
  color: var(--color-text-primary);
  font-size: 14px;
  font-weight: var(--font-weight-semibold);
  text-overflow: ellipsis;
  white-space: nowrap;
}

.product-card__subtitle {
  overflow: hidden;
  margin-block-start: 1px;
  color: var(--color-text-secondary);
  font-size: 10px;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.product-card__footer {
  display: flex;
  min-inline-size: 0;
  align-items: flex-end;
  justify-content: space-between;
  gap: 5px;
}

.product-card__price {
  display: grid;
  min-inline-size: 0;
  color: var(--color-brand-primary);
  font-size: 11px;
  font-weight: var(--font-weight-bold);
  white-space: nowrap;
}

.product-card__price del {
  color: var(--color-disabled);
  font-size: 9px;
  font-weight: var(--font-weight-regular);
}

.product-card__add {
  --app-button-inline-padding: 9px;

  min-inline-size: 82px;
  min-block-size: 38px;
  flex: none;
  padding-inline: 9px;
  border-radius: var(--radius-sm);
  font-size: 11px;
}

@media (max-width: 22.5rem) {
  .product-card__body { padding: 8px; }
  .product-card__add { min-inline-size: 50px; padding-inline: 5px; font-size: 10px; }
  .product-card__price { font-size: 10px; }
}

@media (prefers-reduced-motion: reduce) {
  .product-card__image-button img { transition: none; }
}
</style>
