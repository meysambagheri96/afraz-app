<script setup lang="ts">
import { computed, ref, useId } from 'vue'
import { useOverlay } from '../../composables/useOverlay'
import AppIconButton from './AppIconButton.vue'

const model = defineModel<boolean>({ default: false })
const props = withDefaults(
  defineProps<{
    title?: string
    description?: string
    size?: 'sm' | 'md' | 'lg' | 'fullscreen'
    dismissible?: boolean
    showHeader?: boolean
    closeLabel?: string
  }>(),
  {
    title: undefined,
    description: undefined,
    size: 'md',
    dismissible: true,
    showHeader: true,
    closeLabel: 'بستن',
  },
)
const emit = defineEmits<{ close: [] }>()
const panel = ref<HTMLElement | null>(null)
const generatedId = useId()
const titleId = computed(() => props.title ? `modal-title-${generatedId}` : undefined)
const descriptionId = computed(() => props.description ? `modal-description-${generatedId}` : undefined)

function close() {
  if (!props.dismissible) return
  model.value = false
  emit('close')
}

useOverlay(model, panel, () => props.dismissible, close)
</script>

<template>
  <Teleport to="body">
    <Transition name="app-overlay">
      <div
        v-if="model"
        class="app-overlay app-modal"
        @mousedown.self="close"
      >
        <section
          ref="panel"
          class="app-overlay__panel app-modal__panel"
          :class="`app-modal__panel--${size}`"
          role="dialog"
          aria-modal="true"
          :aria-labelledby="titleId"
          :aria-describedby="descriptionId"
          tabindex="-1"
        >
          <header
            v-if="showHeader && (title || description || $slots.header || dismissible)"
            class="app-overlay__header"
          >
            <slot name="header">
              <div class="app-overlay__heading">
                <h2
                  v-if="title"
                  :id="titleId"
                  class="app-overlay__title"
                >
                  {{ title }}
                </h2>
                <p
                  v-if="description"
                  :id="descriptionId"
                  class="app-overlay__description"
                >
                  {{ description }}
                </p>
              </div>
            </slot>
            <AppIconButton
              v-if="dismissible"
              :label="closeLabel"
              size="sm"
              @click="close"
            >
              <svg
                viewBox="0 0 24 24"
                fill="none"
                aria-hidden="true"
              >
                <path
                  d="m7 7 10 10M17 7 7 17"
                  stroke="currentColor"
                  stroke-width="2"
                  stroke-linecap="round"
                />
              </svg>
            </AppIconButton>
          </header>
          <div class="app-overlay__body">
            <slot />
          </div>
          <footer
            v-if="$slots.footer"
            class="app-overlay__footer"
          >
            <slot name="footer" />
          </footer>
        </section>
      </div>
    </Transition>
  </Teleport>
</template>

<style scoped>
.app-modal { display: grid; place-items: center; padding: max(var(--space-4), env(safe-area-inset-top)) var(--space-4) max(var(--space-4), env(safe-area-inset-bottom)); }
.app-modal__panel { max-height: min(85dvh, 46rem); border-radius: var(--radius-xl); }
.app-modal__panel--sm { max-width: 22rem; }
.app-modal__panel--md { max-width: 30rem; }
.app-modal__panel--lg { max-width: 42rem; }
.app-modal:has(.app-modal__panel--fullscreen) { padding: 0; }
.app-modal__panel--fullscreen {
  inline-size: min(100%, var(--mobile-canvas-max-width));
  block-size: 100dvh;
  max-block-size: none;
  border-radius: 0;
}
.app-modal__panel--fullscreen .app-overlay__body {
  block-size: 100%;
  padding: 0;
}
</style>
