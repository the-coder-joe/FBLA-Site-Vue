<script setup>
import { defineProps } from 'vue';

defineProps({
  postingId: {
    type: Number,
    required: true
  }
});
</script>


<template>
  <div>
    <h1>Application Page</h1>
    <p>Posting ID: {{ postingId }}</p>
  </div>
</template>

<style scoped>
/* Add your styles here */
</style>
