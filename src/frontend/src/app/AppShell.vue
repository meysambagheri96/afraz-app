<script setup lang="ts">
import { RouterView } from 'vue-router'
import BottomNavigation from '../components/shared/BottomNavigation.vue'
</script>

<template>
  <div class="application-shell">
    <a class="application-shell__skip-link" href="#main-content">رفتن به محتوای اصلی</a>
    <main id="main-content" class="application-shell__content" tabindex="-1">
      <RouterView />
    </main>
    <BottomNavigation />
  </div>
</template>

<style scoped>
.application-shell {
  min-block-size: 100dvh;
  color: var(--color-text-primary);
  background: var(--color-background);
}

.application-shell__content {
  inline-size: min(100%, var(--mobile-canvas-max-width));
  min-block-size: 100dvh;
  margin-inline: auto;
  padding-block-start: max(var(--space-3), var(--safe-area-top));
  padding-block-end: calc(
    var(--bottom-nav-height) + max(var(--space-2), var(--safe-area-bottom)) + var(--space-6)
  );
  padding-inline: calc(var(--page-gutter) + var(--safe-area-inline-start))
    calc(var(--page-gutter) + var(--safe-area-inline-end));
}

.application-shell__content:focus { outline: none; }

.application-shell__skip-link {
  position: fixed;
  inset-block-start: calc(var(--safe-area-top) + var(--space-2));
  inset-inline-start: var(--space-3);
  z-index: var(--z-toast);
  padding: var(--space-2) var(--space-4);
  border-radius: var(--radius-control);
  color: var(--color-surface);
  background: var(--color-brand-primary);
  transform: translateY(-200%);
  transition: transform var(--motion-fast) var(--ease-standard);
}

.application-shell__skip-link:focus { transform: translateY(0); }

@media (min-width: 64rem) {
  .application-shell { background: var(--color-surface-muted); }

  .application-shell__content {
    border-inline: 1px solid var(--color-border-subtle);
    background: var(--color-background);
  }
}

@media (prefers-reduced-motion: reduce) {
  .application-shell__skip-link { transition: none; }
}
</style>
