<script setup lang="ts">
import { nextTick, onBeforeUnmount, onMounted, ref } from 'vue'
import AppSkeleton from '../../../components/ui/AppSkeleton.vue'
import ProductDetailsHeader from '../components/ProductDetailsHeader.vue'
import ProductGallery from '../components/ProductGallery.vue'
import ProductPurchaseBar from '../components/ProductPurchaseBar.vue'
import ProductReviewOverview from '../components/ProductReviewOverview.vue'
import ProductReviews from '../components/ProductReviews.vue'
import ProductSpecifications from '../components/ProductSpecifications.vue'
import ProductSummary from '../components/ProductSummary.vue'
import ProductTabs from '../components/ProductTabs.vue'
import { mockProductDetails } from '../data/product-details.mock'
import type { ProductTabId } from '../store.types'

const activeTab = ref<ProductTabId>('specifications')
const favorite = ref(false)
const added = ref(false)
const isLoading = ref(true)
const announcement = ref('')
let timer: ReturnType<typeof window.setTimeout> | undefined
let sectionObserver: IntersectionObserver | undefined
const specificationsSection = ref<HTMLElement | null>(null)
const overviewSection = ref<HTMLElement | null>(null)
const reviewsSection = ref<HTMLElement | null>(null)

function share() { announcement.value = 'اشتراک‌گذاری محصول در نسخه نمایشی انتخاب شد.' }
function toggleCart() { added.value = !added.value; announcement.value = added.value ? 'محصول به سبد خرید اضافه شد.' : 'محصول از سبد خرید حذف شد.' }

function observeSections() {
  const sections = [specificationsSection.value, overviewSection.value, reviewsSection.value].filter(Boolean) as HTMLElement[]
  sectionObserver = new IntersectionObserver((entries) => {
    const visible = entries
      .filter((entry) => entry.isIntersecting)
      .sort((first, second) => Math.abs(first.boundingClientRect.top - 60) - Math.abs(second.boundingClientRect.top - 60))
    const tab = visible[0]?.target.getAttribute('data-tab') as ProductTabId | null
    if (tab) activeTab.value = tab
  }, { rootMargin: '-56px 0px -62% 0px', threshold: [0, 0.1, 0.25] })
  sections.forEach((section) => sectionObserver?.observe(section))
}

function selectTab(tab: ProductTabId) {
  activeTab.value = tab
  const sections = { specifications: specificationsSection.value, overview: overviewSection.value, reviews: reviewsSection.value }
  sections[tab]?.scrollIntoView({ behavior: 'smooth', block: 'start' })
}

onMounted(() => {
  timer = window.setTimeout(async () => {
    isLoading.value = false
    await nextTick()
    observeSections()
  }, 350)
})
onBeforeUnmount(() => { if (timer) window.clearTimeout(timer); sectionObserver?.disconnect() })
</script>

<template>
  <div class="product-details-page">
    <ProductDetailsHeader
      :cart-count="added ? 1 : 0"
      :category="mockProductDetails.categoryLabel"
      :category-id="mockProductDetails.product.category"
      :title="mockProductDetails.product.title"
      @share="share"
    />
    <template v-if="isLoading"><AppSkeleton height="19rem" /><AppSkeleton shape="text" width="55%" /><AppSkeleton shape="text" /></template>
    <template v-else>
      <ProductGallery :images="mockProductDetails.gallery" />
      <ProductSummary :category="mockProductDetails.categoryLabel" :title="mockProductDetails.product.title" :tagline="mockProductDetails.tagline" :favorite="favorite" @favorite="favorite = !favorite" />
      <ProductTabs v-model="activeTab" @select="selectTab" />
      <div ref="specificationsSection" class="product-details-page__section" data-tab="specifications">
        <ProductSpecifications :introduction="mockProductDetails.introduction" :specifications="mockProductDetails.specifications" />
      </div>
      <div ref="overviewSection" class="product-details-page__section" data-tab="overview">
        <ProductReviewOverview :overview="mockProductDetails.overview" :benefits="mockProductDetails.benefits" />
      </div>
      <div ref="reviewsSection" class="product-details-page__section" data-tab="reviews">
        <ProductReviews :rating="mockProductDetails.rating" :count="mockProductDetails.reviewCount" :distribution="mockProductDetails.ratingDistribution" :reviews="mockProductDetails.reviews" :images="mockProductDetails.gallery.map((image) => image.src)" />
      </div>
    </template>
    <ProductPurchaseBar :price="mockProductDetails.price" :added="added" @add="toggleCart" />
    <p class="visually-hidden" role="status" aria-live="polite">{{ announcement }}</p>
  </div>
</template>

<style scoped>
.product-details-page { min-inline-size: 0; padding-block-end: calc(92px + var(--safe-area-bottom)); overflow-x: clip; }
.product-details-page > :deep(.app-skeleton) { margin-block-end: var(--space-4); }
.product-details-page__section { scroll-margin-block-start: 56px; }
</style>
