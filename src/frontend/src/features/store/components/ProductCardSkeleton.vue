<script setup lang="ts">
import AppSkeleton from '../../../components/ui/AppSkeleton.vue'
</script>

<template>
  <article class="product-card-skeleton" aria-hidden="true">
    <AppSkeleton class="product-card-skeleton__image" />
    <div class="product-card-skeleton__body">
      <AppSkeleton shape="text" width="78%" />
      <AppSkeleton shape="text" width="56%" />
      <div class="product-card-skeleton__footer">
        <AppSkeleton shape="text" width="34%" />
        <AppSkeleton class="product-card-skeleton__button" width="82px" height="38px" />
      </div>
    </div>
  </article>
</template>

<style scoped>
.product-card-skeleton {
  min-inline-size: 0;
  overflow: hidden;
  border: 1px solid var(--color-border-subtle);
  border-radius: var(--radius-card);
  background: var(--color-surface);
}

.product-card-skeleton__image {
  aspect-ratio: 4 / 3;
  border-radius: 0;
}

.product-card-skeleton__body {
  display: grid;
  min-block-size: 126px;
  align-content: space-between;
  gap: var(--space-2);
  padding: 10px;
}

.product-card-skeleton__footer {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-2);
}

.product-card-skeleton__button { flex: none; border-radius: var(--radius-sm); }
</style>
