<script setup lang="ts">
import type { Course } from '@/models';
import { courseService } from '@/api/lms';
import { ref } from 'vue';
import { rules } from '@/components/validation-rules';

const isVisible = defineModel({ default: false });
const isValid = ref(false);
const model = ref({} as Course);

async function submit() {
  await courseService.create(model.value);
  isVisible.value = false;
}

function close() {
  isVisible.value = false;
}

</script>

<template lang="pug">
  v-dialog(v-model="isVisible" persistent width="600" @close="close")
    v-card
      v-card-title
        .d-flex.justify-center
          h3 New course
      v-card-text
        v-form(v-model="isValid")
          v-text-field(v-model="model.title"
            label="Title"
            :rules="[ rules.required ]")

          v-textarea(v-model="model.description"
            label="Description"
            :rules="[ rules.required ]")

      v-card-actions
        v-spacer
        v-btn(@click="close") Close
          v-icon mdi-close

        v-btn.mx-4(@click="submit"
          :disabled="!isValid"
          color="success"
          variant="elevated") Save
</template>