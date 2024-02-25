<script setup lang="ts">
  import { ref, onMounted, computed } from 'vue';
  import { LessonAnswerStatus, type Course, type Lesson, type LessonAnswer } from '@/models';
  import { courseService, lessonAnswerService, lessonService } from '@/api/lms';
  import { useRoute } from 'vue-router';
  import DynamicFieldEditor from '@/components/dynamic-fields/dynamic-field-editor.vue'
  import store from '@/store';
  import { lessonAnswerStatuses } from '../helpers';

  const route = useRoute();
  const loading = ref(true);
  const lesson = ref<Lesson>();
  const course = ref<Course>();
  const answer = ref<LessonAnswer>();
  const isValid = ref(false);

  const breadcrumbs = computed(() => {
      return [
      {
        title: 'Back to courses',
        disabled: false,
        to: { name: 'my-courses-index' },
      },
      {
        title: course?.value?.title,
        disabled: false,
        to: { name: 'course-page', params: { courseId: course?.value?.id }}
      }]
    }
  )

  onMounted(async () => {
    lesson.value = await lessonService.getById(route.params.lessonId as any);
    course.value = await courseService.getById(lesson.value.courseId);
    await loadAnswer(lesson.value.id);
    loading.value = false;
  })

  async function loadAnswer(lessonId: number) { 
    answer.value = await lessonAnswerService.getByLessonId(lessonId, store.state.auth.userId)
      || { lessonId: lessonId, data: {}, status: LessonAnswerStatus.Draft } as LessonAnswer;
  }

  async function save() { 
    if (!answer.value) return;

    if (answer.value.id) {
      answer.value = await lessonAnswerService.update(answer.value.id, answer.value);
      return;
    }
    const id = await lessonAnswerService.create(answer.value);
    answer.value.id = id;
  }

  async function saveAndSendToCheck() { 
    if (!answer.value) return;
    await save()
    await lessonAnswerService.sendToCheck(answer.value.id);
    answer.value = await lessonAnswerService.get(answer.value.id);
  }

</script>

<template lang="pug">
  v-row
    v-breadcrumbs(:items="breadcrumbs") /
      template(#prepend)
        v-icon mdi-home

  v-row
    v-card.w-full(:loading="loading" flat)
      v-card-title.mx-3
        .d-flex(v-if="lesson")
          v-col
            v-row
              h2 {{ lesson.title }}
              v-badge.mx-3(v-if="answer?.id"
                :content="lessonAnswerStatuses[answer.status].text"
                :color="lessonAnswerStatuses[answer.status].color")
            v-row
              h5 {{ lesson.description }}
          v-btn.mx-2(@click="save") Save draft
          v-btn(@click="saveAndSendToCheck" :disabled="!isValid" color="success") Complete

      v-card-text(v-if="lesson && answer")
        v-form(v-model="isValid")
          v-col(v-for="(field, index) in lesson.fields" :key="index")
            h2(v-text="field.title" label="Title")
            h5(v-text="field.description" label="Description")
            DynamicFieldEditor(v-model="answer.data[field.id]"
              :field="field"
              required
              variant="underlined")
          v-divider.mx-2.mt-4

</template>