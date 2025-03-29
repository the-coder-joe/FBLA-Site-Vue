<script setup lang="ts">
import { Posting } from '@/models/application.models';

const props = defineProps(
  {
    posting: {
      type: Posting,
      required: true
    }
  }
)

console.log(props.posting);
</script>

<template>
  <div class="posting">
    <h2>{{ posting.title }} at {{ posting.employer }}</h2>
    <p>{{ posting.description }}</p>
  </div>




<slot name="actions"></slot>
</template>

<style scoped>

</style>
