<script setup lang="ts">
import SectionHeader from '../../../components/shared/SectionHeader.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { AlbumPreviewItem } from '../home.types'

defineProps<{ items: readonly AlbumPreviewItem[] }>()
</script>

<template>
  <section aria-label="پیشنهادهای آلبوم">
    <SectionHeader title="آلبوم‌های محبوب" :to="{ name: 'store', query: { category: 'albums' } }" />
    <div class="album-preview">
      <RouterLink
        v-for="item in items"
        :key="item.id"
        class="album-preview__item"
        :class="`album-preview__item--${item.accent}`"
        :to="item.to"
      >
        <span class="album-preview__icon"><AppIcon name="album" size="lg" /></span>
        <strong>{{ item.title }}</strong>
        <small>{{ item.caption }}</small>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.album-preview {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: var(--space-3);
}

.album-preview__item {
  display: flex;
  min-inline-size: 0;
  min-block-size: 8rem;
  justify-content: flex-end;
  flex-direction: column;
  padding: var(--space-3);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-card);
  color: var(--color-text-primary);
  background: var(--color-brand-soft);
  text-decoration: none;
}

.album-preview__item--pink { background: var(--color-danger-soft); }
.album-preview__item--lilac { background: color-mix(in srgb, var(--color-accent-lilac) 28%, var(--color-surface)); }

.album-preview__icon {
  display: grid;
  inline-size: 2.75rem;
  block-size: 2.75rem;
  margin-block-end: auto;
  place-items: center;
  border-radius: 50%;
  color: var(--color-brand-primary);
  background: color-mix(in srgb, var(--color-surface) 80%, transparent);
}

.album-preview__item strong { margin-block-start: var(--space-3); font-size: var(--font-size-sm); }
.album-preview__item small { overflow: hidden; color: var(--color-text-secondary); font-size: var(--font-size-xs); text-overflow: ellipsis; white-space: nowrap; }

@media (max-width: 22.5rem) {
  .album-preview { display: flex; overflow-x: auto; scroll-snap-type: inline mandatory; scrollbar-width: none; }
  .album-preview__item { min-inline-size: 8rem; scroll-snap-align: start; }
  .album-preview::-webkit-scrollbar { display: none; }
}
</style>
