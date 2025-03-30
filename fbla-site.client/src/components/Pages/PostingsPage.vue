<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ApplicationService from '@/services/application.service';
import { Posting } from '@/models/application.models';
import PostingDisplay from '../PostingDisplay.vue';
import Button from 'primevue/button';
import { RouterLink } from 'vue-router';

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
            <RouterLink :to="{ name: 'apply', params: { postingId: posting.id } }" class="apply-link">
              <div class="apply-button">
                Apply
              </div>
            </RouterLink>

          </template>
        </PostingDisplay>
      </template>
    </div>
  </div>
</template>

<style scoped>
.postings {
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
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
  flex-direction: row;
  flex-wrap: wrap;
  gap: 20px;
  margin-left: 120px;

}

.posting-item {
  border: 1px solid #ccc;
  padding: 15px;
  border-radius: 5px;
}

  .header {
    margin-top: 30px;
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 2rem;
    font-size: 25px;
  }

  .header p {
    font-size: 18px;
  }

.apply-button {
  border: 1px solid rgb(0 0 0);
  padding: 10px;
  color: rgb(250 250 250);
  background-color: black;
  border-radius: 10px;
}

.apply-link {
  display: inline-block;
  transition: transform 0.3s ease, width 0.3s ease;
}

a:hover {
  background: none !important;
}

.apply-link:hover {
  transform: scale(1.05);
}

.sticky-container:hover {
  transform: scale(1.05);
}

.sticky-container {
  transition: transform 0.3s ease, width 0.3s ease;
}
</style>
