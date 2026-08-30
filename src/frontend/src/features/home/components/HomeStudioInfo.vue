<script setup lang="ts">
import AppIcon from '../../../components/ui/AppIcon.vue'
</script>

<template>
  <section class="studio-info afraz-pattern afraz-pattern--sparkles" aria-labelledby="studio-info-title">
    <div class="studio-info__heading">
      <p>آتلیه تخصصی کودک و نوزاد</p>
      <h2 id="studio-info-title" class="text-section-title">اینجا برای ثبت خاطره‌های شما هستیم</h2>
    </div>
    <div class="studio-info__links">
      <RouterLink :to="{ name: 'studio' }">
        <AppIcon name="location" size="sm" />
        قم، آتلیه افراز
      </RouterLink>
      <a href="tel:+982500000000">
        <AppIcon name="phone" size="sm" />
        تماس با آتلیه
      </a>
      <span>
        <AppIcon name="clock" size="sm" />
        شنبه تا پنجشنبه، ۹ تا ۱۹
      </span>
      <RouterLink :to="{ name: 'studio', query: { section: 'licenses' } }">
        <AppIcon name="shield" size="sm" />
        مجوزها و اعتماد
      </RouterLink>
    </div>
  </section>
</template>

<style scoped>
.studio-info {
  padding: var(--space-5);
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-xl);
  background: var(--color-surface);
  box-shadow: var(--shadow-card);
}

.studio-info__heading p { margin: 0 0 var(--space-1); color: var(--color-brand-primary); font-size: var(--font-size-xs); }
.studio-info__heading h2 { color: var(--color-text-strong); }

.studio-info__links {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: var(--space-3);
  margin-block-start: var(--space-5);
}

.studio-info__links a,
.studio-info__links span {
  display: flex;
  min-block-size: var(--touch-target);
  align-items: center;
  gap: var(--space-2);
  color: var(--color-text-secondary);
  font-size: var(--font-size-xs);
  line-height: var(--line-height-caption);
  text-decoration: none;
}

.studio-info__links a { color: var(--color-brand-primary); font-weight: var(--font-weight-medium); }

@media (max-width: 22.5rem) {
  .studio-info__links { grid-template-columns: 1fr; gap: var(--space-1); }
}
</style>
