<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { HomeStory } from '../home.types'

defineProps<{ stories: readonly HomeStory[] }>()
</script>

<template>
  <section class="home-stories" aria-label="داستان‌های آتلیه">
    <button v-for="story in stories" :key="story.id" class="home-story" type="button">
      <span class="home-story__ring" :class="{ 'home-story__ring--create': story.create }">
        <span v-if="story.create" class="home-story__plus" aria-hidden="true" />
        <AppIcon v-else-if="story.icon" :name="story.icon" size="lg" />
        <img v-else :src="story.imageUrl" :alt="story.imageAlt" width="52" height="52" />
      </span>
      <span class="home-story__label">{{ story.label }}</span>
    </button>
  </section>
</template>

<style scoped>
.home-stories {
  display: grid;
  grid-template-columns: repeat(6, minmax(0, 1fr));
  gap: 8px;
  margin-block-start: 13px;
  padding-inline: 1px;
}

.home-story {
  display: flex;
  min-inline-size: 0;
  min-block-size: 85px;
  align-items: center;
  flex-direction: column;
  gap: 5px;
  padding: 0;
  border: 0;
  color: #111820;
  background: transparent;
  font-size: 12px;
  font-weight: 500;
  line-height: 1.35;
  cursor: pointer;
}

.home-story__ring {
  display: grid;
  inline-size: clamp(50px, 14vw, 60px);
  block-size: clamp(50px, 14vw, 60px);
  flex: none;
  padding: 2px;
  place-items: center;
  border-radius: 50%;
  background: linear-gradient(135deg, #ff2d55, #ff375f 48%, #ff9500);
}

.home-story__ring > img,
.home-story__ring > :deep(svg) {
  inline-size: 100%;
  block-size: 100%;
  border: 2px solid #fff;
  border-radius: 50%;
  background: #fff;
}

.home-story__ring > img { object-fit: cover; }
.home-story__ring > :deep(svg) { padding: 12px; color: #ff7090; }

.home-story__ring--create {
  border: 1px solid #d9dadd;
  background: #fff;
}

.home-story__plus {
  position: relative;
  inline-size: 28px;
  block-size: 28px;
}

.home-story__plus::before,
.home-story__plus::after {
  position: absolute;
  inset: 50% auto auto 50%;
  inline-size: 25px;
  block-size: 2px;
  border-radius: 2px;
  background: #111820;
  content: '';
  transform: translate(-50%, -50%);
}

.home-story__plus::after { transform: translate(-50%, -50%) rotate(90deg); }
.home-story__label { overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }

@media (max-width: 390px) {
  .home-stories { gap: 4px; }
  .home-story { font-size: 11px; }
}
</style>
