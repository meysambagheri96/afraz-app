<script setup lang="ts">
import SectionHeader from '../../../components/shared/SectionHeader.vue'
import type { PortfolioItem } from '../home.types'

defineProps<{ items: readonly PortfolioItem[] }>()
</script>

<template>
  <section aria-labelledby="featured-portfolio-title">
    <SectionHeader
      id="featured-portfolio-title"
      title="نمونه‌کارهای منتخب"
      :to="{ name: 'portfolio' }"
    />
    <div class="featured-portfolio" role="list">
      <RouterLink
        v-for="item in items"
        :key="item.id"
        class="featured-portfolio__item"
        :to="{ name: 'portfolio-category', params: { category: item.id } }"
        role="listitem"
      >
        <img :src="item.imageUrl" :alt="item.alt" width="258" height="278" loading="lazy" />
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.featured-portfolio {
  display: grid;
  grid-auto-columns: clamp(78px, 21vw, 91px);
  grid-auto-flow: column;
  gap: 7px;
  margin-inline: -5px;
  padding-inline: 5px;
  overflow-x: auto;
  scroll-snap-type: inline proximity;
  scrollbar-width: none;
}

.featured-portfolio::-webkit-scrollbar { display: none; }

.featured-portfolio__item {
  display: block;
  aspect-ratio: 1 / 0.95;
  overflow: hidden;
  border-radius: 10px;
  background: var(--color-surface-muted);
  scroll-snap-align: start;
}

.featured-portfolio__item img {
  display: block;
  inline-size: 100%;
  block-size: 100%;
  object-fit: cover;
  transition: transform var(--motion-base) var(--ease-standard);
}

.featured-portfolio__item:active img { transform: scale(0.98); }

@media (max-width: 22.5rem) {
  .featured-portfolio { gap: var(--space-2); }
}

@media (prefers-reduced-motion: reduce) {
  .featured-portfolio__item img { transition: none; }
}
</style>
