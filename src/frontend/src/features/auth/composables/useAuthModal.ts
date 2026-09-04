import { readonly, ref } from 'vue'
import type { RouteLocationRaw } from 'vue-router'

const isOpen = ref(false)
const destination = ref<RouteLocationRaw | null>(null)

export function useAuthModal() {
  function open(nextDestination?: RouteLocationRaw) {
    destination.value = nextDestination ?? null
    isOpen.value = true
  }

  function close() {
    isOpen.value = false
    destination.value = null
  }

  function complete() {
    const nextDestination = destination.value
    isOpen.value = false
    destination.value = null
    return nextDestination
  }

  return {
    isOpen: readonly(isOpen),
    open,
    close,
    complete,
  }
}
