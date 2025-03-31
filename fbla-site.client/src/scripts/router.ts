import { createMemoryHistory, createRouter } from 'vue-router';

import HomePage from '@/components/Pages/HomePage.vue';
import AddPosting from '@/components/Pages/AddPosting.vue';
import LoginPage from '@/components/Pages/LoginPage.vue';
import NotFound from '@/components/Pages/NotFound.vue';
import PostingsPage from '@/components/Pages/PostingsPage.vue';
import ApplicationPage from '@/components/Pages/ApplicationPage.vue';
import AdminPage from '@/components/Pages/AdminPage.vue';
import SourcesPage from '@/components/Pages/SourcesPage.vue';
import { useAuthStore } from '@/stores/authentication.store';

export default class Router {
  private router: any;
  private static instance: Router;

  constructor() {
    if (Router.instance) {
      return Router.instance;
    }

    Router.instance = this;
    const authStore = useAuthStore();

    const routes = [
      { path: '/', name: 'home', component: HomePage, meta: { authRequired: false } },
      { path: '/AddPosting', component: AddPosting, meta: { authRequired: false } },
      { path: '/Postings', component: PostingsPage, meta: { authRequired: false } },
      { path: '/Apply:postingId', name: 'apply', component: ApplicationPage, meta: { authRequired: false } },
      { path: '/LoginPage', component: LoginPage, meta: { authRequired: false } },
      { path: '/SourcesPage', component: SourcesPage, meta: { authRequired: false } },
      { path: '/Admin', name: 'admin', component: AdminPage, meta: { authRequired: true } },
      { path: '/:pathMatch(.*)*', component: NotFound, meta: { authRequired: false } }
    ]

    this.router = createRouter({
      history: createMemoryHistory(),
      routes,
      scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
          return savedPosition
        }
        return { top: 0}
      }
    });


    this.router.beforeEach(async (to, from) => {
      const isAuthenticated = authStore.authenticated;

      if (to.meta.authRequired && !isAuthenticated) {
        return { path: '/LoginPage' };
      }
    });
  }

  public getRouter() {
    return this.router;
  }
}
