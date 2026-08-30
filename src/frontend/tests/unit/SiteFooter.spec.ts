import { RouterLinkStub, mount } from '@vue/test-utils'
import { describe, expect, it } from 'vitest'
import SiteFooter from '../../src/components/shared/SiteFooter.vue'

describe('SiteFooter', () => {
  it('renders all requested Persian sections and links', () => {
    const wrapper = mount(SiteFooter, {
      global: {
        stubs: { RouterLink: RouterLinkStub },
      },
    })

    expect(wrapper.text()).toContain('دسترسی سریع')
    expect(wrapper.text()).toContain('راهنمای مشتریان')
    expect(wrapper.text()).toContain('نمادها')
    expect(wrapper.text()).toContain('شبکه‌های اجتماعی')

    const internalLinks = wrapper.findAllComponents(RouterLinkStub)
    expect(internalLinks.map((link) => link.text())).toEqual([
      'فروشگاه',
      'مقالات',
      'تماس با ما',
      'درباره ما',
      'حریم خصوصی',
      'قوانین و مقررات',
      'سوالات متداول',
    ])
  })

  it('provides accessible external links for all social networks', () => {
    const wrapper = mount(SiteFooter, {
      global: {
        stubs: { RouterLink: RouterLinkStub },
      },
    })

    const socialLinks = wrapper.findAll('.site-footer__social-link')
    expect(socialLinks).toHaveLength(4)
    expect(socialLinks.map((link) => link.attributes('aria-label'))).toEqual([
      'اینستاگرام',
      'واتساپ',
      'تلگرام',
      'ایتا',
    ])
    expect(socialLinks.every((link) => link.attributes('rel') === 'noopener noreferrer')).toBe(true)
  })
})
