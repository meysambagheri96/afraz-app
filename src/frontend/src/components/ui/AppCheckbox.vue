<script setup lang="ts">
import { computed, useId } from 'vue'

const model = defineModel<boolean>({ default: false })
const props = defineProps<{
  id?: string
  label: string
  description?: string
  error?: string
  name?: string
  required?: boolean
  disabled?: boolean
  loading?: boolean
}>()

const generatedId = useId()
const controlId = computed(() => props.id ?? `checkbox-${generatedId}`)
const supportingId = computed(() =>
  props.error || props.description ? `${controlId.value}-supporting` : undefined,
)
</script>

<template>
  <div class="app-choice" :class="{ 'app-choice--disabled': disabled || loading, 'app-choice--error': error }">
    <input
      :id="controlId"
      v-model="model"
      class="app-choice__native"
      type="checkbox"
      :name="name"
      :required="required"
      :disabled="disabled || loading"
      :aria-invalid="error ? 'true' : undefined"
      :aria-describedby="supportingId"
    />
    <label class="app-choice__label" :for="controlId">
      <span class="app-choice__indicator app-checkbox__indicator" aria-hidden="true">
        <span v-if="loading" class="app-spinner" />
        <svg v-else-if="model" viewBox="0 0 16 16" fill="none">
          <path d="m3.5 8 3 3 6-6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
        </svg>
      </span>
      <span class="app-choice__content">
        <span class="app-choice__title">{{ label }}<span v-if="required" class="app-field__required">*</span></span>
        <span v-if="error || description" :id="supportingId" class="app-choice__description" :class="{ 'app-choice__description--error': error }">
          {{ error || description }}
        </span>
      </span>
    </label>
  </div>
</template>

<style scoped>
.app-checkbox__indicator { border-radius: 0.45rem; }
.app-choice__native:checked + .app-choice__label .app-checkbox__indicator {
  color: white;
  border-color: var(--color-brand-primary);
  background: var(--color-brand-primary);
}
.app-checkbox__indicator svg { width: 1rem; height: 1rem; }
</style>
