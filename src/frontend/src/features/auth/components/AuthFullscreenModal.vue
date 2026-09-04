<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import AppModal from '../../../components/ui/AppModal.vue'
import { useAuthModal } from '../composables/useAuthModal'
import AuthFlow from './AuthFlow.vue'

const authModal = useAuthModal()
const router = useRouter()
const model = computed({
  get: () => authModal.isOpen.value,
  set: (value: boolean) => value ? authModal.open() : authModal.close(),
})

async function handleSuccess() {
  const destination = authModal.complete()
  if (destination) await router.push(destination)
}
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
      @success="handleSuccess"
    />
  </AppModal>
</template>
