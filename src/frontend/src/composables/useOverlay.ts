import { nextTick, onBeforeUnmount, watch, type Ref } from 'vue'

let openOverlayCount = 0
let savedBodyOverflow = ''

function lockPageScroll() {
  if (typeof document === 'undefined') return

  if (openOverlayCount === 0) {
    savedBodyOverflow = document.body.style.overflow
    document.body.style.overflow = 'hidden'
  }

  openOverlayCount += 1
}

function unlockPageScroll() {
  if (typeof document === 'undefined' || openOverlayCount === 0) return

  openOverlayCount -= 1
  if (openOverlayCount === 0) document.body.style.overflow = savedBodyOverflow
}

export function useOverlay(
  isOpen: Ref<boolean>,
  panel: Ref<HTMLElement | null>,
  isDismissible: () => boolean,
  requestClose: () => void,
) {
  let active = false
  let previouslyFocused: HTMLElement | null = null

  const focusableSelector = [
    'a[href]',
    'button:not([disabled])',
    'input:not([disabled])',
    'select:not([disabled])',
    'textarea:not([disabled])',
    '[tabindex]:not([tabindex="-1"])',
  ].join(',')

  function handleKeydown(event: KeyboardEvent) {
    if (event.key === 'Escape' && isDismissible()) {
      event.preventDefault()
      requestClose()
      return
    }

    if (event.key !== 'Tab' || !panel.value) return

    const focusable = Array.from(
      panel.value.querySelectorAll<HTMLElement>(focusableSelector),
    ).filter((element) => element.offsetParent !== null)

    if (focusable.length === 0) {
      event.preventDefault()
      panel.value.focus()
      return
    }

    const first = focusable[0]
    const last = focusable[focusable.length - 1]
    if (event.shiftKey && document.activeElement === first) {
      event.preventDefault()
      last?.focus()
    } else if (!event.shiftKey && document.activeElement === last) {
      event.preventDefault()
      first?.focus()
    }
  }

  function activate() {
    if (active || typeof document === 'undefined') return
    active = true
    previouslyFocused = document.activeElement as HTMLElement | null
    lockPageScroll()
    window.addEventListener('keydown', handleKeydown)
    void nextTick(() => panel.value?.focus())
  }

  function deactivate() {
    if (!active || typeof document === 'undefined') return
    active = false
    window.removeEventListener('keydown', handleKeydown)
    unlockPageScroll()
    previouslyFocused?.focus()
    previouslyFocused = null
  }

  watch(isOpen, (open) => (open ? activate() : deactivate()), { immediate: true })
  onBeforeUnmount(deactivate)
}
