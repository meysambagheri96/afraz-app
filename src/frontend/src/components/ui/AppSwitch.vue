<script setup lang="ts">
import { computed, useId } from 'vue'

const model = defineModel<boolean>({ default: false })
const props = defineProps<{
  id?: string
  label: string
  description?: string
  error?: string
  disabled?: boolean
  loading?: boolean
}>()

const generatedId = useId()
const controlId = computed(() => props.id ?? `switch-${generatedId}`)
const supportingId = computed(() =>
  props.error || props.description ? `${controlId.value}-supporting` : undefined,
)
</script>

<template>
  <div class="app-switch" :class="{ 'app-switch--disabled': disabled }">
    <span class="app-switch__content">
      <label class="app-switch__label" :for="controlId">{{ label }}</label>
      <span v-if="error || description" :id="supportingId" class="app-switch__description" :class="{ 'app-switch__description--error': error }">
        {{ error || description }}
      </span>
    </span>
    <button
      :id="controlId"
      type="button"
      role="switch"
      class="app-switch__track"
      :class="{ 'app-switch__track--checked': model }"
      :aria-checked="model"
      :aria-describedby="supportingId"
      :aria-invalid="error ? 'true' : undefined"
      :disabled="disabled || loading"
      :aria-busy="loading || undefined"
      @click="model = !model"
    >
      <span v-if="loading" class="app-spinner app-switch__spinner" aria-hidden="true" />
      <span v-else class="app-switch__thumb" />
    </button>
  </div>
</template>

<style scoped>
.app-switch { display: flex; min-height: var(--touch-target); align-items: center; justify-content: space-between; gap: var(--space-4); }
.app-switch__content { display: grid; gap: var(--space-1); }
.app-switch__label { color: var(--color-text-primary); font-size: var(--font-size-sm); font-weight: var(--font-weight-bold); cursor: pointer; }
.app-switch__description { color: var(--color-text-secondary); font-size: var(--font-size-xs); }
.app-switch__description--error { color: var(--color-danger); }
.app-switch__track { position: relative; flex: none; width: 3.25rem; height: 1.9rem; padding: 0.2rem; border: 0; border-radius: var(--radius-full); background: var(--color-disabled); cursor: pointer; transition: background var(--motion-fast) var(--ease-standard); }
.app-switch__track--checked { background: var(--color-brand-primary); }
.app-switch__thumb { display: block; width: 1.5rem; height: 1.5rem; border-radius: 50%; background: var(--color-surface); box-shadow: var(--shadow-thumb); transform: translateX(0); transition: transform var(--motion-fast) var(--ease-standard); }
[dir='rtl'] .app-switch__track--checked .app-switch__thumb { transform: translateX(-1.35rem); }
[dir='ltr'] .app-switch__track--checked .app-switch__thumb { transform: translateX(1.35rem); }
.app-switch__spinner { margin: auto; }
.app-switch--disabled { opacity: 0.6; }
.app-switch__track:disabled { cursor: not-allowed; }
@media (prefers-reduced-motion: reduce) { .app-switch__track, .app-switch__thumb { transition: none; } }
</style>
