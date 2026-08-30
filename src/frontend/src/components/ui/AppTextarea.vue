<script setup lang="ts">
import { computed, useId } from 'vue'

const model = defineModel<string>({ default: '' })
const props = withDefaults(
  defineProps<{
    id?: string
    label?: string
    hint?: string
    error?: string
    placeholder?: string
    name?: string
    rows?: number
    maxlength?: number
    required?: boolean
    disabled?: boolean
    loading?: boolean
    readonly?: boolean
    resize?: 'none' | 'vertical'
  }>(),
  { rows: 4, resize: 'vertical' },
)

const generatedId = useId()
const controlId = computed(() => props.id ?? `textarea-${generatedId}`)
const supportingId = computed(() =>
  props.error || props.hint ? `${controlId.value}-supporting` : undefined,
)
</script>

<template>
  <div class="app-field" :class="{ 'app-field--error': error, 'app-field--disabled': disabled || loading }">
    <label v-if="label" class="app-field__label" :for="controlId">
      {{ label }}<span v-if="required" class="app-field__required" aria-hidden="true">*</span>
    </label>
    <div class="app-field__control app-field__control--textarea">
      <textarea
        :id="controlId"
        v-model="model"
        class="app-field__input app-field__textarea"
        :class="`app-field__textarea--${resize}`"
        :name="name"
        :placeholder="placeholder"
        :rows="rows"
        :maxlength="maxlength"
        :required="required"
        :disabled="disabled || loading"
        :readonly="readonly"
        :aria-invalid="error ? 'true' : undefined"
        :aria-describedby="supportingId"
      />
      <span v-if="loading" class="app-spinner app-field__spinner" aria-hidden="true" />
    </div>
    <div v-if="error || hint || maxlength" class="app-field__meta">
      <p v-if="error || hint" :id="supportingId" class="app-field__supporting" :role="error ? 'alert' : undefined">
        {{ error || hint }}
      </p>
      <span v-if="maxlength" class="app-field__counter">{{ model.length }}/{{ maxlength }}</span>
    </div>
  </div>
</template>
