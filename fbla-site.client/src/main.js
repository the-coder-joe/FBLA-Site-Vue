import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './scripts/router'
import { PrimeVue } from '@primevue/core'



const app = createApp(App);
app.use(router);
app.use(PrimeVue,
  {

  });

app.mount('#app');
