<script setup lang="ts">
import HomeAlbumPreview from '../components/HomeAlbumPreview.vue'
import HomeCategories from '../components/HomeCategories.vue'
import HomeDynamicIsland from '../components/HomeDynamicIsland.vue'
import HomeFeaturedPortfolio from '../components/HomeFeaturedPortfolio.vue'
import HomeHeader from '../components/HomeHeader.vue'
import HomeHeroBanner from '../components/HomeHeroBanner.vue'
import HomeLastOrderCard from '../components/HomeLastOrderCard.vue'
import HomeQuickActions from '../components/HomeQuickActions.vue'
import HomeStudioInfo from '../components/HomeStudioInfo.vue'
import {
  albumPreviews,
  featuredPortfolio,
  homeHeroImageUrl,
  latestOrder,
  photographyCategories,
  quickActions,
} from '../home.data'
</script>

<template>
  <div class="home-page">
    <HomeDynamicIsland />
    <HomeHeader />

    <div class="home-page__content">
      <HomeHeroBanner :image-url="homeHeroImageUrl" />
      <HomeQuickActions class="home-page__section home-page__section--large" :actions="quickActions" />
      <HomeFeaturedPortfolio class="home-page__section home-page__section--large" :items="featuredPortfolio" />
      <HomeCategories class="home-page__section" :categories="photographyCategories" />
      <HomeLastOrderCard class="home-page__section" :order="latestOrder" />
      <HomeAlbumPreview class="home-page__section home-page__section--large" :items="albumPreviews" />
      <HomeStudioInfo class="home-page__section" />
    </div>
  </div>
</template>

<style scoped>
.home-page { min-inline-size: 0; }

.home-page__content { margin-block-start: var(--space-6); }
.home-page__section { margin-block-start: var(--section-gap); }
.home-page__section--large { margin-block-start: var(--section-gap-lg); }
</style>
