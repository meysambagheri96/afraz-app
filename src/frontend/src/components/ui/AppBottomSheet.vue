<script setup lang="ts">
import { computed, ref, useId } from 'vue'
import { useOverlay } from '../../composables/useOverlay'
import AppIconButton from './AppIconButton.vue'

const model = defineModel<boolean>({ default: false })
const props = withDefaults(
  defineProps<{
    title?: string
    description?: string
    dismissible?: boolean
    closeLabel?: string
    showHandle?: boolean
    flushBottom?: boolean
  }>(),
  { dismissible: true, closeLabel: 'بستن', showHandle: true, flushBottom: false },
)
const emit = defineEmits<{ close: [] }>()
const panel = ref<HTMLElement | null>(null)
const generatedId = useId()
const titleId = computed(() => props.title ? `sheet-title-${generatedId}` : undefined)
const descriptionId = computed(() => props.description ? `sheet-description-${generatedId}` : undefined)

function close() {
  if (!props.dismissible) return
  model.value = false
  emit('close')
}

useOverlay(model, panel, () => props.dismissible, close)
</script>

<template>
  <Teleport to="body">
    <Transition name="app-sheet">
      <div
        v-if="model"
        class="app-overlay app-bottom-sheet"
        :class="{ 'app-bottom-sheet--flush-bottom': flushBottom }"
        @mousedown.self="close"
      >
        <section
          ref="panel"
          class="app-overlay__panel app-bottom-sheet__panel"
          role="dialog"
          aria-modal="true"
          :aria-labelledby="titleId"
          :aria-describedby="descriptionId"
          tabindex="-1"
        >
          <div v-if="showHandle" class="app-bottom-sheet__handle" aria-hidden="true" />
          <header v-if="title || description || $slots.header || dismissible" class="app-overlay__header">
            <slot name="header">
              <div class="app-overlay__heading">
                <h2 v-if="title" :id="titleId" class="app-overlay__title">{{ title }}</h2>
                <p v-if="description" :id="descriptionId" class="app-overlay__description">{{ description }}</p>
              </div>
            </slot>
            <AppIconButton v-if="dismissible" :label="closeLabel" size="sm" @click="close">
              <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                <path d="m7 7 10 10M17 7 7 17" stroke="currentColor" stroke-width="2" stroke-linecap="round" />
              </svg>
            </AppIconButton>
          </header>
          <div class="app-overlay__body"><slot /></div>
          <footer v-if="$slots.footer" class="app-overlay__footer"><slot name="footer" /></footer>
        </section>
      </div>
    </Transition>
  </Teleport>
</template>

<style scoped>
.app-bottom-sheet { display: flex; align-items: flex-end; justify-content: center; }
.app-bottom-sheet--flush-bottom { padding-block-end: 0; }
.app-bottom-sheet__panel { max-width: 40rem; max-height: min(90dvh, 52rem); padding-block-end: env(safe-area-inset-bottom); border-radius: var(--radius-xl) var(--radius-xl) 0 0; }
.app-bottom-sheet__handle { width: 2.5rem; height: 0.3rem; margin: var(--space-2) auto 0; border-radius: var(--radius-full); background: var(--color-border); }
</style>
