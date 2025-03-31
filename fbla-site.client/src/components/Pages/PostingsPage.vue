<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import ApplicationService from '@/services/application.service';
  import { Posting } from '@/models/application.models';
  import PostingDisplay from '../PostingDisplay.vue';
  import Button from 'primevue/button';
  import { RouterLink } from 'vue-router';

  // Initialize application service to fetch job postings
  const applicationService = new ApplicationService();

  // Reactive variable to store job postings
  const postings = ref<Posting[]>([]);
  // Loading state indicator
  const loading = ref(true);

  // Fetch job postings when the component is mounted
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
    <!-- Header section with title and description -->
    <div class="header">
      <h1>Job Postings</h1>
      <p>Check out the latest job postings from employers in your area.</p>
    </div>

    <!-- Loading indicator while fetching job postings -->
    <div v-if="loading" class="loading">Loading...</div>
    <!-- Display message if no postings are available -->
    <div v-if="postings.length === 0" class="no-postings">No postings available.</div>

    <!-- Display job postings when available -->
    <div v-else class="postings-list">
      <template v-for="posting in postings" :key="posting.id">
        <PostingDisplay :posting="posting">
          <template #actions>
            <!-- Apply button linking to the application page -->
            <RouterLink :to="{ name: 'apply', params: { postingId: posting.id } }" class="apply-link">
              <div class="apply-button">Apply</div>
            </RouterLink>
          </template>
        </PostingDisplay>
      </template>
    </div>
  </div>
</template>

<style scoped>
  /* Container styling for the job postings section */
  .postings {
    padding: 20px;
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  /* Loading message styling */
  .loading {
    font-size: 18px;
    color: gray;
  }

  /* No postings message styling */
  .no-postings {
    font-size: 18px;
    color: red;
  }

  /* Styling for the job postings list */
  .postings-list {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    gap: 20px;
    margin-left: 120px;
    margin-bottom: 290px;
  }

  /* Individual posting item styling */
  .posting-item {
    border: 1px solid #ccc;
    padding: 15px;
    border-radius: 5px;
  }

  /* Header section styling */
  .header {
    margin-top: 30px;
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 2rem;
    font-size: 25px;
  }

    /* Header paragraph styling */
    .header p {
      font-size: 18px;
    }

  /* Styling for the apply button */
  .apply-button {
    border: 1px solid rgb(0 0 0);
    padding: 10px;
    color: rgb(250 250 250);
    background-color: black;
    border-radius: 10px;
  }

  /* Apply link styling with transition effect */
  .apply-link {
    display: inline-block;
    transition: transform 0.3s ease, width 0.3s ease;
  }

  /* Prevent background change on hover for links */
  a:hover {
    background: none !important;
  }

  /* Scale effect on hover for apply links */
  .apply-link:hover {
    transform: scale(1.05);
  }

  /* Scale effect on hover for sticky containers */
  .sticky-container:hover {
    transform: scale(1.05);
  }

  /* Smooth transition effect for sticky containers */
  .sticky-container {
    transition: transform 0.3s ease, width 0.3s ease;
  }
</style>
