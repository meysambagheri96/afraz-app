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
</script>

<template>
  <div class="bottom-navigation-frame">
    <nav class="bottom-navigation liquid-glass" aria-label="ناوبری اصلی">
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
    </nav>
  </div>
</template>

<style scoped>
.bottom-navigation-frame {
  position: fixed;
  inset-inline: 0;
  inset-block-end: 0;
  z-index: var(--z-nav);
  padding-block-start: 6px;
  padding-block-end: max(12px, var(--safe-area-bottom));
  padding-inline-start: max(26px, var(--safe-area-inline-start));
  padding-inline-end: max(26px, var(--safe-area-inline-end));
  pointer-events: none;
}

.bottom-navigation {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: 72px;
  margin-inline: auto;
  padding: 7px 8px;
  border-radius: 40px;
  color: #101820;
  background: rgb(255 255 255 / 84%);
  box-shadow: 0 10px 35px rgb(30 35 40 / 8%);
  backdrop-filter: blur(26px) saturate(160%);
  -webkit-backdrop-filter: blur(26px) saturate(160%);
  pointer-events: auto;
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
    color var(--motion-fast) var(--ease-standard),
    background-color var(--motion-fast) var(--ease-standard),
    transform var(--motion-fast) var(--ease-standard);
}

.bottom-navigation__item:active {
  transform: scale(0.97);
}

.bottom-navigation__item--active {
  color: #142632;
  background: transparent;
}

.bottom-navigation__item--active::after { position: absolute; inset-block-end: 0; inline-size: 5px; block-size: 5px; border-radius: 50%; background: #ff3347; content: ''; }

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
  line-height: var(--line-height-caption);
  text-overflow: ellipsis;
  white-space: nowrap;
}

.bottom-navigation__item--active .bottom-navigation__label {
  font-weight: 700;
}

.bottom-navigation__icon :deep(svg) { inline-size: 25px; block-size: 25px; }

@supports not ((backdrop-filter: blur(1rem)) or (-webkit-backdrop-filter: blur(1rem))) {
  .bottom-navigation {
    background: color-mix(in srgb, var(--color-surface) 96%, var(--color-brand-soft));
  }
}

@media (prefers-reduced-motion: reduce) {
  .bottom-navigation__item { transition: none; }
}
</style>
