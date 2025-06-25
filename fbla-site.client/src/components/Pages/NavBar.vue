<script setup>
import { RouterLink, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authentication.store'
import { useToast } from 'primevue/usetoast';
import { ref } from 'vue'

const showAdminDropdown = ref(false)
const authStore = useAuthStore();
const toast = useToast();
const router = useRouter();

function logout() {
  authStore.authenticated = false;
  authStore.role = '';
  authStore.username = '';
  router.push({ name: 'home' });
  toast.add({
    severity: 'info',
    summary: 'Logged Out',
    detail: 'You have successfully logged out.',
    life: 3000
  });
}
</script>

<template>
  <nav class="navbar">
    <div class="School-Title">
      <RouterLink class="nav-link" to="/">
        <div class="logo-name">
          <img src="../../assets/Logo.png" class="logo" />
          <span class="logo-text"> Summit Valley School District</span>
        </div>
      </RouterLink>

      <RouterLink v-if="authStore.role === 'employer' || authStore.role === 'admin'" class="nav-link" to="/AddPosting">
        Add A Posting
      </RouterLink>

      <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>

    <div v-if="authStore.role === 'admin'" class="dropdown" @mouseenter="showAdminDropdown = true" @mouseleave="showAdminDropdown = false">
      <span class="nav-link dropdown-label">Admin Menu ▾</span>
      <div class="dropdown-content" v-show="showAdminDropdown">
        <RouterLink class="nav-link dropdown-link" to="/Admin">Approve Postings</RouterLink>
        <RouterLink class="nav-link dropdown-link" :to="{ path: '/LoginPage', query: { mode: 'signup' } }"> Create Account </RouterLink>
        <RouterLink class="nav-link dropdown-link" to="/Analytics">Analytics</RouterLink>
      </div>
    </div>
    </div>

    <div class="nav-links">
      <RouterLink v-if="!authStore.authenticated" class="nav-link" to="/LoginPage">Login</RouterLink>
      <a v-else class="nav-link" @click="logout()">Log Out</a>
    </div>
  </nav>
</template>

<style scoped>
.navbar {
  position: fixed;
  top: 0;
  z-index: 1000;
  width: 100%;
  background: linear-gradient(to right, #1a1a1aff, #2a2b2bff);
  padding: .75rem 1rem;
  padding-top: 3px;
  padding-bottom: 3px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  box-shadow: 0 2px 8px 0px rgba(0, 255, 255, 0.097);
}

.School-Title {
  color: #ffffff;
  font-size: 1.5rem;
  font-weight: 750;
  text-align: center;
  display: flex;
  align-items: center;
}

.nav-links {
  display: flex;
  gap: .5rem;
}

.nav-link {
  color: #9be9ff;
  font-size: 1.3rem;
  transition: color 0.55s ease;
  display: block;
  user-select: none;
  cursor: pointer;
}

.nav-link:hover,
.nav-link:focus {
  color: #ffffff;
  outline: none;
}

/* Dropdown styles */
.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-content {
  min-width: 170px; 
  margin-top: 2px;
  padding: 5px 0;  
  display: none;
  position: absolute;
  background-color: #1a1a1a;
  box-shadow: 0 4px 8px rgba(0, 255, 255, 0.2);
  z-index: 10;
  border-radius: 5px;
}

.dropdown-link {
  padding: 10px 16px;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.dropdown-label {
  cursor: pointer;
}

.dropdown-link {
  padding: 8px 12px;
  font-size: 1rem;
}

.dropdown-link:hover {
  background-color: #00bfff;
  color: white;
}

/* Existing logo styles */
.logo {
  border-radius: 50%;
  height: 65px;
  padding-right: 10px;
}

.logo-name {
  align-items: center;
  display: flex;
  justify-content: center;
}

@media (max-width: 600px) {
  .logo-text {
    display: none;
  }

  .nav-link {
    font-size: 0.8rem !important;
    margin-inline: 5px;
  }

  .navbar {
    padding: 0 !important;
  }

  .logo {
    padding: 0 !important;
    height: 50px !important;
  }
}
</style>

<style>
body {
  padding-top: 64px;
}
</style>
