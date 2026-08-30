<script setup lang="ts">
import HomeAlbumPreview from '../components/HomeAlbumPreview.vue'
import HomeCategories from '../components/HomeCategories.vue'
import HomeFeaturedPortfolio from '../components/HomeFeaturedPortfolio.vue'
import HomeHeader from '../components/HomeHeader.vue'
import HomeHeroBanner from '../components/HomeHeroBanner.vue'
import HomeLastOrderCard from '../components/HomeLastOrderCard.vue'
import HomeQuickActions from '../components/HomeQuickActions.vue'
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
    <HomeHeader />

    <div class="home-page__content">
      <HomeHeroBanner :image-url="homeHeroImageUrl" />
      <HomeQuickActions class="home-page__section" :actions="quickActions" />
      <HomeFeaturedPortfolio class="home-page__section" :items="featuredPortfolio" />
      <HomeCategories class="home-page__section" :categories="photographyCategories" />
      <HomeLastOrderCard class="home-page__section home-page__section--photo-ready" :order="latestOrder" />
      <HomeAlbumPreview class="home-page__section" :items="albumPreviews" />
    </div>
  </div>
</template>

<style scoped>
.home-page { min-inline-size: 0; color: #111820; }

.home-page__content { margin-block-start: 9px; }
.home-page__section { margin-block-start: 15px; }
.home-page__section--photo-ready { margin-block-start: 20px; }
</style>
