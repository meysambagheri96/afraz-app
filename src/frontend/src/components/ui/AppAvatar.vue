<script setup lang="ts">
import { computed, ref, watch } from 'vue'

const props = withDefaults(
  defineProps<{
    src?: string
    alt?: string
    name?: string
    size?: 'sm' | 'md' | 'lg' | 'xl'
    status?: 'online' | 'offline'
    loading?: boolean
    disabled?: boolean
  }>(),
  { alt: '', size: 'md' },
)

const emit = defineEmits<{ load: [event: Event]; error: [event: Event] }>()
const imageFailed = ref(false)
const initials = computed(() => {
  const words = props.name?.trim().split(/\s+/).filter(Boolean) ?? []
  return words.slice(0, 2).map((word) => word.at(0)).join('') || 'آ'
})

watch(() => props.src, () => { imageFailed.value = false })

function handleError(event: Event) {
  imageFailed.value = true
  emit('error', event)
}
</script>

<template>
  <span class="app-avatar" :class="[`app-avatar--${size}`, { 'app-avatar--disabled': disabled }]">
    <span v-if="loading" class="app-avatar__skeleton" aria-hidden="true" />
    <img
      v-else-if="src && !imageFailed"
      class="app-avatar__image"
      :src="src"
      :alt="alt"
      loading="lazy"
      @load="$emit('load', $event)"
      @error="handleError"
    />
    <span v-else class="app-avatar__fallback" :aria-label="alt || name">{{ initials }}</span>
    <span v-if="status" class="app-avatar__status" :class="`app-avatar__status--${status}`" :aria-label="status === 'online' ? 'آنلاین' : 'آفلاین'" />
  </span>
</template>

<style scoped>
.app-avatar { position: relative; display: inline-grid; flex: none; place-items: center; padding: 2px; border-radius: 50%; background: linear-gradient(135deg, var(--color-accent-pink), var(--color-accent-yellow)); }
.app-avatar--sm { width: 2rem; height: 2rem; }
.app-avatar--md { width: 2.75rem; height: 2.75rem; }
.app-avatar--lg { width: 4rem; height: 4rem; }
.app-avatar--xl { width: 5.5rem; height: 5.5rem; }
.app-avatar__image, .app-avatar__fallback, .app-avatar__skeleton { width: 100%; height: 100%; border: 2px solid var(--color-surface); border-radius: 50%; }
.app-avatar__image { object-fit: cover; }
.app-avatar__fallback { display: grid; place-items: center; color: var(--color-brand-primary); background: var(--color-brand-soft); font-weight: var(--font-weight-bold); }
.app-avatar__skeleton { background: linear-gradient(100deg, var(--color-surface-muted) 30%, white 50%, var(--color-surface-muted) 70%); background-size: 200% 100%; animation: app-shimmer 1.4s infinite; }
.app-avatar__status { position: absolute; inset-inline-end: 0; inset-block-end: 0; width: 0.75rem; height: 0.75rem; border: 2px solid var(--color-surface); border-radius: 50%; }
.app-avatar__status--online { background: var(--color-success); }
.app-avatar__status--offline { background: var(--color-disabled); }
.app-avatar--disabled { filter: grayscale(1); opacity: 0.55; }
</style>
