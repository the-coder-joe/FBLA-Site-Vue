import { createMemoryHistory, createRouter } from 'vue-router';

import HomePage from '@/components/Pages/HomePage.vue';
import AddPosting from '@/components/Pages/AddPosting.vue';
import LoginPage from '@/components/Pages/LoginPage.vue';
import NotFound from '@/components/Pages/NotFound.vue';


const routes = [
  { path: '/', component: HomePage },
  { path: '/AddPosting', component: AddPosting },
  { path: '/LoginPage', component: LoginPage },
  { path: '/:pathMatch(.*)*', component: NotFound }
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router
