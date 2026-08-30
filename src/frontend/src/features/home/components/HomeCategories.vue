<script setup lang="ts">
import SectionHeader from '../../../components/shared/SectionHeader.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { PhotographyCategory } from '../home.types'

defineProps<{ categories: readonly PhotographyCategory[] }>()
</script>

<template>
  <section aria-labelledby="home-categories-title">
    <SectionHeader id="home-categories-title" title="دسته‌بندی‌ها" />
    <div class="home-categories">
      <RouterLink
        v-for="category in categories"
        :key="category.id"
        class="home-category"
        :to="category.to"
      >
        <span class="home-category__icon" aria-hidden="true">
          <AppIcon :name="category.icon" size="xl" tone="default" />
        </span>
        <span>{{ category.label }}</span>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.home-categories {
  display: grid;
  grid-template-columns: repeat(6, minmax(0, 1fr));
  gap: 5px;
}

.home-category {
  display: flex;
  min-inline-size: 0;
  min-block-size: 75px;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  gap: var(--space-2);
  padding: 0;
  border: 0;
  border-radius: 0;
  color: var(--color-text-primary);
  background: transparent;
  box-shadow: none;
  font-size: 11px;
  font-weight: var(--font-weight-medium);
  text-decoration: none;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.home-category__icon {
  display: grid;
  inline-size: clamp(46px, 12vw, 54px);
  block-size: clamp(46px, 12vw, 54px);
  place-items: center;
  border-radius: 50%;
  color: var(--color-icon);
  border: 1px solid #ececec;
  background: #fff;
}

.home-category__icon :deep(svg) { inline-size: 25px; block-size: 25px; }

.home-category:active { transform: scale(0.98); }

@media (max-width: 22.5rem) {
  .home-categories { gap: 2px; }
  .home-category { min-block-size: 70px; font-size: 10px; }
  .home-category__icon { inline-size: 43px; block-size: 43px; }
}

@media (prefers-reduced-motion: reduce) {
  .home-category { transition: none; }
}
</style>
