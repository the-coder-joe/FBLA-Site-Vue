import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import Router from './scripts/router'
import { PrimeVue } from '@primevue/core'
import InputText from 'primevue/inputtext'
import FloatLabel from 'primevue/floatlabel'
import Aura from '@primeuix/themes/aura';
import { definePreset } from '@primeuix/themes'
import Button from 'primevue/button'
import { createPinia } from 'pinia'
import ToastService from 'primevue/toastservice';

const pinia = createPinia();

const MyPreset = definePreset(Aura, {
  semantic: {
    primary: {
      0: '#ffffff',
      50: '{slate.50}',
      100: '{slate.100}',
      200: '{slate.200}',
      300: '{slate.300}',
      400: '{slate.400}',
      500: '{slate.500}',
      600: '{slate.600}',
      700: '{slate.700}',
      800: '{slate.800}',
      900: '{slate.900}',
      950: '{slate.950}'
    }
  }
});

const app = createApp(App);

app.use(ToastService);
app.use(pinia);
app.use((new Router).getRouter());
app.use(PrimeVue,
  {
    theme: {
      preset: MyPreset,
    }
  });

app.component('InputText', InputText);
app.component('FloatLabel', FloatLabel);
app.component('Button', Button);

app.mount('#app');
