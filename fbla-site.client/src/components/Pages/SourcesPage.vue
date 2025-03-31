<template>
    <div class="page">
      <div class="header">
        <h1>Sources</h1>
        <p>Below is a list of references and sources used in creating this project.</p>
      </div>
      <div class="sources-container">
        <ul class="sources-list">
          <li>
            <a href="https://primevue.org" target="_blank">PrimeVue Documentation</a>
          </li>
          <li>
            <a href="" target="_blank">Example</a>
          </li>
          <li>
            <a href="" target="_blank">Example</a>
          </li>
          <li>
            <a href="" target="_blank">Example</a>
          </li>
          <li>
            <a href="" target="_blank">Example</a>
          </li>
          <li>
            <a href="" target="_blank">Example (Add more if needed, could also put them side by side)</a>
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
    color: #9be9ff;
  }
  
  .header {
    text-align: center;
    margin-bottom: 2rem;
  }
  
  .header h1 {
    font-size: 2.5rem;
    color: lightblue;
  }
  
  .header p {
    font-size: 1.2rem;
    color: #cceeff;
  }
  
  .sources-container {
    width: 50%;
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
  