<script setup>
import { RouterLink, RouterView } from 'vue-router';
</script>

<template>
  <nav class="navbar">
    <div class="School-Title"><RouterLink class="nav-link" to="/">
      <div class="logo-name">
        <img src="../src/assets/Logo.png" class="logo"/> Summit Valley School District
      </div>
    </RouterLink>
    <RouterLink class="nav-link" to="/AddPosting">Add A Posting</RouterLink>
      <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>
  </div>
    <div class="nav-links">
      <RouterLink class="nav-link" to="/LoginPage">Login</RouterLink>
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
  padding-top: 3px;
  padding-bottom: 3px;
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
</style>
