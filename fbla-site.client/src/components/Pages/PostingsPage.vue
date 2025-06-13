<script setup lang="ts">
import { ref, onMounted } from 'vue';
import ApplicationService from '@/services/application.service';
import { Posting } from '@/models/application.models';
import PostingDisplay from '../PostingDisplay.vue';
import Button from 'primevue/button';
import { RouterLink } from 'vue-router';
import throttle from 'lodash/throttle';
import InputText from 'primevue/inputtext';

// Initialize application service to fetch job postings
const applicationService = new ApplicationService();

const numberOfPostingsToShow = 10; // Number of postings to show at a time

// Reactive variable to store job postings
const postings = ref<Posting[]>([]);
const filteredPostings = ref<Posting[]>([]);
// Loading state indicator
const loading = ref(true);

const searchQuery = ref<string>('');

var filterPostingsThrottled = throttle(() => {
  filterPostings();
}, 500);

function filterPostings() {
  let words = [];
  if (searchQuery.value?.trim() === '') {
    filteredPostings.value = postings.value;
  } else {
    if (searchQuery.value.includes(',')) {
      words = searchQuery.value.split(',');
    } else {
      words = [searchQuery.value];
    }
    words.forEach((word, index) => {
      words[index] = word.trim().toLowerCase();
    });

    words = words.filter(word => word.length > 0);

    filteredPostings.value = postings.value.filter(posting =>
      postingContains(posting, words)
    );
  }
}

function postingContains(posting: Posting, query: string[]): boolean {
  const postingContainsWord = (posting: Posting, word: string): boolean => {
    return (
      posting.title.toLowerCase().includes(word) ||
      posting.description.toLowerCase().includes(word) ||
      posting.employer.toLowerCase().includes(word)
    );
  };

  for (const word of query) {
    if (postingContainsWord(posting, word)) {
      return true;
    }
  }
}

// Fetch job postings when the component is mounted
onMounted(async () => {
  try {
    const response: Posting[] = await applicationService.getPostings();
    postings.value = response;
    filterPostings()
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
    <div class="header glass">
      <div class="header-content">
        <div style="display: flex; align-items: baseline; gap: 2rem;">
          <h1>Job Postings</h1>
        </div>
        <InputText v-model="searchQuery" placeholder="Search job postings..." class="search-bar"
          @input="filterPostingsThrottled()">
        </InputText>
      </div>
      <div class="information">
        <p>
          Search job postings by title, employer, or description. You can find and apply for postings that intrest you
          all
          on the same page!
        </p>

        <div>
          Showing {{ filteredPostings.length }} of {{ postings.length }} postings.
        </div>

      </div>

    </div>


    <!-- Loading indicator while fetching job postings -->
    <div v-if="loading" class="loading">Loading...</div>
    <!-- Display message if no postings are available -->
    <div v-if="postings.length === 0" class="no-postings">No postings available.</div>

    <!-- Display job postings when available -->
    <template v-if="!loading && filteredPostings.length > 0">
      <TransitionGroup name="filter" tag="div" class="postings-list light-glass">
        <template v-for="posting in filteredPostings" :key="posting.id">
          <PostingDisplay :posting="posting">
            <template #actions>
              <!-- Apply button linking to the application page -->
              <RouterLink :to="{ name: 'apply', params: { postingId: posting.id } }" class="apply-link">
                <div class="apply-button">Apply</div>
              </RouterLink>
            </template>
          </PostingDisplay>
        </template>
      </TransitionGroup>
    </template>

    <template v-else-if="!loading && filteredPostings.length === 0 && searchQuery.trim() !== '' && postings.length > 0">
      <!-- Display message if no postings match the search query -->
      <div class="no-postings">No postings match your search.</div>
    </template>
  </div>
</template>

<style scoped>
/* Container styling for the job postings section */
.postings {
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: -100px;
  width: 100%;
  height: 100%;
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
  margin-bottom: 290px;
  justify-content: center;
  padding: 20px;
  padding-bottom: 40px;
  max-width: 90%;
}

/* Individual posting item styling */
.posting-item {
  border: 1px solid #ccc;
  padding: 15px;
  border-radius: 5px;
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

.information {
  font-size: 18px;
  text-align: center;
  width: 100%;
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}

.search-bar-container {
  width: 100%;
  display: flex;
  justify-content: flex-start;
  margin-bottom: 20px;
  margin-inline: 20px;
}

.search-bar {
  border: 1px solid #ccc;
  width: 30%;
  padding: 10px;
  border-radius: 20px;
  background: #f9f9f913;
  backdrop-filter: blur(5px);
}

.filter-move,
.filter-enter-active,
.filter-leave-active {
  transition: all 1s cubic-bezier(0.18, 0.89, 0.4, 1.02);
}

.filter-enter-from {
  opacity: 0;
  transform: translate(-50px, -50px) scale(0.1) rotateZ(-10deg);
}


.filter-leave-to {
  opacity: 0;
  transform: scale(0.35) rotateZ(-10deg);
}

/* ensure leaving items are taken out of layout flow so that moving
   animations can be calculated correctly. */
.filter-leave-active {
  position: absolute;
}
</style>
