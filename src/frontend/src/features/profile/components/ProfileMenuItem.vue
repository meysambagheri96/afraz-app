<script setup lang="ts">
import { ChevronLeft } from '@lucide/vue'
import AppDivider from '../../../components/ui/AppDivider.vue'
import type { ProfileMenuItem } from '../profile.types'

defineProps<{ item: ProfileMenuItem; divider?: boolean }>()
defineEmits<{ select: [item: ProfileMenuItem] }>()
</script>

<template>
  <div class="profile-menu-item" :class="{ 'profile-menu-item--destructive': item.destructive }">
    <button class="profile-menu-item__button" type="button" @click="$emit('select', item)">
      <span class="profile-menu-item__icon" aria-hidden="true">
        <component :is="item.icon" :size="25" :stroke-width="1.75" />
      </span>
      <span class="profile-menu-item__copy">
        <span class="profile-menu-item__title text-card-title">{{ item.title }}</span>
        <span v-if="item.subtitle" class="profile-menu-item__subtitle text-caption">{{ item.subtitle }}</span>
      </span>
      <ChevronLeft v-if="!item.destructive" class="profile-menu-item__chevron" :size="19" :stroke-width="1.9" aria-hidden="true" />
    </button>
    <AppDivider v-if="divider" class="profile-menu-item__divider" />
  </div>
</template>

<style scoped>
.profile-menu-item__button {
  display: grid;
  inline-size: 100%;
  min-block-size: 76px;
  grid-template-columns: 52px minmax(0, 1fr) 28px;
  align-items: center;
  gap: var(--space-3);
  padding: var(--space-3) var(--space-4);
  border: 0;
  color: var(--color-text-primary);
  background: transparent;
  text-align: start;
  cursor: pointer;
}

.profile-menu-item__icon {
  display: grid;
  inline-size: 48px;
  block-size: 48px;
  place-items: center;
  border-radius: 50%;
  color: var(--color-text-primary);
  background: var(--color-warning-soft);
}

.profile-menu-item__copy { display: grid; min-inline-size: 0; gap: 1px; }
.profile-menu-item__title { color: inherit; }
.profile-menu-item__subtitle { overflow: hidden; color: var(--color-text-secondary); text-overflow: ellipsis; white-space: nowrap; }
.profile-menu-item__chevron { justify-self: end; }
.profile-menu-item__divider { margin-inline: calc(var(--space-4) + 52px + var(--space-3)) var(--space-4); }

.profile-menu-item--destructive .profile-menu-item__button { grid-template-columns: 52px minmax(0, 1fr); color: var(--color-danger); }
.profile-menu-item--destructive .profile-menu-item__icon { color: var(--color-danger); background: var(--color-danger-soft); }
.profile-menu-item--destructive .profile-menu-item__title { font-size: var(--font-size-lg); }

.profile-menu-item__button:hover { background: color-mix(in srgb, var(--color-surface-muted) 60%, transparent); }
.profile-menu-item__button:active { background: var(--color-surface-muted); }
.profile-menu-item__button:focus-visible { position: relative; z-index: 1; outline: none; box-shadow: inset var(--focus-ring); }
</style>
