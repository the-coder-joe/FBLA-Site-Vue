<script setup>
  import { RouterLink, RouterView } from 'vue-router';
</script>


<template>
    <nav class="navbar">
    <RouterLink class="nav-link" to="/">Go to Home</RouterLink>
    <RouterLink class="nav-link" to="/about">Go to About</RouterLink>
  </nav>
  <main>
    <RouterView />
  </main>
</template>

<style scoped>
</style>
