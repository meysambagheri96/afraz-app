<script setup lang="ts">
import SectionHeader from '../../../components/shared/SectionHeader.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { PhotographyCategory } from '../home.types'

defineProps<{ categories: readonly PhotographyCategory[] }>()
</script>

<template>
  <section aria-labelledby="home-categories-title">
    <SectionHeader id="home-categories-title" title="دسته‌بندی‌ها" :to="{ name: 'portfolio' }" />
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
  grid-template-columns: repeat(4, minmax(0, 1fr));
  gap: clamp(var(--space-2), 2.5vw, var(--space-3));
}

.home-category {
  display: flex;
  min-inline-size: 0;
  min-block-size: 6.25rem;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  gap: var(--space-2);
  padding: var(--space-2);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  color: var(--color-text-primary);
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
  font-size: clamp(var(--font-size-xs), 3.2vw, var(--font-size-base));
  font-weight: var(--font-weight-medium);
  text-decoration: none;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.home-category__icon {
  display: grid;
  inline-size: 3rem;
  block-size: 3rem;
  place-items: center;
  border-radius: 50%;
  color: var(--color-icon);
  background: var(--color-warning-soft);
}

.home-category:active { transform: scale(0.98); }

@media (max-width: 22.5rem) {
  .home-category { min-block-size: 5.75rem; padding-inline: var(--space-1); }
  .home-category__icon { inline-size: 2.5rem; block-size: 2.5rem; }
}

@media (prefers-reduced-motion: reduce) {
  .home-category { transition: none; }
}
</style>
