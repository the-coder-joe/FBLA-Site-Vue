<script setup lang="ts">
import { ref, computed } from 'vue'
import ApplicationService from '@/services/application.service'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Button from 'primevue/button'
import { Posting } from '@/models/application.models'
import PostingDisplay from '../PostingDisplay.vue'
import { useAuthStore } from '@/stores/authentication.store'

const authStore = useAuthStore();

const posting = ref<Posting>({
  id: 0,
  title: '',
  employer: '',
  description: '',
  requirements: '',
  additionalInformation: '',
  contactInformation: '',
  questions: [],
  submittedById: authStore.id
})

const submitted = ref(false);
const previewing = ref(false);

// Dynamic questions array
const questions = ref<{ id: string; question: string }[]>([])

function addQuestion() {
  questions.value.push({ id: Math.random().toString(36).substring(7), question: '' })
}

function removeQuestion(index: number) {
  questions.value.splice(index, 1)
}

// Form validation (required: title, employer, description)
const isFormValid = computed(() => {
  return posting.value.title.trim() !== '' &&
    posting.value.employer.trim() !== '' &&
    posting.value.description.trim() !== '' &&
    posting.value.requirements.trim() !== ''
})

// Access the ApplicationService
const applicationService = new ApplicationService()

async function submitForm() {
  try {
    posting.value.questions = questions.value.map(q => q.question);
    const userId = authStore.id;
    await applicationService.addPosting(posting.value, userId);
  } catch (err) {
    console.error('Error adding posting:', err)
  }
  submitted.value = true;
  previewing.value = false;
}

function previewForm() {
  // Add questions to posting for preview
  posting.value.questions = questions.value.map(q => q.question)
  previewing.value = true;
}

function editForm() {
  previewing.value = false;
}

function resetForm() {
  posting.value = {
    id: 0,
    title: '',
    employer: '',
    description: '',
    requirements: '',
    additionalInformation: '',
    contactInformation: '',
    questions: [],
    submittedById: authStore.id
  }
  questions.value = []
  submitted.value = false
  previewing.value = false
}
</script>

<template>
  <div class="page">
    <div class="header glass">
        <div>
          <h1>Add a Job Posting</h1>
          <p>
            Create a new job posting by providing a title, employer, description, requirements, and any additional questions for applicants.<br>
            <span class="header-tip">Tip: Preview your posting before submitting to see how it will appear to job seekers!</span>
          </p>
      </div>
    </div>

    <Transition name="dissolve" mode="out-in">
      <div v-if="submitted" class="preview-container light-glass">
        <h2 class="center-text">Posting Submitted Successfully!</h2>
        <p style="text-align: center; font-size: 1.2rem; color: gray;">
          Your job posting has been submitted successfully. It will be reviewed and published shortly.
        </p>
        <PostingDisplay :posting="posting" />
        <div style="display: flex; gap: 3rem; justify-content: center; margin-top: 4rem;">
          <Button @click="resetForm" label="Add Another Posting"/>
        </div>
      </div>

      <div v-else-if="previewing" class="preview-container light-glass">
        <h2 class="center-text">Preview of Your Posting</h2>
        <p style="text-align: center; font-size: 1.2rem; color: gray;">
          Preview your job posting before submitting to ensure all details are correct.
        </p>
        <PostingDisplay :posting="posting" />
        <div style="display: flex; gap: 3rem; justify-content: center; margin-top: 4rem;">
          <Button style="padding: 1rem;" @click="editForm" label="Edit" />
          <Button style="padding: 1rem;" @click="submitForm" label="Submit" />
        </div>
      </div>

      <div v-else class="form dark-glass">
        <!-- Employer Name -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="employer" class="field-label">Employer Name</label>
            <span>Enter the name of the employer.</span>
          </div>
          <InputText class="field" id="employer" v-model="posting.employer" />
        </div>
        <!-- Job Title -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="title" class="field-label">Job Title</label>
            <span>Enter the title of the job posting.</span>
          </div>
          <InputText class="field" id="title" v-model="posting.title" />
        </div>
        <!-- Job Description -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="description" class="field-label">Job Description</label>
            <span>Provide a detailed description of the job.</span>
          </div>
          <InputText class="field" id="description" v-model="posting.description" />
        </div>
        <!-- Job Requirements -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="requirements" class="field-label">Job Requirements</label>
            <span>List the requirements for the job.</span>
          </div>
          <InputText class="field" id="requirements" v-model="posting.requirements" />
        </div>
        <!-- Additional Information -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="additionalInfo" class="field-label">Additional Information</label>
            <span>Provide any additional information about the job.</span>
          </div>
          <InputText class="field" id="additionalInfo" v-model="posting.additionalInformation" />
        </div>
        <!-- Questions -->
        <div class="form-group dark-glass">
          <div class="field-description">
            <label for="questions" class="field-label">Questions</label>
            <span>Provide any additional questions for applicants.</span>
            <div style="display: flex; flex-direction: row-reverse; flex-grow: 1">
              <Button @click="addQuestion" label="Add Question" style="width: fit-content; margin-inline: 10px;" />
            </div>
          </div>
          <transition-group name="fade" tag="div">
            <div v-for="(question, index) in questions" :key="question.id" class="dynamic-question-container">
              <Textarea class="field dynamic-question" v-model="questions[index].question" />
              <Button @click="removeQuestion(index)" icon="pi pi-times" />
            </div>
          </transition-group>
        </div>
        <!-- Preview Button (disabled if form is invalid) -->
        <Button @click="previewForm" label="Preview" :disabled="!isFormValid" />
      </div>
    </Transition>
  </div>
</template>

<style scoped>
.preview-container {
  display: flex;
  flex-direction: column;
  padding: 4rem;
  max-width: 1200px;
  border-radius: 10px;
  padding-top: 1rem;
  align-items: center;
}


.fade-enter-active,
.fade-leave-active {
  transition: all 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
  transform: translateX(30px);
}

/* Page styling */
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
}

/* Form container styling */
.form {
  padding: 30px;
  display: flex;
  flex-direction: column;
  width: 90%;
  max-width: 1200px;
  margin-bottom: 80px;
  border-radius: 10px;
}

/* Input field styling */
.field {
  border: 1px solid gray;
  border-radius: 5px;
  margin-block: 0.1rem;
  padding: 0.5rem;
  font-size: large;
  transition: all 0.3s;
}

.field:hover,
.field:focus {
  padding: 0.6rem;
  border-color: lightblue;
  margin-block: 0;
}

/* Form group styling */
.form-group {
  border: 1px solid rgba(128, 128, 128, 0.235);
  border-radius: 10px;
  padding: 10px;
  display: flex;
  flex-direction: column;
  margin-bottom: 1rem;
  box-shadow: #2e2e2e53 5px 5px 5px;
  background-color: #1414174b;
  transition: all 0.5s;
}

/* Form group hover effect */
.form-group:hover,
.form-group:focus-within {
  box-shadow: #2e2e2eb7 5px 5px 20px;
  background-color: #1a202faa;
}

/* Label and description styling */
.field-label {
  font-size: 2rem;
  color: lightblue;
}

.field-description {
  margin-bottom: 1rem;
  display: flex;
  align-items: baseline;
}

@media (max-width: 700px) {
  .field-description {
    flex-direction: column;
    align-items: center;
  }
}

.field-description span {
  margin-left: 1rem;
  font-size: 1rem;
  color: gray;
}

/* Styling for dynamic question inputs */
.dynamic-question-container {
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
}

.dynamic-question {
  width: 100%;
  margin-inline: 10px;
}

.dissolve-enter-active,
.dissolve-leave-active {
  transition: all 0.4s ease;
}

.dissolve-enter-active,
.dissolve-leave-active {
  /* position: absolute; */
}

.dissolve-enter-from{
  opacity: 0;
  transform: translateX(200px);
}


.dissolve-leave-to {
  opacity: 0;
  transform: translateX(-200px);
}

.center-text {
  text-align: center;
  font-size: 35px;
}
</style>
