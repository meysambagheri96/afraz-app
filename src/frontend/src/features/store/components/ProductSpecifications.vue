<script setup lang="ts">
import { ChevronLeft, CircleHelp, ClipboardList, ShieldCheck, Tag } from '@lucide/vue'
import type { ProductSpecification } from '../store.types'

defineProps<{ introduction: string; specifications: ProductSpecification[] }>()
const actions = [
  { label: 'گزارش قیمت نامناسب', icon: Tag },
  { label: 'گزارش مشخصات کالا', icon: ClipboardList },
  { label: 'پرسش و پاسخ', icon: CircleHelp },
]
</script>

<template>
  <section class="product-specs" role="tabpanel" aria-labelledby="tab-specifications">
    <h2 class="text-section-title">معرفی کالا</h2>
    <p class="product-specs__intro text-body">{{ introduction }}</p>
    <dl class="product-specs__table">
      <div v-for="row in specifications" :key="row.label"><dt>{{ row.label }}</dt><dd>{{ row.value }}</dd></div>
    </dl>
    <article class="product-specs__terms">
      <ShieldCheck :size="25" :stroke-width="1.7" />
      <div><h3>شرایط و قوانین</h3><p>در صورت نارضایتی از محصول، تا ۷ روز پس از دریافت کالا می‌توانید درخواست بازگشت ثبت کنید.</p></div>
    </article>
    <div class="product-specs__actions">
      <button v-for="action in actions" :key="action.label" type="button"><component :is="action.icon" :size="21" :stroke-width="1.7" /><span>{{ action.label }}</span><ChevronLeft :size="18" /></button>
    </div>
  </section>
</template>

<style scoped>
.product-specs { display: grid; gap: var(--space-5); padding-block: var(--space-6); font-size: var(--font-size-sm); }
.product-specs :deep(.text-section-title) { font-size: var(--font-size-lg); }
.product-specs__intro { color: var(--color-text-secondary); text-align: justify; font-size: var(--font-size-sm); }
.product-specs__table { overflow: hidden; margin: 0; border: 1px solid var(--color-border-subtle); border-radius: var(--radius-md); }
.product-specs__table div { display: grid; grid-template-columns: 1fr 1.15fr; min-block-size: 48px; align-items: center; padding-inline: var(--space-4); border-block-end: 1px solid var(--color-border-subtle); font-size: var(--font-size-xs); }
.product-specs__table div:last-child { border-block-end: 0; }
.product-specs__table dt { color: var(--color-text-secondary); }
.product-specs__table dd { margin: 0; color: var(--color-text-primary); }
.product-specs__terms { display: flex; gap: var(--space-3); padding: var(--space-4); border: 1px solid var(--color-border-subtle); border-radius: var(--radius-md); color: var(--color-brand-primary); }
.product-specs__terms h3, .product-specs__terms p { margin: 0; }
.product-specs__terms h3 { color: var(--color-text-primary); font-size: var(--font-size-sm); }
.product-specs__terms p { margin-block-start: 3px; color: var(--color-text-secondary); font-size: var(--font-size-xs); line-height: var(--line-height-body); }
.product-specs__actions { overflow: hidden; border: 1px solid var(--color-border-subtle); border-radius: var(--radius-md); }
.product-specs__actions button { display: grid; inline-size: 100%; min-block-size: 52px; grid-template-columns: 28px 1fr 24px; align-items: center; gap: var(--space-2); padding-inline: var(--space-4); border: 0; border-block-end: 1px solid var(--color-border-subtle); color: var(--color-text-primary); background: var(--color-surface); text-align: start; font: inherit; cursor: pointer; }
.product-specs__actions button:last-child { border-block-end: 0; }
</style>
