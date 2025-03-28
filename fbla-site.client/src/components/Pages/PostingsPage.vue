<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ApplicationService from '@/services/application.service';
import { Posting } from '@/models/application.models';

const applicationService = new ApplicationService();

const postings = ref<Posting[]>([]);
const loading = ref(true);

const fetchPostings = async () => {
  try {
    const response = await applicationService.getPostings();
    postings.value = response;
  } catch (error) {
    console.error('Error fetching postings:', error);
  } finally {
    loading.value = false;
  }
};

onMounted(fetchPostings);
</script>

<template>
  <div class="postings">
    <div class="header">
      <h1>Job Postings</h1>
      <p>Check out the latest job postings from employers in your area.</p>
    </div>

    <div v-if="loading" class="loading">Loading...</div>
    <div v-else-if="postings.length === 0" class="no-postings">No postings available.</div>
    <div v-else class="postings-list">
      <Posting v-for="posting in postings" :key="posting.id" :posting="posting">
        <template #actions>
          <RouterLink :to="{ name: 'Application', params: { postingId: posting.id } }">Apply</RouterLink>
        </template>
        </Posting>
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
