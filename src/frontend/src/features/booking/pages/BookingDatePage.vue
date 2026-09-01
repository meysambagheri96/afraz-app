<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import AppPageHeader from '../../../components/shared/AppPageHeader.vue'
import AppButton from '../../../components/ui/AppButton.vue'
import AppIcon from '../../../components/ui/AppIcon.vue'
import BookingInfoCard from '../components/BookingInfoCard.vue'
import BookingStickyAction from '../components/BookingStickyAction.vue'
import CalendarStatusLegend from '../components/CalendarStatusLegend.vue'
import PersianCalendar from '../components/PersianCalendar.vue'

const selectedDateKey = ref<string | null>(null)
const selectedDateLabel = ref<string | null>(null)
const router = useRouter()

function handleDateSelection(dateKey: string | null, label: string | null) {
  selectedDateKey.value = dateKey
  selectedDateLabel.value = label
}

function showHelp() {
  document.querySelector('#booking-calendar-help')?.scrollIntoView({
    behavior: 'smooth',
    block: 'center',
  })
}

function continueBooking() {
  if (!selectedDateLabel.value) return
  void router.push({
    name: 'booking-create',
    query: {
      dateKey: selectedDateKey.value,
      dateLabel: selectedDateLabel.value,
    },
  })
}
</script>

<template>
  <article class="booking-date-page">
    <AppPageHeader
      title="رزرو نوبت"
      subtitle="روز مورد نظر خود را انتخاب کنید"
      :back-to="{ name: 'home' }"
      show-help
      @help="showHelp"
    />

    <PersianCalendar @select="handleDateSelection" />
    <CalendarStatusLegend class="booking-date-page__section" />
    <BookingInfoCard class="booking-date-page__section" />

    <BookingStickyAction>
      <AppButton
        class="booking-date-page__cta"
        size="lg"
        block
        :disabled="!selectedDateKey"
        @click="continueBooking"
      >
        مرحله بعد
        <template #trailing>
          <AppIcon
            name="chevron-back"
            size="sm"
          />
        </template>
      </AppButton>
    </BookingStickyAction>
  </article>
</template>

<style scoped>
.booking-date-page {
  inline-size: 100%;
  min-block-size: calc(100dvh - max(14px, var(--safe-area-top)));
  padding-block-end: calc(5.875rem + var(--safe-area-bottom));
}

.booking-date-page__section {
  margin-block-start: var(--space-3);
}

.booking-date-page__cta {
  border-radius: var(--radius-full);
  box-shadow: 0 0.75rem 2rem rgb(7 93 105 / 18%);
  font-size: var(--font-size-base);
}

.booking-date-page__cta :deep(.app-button__label) {
  flex: 1;
  text-align: center;
}

.booking-date-page__cta :deep(svg) {
  inline-size: 1.5rem;
  block-size: 1.5rem;
}
</style>
