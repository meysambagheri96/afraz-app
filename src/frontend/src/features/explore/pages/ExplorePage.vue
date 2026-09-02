<script setup lang="ts">
import { computed, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import ExploreCategories from '../components/ExploreCategories.vue'
import ExploreGrid from '../components/ExploreGrid.vue'
import ExplorePhotoViewer from '../components/ExplorePhotoViewer.vue'
import ExploreToolbar from '../components/ExploreToolbar.vue'
import { exploreCategories, explorePhotos } from '../explore.data'
import { useExploreInfiniteScroll } from '../composables/useExploreInfiniteScroll'
import type { ExploreCategory, ExploreSort } from '../explore.types'

const route = useRoute()
const routeCategory = route.params.category
const selectedCategory = ref<ExploreCategory>(
  typeof routeCategory === 'string' && exploreCategories.some((item) => item.id === routeCategory)
    ? routeCategory as ExploreCategory
    : 'all',
)
const search = ref('')
const sort = ref<ExploreSort>('newest')
const selectedPhotoId = ref<string | null>(null)

const filteredPhotos = computed(() => {
  const normalizedSearch = search.value.trim().toLocaleLowerCase('fa')
  const categoryLabel = exploreCategories.find((item) => item.id === selectedCategory.value)?.label ?? ''
  const photos = explorePhotos.filter((photo) => {
    const categoryMatches = selectedCategory.value === 'all' || photo.category === selectedCategory.value
    const searchMatches = !normalizedSearch || `${photo.alt} ${categoryLabel}`.toLocaleLowerCase('fa').includes(normalizedSearch)
    return categoryMatches && searchMatches
  })

  return [...photos].sort((first, second) => {
    if (sort.value === 'popular') return second.popularity - first.popularity
    const dateOrder = first.createdAt.localeCompare(second.createdAt)
    return sort.value === 'oldest' ? dateOrder : -dateOrder
  })
})

const { visiblePhotos, isInitialLoading, isLoadingMore, hasMore, setSentinel } = useExploreInfiniteScroll(filteredPhotos)

watch(filteredPhotos, () => { selectedPhotoId.value = null })
</script>

<template>
  <div class="explore-page">
    <ExploreToolbar v-model:search="search" v-model:sort="sort" />
    <ExploreCategories v-model="selectedCategory" :items="exploreCategories" />

    <ExploreGrid
      v-if="isInitialLoading || visiblePhotos.length"
      :photos="visiblePhotos"
      :initial-loading="isInitialLoading"
      :loading-more="isLoadingMore"
      @select="selectedPhotoId = $event.id"
    />

    <section v-else class="explore-empty" aria-live="polite">
      <svg viewBox="0 0 64 64" fill="none" aria-hidden="true">
        <rect x="8" y="12" width="48" height="40" rx="8" stroke="currentColor" stroke-width="2" />
        <circle cx="24" cy="27" r="5" stroke="currentColor" stroke-width="2" />
        <path d="m14 47 12-12 8 8 5-5 11 9" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
      </svg>
      <h2 class="text-section-title">عکسی پیدا نشد</h2>
      <p class="text-label">فیلتر یا عبارت جستجو را تغییر دهید.</p>
    </section>

    <div :ref="setSentinel" class="explore-page__sentinel" :aria-hidden="!hasMore" />
    <ExplorePhotoViewer
      :photos="visiblePhotos"
      :selected-id="selectedPhotoId"
      @close="selectedPhotoId = null"
      @change="selectedPhotoId = $event"
    />
  </div>
</template>

<style scoped>
.explore-page { min-inline-size: 0; overflow-x: clip; padding-block-end: 34px; color: var(--color-text-primary); }
.explore-page__sentinel { block-size: 1px; }
.explore-empty { display: grid; min-block-size: 280px; place-items: center; align-content: center; gap: 6px; color: var(--color-text-secondary); text-align: center; }
.explore-empty svg { inline-size: 64px; block-size: 64px; margin-block-end: 8px; color: var(--color-disabled); }
.explore-empty h2 { color: var(--color-text-primary); font-size: var(--font-size-lg); }
.explore-empty p { margin: 0; }
</style>
