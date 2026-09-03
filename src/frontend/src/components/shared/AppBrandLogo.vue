<script setup lang="ts">
withDefaults(
  defineProps<{
    label?: string
    size?: 'md' | 'lg'
  }>(),
  { label: 'نشان آتلیه افراز', size: 'md' },
)
</script>

<template>
  <div
    class="app-brand-logo"
    :class="`app-brand-logo--${size}`"
    role="img"
    :aria-label="label"
  >
    <span class="app-brand-logo__text">افراز</span>
    <span class="app-brand-logo__mark" aria-hidden="true">✦</span>
  </div>
</template>

<style scoped>
.app-brand-logo {
  position: relative;
  display: grid;
  flex: none;
  place-items: center;
  border: 2px solid var(--color-surface);
  border-radius: 50%;
  color: var(--color-accent-yellow);
  background: radial-gradient(circle at 35% 30%, #1598a3, #02707a 68%, #00535e);
  box-shadow: 0 4px 14px rgb(14 66 72 / 16%);
}

.app-brand-logo--md {
  inline-size: 43px;
  block-size: 43px;
}

.app-brand-logo--lg {
  inline-size: 52px;
  block-size: 52px;
}

.app-brand-logo__text {
  font-size: 17px;
  font-weight: 600;
  line-height: 1;
}

.app-brand-logo--lg .app-brand-logo__text { font-size: 19px; }

.app-brand-logo__mark {
  position: absolute;
  inset-block-start: 3px;
  inset-inline-start: 9px;
  color: #ffd22f;
  font-size: 8px;
}

.app-brand-logo--lg .app-brand-logo__mark {
  inset-block-start: 5px;
  inset-inline-start: 11px;
}
</style>
