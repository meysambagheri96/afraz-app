<script setup lang="ts">
import { Camera, ChevronLeft, Crown } from '@lucide/vue'
import AppAvatar from '../../../components/ui/AppAvatar.vue'
import AppBadge from '../../../components/ui/AppBadge.vue'
import type { CustomerProfile } from '../profile.types'

defineProps<{ profile: CustomerProfile }>()
defineEmits<{ edit: []; avatar: [] }>()
</script>

<template>
  <section class="profile-summary surface-card" aria-labelledby="profile-name">
    <div class="profile-summary__identity">
      <div class="profile-summary__avatar-wrap">
        <AppAvatar
          class="profile-summary__avatar"
          :src="profile.avatarUrl"
          :alt="profile.avatarAlt"
          :name="profile.name"
          size="xl"
        />
        <button class="profile-summary__camera" type="button" aria-label="تغییر تصویر پروفایل" @click="$emit('avatar')">
          <Camera :size="19" :stroke-width="1.9" />
        </button>
      </div>
      <div class="profile-summary__copy">
        <h2 id="profile-name" class="profile-summary__name text-section-title">{{ profile.name }}</h2>
        <bdi class="profile-summary__mobile text-label" dir="ltr">{{ profile.mobile }}</bdi>
        <AppBadge class="profile-summary__badge" tone="warning">
          <Crown :size="15" :stroke-width="1.8" aria-hidden="true" />
          {{ profile.membership }}
        </AppBadge>
      </div>
    </div>

    <button class="profile-summary__edit text-label" type="button" @click="$emit('edit')">
      ویرایش اطلاعات
      <ChevronLeft :size="18" :stroke-width="1.9" aria-hidden="true" />
    </button>
  </section>
</template>

<style scoped>
.profile-summary {
  display: flex;
  min-block-size: 164px;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-4);
  padding: var(--space-5);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-lg);
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
}

.profile-summary__identity { display: flex; min-inline-size: 0; align-items: center; gap: var(--space-4); }
.profile-summary__avatar-wrap { position: relative; flex: none; }
.profile-summary__avatar { inline-size: 92px; block-size: 92px; background: var(--color-surface-muted); }

.profile-summary__camera {
  position: absolute;
  inset-inline-start: -3px;
  inset-block-end: -3px;
  display: grid;
  inline-size: 38px;
  block-size: 38px;
  place-items: center;
  padding: 0;
  border: 1px solid var(--color-border-subtle);
  border-radius: 50%;
  color: var(--color-text-primary);
  background: var(--color-surface);
  box-shadow: var(--shadow-control);
  cursor: pointer;
}

.profile-summary__copy { display: grid; min-inline-size: 0; justify-items: start; }
.profile-summary__name { color: var(--color-text-primary); font-size: var(--font-size-xl); white-space: nowrap; }
.profile-summary__mobile { margin-block-start: 2px; color: var(--color-text-secondary); }
.profile-summary__badge { margin-block-start: var(--space-3); gap: var(--space-1); }

.profile-summary__edit {
  display: inline-flex;
  min-block-size: var(--touch-target);
  flex: none;
  align-items: center;
  gap: var(--space-1);
  padding: 0;
  border: 0;
  color: var(--color-info);
  background: transparent;
  cursor: pointer;
}

.profile-summary button:focus-visible { outline: none; box-shadow: var(--focus-ring); }
.profile-summary button:active { transform: scale(.98); }

@media (max-width: 23rem) {
  .profile-summary { align-items: stretch; flex-direction: column; }
  .profile-summary__edit { align-self: flex-end; }
}
</style>
