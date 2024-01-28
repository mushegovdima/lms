<script setup lang="ts">
import type { LessonPostRequest } from '@/models';
import { lessonService } from '@/api/lms';
import { ref } from 'vue';
import { rules } from '@/components/validation-rules';

const emit = defineEmits(['update']);
const isVisible = defineModel({ default: false });
const props = defineProps({
  courseId: { type: Number, required: true }
})

const isValid = ref(false);
const model = ref<LessonPostRequest>({ courseId: props.courseId } as LessonPostRequest);

async function submit() {
  await lessonService.create(model.value);
  emit('update');
  isVisible.value = false;
}

function close() {
  model.value = { courseId: props.courseId } as LessonPostRequest;
  isVisible.value = false;
}

</script>

<template lang="pug">
  v-dialog(v-model="isVisible" persistent width="600" @close="close")
    v-card
      v-card-title
        .d-flex.justify-center
          h3 Create lesson
      v-card-text
        v-form(v-model="isValid")
          v-text-field(v-model="model.title"
            label="Title"
            :rules="[ rules.required ]")

          v-textarea(v-model="model.description"
            label="Description")

          v-text-field(v-model="model.image"
            label="Image (link)")

      v-card-actions
        v-spacer
        v-btn(@click="close") Close
          v-icon mdi-close

        v-btn.mx-4(@click="submit"
          :disabled="!isValid"
          color="success"
          variant="elevated") Save
</template>