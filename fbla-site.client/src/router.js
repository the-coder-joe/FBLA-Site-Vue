import { createMemoryHistory, createRouter } from 'vue-router';

import HomePage from './components/Pages/HomePage.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/SubmitApplication'}, 
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router
