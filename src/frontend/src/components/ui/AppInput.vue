<script setup lang="ts">
import { computed, useId } from 'vue'
import type { AppControlSize } from './types'

const model = defineModel<string>({ default: '' })
const props = withDefaults(
  defineProps<{
    id?: string
    label?: string
    hint?: string
    error?: string
    placeholder?: string
    name?: string
    type?: 'text' | 'email' | 'tel' | 'password' | 'search' | 'url' | 'number'
    inputmode?: 'none' | 'text' | 'decimal' | 'numeric' | 'tel' | 'search' | 'email' | 'url'
    autocomplete?: string
    ariaLabel?: string
    dir?: 'rtl' | 'ltr' | 'auto'
    size?: AppControlSize
    required?: boolean
    disabled?: boolean
    loading?: boolean
    readonly?: boolean
  }>(),
  { type: 'text', ariaLabel: '', dir: 'rtl', size: 'md' },
)

const generatedId = useId()
const controlId = computed(() => props.id ?? `input-${generatedId}`)
const supportingId = computed(() =>
  props.error || props.hint ? `${controlId.value}-supporting` : undefined,
)
</script>

<template>
  <div class="app-field" :class="{ 'app-field--error': error, 'app-field--disabled': disabled || loading }">
    <label v-if="label" class="app-field__label" :for="controlId">
      {{ label }}<span v-if="required" class="app-field__required" aria-hidden="true">*</span>
    </label>
    <div class="app-field__control" :class="`app-field__control--${size}`">
      <span v-if="$slots.leading" class="app-field__adornment" aria-hidden="true">
        <slot name="leading" />
      </span>
      <input
        :id="controlId"
        v-model="model"
        class="app-field__input"
        :type="type"
        :name="name"
        :placeholder="placeholder"
        :inputmode="inputmode"
        :autocomplete="autocomplete"
        :aria-label="ariaLabel || label || placeholder"
        :dir="dir"
        :required="required"
        :disabled="disabled || loading"
        :readonly="readonly"
        :aria-invalid="error ? 'true' : undefined"
        :aria-describedby="supportingId"
      />
      <span v-if="loading" class="app-spinner app-field__spinner" aria-hidden="true" />
      <span v-else-if="$slots.trailing" class="app-field__adornment">
        <slot name="trailing" />
      </span>
    </div>
    <p v-if="error || hint" :id="supportingId" class="app-field__supporting" :role="error ? 'alert' : undefined">
      {{ error || hint }}
    </p>
  </div>
</template>
