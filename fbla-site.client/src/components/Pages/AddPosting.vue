<script setup lang="ts">
import Button from 'primevue/button'
import ApplicationService from '@/services/application.service';
import { ref } from 'vue';
import InputText from 'primevue/inputtext'
import { Textarea } from 'primevue';
import { Posting } from '@/models/application.models';

const applicationService: ApplicationService = new ApplicationService();

const jobTitle = ref('');
const jobDescription = ref('');
const jobRequirements = ref('');
const additionalInfo = ref('');
const employerName = ref('');

const questions = ref([]);

function addQuestion() {
  questions.value.push({id:Math.random().toString(36).substring(7), question: ''});
}

function removeQuestion(index: number) {
  questions.value.splice(index, 1);
}

function submitForm() {
  const postingData: Posting = {
    title: jobTitle.value,
    description: jobDescription.value,
    questions: questions.value.map(q => q.question),
    requirements: jobRequirements.value,
    additionalInformation: additionalInfo.value,
    employer: employerName.value
  };

  applicationService.addPosting(postingData);
  jobTitle.value = '';
  jobDescription.value = '';
  jobRequirements.value = '';
  additionalInfo.value = '';
  employerName.value = '';
  questions.value = [];
}

</script>

<template>
  <div class="page">
    <div class="header">
      <h1>Add Posting</h1>
      <p>Here, you can create a new job posting by providing a title, description, and any additional questions for
        applicants.</p>
    </div>
    <div class="form">
      <div class="form-group">
        <div class="field-description">
          <label for="title" class="field-label">Job Title</label>
          <span>Enter the title of the job posting.</span>
        </div>
        <InputText class="field" id="title" v-model="jobTitle" />
      </div>
      <div class="form-group">
        <div class="field-description">
          <label for="employer" class="field-label">Employer Name</label>
          <span>Enter the name of the employer.</span>
        </div>
        <InputText class="field" id="employer" v-model="employerName" />
      </div>

      <div class="form-group">
        <div class="field-description">
          <label for="description" class="field-label">Job Description</label>
          <span>Provide a detailed description of the job.</span>
        </div>
        <InputText class="field" id="description" v-model="jobDescription" />
      </div>

      <div class="form-group">
        <div class="field-description">
          <label for="requirements" class="field-label">Job Requirements</label>
          <span>List the requirements for the job.</span>
        </div>
        <InputText class="field" id="requirements" v-model="jobRequirements" />
      </div>

      <div class="form-group">
        <div class="field-description">
          <label for="additionalInfo" class="field-label">Additional Information</label>
          <span>Provide any additional information about the job.</span>
        </div>
        <InputText class="field" id="additionalInfo" v-model="additionalInfo" />
      </div>


      <div class="form-group">
        <div class="field-description">
          <label for="questions" class="field-label">Questions</label>
          <span>Provide any additional questions for applicants.</span>
          <div style="display: flex; flex-direction: row-reverse; flex-grow: 1"><Button @click="addQuestion" label="Add Question" style="width: fit-content; margin-inline: 10px; flex: end;"/></div>
        </div>
        <TransitionGroup name="fade" tag="div">
        <div v-for="(question, index) in questions" :key="question.id" class="dynamic-question-container">
          <Textarea class="field dynamic-question" v-model="questions[index].question" />
          <Button @click="removeQuestion(index)" icon="pi pi-times" />
        </div>
        </TransitionGroup>
      </div>
      <Button @click="submitForm" label="Submit" />
    </div>

  </div>
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: all 0.5s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
  transform: translateX(30px);
}

.header {
  margin-top: 10px;
  text-align: center;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

.page {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.form {
  padding: 30px;
  display: flex;
  flex-direction: column;
  width: 50%;
  background-color: #111111;
  box-shadow: #404040 5px 5px 20px;
  background: linear-gradient(145deg, #12121b, #0a1418);

}

.field {
  border: 1px solid gray;
  border-radius: 5px;
  margin-block: 0.1rem;
  padding: 0.5rem;
  font-size: large;
  transition: all 0.3s;
}

.field:hover, .field:focus {
  padding: 0.6rem;
  border-color: lightblue;
  margin-block: 0;;
}

.form-group {
  border: 1px solid rgba(128, 128, 128, 0.235);
  border-radius: 10px;
  padding: 10px;
  display: flex;
  flex-direction: column;
  margin-bottom: 1rem;
  box-shadow: #2e2e2e53 5px 5px 5px;
  background-color: #1414174b;
  transition: all 0.5s;
}

.form-group:hover, .form-group:focus-within {
  box-shadow: #2e2e2eb7 5px 5px 20px;
  background-color: #1a202faa;
}

.field-label {
  font-size: 2rem;
  color: lightblue;
}

.field-description {
  margin-bottom: 1rem;
  display: flex;
  align-items: baseline;
}

.field-description span {
  margin-left: 1rem;
  font-size: 1rem;
  color: gray;
}

.dynamic-question-container {
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
}

.dynamic-question {
  width: 100%;
  margin-inline: 10px;
}
</style>
