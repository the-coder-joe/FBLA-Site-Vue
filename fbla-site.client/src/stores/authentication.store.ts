
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export const useAuthStore= defineStore('counter', () => {
  const authenticated = ref(false);
  const username = ref('');
  const role = ref('');

  return { authenticated, username, role }
});
