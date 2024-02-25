<script setup lang="ts">
  import { ref, onMounted, computed } from 'vue';
  import type { Course, LessonListItem } from '@/models';
  import { courseService, lessonService } from '@/api/lms';
  import { useRoute } from 'vue-router';

  const route = useRoute();
  const loading = ref(true);
  const course = ref<Course>();
  const lessons = ref<LessonListItem[]>([]);

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
      }]
    }
  )

  onMounted(async () => {
    course.value = await courseService.getById(route.params.courseId as any);
    await loadLessons();
    loading.value = false;
  })

  async function loadLessons() {
    if (!course.value) return;
    lessons.value = await lessonService.getByCourseId(course.value.id);
  }

</script>

<template lang="pug">
  v-row
    v-breadcrumbs(:items="breadcrumbs") /
      template(#prepend)
        v-icon mdi-home

  v-row
    v-card.w-full(:loading="loading" flat)
      v-card-title
        .d-flex(v-if="course")
          v-col
            v-row
              h2 {{ course.title }}
            v-row
              h5 {{ course.description }}

      v-card-text
        v-list
          v-list-item(v-for="item in lessons"
            :title="item.title"
            :subtitle="item.description"
            :to="{ name: 'lesson-page', params: { courseId: course?.value?.id, lessonId: item.id } }")

</template>