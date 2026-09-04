<script setup lang="ts">
import { computed, ref } from 'vue'
import { requestOtp, verifyOtp } from '../api/auth.api'
import { useAuthStore } from '../stores/auth.store'
import LoginStep from './LoginStep.vue'
import OtpStep from './OtpStep.vue'

const emit = defineEmits<{ close: []; success: [] }>()
const authStore = useAuthStore()

const step = ref<'login' | 'otp'>('login')
const mobile = ref('')
const isSubmitting = ref(false)
const errorMessage = ref('')
const internationalMobile = computed(() => `+98${mobile.value}`)

async function showOtp(value: string) {
  if (isSubmitting.value) return

  isSubmitting.value = true
  errorMessage.value = ''
  mobile.value = value
  try {
    await requestOtp(value)
    step.value = 'otp'
  } catch {
    errorMessage.value = 'ارسال کد ورود انجام نشد. لطفاً دوباره تلاش کنید.'
  } finally {
    isSubmitting.value = false
  }
}

async function completeOtp(code: string) {
  if (isSubmitting.value) return

  isSubmitting.value = true
  errorMessage.value = ''
  try {
    const normalizedCode = code.replace(/[۰-۹]/g, (digit) => String('۰۱۲۳۴۵۶۷۸۹'.indexOf(digit)))
    authStore.setSession(await verifyOtp(mobile.value, normalizedCode))
    emit('success')
  } catch {
    errorMessage.value = 'کد واردشده نادرست یا منقضی شده است.'
  } finally {
    isSubmitting.value = false
  }
}

async function resendOtp() {
  if (isSubmitting.value) return

  isSubmitting.value = true
  errorMessage.value = ''
  try {
    await requestOtp(mobile.value)
  } catch {
    errorMessage.value = 'ارسال مجدد کد انجام نشد. لطفاً دوباره تلاش کنید.'
  } finally {
    isSubmitting.value = false
  }
}

function returnToLogin() {
  errorMessage.value = ''
  step.value = 'login'
}
</script>

<template>
  <Transition name="auth-step" mode="out-in">
    <LoginStep
      v-if="step === 'login'"
      key="login"
      :initial-mobile="mobile"
      :loading="isSubmitting"
      :server-error="errorMessage"
      @close="$emit('close')"
      @submit="showOtp"
    />
    <OtpStep
      v-else
      key="otp"
      :mobile="internationalMobile"
      :loading="isSubmitting"
      :server-error="errorMessage"
      @back="returnToLogin"
      @verify="completeOtp"
      @resend="resendOtp"
    />
  </Transition>
</template>

<style scoped>
.auth-step-enter-active,
.auth-step-leave-active {
  transition: opacity var(--motion-page) var(--ease-standard);
}

.auth-step-enter-from,
.auth-step-leave-to {
  opacity: 0;
}

@media (prefers-reduced-motion: reduce) {
  .auth-step-enter-active,
  .auth-step-leave-active {
    transition: none;
  }
}
</style>
