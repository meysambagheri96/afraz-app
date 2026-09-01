<template>
  <div class="auth-brand">
    <div
      class="auth-brand__logo"
      role="img"
      aria-label="نشان آتلیه افراز"
    >
      <span>افراز</span>
      <i aria-hidden="true">✦</i>
    </div>
    <h1 class="text-page-title">
      آتلیه افراز
    </h1>
  </div>
</template>

<style scoped>
.auth-brand {
  display: grid;
  justify-items: center;
  gap: var(--space-2);
  text-align: center;
}

.auth-brand__logo {
  position: relative;
  display: grid;
  inline-size: 4.25rem;
  block-size: 4.25rem;
  place-items: center;
  border: 3px solid var(--color-surface);
  border-radius: var(--radius-full);
  color: var(--color-accent-yellow);
  background: radial-gradient(circle at 35% 30%, #1598a3, var(--color-brand-primary) 68%, var(--color-brand-dark));
  box-shadow: 0 0.45rem 1.25rem rgb(14 66 72 / 18%);
}

.auth-brand__logo span {
  font-size: var(--font-size-xl);
  font-weight: var(--font-weight-bold);
}

.auth-brand__logo i {
  position: absolute;
  inset-block-start: var(--space-1);
  inset-inline-start: var(--space-3);
  color: var(--color-accent-yellow);
  font-size: var(--font-size-xs);
  font-style: normal;
}

.auth-brand h1 {
  margin: 0;
  color: var(--color-text-primary);
}
</style>
