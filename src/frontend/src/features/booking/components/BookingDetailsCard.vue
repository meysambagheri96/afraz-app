<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
import BookingDetailRow from './BookingDetailRow.vue'

defineProps<{ dateLabel: string }>()
</script>

<template>
  <section
    class="booking-details app-surface-card"
    aria-labelledby="booking-details-title"
  >
    <h2
      id="booking-details-title"
      class="text-card-title"
    >
      جزئیات رزرو
    </h2>

    <div class="booking-details__rows">
      <BookingDetailRow
        label="خدمات انتخابی"
        value="آتلیه کودک"
        dot
      />
      <BookingDetailRow
        label="تاریخ"
        :value="dateLabel"
      />
      <BookingDetailRow
        label="ساعت"
        value="ساعت تعیین می‌شود"
        icon="clock"
      />
    </div>

    <div class="booking-details__deposit">
      <div class="booking-details__deposit-copy">
        <h3>مبلغ بیعانه</h3>
        <p>برای قطعی شدن رزرو نوبت</p>
      </div>
      <div class="booking-details__amount">
        <AppIcon
          name="info"
          size="sm"
        />
        <strong>۷۰۰,۰۰۰</strong>
        <span>تومان</span>
      </div>
    </div>

    <aside
      class="booking-details__notice"
      aria-label="شرایط بازگشت بیعانه"
    >
      <span
        class="booking-details__notice-icon"
        aria-hidden="true"
      >
        <AppIcon
          name="shield"
          size="md"
        />
      </span>
      <p>در صورت لغو نوبت تا ۲۴ ساعت قبل از موعد، بیعانه به شما بازگردانده می‌شود.</p>
    </aside>
  </section>
</template>

<style scoped>
.booking-details {
  padding: var(--space-4);
  border-radius: var(--radius-xl);
}

.booking-details > h2 {
  color: var(--color-text-primary);
}

.booking-details__rows {
  display: grid;
  margin-block-start: var(--space-2);
  padding-block-end: var(--space-2);
  border-block-end: 1px dashed var(--color-border-subtle);
}

.booking-details__deposit {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-3);
  margin-block-start: var(--space-3);
  padding: var(--space-3);
  border: 1px solid color-mix(in srgb, var(--color-accent-yellow) 32%, transparent);
  border-radius: var(--radius-md);
  background: color-mix(in srgb, var(--color-warning-soft) 55%, var(--color-surface));
}

.booking-details__deposit-copy h3,
.booking-details__deposit-copy p,
.booking-details__notice p {
  margin: 0;
}

.booking-details__deposit-copy h3 {
  font-size: var(--font-size-base);
  font-weight: var(--font-weight-bold);
}

.booking-details__deposit-copy p {
  margin-block-start: var(--space-1);
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
}

.booking-details__amount {
  display: inline-flex;
  flex: none;
  align-items: baseline;
  gap: var(--space-1);
  color: var(--color-text-primary);
}

.booking-details__amount svg {
  align-self: center;
  color: var(--color-text-secondary);
}

.booking-details__amount strong {
  font-size: var(--font-size-lg);
  font-weight: var(--font-weight-bold);
}

.booking-details__amount span {
  font-size: var(--font-size-xs);
}

.booking-details__notice {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  margin-block-start: var(--space-2);
  padding: var(--space-2) var(--space-3);
  border: 1px solid color-mix(in srgb, var(--color-accent-yellow) 24%, transparent);
  border-radius: var(--radius-md);
  color: #7b5a1d;
  background: color-mix(in srgb, var(--color-warning-soft) 78%, var(--color-surface));
}

.booking-details__notice-icon {
  display: grid;
  inline-size: 2.25rem;
  block-size: 2.25rem;
  flex: none;
  place-items: center;
  border-radius: var(--radius-full);
  color: var(--color-warning);
  background: color-mix(in srgb, var(--color-accent-yellow) 12%, var(--color-surface));
}

.booking-details__notice p {
  font-size: var(--font-size-xs);
  line-height: var(--line-height-body);
}

@media (max-width: 22.5rem) {
  .booking-details {
    padding: var(--space-4);
  }

  .booking-details__deposit {
    align-items: flex-start;
    flex-direction: column;
  }

  .booking-details__amount {
    align-self: flex-end;
  }
}
</style>
