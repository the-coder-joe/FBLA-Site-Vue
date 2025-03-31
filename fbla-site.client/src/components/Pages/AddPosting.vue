<script setup lang="ts">
import { ref, computed } from 'vue'
import ApplicationService from '@/services/application.service'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import Button from 'primevue/button'
import { Posting } from '@/models/application.models'

// Form fields
const employerName = ref('')
const jobTitle = ref('')  // Swapped order: now employerName comes first, then jobTitle
const jobDescription = ref('')
const jobRequirements = ref('')
const additionalInfo = ref('')

// Dynamic questions array
const questions = ref<{ id: string; question: string }[]>([])

function addQuestion() {
  questions.value.push({ id: Math.random().toString(36).substring(7), question: '' })
}

// Function to remove a question input field
function removeQuestion(index: number) {
  questions.value.splice(index, 1)
}

// Toast message state
const toastMessage = ref('')

// Form validation (required: title, employer, description)
const isFormValid = computed(() => {
  return jobTitle.value.trim() !== '' &&
  employerName.value.trim() !== '' &&
  jobDescription.value.trim() !== ''
})

// Access the ApplicationService
const applicationService = new ApplicationService()

async function submitForm() {
  // Build Posting object (id: 0 indicates new posting)
  const postingData: Posting = {
    id: 0,
    title: jobTitle.value,
    employer: employerName.value,
    description: jobDescription.value,
    requirements: jobRequirements.value,
    additionalInformation: additionalInfo.value,
    contactInformation: '',  // Removed contact info (sent as blank)
    questions: questions.value.map(q => q.question).filter(q => q.trim() !== '')
  }

  console.log('📤 Payload being sent:', postingData)

  try {
    const result = await applicationService.addPosting(postingData)
    console.log('Server response:', result)

    // Show toast confirmation
    toastMessage.value = 'Posting submitted successfully!'

    // Clear form fields on success
    employerName.value = ''
    jobTitle.value = ''
    jobDescription.value = ''
    jobRequirements.value = ''
    additionalInfo.value = ''
    questions.value = []

    // Hide toast after 3 seconds
    setTimeout(() => {
      toastMessage.value = ''
    }, 3000)
  } catch (err) {
    console.error('Error adding posting:', err)
  }
}
</script>

<template>
  <div class="page">
    <!-- Page Header with instructions for adding a job posting -->
    <div class="header">
      <h1>Add Posting</h1>
      <p>Here, you can create a new job posting by providing a title, description, and any additional questions for applicants.</p>
    </div>

    <!-- Job posting form -->
    <div class="form">
      <!-- Employer Name (now first) -->
      <div class="form-group">
        <div class="field-description">
          <label for="employer" class="field-label">Employer Name</label>
          <span>Enter the name of the employer.</span>
        </div>
        <InputText class="field" id="employer" v-model="employerName" />
      </div>
      <!-- Job Title (now second) -->
      <div class="form-group">
        <div class="field-description">
          <label for="title" class="field-label">Job Title</label>
          <span>Enter the title of the job posting.</span>
        </div>
        <InputText class="field" id="title" v-model="jobTitle" />
      </div>
      <!-- Job Description -->
      <div class="form-group">
        <div class="field-description">
          <label for="description" class="field-label">Job Description</label>
          <span>Provide a detailed description of the job.</span>
        </div>
        <InputText class="field" id="description" v-model="jobDescription" />
      </div>
      <!-- Job Requirements -->
      <div class="form-group">
        <div class="field-description">
          <label for="requirements" class="field-label">Job Requirements</label>
          <span>List the requirements for the job.</span>
        </div>
        <InputText class="field" id="requirements" v-model="jobRequirements" />
      </div>
      <!-- Additional Information -->
      <div class="form-group">
        <div class="field-description">
          <label for="additionalInfo" class="field-label">Additional Information</label>
          <span>Provide any additional information about the job.</span>
        </div>
        <InputText class="field" id="additionalInfo" v-model="additionalInfo" />
      </div>
      <!-- Questions -->
      <div class="form-group">
        <div class="field-description">
          <label for="questions" class="field-label">Questions</label>
          <span>Provide any additional questions for applicants.</span>
          <div style="display: flex; flex-direction: row-reverse; flex-grow: 1">
            <Button @click="addQuestion" label="Add Question" style="width: fit-content; margin-inline: 10px;"/>
          </div>
        </div>
        <transition-group name="fade" tag="div">
          <div v-for="(question, index) in questions" :key="question.id" class="dynamic-question-container">
            <Textarea class="field dynamic-question" v-model="questions[index].question" />
            <Button @click="removeQuestion(index)" icon="pi pi-times" />
          </div>
        </transition-group>
      </div>
      <!-- Submit Button (disabled if form is invalid) -->
      <Button @click="submitForm" label="Submit" :disabled="!isFormValid" />
    </div>
    <!-- Toast Confirmation -->
    <div v-if="toastMessage" class="toast-message">{{ toastMessage }}</div>
  </div>
</template>

<style scoped>
.fade-enter-active, .fade-leave-active {
  transition: all 0.5s ease;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
  transform: translateX(30px);
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
.header p {
  font-size: 18px;
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
  width: 50%;
  box-shadow: #404040 5px 5px 20px;
  background: linear-gradient(145deg, #12121b, #0a1418);
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
.field:hover, .field:focus {
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
.form-group:hover, .form-group:focus-within {
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

/* Toast styling */
.toast-message {
  margin-top: 1rem;
  background-color: #1a90ff;
  color: #fff;
  padding: 0.75rem 1.5rem;
  border-radius: 5px;
  text-align: center;
}
</style>
