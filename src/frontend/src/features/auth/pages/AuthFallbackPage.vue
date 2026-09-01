<script setup lang="ts">
import { useRouter } from 'vue-router'
import AuthFlow from '../components/AuthFlow.vue'

const router = useRouter()
const finish = () => router.push({ name: 'home' })
</script>

<template>
  <main class="auth-fallback-page">
    <AuthFlow
      @close="finish"
      @success="finish"
    />
  </main>
</template>

<style scoped>
.auth-fallback-page {
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: 100dvh;
  margin-inline: auto;
  overflow: hidden;
  background: var(--color-background);
}
</style>
