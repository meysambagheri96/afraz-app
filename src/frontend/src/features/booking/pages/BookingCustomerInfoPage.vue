<script setup lang="ts">
import { computed, reactive, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { z } from 'zod'
import AppPageHeader from '../../../components/shared/AppPageHeader.vue'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import AppInput from '../../../components/ui/AppInput.vue'
import BookingDetailsCard from '../components/BookingDetailsCard.vue'
import BookingStickyAction from '../components/BookingStickyAction.vue'
import SelectedBookingDateCard from '../components/SelectedBookingDateCard.vue'

const route = useRoute()
const router = useRouter()

const form = reactive({
  firstName: '',
  lastName: '',
  mobile: '',
})
const errors = reactive<Partial<Record<keyof typeof form, string>>>({})
const announcement = ref('')

const bookingFormSchema = z.object({
  firstName: z.string().trim().min(1, 'نام خود را وارد کنید.'),
  lastName: z.string().trim().min(1, 'نام خانوادگی خود را وارد کنید.'),
  mobile: z.string().trim().regex(/^(?:09|۰۹)[0-9۰-۹]{9}$/, 'شماره موبایل باید با ۰۹ شروع شود و ۱۱ رقم باشد.'),
})

const selectedDateLabel = computed(() => {
  const dateLabel = route.query.dateLabel
  return typeof dateLabel === 'string' && dateLabel.trim()
    ? dateLabel
    : 'دوشنبه ۱۲ فروردین ۱۴۰۴'
})

function changeDate() {
  void router.push({ name: 'booking' })
}

function continueBooking() {
  errors.firstName = undefined
  errors.lastName = undefined
  errors.mobile = undefined
  announcement.value = ''

  const result = bookingFormSchema.safeParse(form)
  if (!result.success) {
    for (const issue of result.error.issues) {
      const field = issue.path[0] as keyof typeof form
      errors[field] ??= issue.message
    }
    announcement.value = 'لطفاً خطاهای فرم را بررسی کنید.'
    requestAnimationFrame(() => {
      document.querySelector<HTMLInputElement>('[aria-invalid="true"]')?.focus()
    })
    return
  }

  void router.push({
    name: 'booking-success',
    query: { dateLabel: selectedDateLabel.value },
  })
}
</script>

<template>
  <article class="booking-customer-page">
    <AppPageHeader
      title="رزرو نوبت"
      subtitle="مشخصات خود را وارد کنید"
      :back-to="{ name: 'booking' }"
    />

    <SelectedBookingDateCard
      :date-label="selectedDateLabel"
      @change="changeDate"
    />

    <form
      class="booking-customer-page__form"
      novalidate
      @submit.prevent="continueBooking"
    >
      <section
        class="booking-customer-page__customer"
        aria-labelledby="customer-info-title"
      >
        <h2
          id="customer-info-title"
          class="text-section-title"
        >
          مشخصات شما
        </h2>

        <div class="booking-customer-page__fields">
          <AppInput
            v-model="form.firstName"
            aria-label="نام"
            autocomplete="given-name"
            placeholder="نام"
            :error="errors.firstName"
          >
            <template #leading>
              <span class="booking-customer-page__field-icon"><AppIcon
                name="profile"
                size="md"
              /></span>
            </template>
          </AppInput>

          <AppInput
            v-model="form.lastName"
            aria-label="نام خانوادگی"
            autocomplete="family-name"
            placeholder="نام خانوادگی"
            :error="errors.lastName"
          >
            <template #leading>
              <span class="booking-customer-page__field-icon"><AppIcon
                name="profile"
                size="md"
              /></span>
            </template>
          </AppInput>

          <AppInput
            v-model="form.mobile"
            aria-label="شماره موبایل"
            autocomplete="tel"
            inputmode="tel"
            type="tel"
            placeholder="شماره موبایل"
            :error="errors.mobile"
          >
            <template #leading>
              <span class="booking-customer-page__field-icon"><AppIcon
                name="mobile"
                size="md"
              /></span>
            </template>
            <template #trailing>
              <span class="booking-customer-page__mobile-example">
                مثال:
                <bdi dir="ltr">۰۹۱۲ ۱۲۳ ۴۵۶۷</bdi>
              </span>
            </template>
          </AppInput>

          <p class="booking-customer-page__mobile-hint">
            کد تایید برای این شماره ارسال خواهد شد.
          </p>
        </div>
      </section>

      <BookingDetailsCard
        class="booking-customer-page__details"
        :date-label="selectedDateLabel"
      />

      <p class="booking-customer-page__security text-label">
        <AppIcon
          name="lock"
          size="xs"
        />
        اطلاعات شما محفوظ و امن است.
      </p>

      <BookingStickyAction>
        <AppButton
          class="booking-customer-page__cta"
          type="submit"
          size="lg"
          block
        >
          پرداخت
          <template #trailing>
            <AppIcon
              name="chevron-back"
              size="sm"
            />
          </template>
        </AppButton>
      </BookingStickyAction>

      <p
        class="visually-hidden"
        aria-live="polite"
      >
        {{ announcement }}
      </p>
    </form>
  </article>
</template>

<style scoped>
.booking-customer-page {
  inline-size: 100%;
  min-block-size: calc(100dvh - max(14px, var(--safe-area-top)));
  padding-block-end: calc(5.875rem + var(--safe-area-bottom));
}

.booking-customer-page__form,
.booking-customer-page__customer {
  display: grid;
}

.booking-customer-page__form {
  gap: var(--space-4);
  margin-block-start: var(--space-4);
}

.booking-customer-page__customer {
  gap: var(--space-3);
}

.booking-customer-page__customer > h2 {
  color: var(--color-text-primary);
  font-size: var(--font-size-lg);
}

.booking-customer-page__fields {
  display: grid;
  gap: var(--space-3);
}

.booking-customer-page__fields :deep(.app-field__control) {
  min-block-size: 3.5rem;
  padding-inline: var(--space-2) var(--space-4);
  border-color: var(--color-border-subtle);
  border-radius: var(--radius-md);
  background: var(--color-surface);
  box-shadow: var(--shadow-control);
}

.booking-customer-page__fields :deep(.app-field__input) {
  font-size: var(--font-size-sm);
}

.booking-customer-page__field-icon {
  display: grid;
  inline-size: 2.5rem;
  block-size: 2.5rem;
  place-items: center;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-full);
  color: var(--color-text-secondary);
  background: var(--color-surface-muted);
}

.booking-customer-page__mobile-example {
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  white-space: nowrap;
}

.booking-customer-page__mobile-hint {
  margin: calc(var(--space-1) * -1) var(--space-3) 0;
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  line-height: var(--line-height-caption);
}

.booking-customer-page__details {
  margin-block-start: 0;
}

.booking-customer-page__cta {
  border-radius: var(--radius-full);
  box-shadow: 0 0.75rem 2rem rgb(7 93 105 / 18%);
  font-size: var(--font-size-base);
}

.booking-customer-page__cta :deep(.app-button__label) {
  flex: 1;
  text-align: center;
}

.booking-customer-page__security {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: var(--space-2);
  color: var(--color-disabled);
}

@media (max-width: 22.5rem) {
  .booking-customer-page__mobile-example {
    display: none;
  }
}
</style>
