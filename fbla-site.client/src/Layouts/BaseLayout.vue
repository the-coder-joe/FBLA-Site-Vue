<template>
    <NavBar />
    <main>
      <RouterView />
    </main>
    <Footer />
  </template>
  
  <script setup>
  import { RouterView } from 'vue-router'
  import NavBar from '../components/Pages/NavBar.vue'
  import Footer from '../components/Pages/BottomFooter.vue'
  </script>
  
  <style scoped>
  main {
    margin-bottom: 5rem;
  }
</style>
  