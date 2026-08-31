<template>
  <section
    class="booking-success-hero"
    aria-labelledby="booking-success-title"
  >
    <div
      class="booking-success-hero__mark"
      aria-hidden="true"
    >
      <span class="booking-success-hero__confetti" />
      <span class="booking-success-hero__circle">
        <AppIcon
          name="check"
          size="xl"
        />
      </span>
    </div>
    <h2
      id="booking-success-title"
      class="text-section-title"
    >
      رزرو نوبت با موفقیت ثبت شد
    </h2>
    <p>از اعتماد شما سپاسگزاریم.</p>
  </section>
</template>

<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
</script>

<style scoped>
.booking-success-hero {
  display: grid;
  justify-items: center;
  gap: var(--space-1);
  padding-block: var(--space-1) var(--space-2);
  text-align: center;
}

.booking-success-hero__mark {
  position: relative;
  display: grid;
  inline-size: 5rem;
  block-size: 4.25rem;
  place-items: center;
}

.booking-success-hero__confetti {
  position: absolute;
  inset: 0;
  background: var(--pattern-confetti) center / contain no-repeat;
}

.booking-success-hero__circle {
  z-index: var(--z-content);
  display: grid;
  inline-size: 3.5rem;
  block-size: 3.5rem;
  place-items: center;
  border: var(--space-2) solid color-mix(in srgb, var(--color-success) 14%, transparent);
  border-radius: var(--radius-full);
  color: var(--color-surface);
  background: var(--color-success);
  box-shadow: 0 0.75rem 2rem color-mix(in srgb, var(--color-success) 20%, transparent);
}

.booking-success-hero h2,
.booking-success-hero p {
  margin: 0;
}

.booking-success-hero h2 {
  color: var(--color-text-primary);
  font-size: var(--font-size-xl);
}

.booking-success-hero p {
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
}
</style>
