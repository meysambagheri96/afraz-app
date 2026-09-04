<script setup lang="ts">
import { ref, watch } from 'vue'
import { useForm } from 'vee-validate'
import AppButton from '../../../components/ui/AppButton.vue'
import AppDivider from '../../../components/ui/AppDivider.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppIconButton from '../../../components/ui/AppIconButton.vue'
import AppInput from '../../../components/ui/AppInput.vue'
import BookingStickyAction from '../../booking/components/BookingStickyAction.vue'
import { startGoogleOAuthRedirect } from '../google-oauth'
import { mobileSchema, normalizeMobileDigits } from '../schemas/auth.schema'
import AuthBrand from './AuthBrand.vue'

const props = withDefaults(
  defineProps<{ initialMobile?: string; loading?: boolean; serverError?: string }>(),
  { initialMobile: '', loading: false, serverError: '' },
)
const emit = defineEmits<{ close: []; submit: [mobile: string] }>()
const googleAnnouncement = ref('')

const { defineField, errors, handleSubmit } = useForm<{ mobile: string }>({
  initialValues: { mobile: props.initialMobile },
  validationSchema: {
    mobile(value: unknown) {
      const result = mobileSchema.safeParse(String(value ?? ''))
      return result.success || result.error.issues[0]?.message || 'شماره موبایل معتبر نیست.'
    },
  },
})
const [mobile] = defineField('mobile')
const submit = handleSubmit((values) => emit('submit', normalizeMobileDigits(values.mobile)))

watch(
  () => props.initialMobile,
  (value) => {
    mobile.value = value
  },
  { immediate: true },
)

function continueWithGoogle() {
  try {
    startGoogleOAuthRedirect()
  } catch {
    googleAnnouncement.value = 'تنظیمات ورود با گوگل کامل نیست. لطفاً دوباره تلاش کنید.'
  }
}
</script>

<template>
  <section class="auth-login-step" aria-labelledby="auth-login-title">
    <header class="auth-login-step__header">
      <AppIconButton label="بستن ورود" variant="ghost" @click="$emit('close')">
        <AppIcon name="arrow-forward" size="lg" />
      </AppIconButton>
    </header>

    <AuthBrand />

    <form class="auth-login-step__form" novalidate @submit.prevent="submit">
      <h2 id="auth-login-title" class="visually-hidden">ورود به آتلیه افراز</h2>
      <div class="auth-login-step__content">
        <AppInput
          v-model="mobile"
          aria-label="شماره موبایل"
          type="tel"
          inputmode="numeric"
          autocomplete="tel-national"
          dir="ltr"
          size="lg"
          :maxlength="10"
          placeholder="شماره موبایل ۱۰ رقمی"
          :error="errors.mobile || serverError"
          :loading="loading"
        >
          <template #leading>
            <AppIcon name="phone" size="md" />
          </template>
          <template #trailing>
            <bdi class="auth-login-step__prefix" dir="ltr">+98</bdi>
          </template>
        </AppInput>

        <AppDivider label="یا" />

        <button
          type="button"
          class="auth-login-step__google"
          aria-label="ادامه با گوگل"
          @click="continueWithGoogle"
        >
          <svg viewBox="0 0 24 24" aria-hidden="true">
            <path
              fill="#4285F4"
              d="M21.6 12.2c0-.7-.1-1.5-.2-2.2H12v4.1h5.4a4.6 4.6 0 0 1-2 3v2.7h3.3c1.9-1.8 2.9-4.4 2.9-7.6Z"
            />
            <path
              fill="#34A853"
              d="M12 22c2.7 0 5-.9 6.7-2.4l-3.3-2.7c-.9.6-2.1 1-3.4 1-2.6 0-4.8-1.8-5.6-4.2H3v2.8A10 10 0 0 0 12 22Z"
            />
            <path
              fill="#FBBC05"
              d="M6.4 13.7A6 6 0 0 1 6 12c0-.6.1-1.2.4-1.7V7.5H3A10 10 0 0 0 2 12c0 1.6.4 3.1 1 4.5l3.4-2.8Z"
            />
            <path
              fill="#EA4335"
              d="M12 6.1c1.5 0 2.8.5 3.9 1.5l2.9-2.9A9.8 9.8 0 0 0 3 7.5l3.4 2.8A6 6 0 0 1 12 6.1Z"
            />
          </svg>
          <span class="visually-hidden">ادامه با گوگل</span>
        </button>
      </div>

      <div class="auth-login-step__footer">
        <p class="auth-login-step__terms">
          <AppIcon name="shield" size="xs" />
          ورود به معنای پذیرش
          <RouterLink :to="{ name: 'terms' }"> قوانین </RouterLink>
          و
          <RouterLink :to="{ name: 'privacy' }"> حریم خصوصی </RouterLink>
          است.
        </p>
      </div>

      <BookingStickyAction>
        <AppButton type="submit" size="lg" block :loading="loading" loading-label="در حال ارسال کد">
          دریافت کد ورود
          <template #trailing>
            <AppIcon name="chevron-back" size="sm" />
          </template>
        </AppButton>
      </BookingStickyAction>

      <p class="visually-hidden" aria-live="polite">
        {{ googleAnnouncement }}
      </p>
    </form>
  </section>
</template>

<style scoped>
.auth-login-step {
  display: grid;
  grid-template-rows: auto auto minmax(0, 1fr);
  gap: var(--space-3);
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: 100dvh;
  margin-inline: auto;
  padding: max(var(--space-4), var(--safe-area-top)) var(--space-5)
    calc(5.875rem + var(--safe-area-bottom));
  background: var(--color-background);
}

.auth-login-step__header {
  display: flex;
  min-block-size: var(--touch-target);
  justify-content: flex-start;
}

.auth-login-step__form {
  display: flex;
  min-block-size: 0;
  flex-direction: column;
}

.auth-login-step__content {
  display: grid;
  gap: var(--space-4);
  inline-size: 100%;
  margin-block: auto;
}

.auth-login-step__form :deep(.app-field__control) {
  padding-inline: var(--space-4);
  border-radius: var(--radius-md);
  background: color-mix(in srgb, var(--color-surface) 88%, transparent);
}

.auth-login-step__form :deep(.app-field__input) {
  direction: ltr;
  text-align: left;
}

.auth-login-step__form :deep(.app-field__input::placeholder) {
  direction: rtl;
  text-align: right;
}

.auth-login-step__prefix {
  padding-inline-start: var(--space-3);
  border-inline-start: 1px solid var(--color-border-subtle);
  color: var(--color-text-secondary);
  font-size: var(--font-size-base);
}

.auth-login-step__google {
  display: grid;
  inline-size: 3.5rem;
  block-size: 3.5rem;
  margin-inline: auto;
  place-items: center;
  padding: 0;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-full);
  background: var(--color-surface);
  box-shadow: var(--shadow-control);
  cursor: pointer;
  transition: transform var(--motion-fast) var(--ease-standard);
}

.auth-login-step__google:active {
  transform: scale(0.97);
}

.auth-login-step__google:focus-visible {
  outline: 0;
  box-shadow: var(--focus-ring);
}

.auth-login-step__google svg {
  inline-size: 1.75rem;
  block-size: 1.75rem;
}

.auth-login-step__footer {
  display: grid;
  gap: var(--space-4);
  margin-block-start: var(--space-6);
}

.auth-login-step__terms {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-wrap: wrap;
  gap: 0 var(--space-1);
  margin: 0;
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  text-align: center;
}

.auth-login-step__terms a {
  color: var(--color-brand-primary);
  font-weight: var(--font-weight-semibold);
  text-decoration: none;
}
</style>
