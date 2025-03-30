<script setup lang="ts">
import { useRoute } from 'vue-router';
import { defineProps, onMounted, ref } from 'vue';
import ApplicationService from '@/services/application.service';

const applicationService = new ApplicationService();

const route = useRoute();
const postingId = route.params.postingId;

let posting = ref(null);
onMounted(async () => {
  try {
    posting.value = await applicationService.getPosting(parseInt(postingId as string));
  } catch (error) {
    console.error('Error fetching posting:', error);
  }
});
</script>


<template>
  <div v-if="posting" class="application-page">
    {{ posting.title }} at {{ posting.employer }}
  </div>
</template>

<style scoped>
/* Add your styles here */
</style>
