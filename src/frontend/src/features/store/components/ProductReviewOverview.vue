<script setup lang="ts">
import { CheckCircle2, PackageCheck, Sparkles, Truck } from '@lucide/vue'

defineProps<{ overview: string; benefits: string[] }>()
const serviceBenefits = [
  { label: 'تضمین کیفیت چاپ', icon: Sparkles },
  { label: 'بسته‌بندی ایمن', icon: PackageCheck },
  { label: 'ارسال سریع', icon: Truck },
]
</script>

<template>
  <section class="product-overview" role="tabpanel" aria-labelledby="tab-overview">
    <div class="product-overview__hero"><span>انتخاب ماندگار افراز</span><h2>معرفی کالا</h2><p>{{ overview }}</p></div>
    <article class="product-overview__card"><h2 class="text-section-title">بررسی تخصصی</h2><p>ترکیب پارچه باکیفیت و صحافی دقیق، این مدل را به انتخابی شیک برای آرشیو خاطرات کودک تبدیل کرده است.</p></article>
    <article class="product-overview__card"><h2 class="text-section-title">ویژگی‌های کلیدی</h2><ul><li v-for="benefit in benefits" :key="benefit"><CheckCircle2 :size="20" />{{ benefit }}</li></ul></article>
    <div class="product-overview__services"><div v-for="item in serviceBenefits" :key="item.label"><component :is="item.icon" :size="25" /><span>{{ item.label }}</span></div></div>
    <article class="product-overview__teaser"><strong>نظر خریداران</strong><span>امتیاز ۴٫۸ از ۵ بر اساس تجربه خریداران</span></article>
  </section>
</template>

<style scoped>
.product-overview { display: grid; gap: var(--space-4); padding-block: var(--space-6); font-size: var(--font-size-sm); }
.product-overview :deep(.text-section-title) { font-size: var(--font-size-lg); }
.product-overview__hero, .product-overview__card, .product-overview__teaser { padding: var(--space-5); border: 1px solid var(--color-border-subtle); border-radius: var(--radius-lg); background: var(--color-surface); }
.product-overview__hero { background: linear-gradient(135deg, var(--color-brand-soft), var(--color-surface)); }
.product-overview__hero span { color: var(--color-brand-primary); font-size: var(--font-size-xs); font-weight: var(--font-weight-bold); }
.product-overview h2, .product-overview p { margin: 0; }
.product-overview h2 { margin-block-start: var(--space-2); }
.product-overview p { margin-block-start: var(--space-2); color: var(--color-text-secondary); line-height: var(--line-height-body); }
.product-overview ul { display: grid; gap: var(--space-3); margin: var(--space-4) 0 0; padding: 0; list-style: none; }
.product-overview li { display: flex; align-items: center; gap: var(--space-2); color: var(--color-text-secondary); }
.product-overview li svg { color: var(--color-brand-primary); }
.product-overview__services { display: grid; grid-template-columns: repeat(3, 1fr); gap: var(--space-2); }
.product-overview__services div { display: grid; min-block-size: 96px; place-items: center; align-content: center; gap: var(--space-2); border-radius: var(--radius-md); color: var(--color-brand-primary); background: var(--color-surface-muted); text-align: center; font-size: var(--font-size-xs); }
.product-overview__teaser { display: grid; gap: var(--space-1); color: var(--color-text-secondary); }
.product-overview__teaser strong { color: var(--color-text-primary); }
</style>
