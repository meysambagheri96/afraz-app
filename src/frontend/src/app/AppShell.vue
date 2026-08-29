<script setup lang="ts">
import { version as vueVersion } from 'vue'

defineProps<{
  title: string
  description: string
}>()
</script>

<template>
  <main class="app-shell">
    <section
      class="foundation-card"
      aria-labelledby="page-title"
    >
      <p class="eyebrow">
        Afraz Studio
      </p>
      <h1 id="page-title">
        {{ title }}
      </h1>
      <p class="description">
        {{ description }}
      </p>
      <dl
        class="versions"
        aria-label="نسخه فناوری‌های اصلی"
      >
        <div class="version-pill">
          <dt>.NET</dt>
          <dd>10</dd>
        </div>
        <div class="version-pill">
          <dt>Vue</dt>
          <dd>{{ vueVersion }}</dd>
        </div>
      </dl>
      <div
        class="status"
        role="status"
      >
        <span aria-hidden="true" />
        زیرساخت اولیه آماده توسعه است
      </div>
    </section>
  </main>
</template>
