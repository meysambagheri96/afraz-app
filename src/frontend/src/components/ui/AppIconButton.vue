<script setup lang="ts">
import type { AppButtonVariant, AppControlSize } from './types'

withDefaults(
  defineProps<{
    label: string
    variant?: AppButtonVariant
    size?: AppControlSize
    type?: 'button' | 'submit' | 'reset'
    disabled?: boolean
    loading?: boolean
  }>(),
  { variant: 'ghost', size: 'md', type: 'button' },
)

defineEmits<{ click: [event: MouseEvent] }>()
</script>

<template>
  <button
    :type="type"
    class="app-icon-button"
    :class="[`app-icon-button--${variant}`, `app-icon-button--${size}`]"
    :disabled="disabled || loading"
    :aria-label="label"
    :title="label"
    :aria-busy="loading || undefined"
    @click="$emit('click', $event)"
  >
    <span v-if="loading" class="app-spinner" aria-hidden="true" />
    <slot v-else />
  </button>
</template>
