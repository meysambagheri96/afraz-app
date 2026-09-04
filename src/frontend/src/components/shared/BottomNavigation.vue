<script setup lang="ts">
import { computed, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthModal } from '../../features/auth/composables/useAuthModal'
import { useAuthStore } from '../../features/auth/stores/auth.store'
import AppIcon from '../ui/AppIcon.vue'
import { primaryNavigationItems, type NavigationItemId } from './navigation'

const route = useRoute()
const authModal = useAuthModal()
const authStore = useAuthStore()
const activeItem = computed<NavigationItemId>(() => {
  const navigationId = route.matched.at(-1)?.meta.navigation
  return primaryNavigationItems.find((item) => item.id === navigationId)?.id ?? 'home'
})

const activeIndex = computed(() => {
  const index = primaryNavigationItems.findIndex((item) => item.id === activeItem.value)
  return index < 0 ? 0 : index
})

const previewIndex = ref<number | null>(null)
const lensIndex = computed(() => previewIndex.value ?? activeIndex.value)

watch(activeIndex, () => {
  previewIndex.value = null
})

function previewSelection(index: number) {
  previewIndex.value = index
}

function cancelPreview() {
  previewIndex.value = null
}

function finishPreview() {
  window.setTimeout(cancelPreview, 120)
}

function handleNavigation(event: MouseEvent, itemId: NavigationItemId) {
  if (itemId !== 'profile' || authStore.isAuthenticated) return
  event.preventDefault()
  authModal.open({ name: 'profile' })
}
</script>

<template>
  <div class="bottom-navigation-frame">
    <nav
      class="bottom-navigation"
      :style="{ '--active-index': lensIndex }"
      aria-label="ناوبری اصلی"
      @pointerleave="cancelPreview"
      @pointercancel="cancelPreview"
    >
      <span class="bottom-navigation__lens" aria-hidden="true" />
      <div class="bottom-navigation__items">
        <RouterLink
          v-for="(item, index) in primaryNavigationItems"
          :key="item.id"
          :to="
            item.id === 'profile' && !authStore.isAuthenticated
              ? route.fullPath
              : { name: item.routeName }
          "
          class="bottom-navigation__item"
          :class="{ 'bottom-navigation__item--active': activeItem === item.id }"
          :aria-current="activeItem === item.id ? 'page' : undefined"
          draggable="false"
          @pointerdown="previewSelection(index)"
          @pointerup="finishPreview"
          @pointerenter="previewIndex === null ? undefined : previewSelection(index)"
          @click="handleNavigation($event, item.id)"
          @dragstart.prevent
        >
          <span class="bottom-navigation__icon" aria-hidden="true">
            <AppIcon :name="item.id" :active="activeItem === item.id" />
          </span>
          <span class="bottom-navigation__label">{{ item.label }}</span>
        </RouterLink>
      </div>
    </nav>
  </div>
</template>

<style scoped>
.bottom-navigation-frame {
  position: fixed;
  inset-inline: 0;
  inset-block-end: 0;
  z-index: var(--z-nav);
  padding-block-start: 8px;
  padding-block-end: max(6px, calc(var(--safe-area-bottom) - 12px));
  padding-inline-start: max(21px, var(--safe-area-inline-start));
  padding-inline-end: max(21px, var(--safe-area-inline-end));
  pointer-events: none;
}

.bottom-navigation {
  --active-index: 0;

  position: relative;
  inline-size: min(100%, var(--mobile-canvas-max-width));
  block-size: 62px;
  margin-inline: auto;
  overflow: hidden;
  padding: 2px;
  border: 1px solid rgb(255 255 255 / 52%);
  border-radius: 31px;
  color: #101820;
  background:
    radial-gradient(circle at 12% -25%, rgb(255 255 255 / 34%), transparent 38%),
    linear-gradient(
      180deg,
      rgb(255 255 255 / 22%) 0%,
      rgb(238 241 243 / 6%) 48%,
      rgb(255 255 255 / 13%) 100%
    );
  box-shadow:
    0 14px 34px rgb(15 23 42 / 8%),
    0 2px 8px rgb(15 23 42 / 3%),
    inset 0 1px 0 rgb(255 255 255 / 72%),
    inset 0 -1px 0 rgb(255 255 255 / 18%);
  backdrop-filter: blur(18px) saturate(190%) contrast(108%);
  -webkit-backdrop-filter: blur(18px) saturate(190%) contrast(108%);
  pointer-events: auto;
  isolation: isolate;
}

.bottom-navigation::before,
.bottom-navigation::after {
  position: absolute;
  border-radius: inherit;
  content: '';
  pointer-events: none;
}

.bottom-navigation::before {
  inset: 1px;
  z-index: 3;
  border: 1px solid rgb(255 255 255 / 20%);
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 78%),
    inset 0 -1px 0 rgb(255 255 255 / 16%);
}

.bottom-navigation::after {
  inset: 0;
  z-index: 1;
  background:
    linear-gradient(
      115deg,
      rgb(255 255 255 / 28%) 0%,
      transparent 28%,
      rgb(255 255 255 / 9%) 52%,
      transparent 72%,
      rgb(255 255 255 / 18%) 100%
    ),
    linear-gradient(180deg, rgb(255 255 255 / 18%) 0%, rgb(255 255 255 / 4%) 34%, transparent 62%),
    radial-gradient(ellipse at 50% 115%, rgb(255 255 255 / 12%), transparent 58%);
  mix-blend-mode: screen;
}

.bottom-navigation__lens {
  position: absolute;
  inset-block: 3px;
  inset-inline-start: 2px;
  z-index: 2;
  inline-size: calc((100% - 4px) / 5);
  border: 1px solid rgb(255 255 255 / 56%);
  border-radius: 28px;
  background:
    radial-gradient(circle at 50% -12%, rgb(255 255 255 / 54%), transparent 58%),
    linear-gradient(180deg, rgb(255 255 255 / 28%), rgb(190 197 202 / 10%));
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 82%),
    inset 0 -1px 0 rgb(255 255 255 / 14%),
    inset 1px 0 0 rgb(255 255 255 / 22%),
    0 5px 14px rgb(15 23 42 / 5%);
  backdrop-filter: blur(14px) saturate(205%) contrast(106%);
  -webkit-backdrop-filter: blur(14px) saturate(205%) contrast(106%);
  transform: translateX(calc(var(--active-index) * -100%));
  transform-origin: center;
  transition:
    transform 320ms cubic-bezier(0.2, 0.8, 0.2, 1),
    background 220ms ease,
    box-shadow 220ms ease;
  pointer-events: none;
}

.bottom-navigation__lens::before,
.bottom-navigation__lens::after {
  position: absolute;
  border-radius: inherit;
  content: '';
  pointer-events: none;
}

.bottom-navigation__lens::before {
  inset: 0;
  background: radial-gradient(
    circle at 50% 0%,
    rgb(255 255 255 / 48%),
    rgb(255 255 255 / 8%) 43%,
    transparent 72%
  );
}

.bottom-navigation__lens::after {
  inset: 2px;
  border: 1px solid rgb(255 255 255 / 14%);
  box-shadow: inset 0 -7px 13px rgb(126 137 145 / 5%);
}

.bottom-navigation__items {
  position: relative;
  z-index: 4;
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  block-size: 100%;
}

.bottom-navigation__item {
  position: relative;
  display: flex;
  min-inline-size: 0;
  min-block-size: var(--touch-target);
  align-items: center;
  justify-content: center;
  flex-direction: column;
  gap: 1px;
  border-radius: var(--radius-full);
  color: #111820;
  text-decoration: none;
  transition:
    color 360ms ease,
    transform 200ms ease;
}

.bottom-navigation__item:active {
  transform: scale(0.96);
}

.bottom-navigation__item--active {
  color: #142632;
  background: transparent;
}

.bottom-navigation__item--active::after {
  position: absolute;
  inset-block-end: 0;
  inline-size: 4px;
  block-size: 4px;
  border-radius: 50%;
  background: #ff3048;
  content: '';
}

.bottom-navigation__icon {
  display: grid;
  min-block-size: var(--icon-size-md);
  place-items: center;
}

.bottom-navigation__label {
  max-inline-size: 100%;
  overflow: hidden;
  font-size: 10px;
  font-weight: var(--font-weight-medium);
  line-height: 1.2;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.bottom-navigation__item--active .bottom-navigation__label {
  font-weight: 600;
}

.bottom-navigation__icon :deep(svg) {
  inline-size: 24px;
  block-size: 24px;
}

.bottom-navigation:has(.bottom-navigation__item:active) .bottom-navigation__lens {
  filter: brightness(1.025);
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 88%),
    inset 0 -1px 0 rgb(255 255 255 / 16%),
    0 6px 16px rgb(15 23 42 / 6%);
}

@supports not ((backdrop-filter: blur(1rem)) or (-webkit-backdrop-filter: blur(1rem))) {
  .bottom-navigation {
    background: rgb(247 248 248 / 72%);
  }
}

@media (prefers-reduced-motion: reduce) {
  .bottom-navigation__item,
  .bottom-navigation__lens {
    transition: none;
  }
}
</style>
