<script setup>
import { RouterLink, RouterView } from 'vue-router';
</script>

<template>
  <nav class="navbar">
    <div class="School-Title">Summer Vally</div>
    <div class="nav-links">
      <RouterLink class="nav-link" to="/">Home</RouterLink>
      <RouterLink class="nav-link" to="/AddPosting">Add A Posting</RouterLink>
      <RouterLink class="nav-link" to="/LoginPage">Login</RouterLink>
      <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>
    </div>
  </nav>

  <main>
    <RouterView />
  </main>
</template>

<style scoped>
/*NavBar Styling*/
.navbar {
  position: fixed;
  top: 0;
  z-index: 1000; /*The overlay hierarchy (lower < higher)*/
  width: 100%;
  background: linear-gradient(to right, #1a1a1a, #2a2b2b); /*Changes NavBar background color*/
  padding: .75rem 1rem; 
  display: flex;
  align-items: center; /*Just makes values in middle vertically*/
  justify-content: space-between;
  box-shadow: 0 2px 8px rgba(0, 255, 255, 0.247); /*Gives ambiance*/
}

/*School Text*/
.School-Title {
  color: #ffffff;
  font-size: 1.5rem;
  font-weight: 750; /*100-1000 higher = more bold*/
}

/*Container*/
.nav-links {
  display: flex;
  gap: .5rem;
}

/* LINKS */
.nav-link {
  color: #9be9ff;
  font-size: 1rem;
  font-weight: 500;
  transition: color 0.55s ease;
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
</style>