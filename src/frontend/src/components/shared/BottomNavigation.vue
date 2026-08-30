<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import NavigationIcon from './NavigationIcon.vue'
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
          <NavigationIcon :name="item.id" :active="activeItem === item.id" />
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
  padding-block-start: var(--space-2);
  padding-block-end: max(var(--space-2), var(--safe-area-bottom));
  padding-inline-start: max(var(--space-3), var(--safe-area-inline-start));
  padding-inline-end: max(var(--space-3), var(--safe-area-inline-end));
  pointer-events: none;
}

.bottom-navigation {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: var(--bottom-nav-height);
  margin-inline: auto;
  padding: var(--space-2);
  border-radius: var(--bottom-nav-radius);
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
  gap: var(--space-1);
  border-radius: var(--radius-full);
  color: var(--color-icon);
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
  color: var(--color-brand-primary);
  background: color-mix(in srgb, var(--color-brand-soft) 72%, transparent);
}

.bottom-navigation__icon {
  display: grid;
  min-block-size: var(--icon-size-md);
  place-items: center;
}

.bottom-navigation__label {
  max-inline-size: 100%;
  overflow: hidden;
  font-size: var(--font-size-xs);
  font-weight: var(--font-weight-medium);
  line-height: var(--line-height-caption);
  text-overflow: ellipsis;
  white-space: nowrap;
}

.bottom-navigation__item--active .bottom-navigation__label {
  font-weight: var(--font-weight-bold);
}

@supports not ((backdrop-filter: blur(1rem)) or (-webkit-backdrop-filter: blur(1rem))) {
  .bottom-navigation {
    background: color-mix(in srgb, var(--color-surface) 96%, var(--color-brand-soft));
  }
}

@media (prefers-reduced-motion: reduce) {
  .bottom-navigation__item { transition: none; }
}
</style>
