<script setup lang="ts">
import { computed, ref } from 'vue'
import LoginStep from './LoginStep.vue'
import OtpStep from './OtpStep.vue'

defineEmits<{ close: []; success: [] }>()

const step = ref<'login' | 'otp'>('login')
const mobile = ref('')
const internationalMobile = computed(() => `+98${mobile.value}`)

function showOtp(value: string) {
  mobile.value = value
  step.value = 'otp'
}
</script>

<template>
  <Transition
    name="auth-step"
    mode="out-in"
  >
    <LoginStep
      v-if="step === 'login'"
      key="login"
      :initial-mobile="mobile"
      @close="$emit('close')"
      @submit="showOtp"
    />
    <OtpStep
      v-else
      key="otp"
      :mobile="internationalMobile"
      @back="step = 'login'"
      @success="$emit('success')"
    />
  </Transition>
</template>

<style scoped>
.auth-step-enter-active,
.auth-step-leave-active {
  transition:
    opacity var(--motion-base) var(--ease-standard),
    transform var(--motion-base) var(--ease-emphasized);
}

.auth-step-enter-from {
  opacity: 0;
  transform: translateX(-1rem);
}

.auth-step-leave-to {
  opacity: 0;
  transform: translateX(1rem);
}

@media (prefers-reduced-motion: reduce) {
  .auth-step-enter-active,
  .auth-step-leave-active {
    transition: none;
  }
}
</style>
