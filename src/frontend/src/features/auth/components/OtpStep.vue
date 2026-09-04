<script setup lang="ts">
import { computed, onBeforeUnmount, ref, watch } from 'vue'
import { useForm } from 'vee-validate'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'
import BookingStickyAction from '../../booking/components/BookingStickyAction.vue'
import { otpSchema } from '../schemas/auth.schema'
import OtpInput from './OtpInput.vue'

const props = withDefaults(
  defineProps<{ mobile: string; loading?: boolean; serverError?: string }>(),
  { loading: false, serverError: '' },
)
const emit = defineEmits<{ back: []; verify: [code: string]; resend: [] }>()
const remainingSeconds = ref(105)
const resendAnnouncement = ref('')
const otpResetKey = ref(0)
const isCompleting = ref(false)

const { errors, handleSubmit, resetForm, setFieldValue, submitCount } = useForm<{ otp: string }>({
  initialValues: { otp: '' },
  validationSchema: {
    otp(value: unknown) {
      const result = otpSchema.safeParse(String(value ?? ''))
      return result.success || result.error.issues[0]?.message || 'کد تأیید معتبر نیست.'
    },
  },
})
const submit = handleSubmit((values) => emit('verify', values.otp))
const displayedError = computed(
  () => (submitCount.value > 0 ? errors.value.otp : '') || props.serverError,
)
const showOtpError = computed(() => Boolean(displayedError.value))

const persianMobile = computed(() => {
  const normalized = props.mobile.replace(/[۰-۹]/g, (digit) => String('۰۱۲۳۴۵۶۷۸۹'.indexOf(digit)))
  const formatted = normalized.replace(/^(\+98)(\d{3})(\d{3})(\d{4})$/, '$1 $2 $3 $4')
  return formatted.replace(/\d/g, (digit) => '۰۱۲۳۴۵۶۷۸۹'[Number(digit)] ?? digit)
})

const timerLabel = computed(() => {
  const minutes = Math.floor(remainingSeconds.value / 60)
  const seconds = remainingSeconds.value % 60
  return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`.replace(
    /\d/g,
    (digit) => '۰۱۲۳۴۵۶۷۸۹'[Number(digit)] ?? digit,
  )
})

const timer = window.setInterval(() => {
  if (remainingSeconds.value > 0) remainingSeconds.value -= 1
}, 1000)

function resendCode() {
  remainingSeconds.value = 105
  resendAnnouncement.value = 'کد تأیید جدید به‌صورت آزمایشی ارسال شد.'
  resetForm({ values: { otp: '' } })
  otpResetKey.value += 1
  emit('resend')
}

function updateOtp(value: string) {
  setFieldValue('otp', value, false)

  if (!isCompleting.value && !props.loading && otpSchema.safeParse(value).success) {
    isCompleting.value = true
    emit('verify', value)
  }
}

watch(
  () => props.loading,
  (loading, wasLoading) => {
    if (!loading && wasLoading && props.serverError) isCompleting.value = false
  },
)

onBeforeUnmount(() => window.clearInterval(timer))
</script>

<template>
  <section class="auth-otp-step" aria-labelledby="auth-otp-title">
    <header class="auth-otp-step__header">
      <AppIconButton label="بازگشت به ورود" variant="ghost" @click="$emit('back')">
        <AppIcon name="arrow-forward" size="lg" />
      </AppIconButton>
    </header>

    <form class="auth-otp-step__form" novalidate @submit.prevent="submit">
      <div class="auth-otp-step__heading">
        <h1 id="auth-otp-title" class="text-page-title">تأیید شماره موبایل</h1>
        <p>کد تأیید ۵ رقمی به شماره موبایل شما ارسال شد.</p>
      </div>

      <div class="auth-otp-step__mobile">
        <bdi dir="ltr">{{ persianMobile }}</bdi>
        <AppIconButton
          label="ویرایش شماره موبایل"
          variant="secondary"
          size="sm"
          @click="$emit('back')"
        >
          <AppIcon name="edit" size="sm" />
        </AppIconButton>
      </div>

      <div class="auth-otp-step__code">
        <OtpInput :key="otpResetKey" :invalid="showOtpError" @update:model-value="updateOtp" />
        <p v-if="showOtpError" role="alert">
          {{ displayedError }}
        </p>
      </div>

      <div class="auth-otp-step__resend">
        <span>کد را دریافت نکردید؟</span>
        <button type="button" :disabled="remainingSeconds > 0" @click="resendCode">
          ارسال مجدد
        </button>
        <bdi v-if="remainingSeconds > 0" dir="ltr">{{ timerLabel }}</bdi>
      </div>

      <div class="auth-otp-step__footer">
        <p class="auth-otp-step__security text-label">
          <AppIcon name="lock" size="xs" />
          اطلاعات شما محفوظ و امن است.
        </p>
      </div>

      <BookingStickyAction>
        <AppButton type="submit" size="lg" block :loading="loading" loading-label="در حال بررسی کد">
          تأیید و ورود
          <template #trailing>
            <AppIcon name="chevron-back" size="sm" />
          </template>
        </AppButton>
      </BookingStickyAction>

      <p class="visually-hidden" aria-live="polite">
        {{ resendAnnouncement }}
      </p>
    </form>
  </section>
</template>

<style scoped>
.auth-otp-step {
  display: grid;
  grid-template-rows: auto minmax(0, 1fr);
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: 100dvh;
  margin-inline: auto;
  padding: max(var(--space-4), var(--safe-area-top)) var(--space-5)
    calc(5.875rem + var(--safe-area-bottom));
  background: var(--color-background);
}

.auth-otp-step__header {
  display: flex;
  min-block-size: var(--touch-target);
  justify-content: flex-start;
}

.auth-otp-step__form {
  display: flex;
  align-items: stretch;
  flex-direction: column;
  padding-block-start: clamp(5rem, 14dvh, 8rem);
}

.auth-otp-step__heading {
  display: grid;
  gap: var(--space-4);
  text-align: center;
}

.auth-otp-step__heading h1,
.auth-otp-step__heading p {
  margin: 0;
}

.auth-otp-step__heading h1 {
  color: var(--color-text-primary);
  font-size: var(--font-size-2xl);
}

.auth-otp-step__heading p {
  color: var(--color-text-secondary);
  line-height: var(--line-height-body);
}

.auth-otp-step__mobile {
  display: flex;
  min-block-size: var(--control-height-lg);
  align-items: center;
  justify-content: center;
  gap: var(--space-3);
  margin-block-start: var(--space-8);
  color: var(--color-text-secondary);
  font-size: var(--font-size-base);
  font-weight: var(--font-weight-medium);
}

.auth-otp-step__mobile :deep(.app-icon-button) {
  border-radius: var(--radius-sm);
  color: var(--color-text-primary);
  border-color: var(--color-border-subtle);
}

.auth-otp-step__code {
  display: grid;
  gap: var(--space-2);
  margin-block-start: var(--space-6);
}

.auth-otp-step__code > p {
  margin: 0;
  color: var(--color-danger);
  font-size: var(--font-size-xs);
  text-align: center;
}

.auth-otp-step__resend {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: var(--space-2);
  margin-block-start: var(--space-6);
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
}

.auth-otp-step__resend button {
  min-block-size: var(--touch-target);
  padding: 0;
  border: 0;
  color: var(--color-brand-primary);
  background: transparent;
  font-family: var(--font-family-sans);
  font-weight: var(--font-weight-semibold);
  cursor: pointer;
}

.auth-otp-step__resend button:disabled {
  color: var(--color-text-secondary);
  cursor: default;
}

.auth-otp-step__resend bdi {
  color: var(--color-accent-pink);
  font-weight: var(--font-weight-semibold);
}

.auth-otp-step__footer {
  display: grid;
  gap: var(--space-3);
  margin-block-start: auto;
}

.auth-otp-step__security {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: var(--space-2);
  margin: 0;
  color: var(--color-disabled);
}

@media (max-height: 44rem) {
  .auth-otp-step__form {
    padding-block-start: var(--space-8);
  }
}
</style>
