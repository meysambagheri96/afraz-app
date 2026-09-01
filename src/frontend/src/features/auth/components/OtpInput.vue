<script setup lang="ts">
import { computed, nextTick, onMounted, ref } from 'vue'

const props = withDefaults(defineProps<{ length?: number; invalid?: boolean }>(), { length: 5 })
const emit = defineEmits<{ 'update:modelValue': [value: string] }>()
const input = ref<HTMLInputElement | null>(null)
const value = ref('')
const focused = ref(false)

const digits = computed(() =>
  Array.from({ length: props.length }, (_, index) => value.value[index] ?? ''),
)

function normalizeDigits(inputValue: string) {
  return inputValue.replace(/[^0-9۰-۹]/g, '').slice(0, props.length)
}

function handleInput(event: Event) {
  const target = event.target as HTMLInputElement
  value.value = normalizeDigits(target.value)
  target.value = value.value
  emit('update:modelValue', value.value)
  void nextTick(() => target.setSelectionRange(value.value.length, value.value.length))
}

function focusInput() {
  input.value?.focus()
}

onMounted(focusInput)
</script>

<template>
  <div
    class="otp-input"
    :class="{ 'otp-input--invalid': invalid }"
    @click="focusInput"
  >
    <input
      ref="input"
      class="otp-input__native"
      :value="value"
      type="text"
      inputmode="numeric"
      dir="ltr"
      pattern="[0-9۰-۹]*"
      :maxlength="length"
      autocomplete="one-time-code"
      aria-label="کد تأیید پنج رقمی"
      :aria-invalid="invalid || undefined"
      @focus="focused = true"
      @blur="focused = false"
      @input="handleInput"
    >
    <div
      class="otp-input__boxes"
      aria-hidden="true"
    >
      <span
        v-for="(digit, index) in digits"
        :key="index"
        :class="{ 'otp-input__box--active': focused && index === Math.min(value.length, length - 1) }"
      >
        {{ digit }}
      </span>
    </div>
  </div>
</template>

<style scoped>
.otp-input {
  position: relative;
  cursor: text;
}

.otp-input__native {
  position: absolute;
  inset: 0;
  z-index: var(--z-content);
  inline-size: 100%;
  block-size: 100%;
  border: 0;
  outline: 0;
  opacity: 0;
  cursor: text;
}

.otp-input__boxes {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  gap: var(--space-3);
  direction: ltr;
}

.otp-input__boxes span {
  display: grid;
  inline-size: 100%;
  min-inline-size: 0;
  block-size: 5.25rem;
  place-items: center;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  color: var(--color-text-primary);
  background: var(--color-surface);
  box-shadow: var(--shadow-control);
  font-size: var(--font-size-xl);
  font-weight: var(--font-weight-semibold);
  transition:
    border-color var(--motion-fast) var(--ease-standard),
    box-shadow var(--motion-fast) var(--ease-standard);
}

.otp-input__boxes .otp-input__box--active {
  border-color: var(--color-brand-primary);
  box-shadow: var(--focus-ring);
}

.otp-input--invalid .otp-input__boxes span {
  border-color: var(--color-danger);
}

@media (max-width: 22.5rem) {
  .otp-input__boxes {
    gap: var(--space-2);
  }

  .otp-input__boxes span {
    block-size: 4.5rem;
  }
}
</style>
