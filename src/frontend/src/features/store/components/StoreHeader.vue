<script setup lang="ts">
import { Bell, ShoppingCart } from '@lucide/vue'
import { useRouter } from 'vue-router'
import AppBrandLogo from '../../../components/shared/AppBrandLogo.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'

withDefaults(defineProps<{ cartCount?: number }>(), { cartCount: 0 })

const router = useRouter()
</script>

<template>
  <header class="store-header">
    <div class="store-header__brand">
      <AppBrandLogo />
      <div class="store-header__copy">
        <div class="store-header__title-row">
          <h1 class="store-header__title text-page-title">فروشگاه</h1>
          <AppIcon name="chevron-down" size="xs" aria-hidden="true" />
        </div>
        <p class="store-header__subtitle text-caption">خرید آلبوم و محصولات چاپی</p>
      </div>
    </div>

    <div class="store-header__actions">
      <AppIconButton
        class="store-header__action"
        label="اعلان‌ها"
        variant="ghost"
        @click="router.push({ name: 'notifications' })"
      >
        <Bell :stroke-width="1.8" />
        <span class="store-header__dot" aria-hidden="true" />
      </AppIconButton>
      <AppIconButton class="store-header__action" label="سبد خرید" variant="ghost">
        <ShoppingCart :stroke-width="1.8" />
        <span v-if="cartCount" class="store-header__cart-count">{{ cartCount }}</span>
      </AppIconButton>
    </div>
  </header>
</template>

<style scoped>
.store-header {
  display: flex;
  min-block-size: 59px;
  align-items: center;
  justify-content: space-between;
  gap: 10px;
}

.store-header__brand {
  display: flex;
  min-inline-size: 0;
  align-items: center;
  gap: 10px;
}

.store-header__copy { min-inline-size: 0; }

.store-header__title-row {
  display: flex;
  align-items: center;
  gap: var(--space-2);
}

.store-header__title {
  overflow: hidden;
  color: var(--color-text-primary);
  font-size: 19px;
  font-weight: 700;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.store-header__subtitle {
  margin-block-start: 1px;
  color: var(--color-text-secondary);
  font-size: 11px;
  white-space: nowrap;
}

.store-header__actions {
  display: flex;
  flex: none;
  gap: 4px;
}

.store-header__action {
  position: relative;
  inline-size: 42px;
  block-size: 44px;
  border-radius: 50%;
  color: var(--color-text-primary);
}

.store-header__action :deep(svg) {
  inline-size: var(--icon-size-header-action);
  block-size: var(--icon-size-header-action);
}

.store-header__dot {
  position: absolute;
  inset-block-start: 7px;
  inset-inline-start: 7px;
  inline-size: 7px;
  block-size: 7px;
  border: 2px solid var(--color-surface);
  border-radius: 50%;
  background: var(--color-accent-pink);
}

.store-header__cart-count {
  position: absolute;
  inset-block-start: 2px;
  inset-inline-start: 1px;
  display: grid;
  min-inline-size: 17px;
  block-size: 17px;
  place-items: center;
  padding-inline: 4px;
  border: 2px solid var(--color-surface);
  border-radius: var(--radius-full);
  color: var(--color-surface);
  background: var(--color-accent-pink);
  font-size: 9px;
  font-weight: var(--font-weight-bold);
  line-height: 1;
}

@media (max-width: 22.5rem) {
  .store-header__subtitle { display: none; }
  .store-header__brand { gap: var(--space-2); }
  .store-header__actions { gap: var(--space-1); }
}
</style>
