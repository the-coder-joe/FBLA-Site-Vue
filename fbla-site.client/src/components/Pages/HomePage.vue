<script setup lang="ts">
import { ref } from 'vue';

// Array of image filenames used in the carousel
const images = ref([
  { src: "cafe.jpg", alt: "The cafe at Summit Valley" },
  { src: "football.jpg", alt: "The Summit Valley football team" },
  { src: "fieldhouse.jpg", alt: "The gymnasium at Summit Valley" },
  { src: "classroom.jpg", alt: "A classroom full of engaged students" },
  { src: "library.jpg", alt: "The Summit Valley Library" }
]);

// Importing the PrimeVue carousel component
import Carousel from 'primevue/carousel';
</script>

<template>
  <!-- Background section with the main image and title -->
  <div class="main-bg">
    <div class="main-bg-pic">
      <img :src="'src/assets/homepage.jpg'" id="mainbgpic"
        alt="Summit Valley School District building on a sunny day" />
      <div class="title-container">
        <h1 class="display-4 main-title">Summit Valley</h1>
        <h2 class="title-subtitle">School District</h2>
      </div>
    </div>
  </div>

  <!-- Information section including about us text and image carousel -->
  <section class="information-container">
    <div class="about-container dark-glass">
      <h1 id="about-us">Our Values</h1>
      <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; At Summit Valley School District, we are committed to fostering
        a community of respect, collaboration, and
        excellence. Our core values center around providing a safe and inclusive learning environment where every
        student is empowered to reach their full potential. We believe in nurturing creativity, promoting lifelong
        learning, and supporting the development of responsible, compassionate citizens. Together with families and the
        community, we strive to inspire a passion for knowledge, cultivate personal growth, and prepare students for a
        successful future in an ever-changing world.
      </p>
    </div>
  </section>

  <!-- Image carousel displaying school-related images -->
  <template>
    <Carousel :value="images" :numVisible="1" :numScroll="1">
      <template #item="slotProps">
        <div class="carousel-item">
          <img :src="slotProps.data.src" :alt="slotProps.data.alt" class="w-full h-auto rounded-xl" />
        </div>
      </template>
    </Carousel>
  </template>
</template>


<style lang="css" scoped>
/* Styling for the carousel component */
:deep(.p-carousel-item) {
  width: 80px;
}

/* Main background styling with centered text */
.main-bg {
  position: relative;
  text-align: center;
  color: white;
}

/* Background image container */
.main-bg-pic {
  width: 100%;
  max-height: 75vh;
  object-position: 50% 100%;
  object-fit: cover;
  border-bottom: var(--primary-color) solid 1px;
  margin-bottom: 150px;
  overflow: hidden;
}

/* Background image styling */
#mainbgpic {
  width: 100%;
  filter: brightness(80%);
  margin-bottom: -6px;
}

/* Title container positioned within the main background */
.title-container {
  position: absolute;
  top: 85%;
  left: 50%;
  transform: translate(-50%, -60%);
  border-radius: 50px;
  padding-inline: 15px;
}

/* Main title styling */
.main-title {
  color: rgb(255, 255, 255);
  font-size: 140px;
  font-weight: 700;
  text-wrap: nowrap;
}

/* Subtitle styling */
.title-subtitle {
  color: var(--primary-color);
  font-size: 50px;
  font-weight: 600;
  position: relative;
  top: -50px;
}

/* Responsive adjustments for smaller screens */
@media (max-width: 1000px) {
  .main-title {
    font-size: 50px;
    margin-bottom: 30px;
  }

  .title-subtitle {
    font-size: 40px;
  }
}

  /* About section container styling */
  .about-container {
    color: rgb(218, 218, 218);
    min-width: 28rem;
    max-width: 90%;
    max-width: 1000px;
    padding: 20px;
    border-radius: 40px;
    font-size: 20px;
  }

/* Styling for the carousel container */
#home-carousel {
  width: 1000px;
  margin-inline: 20px;
  border-radius: 20px;
}

/* About us title styling */
#about-us {
  text-align: center;
  margin-inline: 20px;
  margin-bottom: 10px;
  font-size: 30px;
  color: var(--primary-color)
}

/* Carousel container styling */
.carousel {
  width: 90%;
  max-width: 1000px;
  overflow: hidden;
  position: relative;
}

/* Information section layout styling */
.information-container {
  margin: 20px;
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: space-around;
  margin-top: -70px;
}

/* Contact section styling */
.contact-container {
  float: right;
  width: 40%;
}

/* Footer layout styling */
.footer {
  display: flex;
}

/* Image styling for carousel items */
.cpics {
  width: 400px;
  height: 250px;
}
</style>
