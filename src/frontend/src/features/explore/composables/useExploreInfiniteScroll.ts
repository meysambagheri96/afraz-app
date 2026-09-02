import { computed, onBeforeUnmount, onMounted, ref, watch, type Ref } from 'vue'
import type { ExplorePhoto } from '../explore.types'

const INITIAL_BATCH_SIZE = 18
const NEXT_BATCH_SIZE = 9

export function useExploreInfiniteScroll(source: Ref<readonly ExplorePhoto[]>) {
  const visibleCount = ref(INITIAL_BATCH_SIZE)
  const isInitialLoading = ref(true)
  const isLoadingMore = ref(false)
  const sentinel = ref<HTMLElement | null>(null)
  const visiblePhotos = computed(() => source.value.slice(0, visibleCount.value))
  const hasMore = computed(() => visibleCount.value < source.value.length)
  let observer: IntersectionObserver | undefined
  let initialTimer: number | undefined
  let loadTimer: number | undefined

  function setSentinel(element: unknown) {
    sentinel.value = element instanceof HTMLElement ? element : null
  }

  function loadMore() {
    if (isInitialLoading.value || isLoadingMore.value || !hasMore.value) return
    isLoadingMore.value = true
    loadTimer = window.setTimeout(() => {
      visibleCount.value = Math.min(visibleCount.value + NEXT_BATCH_SIZE, source.value.length)
      isLoadingMore.value = false
    }, 420)
  }

  function reset() {
    window.clearTimeout(loadTimer)
    isLoadingMore.value = false
    visibleCount.value = INITIAL_BATCH_SIZE
  }

  watch(source, reset)
  watch(sentinel, (element) => {
    observer?.disconnect()
    if (!element) return
    observer = new IntersectionObserver((entries) => {
      if (entries[0]?.isIntersecting) loadMore()
    }, { rootMargin: '64px 0px' })
    observer.observe(element)
  })

  onMounted(() => {
    initialTimer = window.setTimeout(() => { isInitialLoading.value = false }, 560)
  })

  onBeforeUnmount(() => {
    observer?.disconnect()
    window.clearTimeout(initialTimer)
    window.clearTimeout(loadTimer)
  })

  return { visiblePhotos, isInitialLoading, isLoadingMore, hasMore, setSentinel }
}
