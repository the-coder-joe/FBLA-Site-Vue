<script setup lang="ts">
import { ref, onMounted } from 'vue'
import ApplicationService from '@/services/application.service'
import { AuthenticationService } from '@/services/authentication.service'

const applicationService = new ApplicationService()
const authenticationService = new AuthenticationService()

const totalUsers = ref(0)
const students = ref(0)
const employers = ref(0)
const admins = ref(0)

const totalPostings = ref(0)
const approvedPostings = ref(0)
const pendingApproval = ref(0)

onMounted(async () => {
  const userStats = await authenticationService.fetchUserStats()
  totalUsers.value = userStats.total
  students.value = userStats.students
  employers.value = userStats.employers
  admins.value = userStats.admins

  const postingStats = await applicationService.fetchPostingStats()
  totalPostings.value = postingStats.total
  approvedPostings.value = postingStats.approved
  pendingApproval.value = postingStats.pending
})
</script>

<template>
  <div class="analytics-page">
    <h1 class="dashboard-title"> Anlytics Dashboard</h1>

    <div class="metrics-container">
      <!-- User Metrics -->
      <div class="metric-card dark-glass">
        <h2> Account Metrics</h2>
        <ul>
          <li>Total Users: <strong>{{ totalUsers }}</strong></li>
          <li>Students: <strong>{{ students }}</strong></li>
          <li>Employers: <strong>{{ employers }}</strong></li>
          <li>Admins: <strong>{{ admins }}</strong></li>
        </ul>
      </div>

      <!-- Posting Metrics -->
      <div class="metric-card dark-glass">
        <h2> Posting Metrics</h2>
        <ul>
          <li>Total Job Postings: <strong>{{ totalPostings }}</strong></li>
          <li>Approved Postings: <strong>{{ approvedPostings }}</strong></li>
          <li>Pending Approval: <strong>{{ pendingApproval }}</strong></li>
        </ul>
      </div>

      <div class="metric-card dark-glass placeholder">
        <h2> Extra Metrics</h2>
        <p>More stuff if we need it, placeholder</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.analytics-page {
  padding: 2rem;
  color: #9be9ff;
  min-height: 70vh;
}

.dashboard-title {
  text-align: center;
  margin-bottom: 2rem;
  font-size: 2rem;
}

.metrics-container {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 1.5rem;
  justify-items: center;
}

.metric-card {
  width: 100%;
  max-width: 400px;
  padding: 1.5rem;
  border-radius: 12px;
  box-shadow: 0 0 15px rgba(0, 255, 255, 0.1);
  background-color: rgba(20, 20, 30, 0.6);
}

.metric-card h2 {
  font-size: 1.4rem;
  border-bottom: 1px solid rgba(128, 128, 128, 0.3);
  padding-bottom: 0.5rem;
  margin-bottom: 1rem;
  color: white;
}

.metric-card ul {
  list-style: none;
  padding: 0;
}

.metric-card li {
  margin-bottom: 0.6rem;
  font-size: 1.1rem;
}

strong {
  color: #fff;
}

.placeholder p {
  font-size: 0.9rem;
  color: #aaa;
}
</style>