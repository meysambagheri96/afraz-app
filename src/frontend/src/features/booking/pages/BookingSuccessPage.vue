<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import AppPageHeader from '../../../components/shared/AppPageHeader.vue'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import type { AppIconName } from '../../../components/ui/icon.types'
import BookingInformationCard from '../components/BookingInformationCard.vue'
import BookingRequestCodeCard from '../components/BookingRequestCodeCard.vue'
import BookingSuccessHero from '../components/BookingSuccessHero.vue'

const route = useRoute()
const router = useRouter()

const dateLabel = computed(() => {
  const value = route.query.dateLabel
  return typeof value === 'string' && value.trim() ? value : 'دوشنبه ۱۲ فروردین ۱۴۰۴'
})

const userRows: readonly {
  label: string
  value: string
  icon: AppIconName
  direction?: 'rtl' | 'ltr'
}[] = [
  { label: 'نام و نام خانوادگی', value: 'علی احمدی', icon: 'profile' },
  { label: 'شماره موبایل', value: '0912 123 4567', icon: 'phone', direction: 'ltr' },
  { label: 'ایمیل', value: 'ali.ahmadi@email.com', icon: 'mail', direction: 'ltr' },
]

const detailRows = computed(() => [
  { label: 'خدمات انتخابی', value: 'آتلیه کودک', icon: 'camera' as const },
  { label: 'تاریخ رزرو', value: dateLabel.value, icon: 'booking' as const },
  { label: 'استودیو', value: 'آتلیه افراز قم - شعبه مرکزی', icon: 'location' as const },
  { label: 'برای', value: 'علی احمدی', icon: 'profile' as const },
  { label: 'مبلغ بیعانه', value: '۷۰۰,۰۰۰ تومان', icon: 'wallet' as const, tone: 'warning' as const },
])
</script>

<template>
  <article class="booking-success-page">
    <AppPageHeader
      title="جزئیات نوبت"
      subtitle="اطلاعات رزرو و جزئیات درخواست شما"
      :back-to="{ name: 'home' }"
    />

    <BookingSuccessHero />
    <BookingRequestCodeCard code="84273" />

    <section
      class="booking-success-page__section"
      aria-labelledby="booking-user-title"
    >
      <h2
        id="booking-user-title"
        class="text-section-title"
      >
        اطلاعات کاربر
      </h2>
      <BookingInformationCard :rows="userRows" />
    </section>

    <section
      class="booking-success-page__section"
      aria-labelledby="booking-request-title"
    >
      <h2
        id="booking-request-title"
        class="text-section-title"
      >
        جزئیات درخواست
      </h2>
      <BookingInformationCard :rows="detailRows">
        <template #footer>
          <aside
            class="booking-success-page__notice"
            aria-label="تأیید ثبت بیعانه"
          >
            <div>
              <strong>بیعانه شما ثبت شده است.</strong>
              <span>این مبلغ از کل هزینه نهایی کسر خواهد شد.</span>
            </div>
            <span
              class="booking-success-page__notice-icon"
              aria-hidden="true"
            >
              <AppIcon
                name="shield"
                size="md"
              />
            </span>
          </aside>
        </template>
      </BookingInformationCard>
    </section>

    <AppButton
      class="booking-success-page__cta"
      size="lg"
      block
      @click="router.push({ name: 'bookings' })"
    >
      مشاهده نوبت‌های من
      <template #trailing>
        <AppIcon
          name="orders"
          size="sm"
        />
      </template>
    </AppButton>
  </article>
</template>

<style scoped>
.booking-success-page {
  display: grid;
  gap: var(--space-3);
  inline-size: 100%;
  min-block-size: calc(100dvh - max(14px, var(--safe-area-top)));
  padding-block-end: max(var(--space-6), var(--safe-area-bottom));
}

.booking-success-page > :deep(.app-page-header) {
  margin-block-end: calc(var(--space-5) * -1);
}

.booking-success-page__section {
  display: grid;
  gap: var(--space-2);
}

.booking-success-page__section > h2 {
  color: var(--color-text-primary);
  font-size: var(--font-size-lg);
}

.booking-success-page__notice {
  display: grid;
  grid-template-columns: minmax(0, 1fr) 2.75rem;
  align-items: center;
  gap: var(--space-3);
  margin-block-start: var(--space-2);
  padding: var(--space-2) var(--space-3);
  border-radius: var(--radius-md);
  color: var(--color-brand-dark);
  background: var(--color-success-soft);
}

.booking-success-page__notice > div {
  display: grid;
  gap: var(--space-1);
}

.booking-success-page__notice strong {
  font-size: var(--font-size-sm);
}

.booking-success-page__notice span {
  font-size: var(--font-size-xs);
}

.booking-success-page__notice-icon {
  display: grid;
  inline-size: 2.5rem;
  block-size: 2.5rem;
  place-items: center;
  border-radius: var(--radius-full);
  color: var(--color-brand-primary);
  background: color-mix(in srgb, var(--color-accent-mint) 18%, var(--color-surface));
}

.booking-success-page__cta {
  margin-block-start: var(--space-1);
  border-radius: var(--radius-full);
  box-shadow: 0 0.75rem 2rem rgb(7 93 105 / 18%);
  font-size: var(--font-size-base);
}

.booking-success-page__cta :deep(.app-button__label) {
  flex: 1;
  text-align: center;
}

@media (max-width: 22.5rem) {
  .booking-success-page {
    gap: var(--space-4);
  }

}
</style>
