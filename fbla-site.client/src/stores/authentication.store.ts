
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export const useAuthStore= defineStore('counter', () => {
  const authenticated = ref(false);
  const username = ref('');
  const role = ref('');
  const id = ref<number>(0);

  return { authenticated, username, role, id }
});
