<script setup lang="ts">
import type { ProfileMenuItem as ProfileMenuItemType } from '../profile.types'
import ProfileMenuItem from './ProfileMenuItem.vue'

defineProps<{ items: ProfileMenuItemType[]; label: string }>()
defineEmits<{ select: [item: ProfileMenuItemType] }>()
</script>

<template>
  <section class="profile-menu" :aria-label="label">
    <ProfileMenuItem
      v-for="(item, index) in items"
      :key="item.id"
      :item="item"
      :divider="index < items.length - 1"
      @select="$emit('select', $event)"
    />
  </section>
</template>

<style scoped>
.profile-menu {
  overflow: hidden;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-lg);
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
}
</style>
