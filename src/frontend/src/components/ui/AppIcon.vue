<script setup lang="ts">
import { computed, useId } from 'vue'
import iconSpriteUrl from '../../assets/icons/afraz-icons.svg?url'
import type { AppIconName, AppIconSize, AppIconTone } from './icon.types'

const filledIcons = new Set<AppIconName>(['home', 'booking', 'orders', 'store', 'profile'])
const props = withDefaults(
  defineProps<{
    name: AppIconName
    size?: AppIconSize
    tone?: AppIconTone
    active?: boolean
    label?: string
  }>(),
  { size: 'md', tone: 'inherit' },
)

const titleId = `app-icon-${useId()}`
const variant = computed(() => props.active && filledIcons.has(props.name) ? 'filled' : 'outline')
const symbolHref = computed(() => `${iconSpriteUrl}#afraz-${props.name}-${variant.value}`)
</script>

<template>
  <svg
    class="app-icon-svg"
    :class="[`app-icon-svg--${size}`, `app-icon-svg--${tone}`, { 'app-icon-svg--active': active }]"
    viewBox="0 0 24 24"
    :role="label ? 'img' : undefined"
    :aria-labelledby="label ? titleId : undefined"
    :aria-hidden="label ? undefined : 'true'"
    focusable="false"
  >
    <title v-if="label" :id="titleId">{{ label }}</title>
    <use :href="symbolHref" />
  </svg>
</template>

<style scoped>
.app-icon-svg {
  display: inline-block;
  flex: none;
  overflow: visible;
  vertical-align: middle;
  stroke-width: var(--icon-stroke-width);
}

.app-icon-svg--xs { inline-size: var(--icon-size-xs); block-size: var(--icon-size-xs); }
.app-icon-svg--sm { inline-size: var(--icon-size-sm); block-size: var(--icon-size-sm); }
.app-icon-svg--md { inline-size: var(--icon-size-md); block-size: var(--icon-size-md); }
.app-icon-svg--lg { inline-size: var(--icon-size-lg); block-size: var(--icon-size-lg); }
.app-icon-svg--xl { inline-size: var(--icon-size-xl); block-size: var(--icon-size-xl); }
.app-icon-svg--inherit { color: inherit; }
.app-icon-svg--default { color: var(--color-icon); }
.app-icon-svg--brand,
.app-icon-svg--active { color: var(--color-brand-primary); }
.app-icon-svg--muted { color: var(--color-text-secondary); }
.app-icon-svg--accent { color: var(--color-accent-pink); }
</style>
