<script setup lang="ts">
import { Posting } from '@/models/application.models';

const props = defineProps(
  {
    posting: {
      type: Posting,
      required: true
    }
  }
)
</script>

<template>
  <div class="sticky-container">
    <div class="sticky-outer">
      <div class="sticky">
        <svg width="0" height="0">
          <defs>
            <clipPath id="stickyClip" clipPathUnits="objectBoundingBox">
              <path d="M 0 0 Q 0 0.69, 0.03 0.96 0.03 0.96, 1 0.96 Q 0.96 0.69, 0.96 0 0.96 0, 0 0"
                stroke-linejoin="round" stroke-linecap="square" />
            </clipPath>
          </defs>
        </svg>
        <div class="posting">
          <h2>{{ posting.title }} at {{ posting.employer }}</h2>
          <p>{{ posting.description }}</p>
          <slot name="actions"></slot>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.sticky-container {
  position: relative;
  width: 350px;
}

.sticky-outer {
  display: flex;
  padding-top: 92.5925926%;
  position: relative;

  width: 100%;
}

.sticky {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

/* Shadow behind the sticky note */
.sticky:before {
  box-shadow: -2px 2px 15px 0 rgba(0, 0, 0, 0.5);
  background-color: rgba(0, 0, 0, 0.25);
  content: '';
  width: 90%;
  left: 5px;
  height: 83%;
  position: absolute;
  top: 30%;
}

/* The sticky note itself */
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
  font-family: 'Kalam', cursive;
  font-size: 1rem;
  color: black;
  clip-path: url(#stickyClip);
  padding: 20px;
}

/* Add responsiveness */
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
