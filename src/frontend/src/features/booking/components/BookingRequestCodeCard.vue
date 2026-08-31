<script setup lang="ts">
import { ref } from 'vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'

const props = defineProps<{ code: string }>()
const copied = ref(false)

async function copyCode() {
  try {
    await navigator.clipboard.writeText(props.code)
    copied.value = true
  } catch {
    copied.value = false
  }
}
</script>

<template>
  <div class="booking-request-code">
    <div class="booking-request-code__card app-surface-card">
      <span class="booking-request-code__label">کد درخواست</span>
      <bdi
        class="booking-request-code__value"
        dir="ltr"
      >{{ code }}</bdi>
      <AppIconButton
        :label="copied ? 'کد کپی شد' : 'کپی کد درخواست'"
        variant="glass"
        @click="copyCode"
      >
        <AppIcon
          name="copy"
          size="md"
        />
      </AppIconButton>
    </div>
    <p>لطفاً این کد را برای پیگیری‌های بعدی نزد خود نگه دارید.</p>
    <span
      class="visually-hidden"
      aria-live="polite"
    >{{ copied ? 'کد درخواست کپی شد.' : '' }}</span>
  </div>
</template>

<style scoped>
.booking-request-code {
  display: grid;
  gap: var(--space-2);
  text-align: center;
}

.booking-request-code__card {
  display: grid;
  grid-template-columns: minmax(0, 1fr) auto var(--touch-target);
  align-items: center;
  gap: var(--space-4);
  min-block-size: 4.25rem;
  padding: var(--space-2) var(--space-4);
  border-radius: var(--radius-lg);
}

.booking-request-code__label {
  color: var(--color-text-secondary);
  font-weight: var(--font-weight-semibold);
  text-align: start;
}

.booking-request-code__value {
  color: var(--color-success);
  font-size: var(--font-size-2xl);
  font-weight: var(--font-weight-bold);
  letter-spacing: 0.04em;
}

.booking-request-code p {
  margin: 0;
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
}
</style>
