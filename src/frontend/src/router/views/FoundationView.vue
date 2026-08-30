<script setup lang="ts">
defineProps<{
  title: string
  description: string
}>()
</script>

<template>
  <section class="foundation-card" aria-labelledby="page-title">
    <p class="eyebrow">استودیو افراز</p>
    <h1 id="page-title" class="text-page-title">{{ title }}</h1>
    <p class="description">{{ description }}</p>
    <div class="status" role="status">
      <span aria-hidden="true" />
      پوسته اصلی و ناوبری این بخش آماده است
    </div>
  </section>
</template>
