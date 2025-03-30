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
    </div>
    <div class="footer-bottom">
      <p>&copy; 2025 Summit Valley School District. All rights reserved.</p>
    </div>
  </footer>
</template>

<script setup>
// No logic needed
</script>

<style scoped>
.footer {
  background: linear-gradient(to right, #1a1a1a, #2a2b2b);
  color: #9be9ff;
  padding: 2rem 1rem 1rem;
  font-size: 0.95rem;
  border-top: 1px solid #333;
  box-shadow: 0 -2px 8px rgba(0, 255, 255, 0.1);
}

.footer-content {
  display: flex;
  justify-content: center;
  gap: 4rem;
  flex-wrap: wrap;
  max-width: 1200px;
  margin: 0 auto;
  text-align: center;
}

.footer-section {
  flex: 1 1 300px;
  min-width: 200px;
}

.footer-section h3 {
  margin-bottom: 0.5rem;
  color: lightblue;
  font-size: 1.25rem;
}

.footer-section p {
  margin: 0.3rem 0;
  line-height: 1.5;
  color: #cceeff;
}

.footer-bottom {
  margin-top: 2rem;
  text-align: center;
  color: #7fbbcc;
  font-size: 0.85rem;
  border-top: 1px solid #444;
  padding-top: 1rem;
}
</style>
