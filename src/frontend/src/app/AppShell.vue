<script setup lang="ts">
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
