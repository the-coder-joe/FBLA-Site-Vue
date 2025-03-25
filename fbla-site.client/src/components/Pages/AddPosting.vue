<script setup lang="ts">
import { Button, InputText } from 'primevue';
import { BASE_URL } from '@/models/constants';
import ApplicationService from '@/services/application.service';
import { ref } from 'vue';

const applicationService: ApplicationService = new ApplicationService();

const jobTitle = ref('');
const jobDescription = ref('');


async function AddPosting() {
  const url = new URL(`${BASE_URL}/api/addPosting`);

  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(posting)
  }

  const response = await fetch(url, request);

  return response.json();
}
</script>

<template>

  <h1>Add Posting</h1>

  <div class="form">
    <label for="title">Title</label>
    <InputText id="title" v-model="jobTitle" />
    <InputText class="description" id="description" v-model="jobDescription" />

    <Button label="Add Posting" @click=""></Button>
  </div>
</template>
