<script setup lang="ts">
import enamadBadgeUrl from '../../assets/images/certifications/enamad.webp'
import samandehiBadgeUrl from '../../assets/images/certifications/samandehi.webp'
import AppIcon from '../ui/AppIcon.vue'
import SocialBrandIcon from './SocialBrandIcon.vue'

const quickLinks = [
  { label: 'فروشگاه', to: { name: 'store' } },
  { label: 'تماس با ما', to: { name: 'contact' } },
  { label: 'درباره ما', to: { name: 'studio' } },
]

const socialLinks = [
  {
    label: 'اینستاگرام',
    name: 'instagram' as const,
    href: 'https://www.instagram.com/afraz_studio_qom/',
  },
  { label: 'واتساپ', name: 'whatsapp' as const, href: 'https://wa.me/989013623000' },
  { label: 'تلگرام', name: 'telegram' as const, href: 'https://t.me/+989013623000' },
  { label: 'ایتا', name: 'eitaa' as const, href: 'https://web.eitaa.com/#@afrastudio_qom' },
]

</script>

<template>
  <footer class="site-footer" aria-label="اطلاعات و پیوندهای آتلیه افراز">
    <div class="site-footer__inner">
      <section class="site-footer__section" aria-labelledby="footer-contact-title">
        <h2 id="footer-contact-title" class="site-footer__title text-card-title">ارتباط با ما</h2>
        <address class="site-footer__contact">
          <a
            href="https://www.google.com/maps?q=34.657239880600685,50.885920022952405"
            target="_blank"
            rel="noopener noreferrer"
          >
            <AppIcon name="location" size="sm" />
            <span>قم، کیوانفر، نبش کوچه ۵</span>
          </a>
          <a href="tel:+989013623000">
            <AppIcon name="phone" size="sm" />
            <span dir="ltr">+98 901 362 3000</span>
          </a>
        </address>
      </section>

      <section class="site-footer__section" aria-labelledby="footer-quick-title">
        <h2 id="footer-quick-title" class="site-footer__title text-card-title">دسترسی سریع</h2>
        <nav aria-label="دسترسی سریع">
          <ul class="site-footer__links">
            <li v-for="link in quickLinks" :key="link.label">
              <RouterLink :to="link.to">{{ link.label }}</RouterLink>
            </li>
          </ul>
        </nav>
      </section>

      <section
        class="site-footer__section site-footer__section--full site-footer__section--badges"
        aria-labelledby="footer-badges-title"
      >
        <h2 id="footer-badges-title" class="site-footer__title text-card-title">نمادها</h2>
        <div class="site-footer__badges" aria-label="نمادها و مجوزهای آتلیه">
          <div class="site-footer__badge" aria-label="نماد اعتماد الکترونیکی">
            <img
              :src="enamadBadgeUrl"
              alt="نماد اعتماد الکترونیکی"
              width="96"
              height="96"
              loading="lazy"
            />
          </div>
          <div class="site-footer__badge" aria-label="نشان ساماندهی">
            <img
              :src="samandehiBadgeUrl"
              alt="نشان ساماندهی"
              width="96"
              height="96"
              loading="lazy"
            />
          </div>
        </div>
      </section>

      <section
        class="site-footer__section site-footer__section--full site-footer__section--social"
        aria-labelledby="footer-social-title"
      >
        <h2 id="footer-social-title" class="site-footer__title text-card-title">
          شبکه‌های اجتماعی
        </h2>
        <div class="site-footer__socials">
          <a
            v-for="social in socialLinks"
            :key="social.name"
            class="site-footer__social-link"
            :href="social.href"
            target="_blank"
            rel="noopener noreferrer"
            :aria-label="social.label"
            :title="social.label"
          >
            <SocialBrandIcon :name="social.name" />
          </a>
        </div>
      </section>
    </div>

    <div class="site-footer__bottom">
      <p class="text-caption">تمامی حقوق برای آتلیه افراز محفوظ است، ۱۴۰۵</p>
    </div>
  </footer>
</template>

<style scoped>
.site-footer {
  position: relative;
  inline-size: min(100%, var(--mobile-canvas-max-width));
  margin-inline: auto;
  color: var(--color-text-primary);
  background: var(--color-background);
}

.site-footer::before {
  position: absolute;
  inset-block-start: 0;
  inset-inline: 0;
  block-size: 1px;
  background: var(--color-border-subtle);
  content: '';
}

.site-footer__inner,
.site-footer__bottom {
  inline-size: min(100%, var(--content-max-width));
  margin-inline: auto;
  padding-inline: var(--page-gutter);
}

.site-footer__inner {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  column-gap: var(--space-8);
  row-gap: var(--space-1);
  padding-block: var(--space-4) var(--space-6);
}

.site-footer__section {
  min-inline-size: 0;
}

.site-footer__section--full {
  grid-column: 1 / -1;
}

.site-footer__section--badges {
  margin-block-start: calc(var(--space-2) * -1);
  margin-block-end: 0;
}

.site-footer__section--social {
  margin-block-start: var(--space-4);
}

.site-footer__title {
  position: relative;
  padding-block-end: var(--space-3);
  color: var(--color-text-strong);
}

.site-footer__title::after {
  position: absolute;
  inset-block-end: 0;
  inset-inline-start: 0;
  inline-size: var(--space-8);
  block-size: 2px;
  border-radius: var(--radius-full);
  background: var(--color-brand-primary);
  content: '';
}

.site-footer__links {
  display: grid;
  gap: 0;
  margin: var(--space-2) 0 0;
  padding: 0;
  list-style: none;
}

.site-footer__links a {
  display: inline-flex;
  min-block-size: 2rem;
  align-items: center;
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
  text-decoration: none;
  transition:
    color var(--motion-fast) var(--ease-standard),
    transform var(--motion-fast) var(--ease-standard);
}

.site-footer__links a:hover,
.site-footer__links a:focus-visible {
  color: var(--color-brand-primary);
  transform: translateX(-2px);
}

.site-footer__contact {
  display: grid;
  gap: 0;
  margin-block-start: var(--space-2);
  font-style: normal;
}

.site-footer__contact a {
  display: flex;
  min-block-size: 2rem;
  align-items: center;
  gap: var(--space-2);
  color: var(--color-text-secondary);
  font-size: var(--font-size-sm);
  line-height: var(--line-height-body);
  text-decoration: none;
  transition: color var(--motion-fast) var(--ease-standard);
}

.site-footer__contact a:hover,
.site-footer__contact a:focus-visible {
  color: var(--color-brand-primary);
}

.site-footer__badges {
  display: flex;
  flex-wrap: wrap;
  gap: var(--space-3);
  margin-block-start: var(--space-2);
}

.site-footer__badge {
  display: grid;
  inline-size: 5.5rem;
  min-block-size: 5.5rem;
  place-items: center;
  align-content: center;
  overflow: hidden;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-md);
  background: var(--color-surface);
  box-shadow: var(--shadow-control);
}

.site-footer__badge img {
  inline-size: 100%;
  block-size: 100%;
  padding: var(--space-2);
  object-fit: contain;
}

.site-footer__socials {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: var(--space-3);
  margin-block-start: var(--space-2);
}

.site-footer__social-link {
  display: grid;
  inline-size: var(--touch-target);
  block-size: var(--touch-target);
  place-items: center;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-sm);
  color: var(--color-icon);
  background: var(--color-surface);
  transition:
    transform var(--motion-fast) var(--ease-standard),
    opacity var(--motion-fast) var(--ease-standard);
}

.site-footer__social-link:hover,
.site-footer__social-link:focus-visible {
  transform: translateY(-2px);
}
.site-footer__social-link svg {
  inline-size: var(--icon-size-md);
  block-size: var(--icon-size-md);
}

.site-footer__bottom {
  padding-block: var(--space-5)
    calc(var(--bottom-nav-height) + var(--safe-area-bottom) + var(--space-6));
  border-block-start: 1px solid var(--color-border-subtle);
}

.site-footer__bottom p {
  color: var(--color-text-secondary);
  text-align: center;
}

@media (min-width: 48rem) {
  .site-footer__inner {
    padding-block-start: var(--space-6);
  }
}

@media (min-width: 64rem) {
  .site-footer {
    border-inline: 1px solid var(--color-border-subtle);
  }
}

@media (prefers-reduced-motion: reduce) {
  .site-footer__links a,
  .site-footer__social-link {
    transition: none;
  }
}
</style>
