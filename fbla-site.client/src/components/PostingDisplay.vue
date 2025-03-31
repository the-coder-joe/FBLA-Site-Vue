<script setup lang="ts">
import { Posting } from '@/models/application.models'; // Import Posting model for type definition

const props = defineProps<{
  posting: Posting;
}>();

</script>

<template>
  <div class="sticky-container">
    <!-- Outer container for positioning the sticky element -->
    <div class="sticky-outer">
      <!-- Inner container for the sticky element -->
      <div class="sticky">
        <!-- Invisible SVG used for clipping the sticky note -->
        <svg width="0" height="0">
          <defs>
            <clipPath id="stickyClip" clipPathUnits="objectBoundingBox">
              <path d="M 0 0 Q 0 0.69, 0.03 0.96 0.03 0.96, 1 0.96 Q 0.96 0.69, 0.96 0 0.96 0, 0 0"
                stroke-linejoin="round" stroke-linecap="square" />
            </clipPath>
          </defs>
        </svg>
        <!-- Sticky note content -->
        <div class="posting">
          <h2>{{ posting.title }} at {{ posting.employer }}</h2>
          <p>{{ posting.description }}</p>
          <!-- Slot for rendering additional action buttons (like apply buttons) -->
          <slot name="actions"></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Outer container that defines the size and positioning of the sticky note */
.sticky-container {
  position: relative;
  width: 350px;
}

/* Container that creates the flexible space for the sticky note */
.sticky-outer {
  display: flex;
  padding-top: 92.5925926%; /* Aspect ratio control */
  position: relative;
  width: 100%;
}

/* Sticky element's absolute positioning to make it stay fixed */
.sticky {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

/* Shadow effect behind the sticky note */
.sticky:before {
  box-shadow: -2px 2px 15px 0 rgba(0, 0, 0, 0.5);
  background-color: rgba(0, 0, 0, 0.25);
  content: '';
  width: 90%;
  left: 5px;
  height: 83%; /* Adjust shadow height */
  position: absolute;
  top: 30%;
}

/* The sticky note appearance and layout */
.posting {
  background: linear-gradient(180deg,
      rgba(187, 235, 255, 1) 0%,
      rgba(187, 235, 255, 1) 12%,
      rgba(170, 220, 241, 1) 75%,
      rgba(195, 229, 244, 1) 100%);
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  font-family: 'Kalam', cursive; /* Font style for sticky note text */
  font-size: 1rem;
  color: black;
  clip-path: url(#stickyClip); /* Applying the clipping path for rounded sticky note edges */
  padding: 20px;
}

/* Responsive design: Adjust sticky shadow height and font size on larger screens */
@media screen and (min-width: 640px) {
  .sticky:before {
    height: 79%;
    width: 90%;
  }

  .sticky-content {
    font-size: 1.25rem;
  }
}

@media screen and (min-width: 768px) {
  .sticky:before {
    height: 75%;
    width: 90%;
  }

  .sticky-content {
    font-size: 1.5rem;
  }
}

@media screen and (min-width: 1024px) {
  .sticky:before {
    height: 73%;
    width: 90%;
  }

  .sticky-content {
    font-size: 1.875rem;
  }
}
</style>
