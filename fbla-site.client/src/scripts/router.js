import { createMemoryHistory, createRouter } from 'vue-router';

import HomePage from '@/components/Pages/HomePage.vue';
import AddPosting from '@/components/Pages/AddPosting.vue';
import LoginPage from '@/components/Pages/LoginPage.vue';
import NotFound from '@/components/Pages/NotFound.vue';
import PostingsPage from '@/components/Pages/PostingsPage.vue';
import ApplicationPage from '@/components/Pages/ApplicationPage.vue';
import AdminPage from '@/components/Pages/AdminPage.vue';



const routes = [
  { path: '/', component: HomePage, meta: { authRequired: false } },
  { path: '/AddPosting', component: AddPosting, meta: { authRequired: false } },
  { path: '/Postings', component: PostingsPage, meta: { authRequired: false } },
  { path: '/Apply:postingId', name: 'apply', component: ApplicationPage, meta: { authRequired: false } },
  { path: '/LoginPage', component: LoginPage, meta: { authRequired: false } },
  { path: '/Admin', name: 'admin', component: AdminPage, meta: { authRequired: true } },
  { path: '/:pathMatch(.*)*', component: NotFound, meta: { authRequired: false } }
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
});


router.beforeEach(async (to, from) => {
  const isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';

  if (to.meta.authRequired && !isAuthenticated) {
    return { path: '/LoginPage' };
  }
});

export default router
