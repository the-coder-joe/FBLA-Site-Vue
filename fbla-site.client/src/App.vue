<script setup>
import { RouterLink, RouterView } from 'vue-router';
</script>
<template>
  <nav class="navbar">
    <!-- School Title and Logo Link to Homepage -->
    <div class="School-Title">
      <RouterLink class="nav-link" to="/">
        <div class="logo-name">
          <img src="../src/assets/Logo.png" class="logo"/> Summit Valley School District
        </div>
      </RouterLink>
    </div>

    <!-- Navigation Links -->
    <RouterLink class="nav-link" to="/AddPosting">Add A Posting</RouterLink>
    <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>

    <!-- Login Link -->
    <div class="nav-links">
      <RouterLink class="nav-link" to="/LoginPage">Login</RouterLink>
    </div>
  </nav>

  <!-- Main Content View -->
  <main>
    <RouterView />
  </main>
</template>

<style scoped>
/* NavBar Styling */
.navbar {
  position: fixed;
  top: 0;
  z-index: 1000;
  width: 100%;
  background: linear-gradient(to right, #1a1a1a, #2a2b2b);
  padding: .75rem 1rem;
  display: flex;
  justify-content: space-between;
}

/* School Title Styling */
.School-Title {
  color: #ffffff;
  font-size: 1.5rem;
  font-weight: 750;
  display: flex;
  align-items: center;
}

/* Nav Links Container */
.nav-links {
  display: flex;
  gap: .5rem;
}

/* Navigation Links */
.nav-link {
  color: #9be9ff;
  font-size: 1.3rem;
  transition: color 0.55s ease;
}

/* Link Hover/Focus Styling */
.nav-link:hover,
.nav-link:focus {
  color: #ffffff;
  outline: none;
}
</style>

<!-- Ensure Navbar Doesn't Cover Content -->
<style>
body {
  padding-top: 64px;
}

.logo {
  border-radius: 50%;
  height: 65px;
  padding-right: 10px;
}
</style>
