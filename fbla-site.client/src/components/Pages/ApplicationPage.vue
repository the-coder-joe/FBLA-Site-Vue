<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Textarea from 'primevue/textarea'
import ProgressSpinner from 'primevue/progressspinner'
import ApplicationService from '@/services/application.service'
import { Application } from '@/models/application.models'

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
const router = useRouter()

const posting = ref<Posting | null>(null)
const studentAnswers = ref<string[]>([])
const studentName = ref('')
const studentContact = ref('')
const loading = ref(false)
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

async function submitApplication() {
  loading.value = true

  console.log('Application Submitted:')
  console.log('Student Name:', studentName.value)
  console.log('Student Contact:', studentContact.value)
  console.log('Answers:', studentAnswers.value)

  const applicationData: Application = {
    forPostingId: posting.value?.id || 0,
    name: studentName.value,
    contactInfo: studentContact.value,
    answers: studentAnswers.value,
    id: 0
  }

  applicationService.submitApplication(applicationData)
    .then(() => {
      console.log('Application submitted successfully')
    })
    .catch((error) => {
      console.error('Error submitting application:', error)
      toastMessage.value = 'Failed to submit application. Please try again.'
    })

  // Clear form fields
  studentName.value = ''
  studentContact.value = ''
  studentAnswers.value = studentAnswers.value.map(() => '')

  showToast('Application submitted successfully!')
  setTimeout(() => {
    router.push({ name: 'home' })
  }, 2000)

  setTimeout(() => {
    loading.value = false
  }, 2000)

  router.push({ name: 'home', query: { toast: 'Application submitted successfully!' } })

}

// Show toast
function showToast(message: string) {
  toastMessage.value = message
  setTimeout(() => {
    toastMessage.value = ''
  }, 3000)
}
</script>

<template>
  <div class="page">
    <Transition name="toast">
      <div class="toast" v-if="toastMessage">{{ toastMessage }}</div>
    </Transition>

    <div class="application-page" v-if="posting">
      <div class="posting-details light-glass">
        <h1>{{ posting.title }}</h1>
        <h2>{{ posting.employer }}</h2>
        <p><strong>Description:</strong> {{ posting.description }}</p>
        <p><strong>Requirements:</strong> {{ posting.requirements }}</p>
        <p><strong>Additional Information:</strong> {{ posting.additionalInformation }}</p>
      </div>

      <div class="application-form glass">
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

        <template v-if="!loading">
          <Button
            @click="submitApplication"
            label="Submit Application"
            class="full-btn"
            :disabled="!isAppFormValid"
          />
        </template>
        <template v-else>
          <ProgressSpinner style="width: 30px; height: 30px; margin-top: 10px;" />
        </template>
      </div>
    </div>

    <div v-else class="loading">
      <p>Loading posting details...</p>
    </div>
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
  width: 90%;max-width: 1000px;
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
  width: 90%;max-width: 1000px;

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
  color: #d9d9d9;
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

.toast {
  position: fixed;
  top: 20px;
  right: 20px;
  background: #1f1f1f;
  color: white;
  padding: 12px 20px;
  border-radius: 6px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.3);
  z-index: 9999;
  font-size: 14px;
  opacity: 0.95;
}

.toast-enter-active,
.toast-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}

.toast-enter-from,
.toast-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}

</style>
