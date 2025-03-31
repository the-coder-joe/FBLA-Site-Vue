<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ApplicationService from '@/services/application.service';
import { Posting } from '@/models/application.models';
import PostingDisplay from '../PostingDisplay.vue';
import Button from 'primevue/button';
import { RouterLink } from 'vue-router';
import { Dialog } from 'primevue';

// Initialize application service to fetch job postings
const applicationService = new ApplicationService();

// Reactive variable to store job postings
const postings = ref<Posting[]>([]);
// Loading state indicator
const loading = ref(true);

const visible = ref([]);

async function getPostings() {
  try {
    const response: Posting[] = await applicationService.getPostingsQueue();
    postings.value = response;
    postings.value.forEach(x => {
      visible[x.id] = false;
    });
  } catch (error) {
    console.error('Error fetching postings:', error);
  } finally {
    loading.value = false;
  }
}

// Fetch job postings when the component is mounted
onMounted(async () => {
  getPostings();
});

function approve(posting: Posting) {
  applicationService.approvalPosting(posting.id, true)
    .then(() => {
      getPostings();
      visible.value[posting.id] = false;
    });
}

function deny(posting: Posting) {
  applicationService.approvalPosting(posting.id, false)
    .then(() => {
      getPostings();
      visible.value[posting.id] = false;
    });
}
</script>
<template>
  <div class="postings">
    <!-- Header section with title and description -->
    <div class="header">
      <h1>Approve Postings</h1>
      <p>Approve employer postings to add them to postings page or deny.</p>
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
            <Button class="view-button" label="Show" @click="visible[posting.id] = true">View form</Button>
            <Dialog v-model:visible="visible[posting.id]" modal header="Approve Posting"
              :style="{ width: '50rem', borderRadius: '10px', padding: '15px', boxShadow: '0px 0px 20px rgba(255, 255, 255, 1)', backgroundColor: 'black' }">

              <div class="modal-list-item">
                <div class="describer">Employer:</div> {{ posting.employer }}
              </div>
              <div class="modal-list-item">
                <div class="describer">Job Title:</div> {{ posting.title }}
              </div>
              <div class="modal-list-item">
                <div class="describer">Requirements:</div> {{ posting.requirements }}
              </div>
              <div class="modal-list-item">
                <div class="describer">Job Description:</div> {{ posting.description }}
              </div>
              <div class="modal-list-item">
                Additional Information: {{ posting.additionalInformation }}
              </div>
              <div class="modal-list-item">
                <div class="describer">Contact Info:</div> {{ posting.contactInformation }}
              </div>
              <div class="modal-list-item">
                <div class="describer">Questions For Applicants:</div>
                <div v-for="q in posting.questions">
                  {{ q }}
                </div>
                <div class="approve-deny-button">
                  <Button class="approve-button" @click="approve(posting)">Approve</Button>
                  <Button class="deny-button" @click="deny(posting)">Deny</Button>
                </div>
              </div>
            </Dialog>
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
  min-height: 800px;
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
  justify-content: center;
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

.modal-list-item {
  padding: 10px;
}

.describer {
  color: var(--primary-color)
}

.view-button {
  border: 1px solid rgb(0 0 0);
  padding: 10px;
  color: rgb(250 250 250);
  background-color: black;
  border-radius: 10px;
}

.approve-button {
  background-color: green;
  color: white;
  margin: 10px;
}

.deny-button {
  background-color: red;
  color: white;
  float: right;
  margin: 10px;
}

.p-dialog-mask {
  background-color: rgba(0, 0, 0, 0.5) !important;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: scale(0.95);
  }

  to {
    opacity: 1;
    transform: scale(1);
  }
}
</style>
