<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import AppIcon from '../ui/AppIcon.vue'
import { primaryNavigationItems, type NavigationItemId } from './navigation'

const route = useRoute()
const activeItem = computed<NavigationItemId>(() => {
  const navigationId = route.matched.at(-1)?.meta.navigation
  return primaryNavigationItems.find((item) => item.id === navigationId)?.id ?? 'home'
})

const activeIndex = computed(() => {
  const index = primaryNavigationItems.findIndex((item) => item.id === activeItem.value)
  return index < 0 ? 0 : index
})
</script>

<template>
  <div class="bottom-navigation-frame">
    <nav
      class="bottom-navigation"
      :style="{ '--active-index': activeIndex }"
      aria-label="ناوبری اصلی"
    >
      <span class="bottom-navigation__lens" aria-hidden="true" />
      <div class="bottom-navigation__items">
        <RouterLink
          v-for="item in primaryNavigationItems"
          :key="item.id"
          :to="{ name: item.routeName }"
          class="bottom-navigation__item"
          :class="{ 'bottom-navigation__item--active': activeItem === item.id }"
          :aria-current="activeItem === item.id ? 'page' : undefined"
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
  padding-block-end: calc(14px + var(--safe-area-bottom));
  padding-inline-start: max(21px, var(--safe-area-inline-start));
  padding-inline-end: max(21px, var(--safe-area-inline-end));
  pointer-events: none;
}

.bottom-navigation {
  --active-index: 0;

  position: relative;
  inline-size: min(100%, var(--mobile-canvas-max-width));
  block-size: 74px;
  margin-inline: auto;
  overflow: hidden;
  padding: 4px;
  border: 1px solid rgb(255 255 255 / 72%);
  border-radius: 38px;
  color: #101820;
  background:
    linear-gradient(180deg, rgb(255 255 255 / 42%) 0%, rgb(255 255 255 / 19%) 48%, rgb(255 255 255 / 25%) 100%);
  box-shadow:
    0 12px 30px rgb(15 23 42 / 10%),
    0 2px 8px rgb(15 23 42 / 4%),
    inset 0 1px 0 rgb(255 255 255 / 72%);
  backdrop-filter: blur(22px) saturate(165%) contrast(104%);
  -webkit-backdrop-filter: blur(22px) saturate(165%) contrast(104%);
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
  border: 1px solid rgb(255 255 255 / 24%);
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 86%),
    inset 0 -1px 0 rgb(255 255 255 / 12%);
}

.bottom-navigation::after {
  inset: 0;
  z-index: 1;
  background:
    linear-gradient(180deg, rgb(255 255 255 / 36%) 0%, rgb(255 255 255 / 10%) 34%, transparent 62%),
    radial-gradient(ellipse at 50% 115%, rgb(255 255 255 / 16%), transparent 58%);
  mix-blend-mode: screen;
}

.bottom-navigation__lens {
  position: absolute;
  inset-block: 5px;
  inset-inline-start: 4px;
  z-index: 2;
  inline-size: calc((100% - 8px) / 5);
  border: 1px solid rgb(255 255 255 / 68%);
  border-radius: 31px;
  background:
    radial-gradient(circle at 50% -10%, rgb(255 255 255 / 78%), transparent 57%),
    linear-gradient(180deg, rgb(255 255 255 / 56%), rgb(236 239 240 / 29%));
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 94%),
    inset 0 -1px 0 rgb(255 255 255 / 18%),
    inset 1px 0 0 rgb(255 255 255 / 28%),
    0 5px 14px rgb(15 23 42 / 7%);
  backdrop-filter: blur(11px) saturate(180%) contrast(103%);
  -webkit-backdrop-filter: blur(11px) saturate(180%) contrast(103%);
  transform: translateX(calc(var(--active-index) * -100%));
  transform-origin: center;
  transition:
    transform 320ms cubic-bezier(.2, .8, .2, 1),
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
  background: radial-gradient(circle at 50% 0%, rgb(255 255 255 / 68%), rgb(255 255 255 / 16%) 43%, transparent 72%);
}

.bottom-navigation__lens::after {
  inset: 2px;
  border: 1px solid rgb(255 255 255 / 20%);
  box-shadow: inset 0 -7px 13px rgb(126 137 145 / 7%);
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
  gap: 2px;
  border-radius: var(--radius-full);
  color: #111820;
  text-decoration: none;
  transition: color 180ms ease, transform 100ms ease;
}

.bottom-navigation__item:active {
  transform: scale(.96);
}

.bottom-navigation__item--active {
  color: #142632;
  background: transparent;
}

.bottom-navigation__item--active::after { position: absolute; inset-block-end: 2px; inline-size: 5px; block-size: 5px; border-radius: 50%; background: #ff3048; content: ''; }

.bottom-navigation__icon {
  display: grid;
  min-block-size: var(--icon-size-md);
  place-items: center;
}

.bottom-navigation__label {
  max-inline-size: 100%;
  overflow: hidden;
  font-size: 11px;
  font-weight: var(--font-weight-medium);
  line-height: 1.2;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.bottom-navigation__item--active .bottom-navigation__label {
  font-weight: 700;
}

.bottom-navigation__icon :deep(svg) { inline-size: 26px; block-size: 26px; }

.bottom-navigation:has(.bottom-navigation__item:active) .bottom-navigation__lens {
  filter: brightness(1.025);
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / 100%),
    inset 0 -1px 0 rgb(255 255 255 / 22%),
    0 6px 16px rgb(15 23 42 / 8%);
}

@supports not ((backdrop-filter: blur(1rem)) or (-webkit-backdrop-filter: blur(1rem))) {
  .bottom-navigation {
    background: rgb(247 248 248 / 92%);
  }
}

@media (prefers-reduced-motion: reduce) {
  .bottom-navigation__item,
  .bottom-navigation__lens { transition: none; }
}
</style>
