<script setup lang="ts">
import type { AppButtonVariant, AppControlSize } from './types'

withDefaults(
  defineProps<{
    variant?: AppButtonVariant
    size?: AppControlSize
    type?: 'button' | 'submit' | 'reset'
    disabled?: boolean
    loading?: boolean
    block?: boolean
    loadingLabel?: string
  }>(),
  {
    variant: 'primary',
    size: 'md',
    type: 'button',
    loadingLabel: 'در حال پردازش',
  },
)

defineEmits<{ click: [event: MouseEvent] }>()
</script>

<template>
  <button
    :type="type"
    class="app-button"
    :class="[`app-button--${variant}`, `app-button--${size}`, { 'app-button--block': block }]"
    :disabled="disabled || loading"
    :aria-busy="loading || undefined"
    @click="$emit('click', $event)"
  >
    <span
      v-if="loading || $slots.leading"
      class="app-button__icon app-button__icon--leading"
      aria-hidden="true"
    >
      <span
        v-if="loading"
        class="app-spinner"
      />
      <slot
        v-else
        name="leading"
      />
    </span>
    <span class="app-button__label">
      <span v-if="loading">{{ loadingLabel }}</span>
      <slot v-else />
    </span>
    <span
      v-if="!loading && $slots.trailing"
      class="app-button__icon app-button__icon--trailing"
      aria-hidden="true"
    >
      <slot name="trailing" />
    </span>
  </button>
</template>
