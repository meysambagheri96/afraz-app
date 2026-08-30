<script setup lang="ts">
import { computed, useId } from 'vue'
import type { AppSelectValue } from './types'

const model = defineModel<AppSelectValue | null>({ default: null })
const props = defineProps<{
  value: AppSelectValue
  label: string
  id?: string
  name?: string
  description?: string
  error?: string
  required?: boolean
  disabled?: boolean
  loading?: boolean
}>()

const generatedId = useId()
const controlId = computed(() => props.id ?? `radio-${generatedId}`)
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
      type="radio"
      :value="value"
      :name="name"
      :required="required"
      :disabled="disabled || loading"
      :aria-invalid="error ? 'true' : undefined"
      :aria-describedby="supportingId"
    />
    <label class="app-choice__label" :for="controlId">
      <span class="app-choice__indicator app-radio__indicator" aria-hidden="true">
        <span v-if="loading" class="app-spinner" />
        <span v-else-if="model === value" class="app-radio__dot" />
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
.app-radio__indicator { border-radius: 50%; }
.app-choice__native:checked + .app-choice__label .app-radio__indicator { border-color: var(--color-brand-primary); background: var(--color-surface); }
.app-radio__dot { width: 0.65rem; height: 0.65rem; border-radius: 50%; background: var(--color-brand-primary); }
</style>
