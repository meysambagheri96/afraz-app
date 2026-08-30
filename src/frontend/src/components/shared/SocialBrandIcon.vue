<script setup lang="ts">
defineProps<{
  name: 'instagram' | 'whatsapp' | 'telegram' | 'eitaa'
}>()
</script>

<template>
  <svg
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="1.8"
    stroke-linecap="round"
    stroke-linejoin="round"
    aria-hidden="true"
    focusable="false"
  >
    <g v-if="name === 'instagram'">
      <rect x="3.25" y="3.25" width="17.5" height="17.5" rx="5" />
      <circle cx="12" cy="12" r="4.15" />
      <circle cx="17.35" cy="6.85" r="0.65" fill="currentColor" stroke="none" />
    </g>

    <g v-else-if="name === 'whatsapp'">
      <path d="M19.75 11.7a7.7 7.7 0 0 1-11.42 6.74L4 19.58l1.16-4.2A7.7 7.7 0 1 1 19.75 11.7Z" />
      <path
        d="M8.8 8.2c.7 3.4 3.45 6.15 6.85 6.85l1.25-1.25c.25-.25.6-.32.92-.2l2.05.8c.38.15.63.52.63.93v2.15c0 .55-.45 1-1 1C11.77 18.48 5.5 12.2 5.5 4.48c0-.55.45-1 1-1h2.15c.41 0 .78.25.93.63l.8 2.05c.12.32.05.67-.2.92L8.8 8.2Z"
      />
    </g>

    <g v-else-if="name === 'telegram'">
      <path d="m3.5 11.45 16.2-6.2c.7-.27 1.38.4 1.1 1.1l-6.2 16.15-3.05-7.05-8.05-4Z" />
      <path d="m11.55 15.45 4.25-4.25" />
    </g>

    <g v-else>
      <path d="M12 3.25a8.75 8.75 0 0 0-7.04 13.95L4.2 20l2.94-.7A8.75 8.75 0 1 0 12 3.25Z" />
      <path
        d="M8.4 13.65c1.45-2.7 3.3-4.52 6.63-5.55-.55 3.15-2.15 5.27-5.18 6.47l-1.45-.92Z"
        fill="none"
      />
      <circle cx="9.1" cy="15.5" r="0.7" fill="currentColor" stroke="none" />
    </g>
  </svg>
</template>
