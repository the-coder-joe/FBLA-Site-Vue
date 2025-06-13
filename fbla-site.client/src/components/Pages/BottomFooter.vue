<template>
  <footer class="footer">
    <div class="footer-content">
      <div class="footer-section">
        <h3>Location</h3>
        <p>📍 123 School Ave<br />Summit Valley, WI 54481</p>
      </div>

      <div class="footer-section">
        <h3>Contact</h3>
        <p>📞 (715) 123-4567</p>
        <p>✉️ guidance@summitvalleyschool.org</p>
      </div>

      <div class="footer-section">
        <h3>Quick Links</h3>
        <ul>
          <li><RouterLink to="/SourcesPage">Sources</RouterLink></li>
        </ul>
      </div>
    </div>

    <div class="footer-bottom">
      <p>&copy; 2025 Summit Valley School District. All rights reserved.</p>
    </div>
  </footer>
</template>

<script setup>
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.footer {
  background: linear-gradient(to right, #1a1a1a, #2a2b2b);
  color: #9be9ff;
  padding: 2rem 1rem 1rem;
  font-size: 0.95rem;
  border-top: 1px solid #333;
  box-shadow: 0 0 10px 3px rgba(0, 255, 255, 0.062);
}

.footer-content {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  gap: 2rem;
  max-width: 1200px;
  margin: auto;
}

.footer-section {
  flex: 1 1 250px;
}

.footer-section h3 {
  margin-bottom: 0.5rem;
  color: lightblue;
}

.footer-section p,
.footer-section a,
.footer-section ul li a {
  color: #cceeff;
  line-height: 1.5;
  text-decoration: none;
}

.footer-section a:hover {
  text-decoration: underline;
  color: white;
}

.footer-bottom {
  margin-top: 2rem;
  text-align: center;
  color: #7fbbcc;
  font-size: 0.85rem;
  border-top: 1px solid #444;
  padding-top: 1rem;
}

ul {
  list-style: none;
  padding-left: 0;
}

ul li {
  margin-bottom: 0.5rem;
}
</style>
