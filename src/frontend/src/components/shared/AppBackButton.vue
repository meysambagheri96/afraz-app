<script setup lang="ts">
import { useRouter, type RouteLocationRaw } from 'vue-router'
import AppIcon from '../ui/AppIcon.vue'
import AppIconButton from '../ui/AppIconButton.vue'

const props = withDefaults(
  defineProps<{
    label?: string
    to?: RouteLocationRaw
  }>(),
  { label: 'بازگشت' },
)

const router = useRouter()

function goBack() {
  if (props.to) {
    void router.push(props.to)
    return
  }

  router.back()
}
</script>

<template>
  <AppIconButton class="app-back-button" :label="label" variant="ghost" @click="goBack">
    <AppIcon name="arrow-forward" size="lg" />
  </AppIconButton>
</template>

<style scoped>
.app-back-button {
  color: var(--color-text-primary);
  border-radius: var(--radius-full);
}

.app-back-button :deep(svg) {
  inline-size: 1.75rem;
  block-size: 1.75rem;
}
</style>
