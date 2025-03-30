<script setup lang="ts">
import { ref } from 'vue';

const images = ref([
  "cafe.jpg",
  "football.jpg",
  "fieldhouse.jpg",
  "classroom.jpg",
  "library.jpg"

]);

import Carousel from 'primevue/carousel';

</script>

<template>



  <div class="main-bg">
    <div class="main-bg-pic">
      <img :src="'src/assets/homepage.jpg'" id="mainbgpic" />
      <div class="title-container">
        <h1 class="display-4 main-title">Summit Valley</h1>
        <h2 class="title-subtitle">School District</h2>
      </div>
    </div>
  </div>



  <section class="information-container">
    <div class="about-container">
      <h1 id="about-us">Our Values</h1>
      <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  At Summit Valley School District, we are committed to fostering a community of respect, collaboration, and
        excellence. Our core values center around providing a safe and inclusive learning environment where every
        student is empowered to reach their full potential. We believe in nurturing creativity, promoting lifelong
        learning, and supporting the development of responsible, compassionate citizens. Together with families and the
        community, we strive to inspire a passion for knowledge, cultivate personal growth, and prepare students for a
        successful future in an ever-changing world.</p>

    </div>

    <Carousel :value="images" :numVisible="1" :numScroll="1" orientation="horizontal"
      containerClass="flex items-center">
      <template #item="slotProps">
        <img :src="'src/assets/carousel/' + slotProps.data" class="w-full rounded cpics" />
      </template>
    </Carousel>


  </section>


</template>


<style lang="css" scoped>

:deep(.p-carousel-item) {
  width: 80px;
}

.main-bg {
  position: relative;
  text-align: center;
  color: white;
}

.main-bg-pic {
  width: 100%;
  height: 75vh;
  object-position: 50% 100%;
  object-fit: cover;
  border-bottom: var(--primary-color) solid 1px;
  margin-bottom: 150px;
  overflow: hidden;
}

#mainbgpic {
  width: 100%;
  filter: brightness(80%);
}


.title-container {
  position: absolute;
    top: 85%;
    left: 50%;
    transform: translate(-50%, -60%);
    border-radius: 50px;
    padding-inline: 15px;
}

.main-title {
  color: rgb(255, 255, 255);
  font-size: 140px;
  font-weight:700;
  text-wrap: nowrap;
}

.title-subtitle {
  color: var(--primary-color);
  font-size: 50px;
  font-weight: 600;
  position: relative;
  top: -50px;
}

@media (max-width: 500px) {
  .main-title {
    font-size: 50px;
  }

  .title-subtitle {
    color: rgb(0 0 0);
    font-size: 40px;
  }
}

.about-container {
  color: rgb(218, 218, 218);
  min-width: 28rem;
  max-width: 50%;
  background-color: rgba(0, 0, 0, 0.3);
  backdrop-filter: blur(5px);
  padding: 20px;
  border-radius: 40px;
  box-shadow: 0px 0px 20px rgba(75, 75, 75, 0.611);
}

#home-carousel {
  width: 500px;
  margin-inline: 20px;
  border-radius: 20px;
}

#about-us {
  text-align: center;
  margin-inline: 20px;
  margin-bottom: 10px;
  font-size: 30px;
  color: var(--primary-color)
}

.carousel {
  width: 50%;
  overflow: hidden;
  position: relative;
}

.information-container {
  margin: 20px;
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: space-around;
  margin-top: -70px;
}


.contact-container {
  float: right;
  width: 40%;
}

.footer {
  display: flex;
}

.cpics {
  width: 400px;
  height: 250px;
}
</style>
