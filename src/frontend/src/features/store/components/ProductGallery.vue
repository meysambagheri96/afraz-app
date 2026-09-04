<script setup lang="ts">
import { computed, ref } from 'vue'
import type { ProductGalleryImage } from '../store.types'

const props = defineProps<{ images: ProductGalleryImage[] }>()
const activeIndex = ref(0)
const pointerStart = ref(0)
const currentImage = computed(() => props.images[activeIndex.value]!)

function show(index: number) { activeIndex.value = (index + props.images.length) % props.images.length }
function finishSwipe(event: PointerEvent) {
  const delta = event.clientX - pointerStart.value
  if (Math.abs(delta) < 38) return
  show(activeIndex.value + (delta < 0 ? 1 : -1))
}
</script>

<template>
  <section class="product-gallery" aria-label="گالری تصاویر محصول">
    <div class="product-gallery__viewport" @pointerdown="pointerStart = $event.clientX" @pointerup="finishSwipe">
      <Transition name="gallery-fade" mode="out-in">
        <img :key="currentImage.src" :src="currentImage.src" :alt="currentImage.alt" width="900" height="675" draggable="false" />
      </Transition>
      <span class="product-gallery__counter"><bdi dir="ltr">{{ activeIndex + 1 }}/{{ images.length }}</bdi></span>
    </div>
    <div class="product-gallery__dots" aria-label="انتخاب تصویر">
      <button v-for="(_, index) in images" :key="index" type="button" :class="{ active: index === activeIndex }" :aria-label="`تصویر ${index + 1}`" :aria-current="index === activeIndex ? 'true' : undefined" @click="show(index)" />
    </div>
  </section>
</template>

<style scoped>
.product-gallery__viewport { position: relative; aspect-ratio: 4 / 3; overflow: hidden; border-radius: var(--radius-lg); background: var(--color-surface-muted); touch-action: pan-y; }
.product-gallery__viewport img { display: block; inline-size: 100%; block-size: 100%; object-fit: cover; user-select: none; }
.product-gallery__counter { position: absolute; inset-inline-start: var(--space-3); inset-block-end: var(--space-3); padding: 5px 10px; border-radius: var(--radius-full); background: rgb(255 255 255 / 82%); font-size: var(--font-size-xs); backdrop-filter: blur(8px); }
.product-gallery__dots { display: flex; min-block-size: 34px; align-items: center; justify-content: center; gap: var(--space-2); }
.product-gallery__dots button { inline-size: 8px; block-size: 8px; padding: 0; border: 0; border-radius: 50%; background: var(--color-disabled-soft); cursor: pointer; }
.product-gallery__dots button.active { background: var(--color-brand-primary); transform: scale(1.25); }
.gallery-fade-enter-active, .gallery-fade-leave-active { transition: opacity var(--motion-instant) var(--ease-standard); }
.gallery-fade-enter-from, .gallery-fade-leave-to { opacity: 0; }
@media (prefers-reduced-motion: reduce) { .gallery-fade-enter-active, .gallery-fade-leave-active { transition: none; } }
</style>
