<script setup lang="ts">
import { computed, onBeforeUnmount, ref, watch } from 'vue'
import AppModal from '../../../components/ui/AppModal.vue'
import type { ExplorePhoto } from '../explore.types'

const props = defineProps<{
  photos: readonly ExplorePhoto[]
  selectedId: string | null
}>()
const emit = defineEmits<{ close: []; change: [id: string] }>()
const isOpen = computed({
  get: () => props.selectedId !== null,
  set: (value) => {
    if (!value) emit('close')
  },
})
const currentIndex = computed(() =>
  props.photos.findIndex((photo) => photo.id === props.selectedId),
)
const currentPhoto = computed(() => props.photos[currentIndex.value])
const pointerStartX = ref(0)
const favoritePhotoIds = ref<string[]>([])
const isFavorite = computed(() =>
  currentPhoto.value ? favoritePhotoIds.value.includes(currentPhoto.value.id) : false,
)

function go(offset: number) {
  if (!props.photos.length) return
  const nextIndex = (currentIndex.value + offset + props.photos.length) % props.photos.length
  const nextPhoto = props.photos[nextIndex]
  if (nextPhoto) emit('change', nextPhoto.id)
}

function toggleFavorite() {
  const photo = currentPhoto.value
  if (!photo) return
  favoritePhotoIds.value = isFavorite.value
    ? favoritePhotoIds.value.filter((id) => id !== photo.id)
    : [...favoritePhotoIds.value, photo.id]
}

async function sharePhoto() {
  if (!currentPhoto.value || !navigator.share) return
  await navigator.share({ title: currentPhoto.value.alt, url: window.location.href })
}

function handleKeydown(event: KeyboardEvent) {
  if (!isOpen.value) return
  if (event.key === 'ArrowLeft') go(-1)
  if (event.key === 'ArrowRight') go(1)
}

function finishSwipe(event: PointerEvent) {
  const distance = event.clientX - pointerStartX.value
  if (Math.abs(distance) < 44) return
  go(distance < 0 ? 1 : -1)
}

watch(
  isOpen,
  (open) => {
    if (open) window.addEventListener('keydown', handleKeydown)
    else window.removeEventListener('keydown', handleKeydown)
  },
  { immediate: true },
)
onBeforeUnmount(() => window.removeEventListener('keydown', handleKeydown))
</script>

<template>
  <AppModal v-model="isOpen" class="explore-viewer-modal" size="fullscreen" :show-header="false">
    <div
      v-if="currentPhoto"
      class="explore-viewer"
      :style="{ '--viewer-image': `url(${currentPhoto.src})` }"
      @pointerdown="pointerStartX = $event.clientX"
      @pointerup="finishSwipe"
    >
      <button
        type="button"
        class="explore-viewer__dismiss"
        aria-label="بستن جزئیات عکس"
        @click="emit('close')"
      />
      <div class="explore-viewer__scene" aria-hidden="true">
        <div class="explore-viewer__scene-header">
          <span class="explore-viewer__scene-avatar" />
          <span class="explore-viewer__scene-title" />
          <span class="explore-viewer__scene-subtitle" />
          <span class="explore-viewer__scene-toolbar" />
          <div class="explore-viewer__scene-categories">
            <span v-for="index in 6" :key="index" />
          </div>
        </div>
        <div class="explore-viewer__scene-grid">
          <img v-for="photo in photos.slice(0, 12)" :key="photo.id" :src="photo.src" alt="" />
        </div>
        <div class="explore-viewer__scene-nav">
          <span v-for="index in 5" :key="index" />
        </div>
      </div>
      <div class="explore-viewer__layout">
        <article class="explore-viewer__card">
          <header class="explore-viewer__header">
            <span class="explore-viewer__handle" aria-hidden="true" />
            <button
              type="button"
              class="explore-viewer__control"
              aria-label="بستن نمایشگر"
              @click="emit('close')"
            >
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <path
                  d="m6 6 12 12M18 6 6 18"
                  stroke="currentColor"
                  stroke-width="1.8"
                  stroke-linecap="round"
                />
              </svg>
            </button>
            <span class="explore-viewer__position" aria-live="polite"
              >{{ currentIndex + 1 }} / {{ photos.length }}</span
            >
            <button
              type="button"
              class="explore-viewer__favorite"
              :class="{ 'is-active': isFavorite }"
              :aria-label="isFavorite ? 'حذف از علاقه‌مندی‌ها' : 'افزودن به علاقه‌مندی‌ها'"
              :aria-pressed="isFavorite"
              @click="toggleFavorite"
            >
              <svg viewBox="0 0 24 24" aria-hidden="true">
                <path
                  d="M20.8 4.7a5.5 5.5 0 0 0-7.8 0L12 5.8l-1.1-1.1a5.5 5.5 0 0 0-7.8 7.8l1.1 1.1L12 21l7.8-7.4 1.1-1.1a5.5 5.5 0 0 0-.1-7.8Z"
                />
              </svg>
            </button>
          </header>

          <div class="explore-viewer__stage">
            <img :src="currentPhoto.src" :alt="currentPhoto.alt" draggable="false" />
            <button
              type="button"
              class="explore-viewer__arrow explore-viewer__arrow--previous"
              aria-label="عکس قبلی"
              @click="go(-1)"
            >
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <path
                  d="m15 5-7 7 7 7"
                  stroke="currentColor"
                  stroke-width="1.8"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
            </button>
            <button
              type="button"
              class="explore-viewer__arrow explore-viewer__arrow--next"
              aria-label="عکس بعدی"
              @click="go(1)"
            >
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <path
                  d="m9 5 7 7-7 7"
                  stroke="currentColor"
                  stroke-width="1.8"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
            </button>
          </div>

          <footer class="explore-viewer__footer">
            <div class="explore-viewer__author">
              <img :src="currentPhoto.src" alt="" aria-hidden="true" />
              <span>
                <strong>آتلیه افراز قم</strong>
                <small>۳ روز پیش</small>
              </span>
            </div>
            <a
              class="explore-viewer__action"
              :href="currentPhoto.src"
              download
              aria-label="دانلود عکس"
            >
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <path
                  d="M12 3v12m0 0 4-4m-4 4-4-4M5 19h14"
                  stroke="currentColor"
                  stroke-width="1.7"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
              <span>دانلود</span>
            </a>
            <button
              type="button"
              class="explore-viewer__action"
              aria-label="اشتراک‌گذاری عکس"
              @click="sharePhoto"
            >
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <circle cx="18" cy="5" r="2.25" stroke="currentColor" stroke-width="1.6" />
                <circle cx="6" cy="12" r="2.25" stroke="currentColor" stroke-width="1.6" />
                <circle cx="18" cy="19" r="2.25" stroke="currentColor" stroke-width="1.6" />
                <path
                  d="m8 11 7.8-4.6M8 13l7.8 4.6"
                  stroke="currentColor"
                  stroke-width="1.6"
                  stroke-linecap="round"
                />
              </svg>
              <span>اشتراک‌گذاری</span>
            </button>
          </footer>
        </article>

        <div class="explore-viewer__dots" aria-hidden="true">
          <span
            v-for="(_, index) in Math.min(photos.length, 5)"
            :key="index"
            :class="{ 'is-active': index === currentIndex % Math.min(photos.length, 5) }"
          />
        </div>
      </div>
    </div>
  </AppModal>
</template>

<style scoped>
.explore-viewer {
  position: relative;
  display: grid;
  block-size: 100%;
  overflow: hidden;
  padding: max(20px, var(--safe-area-top)) 10px max(20px, var(--safe-area-bottom));
  place-items: center;
  color: var(--color-surface);
  background: #dededb;
  isolation: isolate;
  touch-action: pan-y;
}

.explore-viewer::after {
  position: absolute;
  z-index: -1;
  background: rgb(15 22 23 / 42%);
  content: '';
  inset: 0;
}

.explore-viewer__dismiss {
  position: absolute;
  z-index: 0;
  border: 0;
  background: transparent;
  cursor: default;
  inset: 0;
}

.explore-viewer__scene {
  position: absolute;
  z-index: -2;
  inset: -24px;
  overflow: hidden;
  background: #f2f0ed;
  filter: blur(9px) saturate(62%);
  transform: scale(1.055);
  pointer-events: none;
}

.explore-viewer__scene-header {
  position: relative;
  block-size: 245px;
  background: #f4f1ed;
}

.explore-viewer__scene-avatar,
.explore-viewer__scene-title,
.explore-viewer__scene-subtitle,
.explore-viewer__scene-toolbar,
.explore-viewer__scene-categories span {
  position: absolute;
  display: block;
  background: #afb6b4;
}

.explore-viewer__scene-avatar {
  inset-block-start: 28px;
  inset-inline-end: 24px;
  inline-size: 58px;
  block-size: 58px;
  border-radius: 50%;
  background: #29726f;
}

.explore-viewer__scene-title {
  inset-block-start: 35px;
  inset-inline-start: 50%;
  inline-size: 142px;
  block-size: 16px;
  border-radius: 999px;
  transform: translateX(-50%);
}

.explore-viewer__scene-subtitle {
  inset-block-start: 62px;
  inset-inline-start: 50%;
  inline-size: 92px;
  block-size: 10px;
  border-radius: 999px;
  transform: translateX(-50%);
}

.explore-viewer__scene-toolbar {
  inset-block-start: 112px;
  inset-inline: 24px;
  block-size: 42px;
  border-radius: 14px;
}

.explore-viewer__scene-categories {
  position: absolute;
  inset-block-start: 174px;
  inset-inline: 18px;
  display: flex;
  justify-content: space-between;
}

.explore-viewer__scene-categories span {
  position: static;
  inline-size: 45px;
  block-size: 45px;
  border-radius: 50%;
}

.explore-viewer__scene-grid {
  display: grid;
  min-block-size: calc(100% - 245px);
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(4, minmax(0, 1fr));
  gap: 3px;
}

.explore-viewer__scene-grid img {
  inline-size: 100%;
  block-size: 100%;
  object-fit: cover;
}

.explore-viewer__scene-nav {
  position: absolute;
  inset-block-end: 22px;
  inset-inline: 22px;
  display: flex;
  block-size: 74px;
  align-items: center;
  justify-content: space-around;
  border-radius: 30px;
  background: rgb(239 239 236 / 82%);
}

.explore-viewer__scene-nav span {
  inline-size: 28px;
  block-size: 28px;
  border-radius: 50%;
  background: #7d8785;
}

.explore-viewer__layout {
  position: relative;
  z-index: 1;
  display: grid;
  inline-size: min(100%, 410px);
  gap: 14px;
  animation: explore-viewer-enter var(--motion-page) var(--ease-emphasized) both;
}

.explore-viewer__card {
  display: grid;
  grid-template-rows: 98px minmax(0, 1fr) 76px;
  block-size: clamp(544px, 67.4dvh, 628px);
  overflow: hidden;
  border: 1px solid rgb(255 255 255 / 20%);
  border-radius: 24px;
  background: rgb(51 62 60 / 18%);
  box-shadow: 0 24px 60px rgb(0 17 18 / 36%);
}

.explore-viewer__header {
  position: relative;
  display: grid;
  grid-template-columns: 44px 1fr 44px;
  direction: ltr;
  align-items: center;
  padding: 8px 6px 4px;
  background: rgb(76 83 80 / 12%);
  backdrop-filter: blur(22px) saturate(72%) brightness(94%);
}

.explore-viewer__handle {
  position: absolute;
  inset-block-start: 10px;
  inset-inline-start: 50%;
  inline-size: 40px;
  block-size: 4px;
  border-radius: 999px;
  background: rgb(255 255 255 / 62%);
  transform: translateX(-50%);
}

.explore-viewer__control,
.explore-viewer__favorite,
.explore-viewer__arrow,
.explore-viewer__action {
  border: 0;
  color: inherit;
  cursor: pointer;
}

.explore-viewer__control {
  position: relative;
  display: grid;
  inline-size: 44px;
  block-size: 44px;
  place-items: center;
  background: transparent;
}

.explore-viewer__control::before {
  position: absolute;
  inline-size: 34px;
  block-size: 34px;
  border-radius: 50%;
  background: rgb(8 28 29 / 68%);
  backdrop-filter: blur(10px);
  content: '';
}

.explore-viewer__control svg {
  position: relative;
  inline-size: 22px;
  block-size: 22px;
}

.explore-viewer__position {
  direction: ltr;
  text-align: center;
  font-size: 15px;
  font-weight: 500;
}

.explore-viewer__favorite {
  display: grid;
  inline-size: 44px;
  block-size: 44px;
  place-items: center;
  background: transparent;
}

.explore-viewer__favorite svg {
  inline-size: 24px;
  block-size: 24px;
  fill: transparent;
  stroke: currentcolor;
  stroke-linecap: round;
  stroke-linejoin: round;
  stroke-width: 1.7;
  transition:
    fill var(--motion-fast) var(--ease-standard),
    color var(--motion-fast) var(--ease-standard);
}

.explore-viewer__favorite.is-active {
  color: #ffe2e3;
}
.explore-viewer__favorite.is-active svg {
  fill: currentcolor;
}

.explore-viewer__stage {
  position: relative;
  min-block-size: 0;
  overflow: hidden;
  border-radius: 0;
  background: rgb(11 25 25 / 42%);
}

.explore-viewer__stage img {
  display: block;
  inline-size: 100%;
  block-size: 100%;
  object-fit: cover;
  user-select: none;
}

.explore-viewer__arrow {
  position: absolute;
  inset-block-start: 50%;
  display: grid;
  inline-size: 44px;
  block-size: 44px;
  place-items: center;
  border: 0;
  background: transparent;
  color: #1d302f;
  transform: translateY(-50%);
}

.explore-viewer__arrow::before {
  position: absolute;
  inline-size: 36px;
  block-size: 36px;
  border: 1px solid rgb(255 255 255 / 42%);
  border-radius: 50%;
  background: rgb(255 255 255 / 78%);
  box-shadow: 0 6px 20px rgb(0 18 19 / 20%);
  backdrop-filter: blur(10px);
  content: '';
}

.explore-viewer__arrow svg {
  position: relative;
  inline-size: 20px;
  block-size: 20px;
}
.explore-viewer__arrow--previous {
  inset-inline-start: 12px;
}
.explore-viewer__arrow--next {
  inset-inline-end: 12px;
}

.explore-viewer__footer {
  position: relative;
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  direction: ltr;
  align-items: center;
  overflow: hidden;
  padding-inline: 16px;
  background: rgb(30 38 36 / 28%);
  isolation: isolate;
}

.explore-viewer__footer::before,
.explore-viewer__footer::after {
  position: absolute;
  content: '';
  inset: -20px;
}

.explore-viewer__footer::before {
  z-index: -2;
  background-image: var(--viewer-image);
  background-position: center 84%;
  background-size: 112% auto;
  filter: blur(18px) saturate(82%);
  transform: scale(1.08);
}

.explore-viewer__footer::after {
  z-index: -1;
  background: rgb(18 27 25 / 34%);
}

.explore-viewer__author {
  display: flex;
  min-inline-size: 0;
  direction: ltr;
  align-items: center;
  justify-self: start;
  gap: 8px;
}

.explore-viewer__author > img {
  inline-size: 32px;
  block-size: 32px;
  flex: 0 0 auto;
  border: 1px solid rgb(255 255 255 / 45%);
  border-radius: 50%;
  object-fit: cover;
}

.explore-viewer__author > span {
  display: grid;
  min-inline-size: 0;
  direction: rtl;
}
.explore-viewer__author strong {
  overflow: hidden;
  font-size: 12px;
  font-weight: 600;
  text-overflow: ellipsis;
  white-space: nowrap;
}
.explore-viewer__author small {
  color: rgb(255 255 255 / 70%);
  font-size: 10px;
}

.explore-viewer__action {
  display: grid;
  min-inline-size: 58px;
  min-block-size: 48px;
  direction: rtl;
  place-content: center;
  justify-items: center;
  gap: 2px;
  text-decoration: none;
  background: transparent;
  font: inherit;
  font-size: 10px;
}

.explore-viewer__action svg {
  inline-size: 21px;
  block-size: 21px;
}

.explore-viewer__dots {
  display: flex;
  direction: ltr;
  justify-content: center;
  gap: 8px;
}

.explore-viewer__dots span {
  inline-size: 6px;
  block-size: 6px;
  border-radius: 50%;
  background: rgb(255 255 255 / 36%);
}

.explore-viewer__dots .is-active {
  inline-size: 8px;
  block-size: 8px;
  margin-block-start: -1px;
  background: var(--color-surface);
}

.explore-viewer-modal :deep(.app-modal__panel) {
  overflow: hidden;
  color: var(--color-surface);
  background: transparent;
  box-shadow: none;
}

:global(.app-modal:has(.explore-viewer)) {
  padding: 0;
  background: transparent;
}

@keyframes explore-viewer-enter {
  from {
    opacity: 0;
    transform: translateY(28px) scale(0.96);
  }

  to {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}

@media (max-height: 760px) {
  .explore-viewer__card {
    grid-template-rows: 82px minmax(0, 1fr) 68px;
  }
  .explore-viewer__header {
    padding-block-start: 16px;
  }
}

@media (prefers-reduced-motion: reduce) {
  .explore-viewer__favorite svg {
    transition: none;
  }

  .explore-viewer__layout {
    animation: none;
  }
}
</style>
