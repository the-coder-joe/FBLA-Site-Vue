<template>
    <div class="page">
      <div class="header glass">
        <h1>Sources</h1>
        <p>Below is a list of references and sources used in creating this project.</p>
      </div>
      <div class="sources-container">
        <ul class="sources-list">
          <li>
            <a href="https://primevue.org" target="_blank">PrimeVue Documentation</a>
          </li>
          <li>
            <a href="https://vuejs.org/guide/introduction" target="_blank">Vue Documentation</a>
          </li>
          <li>
            <a href="https://pinia.vuejs.org/" target="_blank">Pinia Documentation</a>
          </li>
          <li>
            <a href="https://vite.dev/" target="_blank">Vite Documentation</a>
          </li>
          <li>
            <a href="https://github.com/" target="_blank">Github</a>
          </li>
          <li>
            <a href="https://visualstudio.microsoft.com/" target="_blank">Visual Studio</a>
          </li>
          <li>
            <a href="https://stackoverflow.com/questions" target="_blank">Stack Overflow</a>
          </li>
        </ul>
      </div>
    </div>
  </template>

  <script setup>

  </script>

  <style scoped>
  .page {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 2rem;
    min-height: 100vh;
  }

  .header {
  margin-top: 30px;
  text-align: center;
  margin-bottom: 2rem;
  font-size: 25px;
}

  .header h1 {
    font-size: 2.5rem;
    color: gray;
  }

  .header p {
    font-size: 1.2rem;
    color: gray;
  }

  .sources-container {
    width: 90%;max-width: 1000px;
    background-color: #111111;
    box-shadow: 2px 2px 20px #404040;
    padding: 2rem;
    border-radius: 10px;
  }

  .sources-list {
    list-style: none;
    padding-left: 0;
  }

  .sources-list li {
    margin-bottom: 1rem;
    font-size: 1.1rem;
  }

  .sources-list li a {
    color: #9be9ff;
    text-decoration: none;
    transition: color 0.3s ease;
  }

  .sources-list li a:hover {
    color: #00bfff;
    text-decoration: underline;
  }
  </style>
