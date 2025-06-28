<script setup lang="ts">
import { useRouter, useRoute } from 'vue-router'
import { ref, computed, onMounted } from 'vue'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import ProgressSpinner from 'primevue/progressspinner'
import ApplicationService from '@/services/application.service'
import { AuthenticationService } from '@/services/authentication.service'
import { useAuthStore } from '@/stores/authentication.store'

const route = useRoute()

onMounted(() => {
  if (route.query.mode === 'signup') {
    isSignUp.value = true
  }
})

const authenticationService = new AuthenticationService();
const loading = ref(false)

// Switch between Sign In and Create Account modes
const isSignUp = ref(false)

// Allows redirect after successful sign-in
const router = useRouter()
const authStore = useAuthStore()

// Sign In form fields
const email = ref('')
const password = ref('')
const showPassword = ref(false)
const signInErrorMessage = ref('')

const isSignInDisabled = computed(() =>
  email.value.trim() === '' || password.value.trim() === ''
)

async function submitSignIn() {
  loading.value = true
  try {
    const success = await authenticationService.login(email.value, password.value)
    if (success) {
      if (authStore.role === 'admin') {
        isSignUp.value = true
        router.push('/Admin')
      } else if (authStore.role === 'employer') {
        router.push('/AddPosting')
      } else if (authStore.role === 'student') {
        router.push('/Postings')
      }
    } else {
      signInErrorMessage.value = 'Invalid email or password.'
    }
  } catch (err) {
    console.error(err)
    signInErrorMessage.value = 'Login failed. Try again.'
  }
  loading.value = false
}

// Create Account form fields
const signUpEmail = ref('')
const signUpPassword = ref('')
const confirmPassword = ref('')
const showSignUpPassword = ref(false)
const signUpErrorMessage = ref('')

const roles = ref([
  { label: 'Student', value: 'student' },
  { label: 'Employer', value: 'employer' },
  { label: 'Admin', value: 'admin' }
])
const signUpRole = ref(null)

const isSignUpDisabled = computed(() =>
  !signUpRole.value ||
  signUpEmail.value.trim() === '' ||
  signUpPassword.value.trim() === '' ||
  confirmPassword.value.trim() === ''
)

const toastMessage = ref('')

function showToast(message: string) {
  toastMessage.value = message
  setTimeout(() => {
    toastMessage.value = ''
  }, 5000)
}

async function submitSignUp() {
  if (signUpPassword.value !== confirmPassword.value) {
    signUpErrorMessage.value = 'Passwords do not match.'
    return
  }

  loading.value = true
  try {
    const success = await authenticationService.register(
      signUpEmail.value,
      signUpPassword.value,
      signUpRole.value.value
    )

    if (success.success) {
      showToast('Account created successfully!')
      clearAllFields()
      isSignUp.value = false
    } else {
      signUpErrorMessage.value = 'Failed to create account. Please try again.'
    }
  } catch (error) {
    console.error('Error during registration:', error)
    signUpErrorMessage.value = 'An error occurred. Please try again later.'
  }
  loading.value = false
}

function clearAllFields() {
  email.value = ''
  password.value = ''
  signInErrorMessage.value = ''
  showPassword.value = false

  signUpEmail.value = ''
  signUpPassword.value = ''
  confirmPassword.value = ''
  signUpErrorMessage.value = ''
  showSignUpPassword.value = false
  signUpRole.value = null
}

function switchToSignUp() {
  clearAllFields()
  isSignUp.value = true
}

function switchToSignIn() {
  clearAllFields()
  isSignUp.value = false
}

window.addEventListener('keydown', handleKeyDown)

function handleKeyDown(event: KeyboardEvent) {
  if (event.key === 'Enter') {
    if (!isSignUp.value && !isSignInDisabled.value && !loading.value) {
      submitSignIn();
    } else if (isSignUp.value && !isSignUpDisabled.value && !loading.value) {
      submitSignUp();
    }
  }
}
</script>

<template>
  <div class="page">
    <Transition name="toast">
      <div class="toast" v-if="toastMessage">{{ toastMessage }}</div>
    </Transition>

    <div class="header glass">
      <h1 v-if="!isSignUp">Sign In</h1>
      <h1 v-else>Create Account</h1>
      <p v-if="!isSignUp">Use your credentials to access your account.</p>
      <p v-else>Create a new account</p>
    </div>

    <div class="form dark-glass">
      <!-- SIGN IN FORM -->
      <template v-if="!isSignUp">
        <div class="form-group">
          <div class="field-description">
            <label for="email" class="field-label">Email</label>
            <span>Enter your email address.</span>
          </div>
          <InputText id="email" v-model="email" class="field" />
        </div>

        <div class="form-group">
          <div class="field-description">
            <label for="password" class="field-label">Password</label>
            <span>Your secure password.</span>
          </div>
          <InputText :type="showPassword ? 'text' : 'password'" id="password" v-model="password" class="field" />
          <div class="show-password">
            <input type="checkbox" id="showPassword" v-model="showPassword" />
            <label for="showPassword">Show Password</label>
          </div>
          <div v-if="signInErrorMessage" class="error-message">{{ signInErrorMessage }}</div>
        </div>

        <template v-if="!loading">
          <Button @click="submitSignIn" :disabled="isSignInDisabled" label="Sign In" class="full-btn"
            :class="{ active: !isSignInDisabled }" />
        </template>
        <template v-else>
          <ProgressSpinner style="width: 30px; height: 30px; margin-top: 10px" />
        </template>
      </template>

      <!-- CREATE ACCOUNT FORM -->
      <template v-else>
        <div class="form-group">
          <div class="field-description">
            <label for="role" class="field-label">Role</label>
            <span>Select your account type.</span>
          </div>
          <Dropdown id="role" v-model="signUpRole" :options="roles" optionLabel="label" placeholder="Select a role"
            class="field" />
        </div>

        <div class="form-group">
          <div class="field-description">
            <label for="signUpEmail" class="field-label">Email</label>
            <span>Enter your email address.</span>
          </div>
          <InputText id="signUpEmail" v-model="signUpEmail" class="field" />
        </div>

        <div class="form-group">
          <div class="field-description">
            <label for="signUpPassword" class="field-label">Password</label>
            <span>Create a strong password.</span>
          </div>
          <InputText :type="showSignUpPassword ? 'text' : 'password'" id="signUpPassword" v-model="signUpPassword"
            class="field" />
        </div>

        <div class="form-group">
          <div class="field-description">
            <label for="confirmPassword" class="field-label">Confirm Password</label>
            <span>Re-enter your password.</span>
          </div>
          <InputText :type="showSignUpPassword ? 'text' : 'password'" id="confirmPassword" v-model="confirmPassword"
            class="field" />
          <div class="show-password">
            <input type="checkbox" id="showSignUpPassword" v-model="showSignUpPassword" />
            <label for="showSignUpPassword">Show Password</label>
          </div>
          <div v-if="signUpErrorMessage" class="error-message">{{ signUpErrorMessage }}</div>
        </div>

        <template v-if="!loading">
          <Button @click="submitSignUp" :disabled="isSignUpDisabled" label="Create Account" class="full-btn"
            :class="{ active: !isSignUpDisabled }" />
        </template>
        <template v-else>
          <ProgressSpinner style="width: 30px; height: 30px; margin-top: 10px" />
        </template>
      </template>
    </div>
  </div>
</template>


<style scoped>
.page {
  min-height: 70.29vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: 2rem;
  box-sizing: border-box;
  padding-bottom: -10%;
}

/* Toast message */
.toast {
  position: fixed;
  top: 1rem;
  left: 50%;
  transform: translateX(-50%);
  background-color: #1a1a1a;
  color: #9be9ff;
  padding: 1rem 2rem;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 191, 255, 0.5);
  z-index: 1000;
  opacity: 0;
  transition: opacity 0.5s ease;
}

.toast-enter-active,
.toast-leave-active {
  transition: opacity 0.5s;
}

.toast-enter-from,
.toast-leave-to {
  opacity: 0;
}

.toast-enter-to,
.toast-leave-from {
  opacity: 1;
}

/* Header styling */
.header {
  margin-top: 30px;
  text-align: center;
  margin-bottom: 2rem;
  font-size: 25px;
}

.header p {
  font-size: 18px;
}

/* Form styling */
.form {
  padding: 30px;
  display: flex;
  flex-direction: column;
  width: 90%;
  max-width: 1000px;
  border-radius: 10px;
}

/* Field container styling */
.form-group {
  border: 1px solid rgba(128, 128, 128, 0.235);
  border-radius: 10px;
  padding: 10px;
  margin-bottom: 1rem;
  box-shadow: #2e2e2e53 5px 5px 5px;
  background-color: #1414174b;
  transition: all 0.5s;
}

.form-group:hover,
.form-group:focus-within {
  box-shadow: #2e2e2eb7 5px 5px 20px;
  background-color: #1a202faa;
}

/* Field appearance */
.field {
  width: 100%;
  border: 1px solid gray;
  border-radius: 5px;
  margin: 0.2rem 0;
  padding: 0.5rem;
  font-size: large;
  transition: all 0.3s;
}

.field:hover,
.field:focus {
  padding: 0.6rem;
  border-color: lightblue;
  margin: 0.2rem 0;
}

/* Label and prompt styling */
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

/* Password toggle styling */
.show-password {
  margin-top: 0.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: lightblue;
}

/* Error message styling */
.error-message {
  margin-top: 0.2rem;
  color: red;
  font-size: 1rem;
}

/* Button styling */
.login-button {
  background-color: #333;
  border: none;
  color: #9be9ff;
  font-size: 1.2rem;
  padding: 0.75rem 1.5rem;
  margin-top: 1rem;
  transition: all 0.3s ease;
  box-shadow: none;
  border-radius: 5px;
  cursor: not-allowed;
  opacity: 0.5;
}

.login-button.active {
  cursor: pointer;
  opacity: 1;
  background-color: #1a90ff;
  box-shadow: 0 0 10px rgba(0, 191, 255, 0.5);
}

.login-button.active:hover {
  background-color: #00bfff;
  color: white;
  box-shadow: 0 0 20px rgba(0, 191, 255, 0.8);
}

/* Toggle link styling */
.toggle {
  margin-top: 1rem;
  text-align: center;
  font-size: 1rem;
  color: lightblue;
}

.toggle a {
  margin-left: 0.5rem;
  color: #9be9ff;
  cursor: pointer;
  text-decoration: underline;
  transition: color 0.3s ease;
}

.toggle a:hover {
  color: #00bfff;
}

@media (max-width: 700px) {
  .field-description {
    flex-direction: column;
    align-items: center;
  }
}
</style>
