import { mount } from '@vue/test-utils'
import { describe, expect, it } from 'vitest'
import FoundationView from '../../src/router/views/FoundationView.vue'

describe('FoundationView', () => {
  it('renders the Persian foundation heading', () => {
    const wrapper = mount(FoundationView)

    expect(wrapper.get('h1').text()).toBe('استودیو افراز')
  })
})

