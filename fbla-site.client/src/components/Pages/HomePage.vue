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
      <h1 id="aboutus">Our Values</h1>
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
  height: 90vh;
  object-position: 50% 100%;
  object-fit: cover;
  border-bottom: 1px solid rgb(197 129 123);
  margin-bottom: 150px;
}

#mainbgpic {
  width: 100%;
}

.title-container {
  position: absolute;
    top: 85%;
    left: 50%;
    transform: translate(-50%, -60%);
    background: rgb(147 147 147 / 50%);
    backdrop-filter: blur(1px);
    border-radius: 50px;
    padding-inline: 15px;
}

.main-title {
  color: rgb(0 0 0);
  font-size: 90px;
}

.title-subtitle {
  color: rgb(0 0 0);
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
  min-width: 28rem;
  padding: 20px;
  max-width: 50%;


}

#home-carousel {
  width: 500px;
  margin-inline: 20px;
  border-radius: 20px;
}

#aboutus {
  text-align: center;
  margin-inline: 20px;
}

.carousel {
  width: 50%;
  overflow: hidden;
  position: relative;
}



.slide.active {
  display: block;
}


.information-container {
  margin: 20px;
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: space-around;
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
