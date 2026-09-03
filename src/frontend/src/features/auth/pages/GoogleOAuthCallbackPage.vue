<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AuthBrand from '../components/AuthBrand.vue'
import { consumeGoogleOAuthState } from '../google-oauth'

const route = useRoute()
const router = useRouter()

function queryValue(value: unknown) {
  if (Array.isArray(value)) return String(value[0] ?? '')
  return typeof value === 'string' ? value : ''
}

const code = computed(() => queryValue(route.query.code))
const error = computed(() => queryValue(route.query.error))
const errorDescription = computed(() => queryValue(route.query.error_description))
const returnedState = computed(() => queryValue(route.query.state))
const stateIsValid = consumeGoogleOAuthState(returnedState.value || null)
const hasResult = computed(() => Boolean(code.value || error.value))
</script>

<template>
  <main class="google-callback-page">
    <section class="google-callback-page__card" aria-labelledby="google-callback-title">
      <AuthBrand />

      <div
        class="google-callback-page__status"
        :class="{ 'google-callback-page__status--error': error || !hasResult }"
        aria-hidden="true"
      >
        <AppIcon :name="code ? 'check' : 'info'" size="lg" />
      </div>

      <div class="google-callback-page__heading">
        <h1 id="google-callback-title">نتیجه بازگشت از گوگل</h1>
        <p v-if="code">کد OAuth از گوگل دریافت شد.</p>
        <p v-else-if="error">گوگل نتیجه ناموفق برگرداند.</p>
        <p v-else>هیچ نتیجه OAuth در آدرس بازگشت وجود ندارد.</p>
      </div>

      <dl v-if="hasResult" class="google-callback-page__result">
        <div v-if="code">
          <dt>Authorization code</dt>
          <dd><code dir="ltr">{{ code }}</code></dd>
        </div>
        <div v-if="error">
          <dt>Error</dt>
          <dd><code dir="ltr">{{ error }}</code></dd>
        </div>
        <div v-if="errorDescription">
          <dt>توضیحات خطا</dt>
          <dd>{{ errorDescription }}</dd>
        </div>
        <div v-if="returnedState">
          <dt>State</dt>
          <dd><code dir="ltr">{{ returnedState }}</code></dd>
        </div>
      </dl>

      <p v-if="stateIsValid === false" class="google-callback-page__warning" role="alert">
        مقدار state با درخواست اولیه مطابقت ندارد.
      </p>

      <p class="google-callback-page__note">
        این صفحه فقط برای بررسی نتیجه است؛ هیچ کاربر، نشست یا توکن ورود ایجاد نشده است.
      </p>

      <AppButton size="lg" block @click="router.replace({ name: 'home' })">
        بازگشت به افراز
      </AppButton>
    </section>
  </main>
</template>

<style scoped>
.google-callback-page {
  display: grid;
  min-block-size: 100dvh;
  padding:
    max(var(--space-6), var(--safe-area-top))
    calc(var(--space-4) + var(--safe-area-inline-end))
    max(var(--space-6), var(--safe-area-bottom))
    calc(var(--space-4) + var(--safe-area-inline-start));
  place-items: center;
  background: var(--color-background);
}

.google-callback-page__card {
  display: grid;
  inline-size: min(100%, 30rem);
  gap: var(--space-5);
  padding: var(--space-6);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-xl);
  background: var(--color-surface);
  box-shadow: var(--shadow-surface);
}

.google-callback-page__status {
  display: grid;
  inline-size: var(--touch-target);
  block-size: var(--touch-target);
  margin-inline: auto;
  place-items: center;
  border-radius: var(--radius-full);
  color: var(--color-success);
  background: color-mix(in srgb, var(--color-success) 12%, var(--color-surface));
}

.google-callback-page__status--error {
  color: var(--color-danger);
  background: var(--color-danger-soft);
}

.google-callback-page__heading {
  display: grid;
  gap: var(--space-2);
  text-align: center;
}

.google-callback-page__heading h1,
.google-callback-page__heading p,
.google-callback-page__note,
.google-callback-page__warning {
  margin: 0;
}

.google-callback-page__heading h1 {
  font-size: var(--font-size-xl);
}

.google-callback-page__heading p,
.google-callback-page__note {
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
}

.google-callback-page__result {
  display: grid;
  gap: var(--space-3);
  margin: 0;
}

.google-callback-page__result > div {
  display: grid;
  gap: var(--space-2);
}

.google-callback-page__result dt {
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  font-weight: var(--font-weight-semibold);
}

.google-callback-page__result dd {
  min-inline-size: 0;
  margin: 0;
  padding: var(--space-3);
  overflow-wrap: anywhere;
  border-radius: var(--radius-control);
  background: var(--color-surface-muted);
  font-size: var(--font-size-sm);
}

.google-callback-page__result code {
  font: inherit;
  unicode-bidi: plaintext;
}

.google-callback-page__warning {
  padding: var(--space-3);
  border-radius: var(--radius-control);
  color: var(--color-danger);
  background: var(--color-danger-soft);
  font-size: var(--font-size-sm);
}

.google-callback-page__note {
  text-align: center;
}
</style>
