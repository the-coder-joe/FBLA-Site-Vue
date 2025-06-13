<script setup>
import { RouterLink, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authentication.store'
import { useToast } from 'primevue/usetoast';

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
        Add A Posting</RouterLink>
      <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>
      <RouterLink v-if="authStore.role === 'admin'" class="nav-link" to="/Admin">Admin Page
      </RouterLink>
    </div>
    <div class="nav-links">
      <RouterLink v-if="!authStore.authenticated" class="nav-link" to="/LoginPage">Login</RouterLink>
      <a v-else class="nav-link" @click="logout()">Log Out</a>
    </div>
  </nav>

</template>

<style scoped>
/*NavBar Styling*/
.navbar {
  position: fixed;
  top: 0;
  z-index: 1000;
  /*The overlay hierarchy (lower < higher)*/
  width: 100%;
  background: linear-gradient(to right, #1a1a1a, #2a2b2b);
  /*Changes NavBar background color*/
  padding: .75rem 1rem;
  display: flex;
  align-items: center;
  /*Just makes values in middle vertically*/
  justify-content: space-between;
  box-shadow: 0 2px 8px rgba(0, 255, 255, 0.247);
  /*Gives ambiance*/
}

/*School Text*/
/*NavBar Styling*/
.navbar {
  position: fixed;
  top: 0;
  z-index: 1000;
  /*The overlay hierarchy (lower < higher)*/
  width: 100%;
  background: linear-gradient(to right, #1a1a1a, #2a2b2b);
  /*Changes NavBar background color*/
  padding: .75rem 1rem;
  padding-top: 3px;
  padding-bottom: 3px;
  display: flex;
  align-items: center;
  /*Just makes values in middle vertically*/
  justify-content: space-between;
  box-shadow: 0 2px 8px rgba(0, 255, 255, 0.247);
  /*Gives ambiance*/
}

/*School Text*/
.School-Title {
  color: #ffffff;
  font-size: 1.5rem;
  font-weight: 750;
  /*100-1000 higher = more bold*/
  text-align: center;
  display: flex;
  align-items: center;
}

/*Container*/
.nav-links {
  display: flex;
  gap: .5rem;
}

/* LINKS */
.nav-link {
  color: #9be9ff;
  font-size: 1.3rem;
  transition: color 0.55s ease;
  display: block;
  user-select: none;
  cursor: pointer;
}

/*Simply highlights what link/tab their on*/
/* .router-link-active {
  color: white;
  border-bottom: 2px solid #9be9ff;
} */


/*LINK INTERACTION*/
.nav-link:hover,
.nav-link:focus {
  color: #ffffff;
  outline: none;
}
</style>

<!--MNavbar wont cover over the other-->
<style>
body {
  padding-top: 64px;
}

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
