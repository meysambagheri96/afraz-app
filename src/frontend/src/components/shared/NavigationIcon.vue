<script setup lang="ts">
import type { NavigationItemId } from './navigation'

defineProps<{
  name: NavigationItemId
  active?: boolean
}>()
</script>

<template>
  <svg class="navigation-icon" viewBox="0 0 24 24" fill="none" aria-hidden="true">
    <template v-if="name === 'home'">
      <path v-if="active" d="M3.5 10.4 12 3l8.5 7.4v8.1a2.5 2.5 0 0 1-2.5 2.5h-3.1v-6.1H9.1V21H6a2.5 2.5 0 0 1-2.5-2.5v-8.1Z" fill="currentColor" />
      <path v-else d="m3.5 10.4 8.5-7.3 8.5 7.3v8.1A2.5 2.5 0 0 1 18 21h-3.1v-6.1H9.1V21H6a2.5 2.5 0 0 1-2.5-2.5v-8.1Z" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" />
    </template>

    <template v-else-if="name === 'booking'">
      <path v-if="active" fill-rule="evenodd" d="M6 2.5a1 1 0 0 1 1 1V5h10V3.5a1 1 0 1 1 2 0V5a3 3 0 0 1 3 3v10.5a3 3 0 0 1-3 3H5a3 3 0 0 1-3-3V8a3 3 0 0 1 3-3V3.5a1 1 0 0 1 1-1Zm10.7 8.8a1 1 0 0 0-1.4 0L11 15.6l-2.3-2.3a1 1 0 1 0-1.4 1.4l3 3a1 1 0 0 0 1.4 0l5-5a1 1 0 0 0 0-1.4Z" clip-rule="evenodd" fill="currentColor" />
      <g v-else stroke="currentColor" stroke-linecap="round" stroke-linejoin="round">
        <path d="M6 3v3M18 3v3M3.5 9.5h17M6 5h12a2.5 2.5 0 0 1 2.5 2.5v11A2.5 2.5 0 0 1 18 21H6a2.5 2.5 0 0 1-2.5-2.5v-11A2.5 2.5 0 0 1 6 5Z" />
        <path d="m8.5 15 2.2 2 4.8-5" />
      </g>
    </template>

    <template v-else-if="name === 'orders'">
      <path v-if="active" fill-rule="evenodd" d="M7 2.5a1 1 0 0 1 1 1V5h8V3.5a1 1 0 1 1 2 0V5h.5A2.5 2.5 0 0 1 21 7.5v12a2.5 2.5 0 0 1-2.5 2.5h-13A2.5 2.5 0 0 1 3 19.5v-12A2.5 2.5 0 0 1 5.5 5H6V3.5a1 1 0 0 1 1-1ZM7 11a1 1 0 1 0 0 2h10a1 1 0 1 0 0-2H7Zm0 5a1 1 0 1 0 0 2h6a1 1 0 1 0 0-2H7Z" clip-rule="evenodd" fill="currentColor" />
      <g v-else stroke="currentColor" stroke-linecap="round" stroke-linejoin="round">
        <path d="M7 3v3M17 3v3M5.5 5h13A2.5 2.5 0 0 1 21 7.5v12a2.5 2.5 0 0 1-2.5 2.5h-13A2.5 2.5 0 0 1 3 19.5v-12A2.5 2.5 0 0 1 5.5 5Z" />
        <path d="M7 11h10M7 16h6" />
      </g>
    </template>

    <template v-else-if="name === 'store'">
      <path v-if="active" fill-rule="evenodd" d="M8.5 7V6a3.5 3.5 0 1 1 7 0v1H19a2 2 0 0 1 2 2.2l-1 10A2 2 0 0 1 18 21H6a2 2 0 0 1-2-1.8l-1-10A2 2 0 0 1 5 7h3.5Zm2 0h3V6a1.5 1.5 0 0 0-3 0v1Z" clip-rule="evenodd" fill="currentColor" />
      <g v-else stroke="currentColor" stroke-linecap="round" stroke-linejoin="round">
        <path d="M9 8V6a3 3 0 0 1 6 0v2M5 8h14l1 12H4L5 8Z" />
        <path d="M9 11v.2M15 11v.2" />
      </g>
    </template>

    <template v-else>
      <path v-if="active" fill-rule="evenodd" d="M12 2.5a5 5 0 1 0 0 10 5 5 0 0 0 0-10ZM3 20a8 8 0 0 1 18 0 2 2 0 0 1-2 2H5a2 2 0 0 1-2-2Z" clip-rule="evenodd" fill="currentColor" />
      <g v-else stroke="currentColor" stroke-linecap="round" stroke-linejoin="round">
        <circle cx="12" cy="7.5" r="4.5" />
        <path d="M3.5 21a8.5 8.5 0 0 1 17 0" />
      </g>
    </template>
  </svg>
</template>

<style scoped>
.navigation-icon {
  inline-size: var(--icon-size-md);
  block-size: var(--icon-size-md);
  flex: none;
  stroke-width: var(--icon-stroke-width);
}
</style>
