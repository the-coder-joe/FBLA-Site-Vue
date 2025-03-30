<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue'
import { useRoute } from 'vue-router'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import ApplicationService from '@/services/application.service'

interface Posting {
  id: number
  title: string
  description: string
  requirements: string
  additionalInformation?: string
  employer: string
  questions: string[]
  contactInformation: string
}

const route = useRoute()
const postingId = parseInt(route.params.postingId as string, 10)
const applicationService = new ApplicationService()

const posting = ref<Posting | null>(null)
const studentAnswers = ref<string[]>([])
const studentName = ref('')
const studentContact = ref('')
const toastMessage = ref('')

onMounted(async () => {
  try {
    const data = await applicationService.getPosting(postingId)
    posting.value = {
      ...data,
      additionalInformation: data.additionalInformation ?? ''
    }
  } catch (error) {
    console.error('Error fetching posting:', error)
  }
})

watch(posting, (newVal) => {
  if (newVal && newVal.questions) {
    studentAnswers.value = newVal.questions.map(() => '')
  }
})

const isAppFormValid = computed(() => {
  return studentName.value.trim() !== '' && studentContact.value.trim() !== ''
})

function submitApplication() {
  console.log('Application Submitted:')
  console.log('Student Name:', studentName.value)
  console.log('Student Contact:', studentContact.value)
  console.log('Answers:', studentAnswers.value)

  // Clear form fields
  studentName.value = ''
  studentContact.value = ''
  studentAnswers.value = studentAnswers.value.map(() => '')

  // toast
  toastMessage.value = 'Application submitted successfully!'
  setTimeout(() => {
    toastMessage.value = ''
  }, 3000)
}
</script>

<template>
  <div class="application-page" v-if="posting">
    <div class="posting-details">
      <h1>{{ posting.title }}</h1>
      <h2>{{ posting.employer }}</h2>
      <p><strong>Description:</strong> {{ posting.description }}</p>
      <p><strong>Requirements:</strong> {{ posting.requirements }}</p>
      <p><strong>Additional Information:</strong> {{ posting.additionalInformation }}</p>
    </div>

    <div class="application-form">
      <h2>Apply for this Job</h2>
      <div class="form-group">
        <label for="studentName">Your Name</label>
        <InputText id="studentName" v-model="studentName" class="field" placeholder="Enter your name" />
      </div>
      <div class="form-group">
        <label for="studentContact">Contact Information</label>
        <InputText id="studentContact" v-model="studentContact" class="field" placeholder="Enter your email or phone" />
      </div>
      
      <div v-for="(question, index) in posting.questions" :key="index" class="form-group question-group">
        <label :for="'question' + index">{{ question }}</label>
        <Textarea :id="'question' + index" v-model="studentAnswers[index]" class="field" placeholder="Your answer" />
      </div>
      
      <Button 
        @click="submitApplication" 
        label="Submit Application" 
        class="btn submit-btn" 
        :disabled="!isAppFormValid"
      />
    </div>
    <div v-if="toastMessage" class="toast-message">{{ toastMessage }}</div>
  </div>
  <div v-else class="loading">
    <p>Loading posting details...</p>
  </div>
</template>

<style scoped>
.application-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
  background: none;
  color: #9be9ff;
}

.posting-details {
  width: 50%;
  background-color: rgba(17, 17, 17, 0.85);
  box-shadow: #404040 5px 5px 20px;
  padding: 2rem;
  margin-bottom: 2rem;
  border-radius: 10px;
}

.posting-details h1 {
  font-size: 2.5rem;
  color: lightblue;
}

.posting-details h2 {
  font-size: 2rem;
  color: #9be9ff;
  margin-bottom: 1rem;
}

.posting-details p {
  font-size: 1.1rem;
  margin-bottom: 1rem;
  color: #fff; 
}
.posting-details p strong {
  color: #9be9ff; 
}

.application-form {
  width: 50%;
  background-color: rgba(17, 17, 17, 0.85);
  box-shadow: #404040 5px 5px 20px;
  padding: 2rem;
  border-radius: 10px;
}

.application-form h2 {
  text-align: center;
  color: lightblue;
  margin-bottom: 1.5rem;
}

.form-group {
  margin-bottom: 1.5rem;
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-size: 1.2rem;
  margin-bottom: 0.5rem;
  color: lightblue;
}

.field {
  border: 1px solid gray;
  border-radius: 5px;
  padding: 0.5rem;
  font-size: 1rem;
  transition: all 0.3s;
  background-color: #141417;
  color: #9be9ff;
}

.field:focus {
  border-color: lightblue;
}

.btn {
  display: block;
  width: 100%;
  padding: 0.75rem;
  font-size: 1.2rem;
  color: #9be9ff;
  background-color: #1a90ff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn:hover {
  background-color: #00bfff;
}

.loading {
  text-align: center;
  padding: 2rem;
  font-size: 1.5rem;
  color: #cceeff;
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
.fade-enter-active, .fade-leave-active {
  transition: all 0.5s ease;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
  transform: translateX(30px);
}
</style>
