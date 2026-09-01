<script setup lang="ts">
import { computed } from 'vue'
import AppModal from '../../../components/ui/AppModal.vue'
import { useAuthModal } from '../composables/useAuthModal'
import AuthFlow from './AuthFlow.vue'

const authModal = useAuthModal()
const model = computed({
  get: () => authModal.isOpen.value,
  set: (value: boolean) => value ? authModal.open() : authModal.close(),
})
</script>

<template>
  <AppModal
    v-model="model"
    size="fullscreen"
    :show-header="false"
    close-label="بستن ورود"
    @close="authModal.close"
  >
    <AuthFlow
      @close="authModal.close"
      @success="authModal.complete"
    />
  </AppModal>
</template>
