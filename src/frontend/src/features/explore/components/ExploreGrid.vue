<script setup lang="ts">
import AppSkeleton from '../../../components/ui/AppSkeleton.vue'
import type { ExplorePhoto } from '../explore.types'

withDefaults(
  defineProps<{
    photos: readonly ExplorePhoto[]
    initialLoading?: boolean
    loadingMore?: boolean
  }>(),
  { initialLoading: false, loadingMore: false },
)

defineEmits<{ select: [photo: ExplorePhoto] }>()
</script>

<template>
  <div class="explore-grid" :aria-busy="initialLoading || loadingMore">
    <template v-if="initialLoading">
      <AppSkeleton v-for="index in 18" :key="`initial-${index}`" class="explore-grid__skeleton" />
    </template>
    <template v-else>
      <button
        v-for="photo in photos"
        :key="photo.id"
        class="explore-grid__item"
        type="button"
        :aria-label="`نمایش ${photo.alt}`"
        @click="$emit('select', photo)"
      >
        <img
          :src="photo.src"
          :alt="photo.alt"
          :style="{ objectPosition: photo.position }"
          width="320"
          height="336"
          loading="lazy"
          decoding="async"
        />
      </button>
      <AppSkeleton v-for="index in loadingMore ? 6 : 0" :key="`more-${index}`" class="explore-grid__skeleton" />
    </template>
  </div>
</template>

<style scoped>
.explore-grid {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 3px;
  margin-block-start: 9px;
  margin-inline: -1px;
}
.explore-grid__item,
.explore-grid__skeleton {
  display: block;
  inline-size: 100%;
  aspect-ratio: 1 / 1.04;
  overflow: hidden;
  border: 0;
  border-radius: 0;
  background: var(--color-surface-muted);
}
.explore-grid__item { padding: 0; cursor: pointer; }
.explore-grid__item img { display: block; inline-size: 100%; block-size: 100%; object-fit: cover; transition: transform var(--motion-fast) var(--ease-standard), opacity var(--motion-fast) var(--ease-standard); }
.explore-grid__item:active img { transform: scale(.98); opacity: .92; }
.explore-grid__item:focus-visible { position: relative; z-index: var(--z-content); outline: 3px solid var(--color-accent-blue); outline-offset: -3px; }
.explore-grid__skeleton { min-block-size: 0; background: linear-gradient(100deg, #f4f5f5 30%, #fbfbfb 50%, #f1f3f3 70%); background-size: 200% 100%; }
:deep(.explore-grid__skeleton.app-skeleton--rect) { border-radius: 0; }

@media (prefers-reduced-motion: reduce) {
  .explore-grid__item img { transition: none; }
}
</style>
