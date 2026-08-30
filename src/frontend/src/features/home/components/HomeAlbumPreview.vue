<script setup lang="ts">
import SectionHeader from '../../../components/shared/SectionHeader.vue'
import type { AlbumPreviewItem } from '../home.types'

defineProps<{ items: readonly AlbumPreviewItem[] }>()
</script>

<template>
  <section aria-label="فروشگاه آلبوم">
    <SectionHeader title="فروشگاه آلبوم" :to="{ name: 'store', query: { category: 'albums' } }" />
    <div class="album-preview">
      <RouterLink v-for="item in items" :key="item.id" class="album-card" :class="`album-card--${item.accent}`" :to="item.to">
        <span class="album-card__spine" aria-hidden="true" />
        <span class="album-card__ribbon" aria-hidden="true" />
        <span class="album-card__copy"><strong>{{ item.title }}</strong><small>{{ item.caption }}</small></span>
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.album-preview { display: grid; grid-template-columns: repeat(3, minmax(128px, 1fr)); gap: 9px; overflow-x: auto; scrollbar-width: none; }
.album-preview::-webkit-scrollbar { display: none; }
.album-card { position: relative; display: flex; min-inline-size: 0; block-size: 81px; align-items: center; overflow: hidden; padding-inline: 14px 48px; border-radius: 13px; color: #111820; background: #f2e4cf; text-decoration: none; scroll-snap-align: start; }
.album-card--pink { background: #f8d9dc; }
.album-card--mint { background: #d9eae7; }
.album-card__spine { position: absolute; inset-block: 0; inset-inline-start: 0; inline-size: 42px; border-inline-end: 1px solid rgb(108 84 54 / 12%); background: linear-gradient(90deg, rgb(255 255 255 / 32%), transparent 45%, rgb(255 255 255 / 22%)); box-shadow: inset 8px 0 12px rgb(255 255 255 / 18%); }
.album-card__ribbon { position: absolute; inset-block: 0; inset-inline-start: 32px; inline-size: 10px; background: rgb(229 185 123 / 54%); }
.album-card__ribbon::after { position: absolute; inset-block-end: 8px; inset-inline-start: 50%; inline-size: 24px; block-size: 17px; border-radius: 50% 50% 45% 45%; background: inherit; content: ''; transform: translateX(50%) rotate(-18deg); }
.album-card--pink .album-card__ribbon { background: rgb(238 139 153 / 55%); }
.album-card--mint .album-card__ribbon { background: rgb(233 164 125 / 45%); }
.album-card__copy { position: relative; z-index: 1; display: flex; min-inline-size: 0; flex-direction: column; gap: 5px; }
.album-card strong { font-size: 13px; font-weight: 700; white-space: nowrap; }
.album-card small { color: #34383b; font-size: 9px; white-space: nowrap; }

@media (max-width: 400px) {
  .album-preview { grid-template-columns: repeat(3, 122px); }
  .album-card { padding-inline-end: 44px; }
}
</style>
