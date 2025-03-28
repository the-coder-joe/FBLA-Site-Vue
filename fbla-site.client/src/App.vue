<script setup>
  import { RouterLink, RouterView } from 'vue-router';
</script>


<template>
    <nav class="navbar">
    <RouterLink class="nav-link" to="/">Go to Home</RouterLink>
    <RouterLink class="nav-link" to="/AddPosting">Add A Posting</RouterLink>
    <RouterLink class="nav-link" to="/Postings">View Postings</RouterLink>
  </nav>
  <main>
    <RouterView />
  </main>
</template>

<style scoped>
</style>
