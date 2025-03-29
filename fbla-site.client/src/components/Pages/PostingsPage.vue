<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ApplicationService from '@/services/application.service';
import { Posting } from '@/models/application.models';
import PostingDisplay from '../PostingDisplay.vue';

const applicationService = new ApplicationService();

const postings = ref<Posting[]>([]);
const loading = ref(true);


onMounted(async () => {
  try {
    const response: Posting[] = await applicationService.getPostings();
    postings.value = response;
  } catch (error) {
    console.error('Error fetching postings:', error);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div class="postings">
    <div class="header">
      <h1>Job Postings</h1>
      <p>Check out the latest job postings from employers in your area.</p>
    </div>

    <div v-if="loading" class="loading">Loading...</div>
    <div v-if="postings.length === 0" class="no-postings">No postings available.</div>
    <div v-else class="postings-list">
      <template v-for="posting in postings" :key="posting.id">
        <PostingDisplay :posting="posting">
          <template #actions>

          </template>
        </PostingDisplay>
      </template>
    </div>
  </div>
</template>

<style scoped>
.postings {
  padding: 20px;
}

.loading {
  font-size: 18px;
  color: gray;
}

.no-postings {
  font-size: 18px;
  color: red;
}

.postings-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.posting-item {
  border: 1px solid #ccc;
  padding: 15px;
  border-radius: 5px;
}
</style>
