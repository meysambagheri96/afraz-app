<script setup lang="ts">
import { Star, ThumbsDown, ThumbsUp } from '@lucide/vue'
import { ref } from 'vue'
import AppBadge from '../../../components/ui/AppBadge.vue'
import type { ProductReview } from '../store.types'

defineProps<{ rating: number; count: number; distribution: Array<{ stars: number; percent: number }>; reviews: ProductReview[]; images: string[] }>()
const reactions = ref<Record<string, 'like' | 'dislike' | undefined>>({})
</script>

<template>
  <section class="product-reviews" role="tabpanel" aria-labelledby="tab-reviews">
    <article class="rating-summary">
      <div class="rating-summary__score"><strong>{{ rating.toLocaleString('fa-IR') }}</strong><div><Star v-for="n in 5" :key="n" :size="18" fill="currentColor" /></div><span>({{ count.toLocaleString('fa-IR') }} نظر)</span></div>
      <div class="rating-summary__bars"><div v-for="row in distribution" :key="row.stars"><span>{{ row.stars.toLocaleString('fa-IR') }} ستاره</span><i><b :style="{ inlineSize: `${row.percent}%` }" /></i><small>{{ row.percent.toLocaleString('fa-IR') }}٪</small></div></div>
    </article>
    <div class="product-reviews__photos" aria-label="تصاویر خریداران"><img v-for="(image, index) in images.slice(0, 4)" :key="image" :src="image" :alt="`تصویر خریدار ${index + 1}`" width="160" height="130" /></div>
    <article class="product-reviews__digest"><strong>خلاصه دیدگاه‌های خریداران</strong><p>بیشتر خریداران از طراحی شیک، کیفیت پارچه و صحافی محکم آلبوم رضایت داشته‌اند.</p></article>
    <article v-for="review in reviews" :key="review.id" class="review-card">
      <header><div><strong>{{ review.author }}</strong><AppBadge tone="success" size="sm">خریدار</AppBadge></div><time>{{ review.date }}</time></header>
      <div class="review-card__stars"><Star v-for="n in 5" :key="n" :size="17" :fill="n <= review.rating ? 'currentColor' : 'none'" /></div>
      <h3 v-if="review.title">{{ review.title }}</h3><p>{{ review.body }}</p><small>{{ review.variant }}</small>
      <footer><button type="button" :aria-pressed="reactions[review.id] === 'like'" @click="reactions[review.id] = reactions[review.id] === 'like' ? undefined : 'like'"><ThumbsUp :size="19" />{{ review.likes + (reactions[review.id] === 'like' ? 1 : 0) }}</button><button type="button" :aria-pressed="reactions[review.id] === 'dislike'" @click="reactions[review.id] = reactions[review.id] === 'dislike' ? undefined : 'dislike'"><ThumbsDown :size="19" />{{ review.dislikes + (reactions[review.id] === 'dislike' ? 1 : 0) }}</button></footer>
    </article>
    <button class="product-reviews__all" type="button">مشاهده همه دیدگاه‌ها ({{ count.toLocaleString('fa-IR') }} نظر)</button>
  </section>
</template>

<style scoped>
.product-reviews { display: grid; gap: var(--space-4); padding-block: var(--space-6); font-size: var(--font-size-sm); }
.rating-summary { display: grid; grid-template-columns: 120px 1fr; gap: var(--space-4); padding: var(--space-4); border: 1px solid var(--color-border-subtle); border-radius: var(--radius-lg); }
.rating-summary__score { display: grid; align-content: center; justify-items: center; border-inline-end: 1px solid var(--color-border-subtle); }
.rating-summary__score strong { font-size: 44px; line-height: 1; }
.rating-summary__score div, .review-card__stars { display: flex; color: #ffb300; }
.rating-summary__score span { color: var(--color-text-secondary); font-size: 11px; }
.rating-summary__bars { display: grid; align-content: center; gap: 6px; }
.rating-summary__bars > div { display: grid; grid-template-columns: 42px 1fr 30px; align-items: center; gap: 5px; color: var(--color-text-secondary); font-size: 10px; }
.rating-summary__bars i { display: block; block-size: 5px; overflow: hidden; border-radius: var(--radius-full); background: var(--color-disabled-soft); }
.rating-summary__bars b { display: block; block-size: 100%; border-radius: inherit; background: var(--color-brand-primary); }
.product-reviews__photos { display: grid; grid-template-columns: repeat(4, 1fr); gap: var(--space-2); }
.product-reviews__photos img { inline-size: 100%; aspect-ratio: 1; border-radius: var(--radius-sm); object-fit: cover; }
.product-reviews__digest { padding: var(--space-4); border-radius: var(--radius-md); color: #5b36d4; background: #f3f0ff; }
.product-reviews__digest p { margin: var(--space-2) 0 0; color: var(--color-text-primary); font-size: var(--font-size-xs); line-height: var(--line-height-body); }
.review-card { padding: var(--space-4); border: 1px solid var(--color-border-subtle); border-radius: var(--radius-md); background: var(--color-surface); }
.review-card header { display: flex; align-items: center; justify-content: space-between; gap: var(--space-2); }
.review-card header > div { display: flex; align-items: center; gap: var(--space-2); }
.review-card time { color: var(--color-text-secondary); font-size: var(--font-size-xs); }
.review-card__stars { margin-block-start: var(--space-2); }
.review-card h3 { margin: var(--space-2) 0 0; font-size: var(--font-size-sm); }
.review-card p { margin: var(--space-2) 0; color: var(--color-text-primary); font-size: var(--font-size-xs); line-height: var(--line-height-body); }
.review-card small { color: var(--color-text-secondary); }
.review-card footer { display: flex; gap: var(--space-4); margin-block-start: var(--space-3); }
.review-card footer button { display: inline-flex; min-block-size: var(--touch-target); align-items: center; gap: 5px; padding: 0; border: 0; color: var(--color-text-secondary); background: transparent; cursor: pointer; }
.review-card footer button[aria-pressed='true'] { color: var(--color-brand-primary); }
.product-reviews__all { min-block-size: 52px; border: 1px solid var(--color-border-subtle); border-radius: var(--radius-md); color: var(--color-text-primary); background: var(--color-surface); font: inherit; font-weight: var(--font-weight-semibold); cursor: pointer; }
</style>
