<template>
  <div class="booking-sticky-action">
    <div class="booking-sticky-action__content">
      <slot />
    </div>
  </div>
</template>

<style scoped>
.booking-sticky-action {
  position: fixed;
  inset-inline: 0;
  inset-block-end: 0;
  z-index: var(--z-nav);
  padding-block-start: 8px;
  padding-block-end: max(6px, calc(var(--safe-area-bottom) - 12px));
  padding-inline-start: max(21px, var(--safe-area-inline-start));
  padding-inline-end: max(21px, var(--safe-area-inline-end));
  pointer-events: none;
}

.booking-sticky-action__content {
  inline-size: min(100%, var(--mobile-canvas-max-width));
  margin-inline: auto;
  pointer-events: auto;
}
</style>
