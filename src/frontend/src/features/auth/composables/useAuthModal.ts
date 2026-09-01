import { readonly, ref } from 'vue'

const isOpen = ref(false)

export function useAuthModal() {
  function open() {
    isOpen.value = true
  }

  function close() {
    isOpen.value = false
  }

  function complete() {
    isOpen.value = false
  }

  return {
    isOpen: readonly(isOpen),
    open,
    close,
    complete,
  }
}
