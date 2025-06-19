<template>
  <Toast/>
  <BaseLayout />

  <div id="shiny-light"></div>
</template>

<script setup>
import BaseLayout from './Layouts/BaseLayout.vue'
import { Toast } from 'primevue';

document.addEventListener('mousemove', (event) => {
  const light = document.getElementById('shiny-light');
    if (light) {
      // Move the light 70% of the way toward the mouse position for a trailing effect
      const currentTop = parseFloat(light.style.top) || 0;
      const currentLeft = parseFloat(light.style.left) || 0;
      const targetTop = event.pageY;
      const targetLeft = event.pageX;
      light.style.top = `${targetTop}px`;
      light.style.left = `${targetLeft}px`;
    }
  });
</script>

<style>
#shiny-light {
  position: absolute;
  top: 0;
  left: 0;
  height: 0;
  width: 0;
  box-shadow: 0px 0px 100px 100px rgba(159, 159, 159, 0.03);
  z-index: 1000;
}
</style>
