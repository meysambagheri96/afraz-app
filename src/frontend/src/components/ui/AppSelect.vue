<script setup lang="ts">
import { computed, useId } from 'vue'
import type { AppControlSize, AppSelectOption, AppSelectValue } from './types'

const model = defineModel<AppSelectValue | null>({ default: null })
const props = withDefaults(
  defineProps<{
    id?: string
    label?: string
    hint?: string
    error?: string
    placeholder?: string
    name?: string
    options: readonly AppSelectOption[]
    size?: AppControlSize
    required?: boolean
    disabled?: boolean
    loading?: boolean
  }>(),
  { placeholder: 'انتخاب کنید', size: 'md' },
)

const generatedId = useId()
const controlId = computed(() => props.id ?? `select-${generatedId}`)
const supportingId = computed(() =>
  props.error || props.hint ? `${controlId.value}-supporting` : undefined,
)
</script>

<template>
  <div class="app-field" :class="{ 'app-field--error': error, 'app-field--disabled': disabled || loading }">
    <label v-if="label" class="app-field__label" :for="controlId">
      {{ label }}<span v-if="required" class="app-field__required" aria-hidden="true">*</span>
    </label>
    <div class="app-field__control app-field__select-wrap" :class="`app-field__control--${size}`">
      <select
        :id="controlId"
        v-model="model"
        class="app-field__input app-field__select"
        :name="name"
        :required="required"
        :disabled="disabled || loading"
        :aria-invalid="error ? 'true' : undefined"
        :aria-describedby="supportingId"
      >
        <option :value="null" disabled>{{ placeholder }}</option>
        <option v-for="option in options" :key="String(option.value)" :value="option.value" :disabled="option.disabled">
          {{ option.label }}
        </option>
      </select>
      <span v-if="loading" class="app-spinner app-field__select-icon" aria-hidden="true" />
      <svg v-else class="app-field__select-icon" viewBox="0 0 20 20" fill="none" aria-hidden="true">
        <path d="m6 8 4 4 4-4" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round" />
      </svg>
    </div>
    <p v-if="error || hint" :id="supportingId" class="app-field__supporting" :role="error ? 'alert' : undefined">
      {{ error || hint }}
    </p>
  </div>
</template>
