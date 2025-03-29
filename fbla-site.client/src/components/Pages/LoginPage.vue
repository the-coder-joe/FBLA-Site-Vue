<script setup lang="ts">
import { ref, computed } from 'vue'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'

// Fields
const email = ref('')
const password = ref('')
const showPassword = ref(false)
const errorMessage = ref('')

// Button enable logic
const isLoginDisabled = computed(() =>
  email.value.trim() === '' || password.value.trim() === ''
)

// Dummy login logic
function submitLogin() {
  if (email.value === 'user@example.com' && password.value === 'password123') {
    console.log('Logged in!')
    errorMessage.value = ''
  } else {
    errorMessage.value = 'Invalid email or password.'
    password.value = ''
  }
}
</script>

<template>
  <div class="page">
    <div class="header">
      <h1>Login</h1>
      <p>Use your credentials to access the job portal.</p>
    </div>
    <div class="form">
      <!-- Email Field -->
      <div class="form-group">
        <div class="field-description">
          <label for="email" class="field-label">Email</label>
          <span>Enter your school or employer email.</span>
        </div>
        <InputText id="email" v-model="email" class="field" />
      </div>

      <!-- Password Field -->
      <div class="form-group">
        <div class="field-description">
          <label for="password" class="field-label">Password</label>
          <span>Your secure password for login.</span>
        </div>
        <InputText
          :type="showPassword ? 'text' : 'password'"
          id="password"
          v-model="password"
          class="field"
        />
        <div class="show-password">
          <input type="checkbox" id="showPassword" v-model="showPassword" />
          <label for="showPassword">Show Password</label>
        </div>
        <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
      </div>

      <!-- Submit -->
      <Button
        @click="submitLogin"
        :disabled="isLoginDisabled"
        label="Login"
        class="login-button"
        :class="{ active: !isLoginDisabled }"
      />
    </div>
  </div>
</template>

<style scoped>

/*Page*/
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
}
.header {
  margin-top: 10px;
  text-align: center;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

/*Form styling*/
.form {
  padding: 30px;
  display: flex;
  flex-direction: column;
  width: 50%;
  background-color: #111111;
  box-shadow: #404040 5px 5px 20px;
  border-radius: 10px;
}

/*Container*/
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
.form-group:hover,
.form-group:focus-within {
  box-shadow: #2e2e2eb7 5px 5px 20px;
  background-color: #1a202faa;
}

/*Changes field looks*/
.field {
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

/*Label and prompt*/
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

/*Show password logic*/
.show-password {
  margin-top: 0.5rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: lightblue;
}

/*Requires filled pass/email*/
.error-message {
  margin-top: 0.5rem;
  color: red;
  font-size: 1rem;
}

/*Button styling*/
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
</style>
