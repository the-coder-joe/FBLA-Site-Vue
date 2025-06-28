<script setup lang="ts">
import { ref, onMounted } from 'vue'
import ApplicationService from '@/services/application.service';
import { useAuthStore } from '@/stores/authentication.store';
import { useToast } from 'primevue/usetoast';
import { useRouter } from 'vue-router';
import Accordion from 'primevue/accordion';
import AccordionPanel from 'primevue/accordionpanel';
import AccordionHeader from 'primevue/accordionheader';
import AccordionContent from 'primevue/accordioncontent';
import { Application, Posting } from '@/models/application.models';

const applicationService = new ApplicationService();
const authStore = useAuthStore();
const toast = useToast();
const router = useRouter();

const postings = ref<Posting[]>([]);
const applications = ref<Application[]>([]);

onMounted(async () => {
  if (!(authStore?.role === 'employer' || authStore?.role === 'admin')) {
    toast.add({ severity: 'error', summary: 'Access Denied', detail: 'You must be an employer to view this page.', life: 3000 });
    router.push('/'); // Redirect to home or another appropriate page
    return;
  }

  const employerId = authStore?.id;
  if (employerId !== null) {
    postings.value = await applicationService.getPostingsByEmployerId(employerId);
    applications.value = await applicationService.getJobApplicationsByEmployerId(employerId);
  }
});

async function del(id: number) {
  applicationService.deletePosting(id).then(async () => {
    postings.value = postings.value.filter(posting => posting.id !== id);
    toast.add({ severity: 'success', summary: 'Posting Deleted', detail: 'The posting has been successfully deleted.', life: 3000 });
    const employerId = authStore?.id;

    postings.value = await applicationService.getPostingsByEmployerId(employerId);
    applications.value = await applicationService.getJobApplicationsByEmployerId(employerId);
  }).catch(error => {
    console.error('Error deleting posting:', error);
    toast.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete the posting.', life: 3000 });
  });
}


</script>

<template>

  <div class="header dark-glass">
    <h1>Your Postings</h1>
  </div>

  <Accordion class="accordion">
    <AccordionPanel v-for="posting in postings" :key="posting.id" :value="posting.id" class="">
      <AccordionHeader>
        <div
          style="display: flex; justify-content: space-between; align-items: center; width: 100%; padding-inline: 30px;">
          <h2>{{ posting.title }}</h2>
          <Button label="Delete Posting" class="p-button-danger" @click.stop="del(posting.id)" />
        </div>
      </AccordionHeader>

      <AccordionContent>
        <h3>Applications: </h3>
        <ul>
          <li v-for="application in applications.filter(app => app.forPostingId === posting.id)" :key="application.id">
            <strong>{{ application.name }}</strong> - {{ application.contactInfo }}
            <ul>
              <li v-for="(question, index) in posting.questions" :key="index">
                <strong>Question {{ index + 1 }}:</strong> {{ question }}<br>
                <strong>Answer:</strong> {{ application.answers[index] || 'No answer provided' }}
              </li>
            </ul>
          </li>
        </ul>
      </AccordionContent>
    </AccordionPanel>
  </Accordion>




</template>

<style scoped lang="css">
.header {
  margin: 30px auto;
  width: 100%;
}

.accordion {
  width: 90%;
  max-width: 1400px;
  margin: 0 auto;
}
</style>
