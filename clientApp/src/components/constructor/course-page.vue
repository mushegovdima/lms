<script setup lang="ts">
  import { ref, onMounted, computed } from 'vue';
  import type { Course, LessonListItem } from '@/models';
  import { LessonStatus } from '@/models';
  import { courseService, lessonService } from '@/api/lms';
  import LessonCreateDialog from './lesson-create-dialog.vue';
  import CourseAccessDialog from './course-access-dialog.vue';
  import LessonEditor from './lesson-editor.vue';
  import { useRoute } from 'vue-router';

  const route = useRoute();
  const loading = ref(true);
  const isEdit = ref(false);
  const visibleDialog = ref(false);
  const visibleAccessDialog = ref(false);
  const course = ref<Course>();
  const lessons = ref<LessonListItem[]>([]);
  const selectedLessonId = ref<number>();

  const breadcrumbs = computed(() => {
      return [
      {
        title: 'Back to courses',
        disabled: false,
        to: { name: 'constructor-courses' },
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

  function toggleMode() {
    isEdit.value = !isEdit.value;
  }

  function openCreateLessonDialog() {
    visibleDialog.value = true;
  }

  function openAccessDialog() {
    visibleAccessDialog.value = true;
  }

  function editLesson(id: number) {
    selectedLessonId.value = id;
  }

  function clearSelectedLesson() {
    selectedLessonId.value = undefined;
  }

  async function update() {
    if (!course.value) return;
    await courseService.update(course.value.id, course.value)
    toggleMode();
  }

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
    v-card.w-full(v-if="!selectedLessonId" :loading="loading" flat)
      v-card-title
        .d-flex(v-if="course")
          v-col
            v-row
              v-text-field(v-if="isEdit"
                v-model="course.title"
                label="Title")
              h2(v-else) {{ course.title }}
            v-row
              v-text-field(v-if="isEdit"
                v-model="course.description"
                label="Description")
              h5(v-else) {{ course.description }}

          .d-flex(v-if="!isEdit")
            v-btn.mx-2( @click="openAccessDialog" outlined) Members
              v-icon mdi-account-multiple

            v-btn( color="primary" @click="toggleMode") Edit
              v-icon mdi-pencil

          .d-flex(v-else)
            v-btn.mx-2(color="success" @click="update") Save
              v-icon mdi-check-all

            v-btn(color="error" @click="toggleMode") Cancel
              v-icon mdi-cancel

      v-card-text
        v-list
          v-list-item(v-for="item in lessons"
            prepend-icon="mdi-minus"
            :title="item.title"
            :subtitle="item.description"
            @click="editLesson(item.id)")
            template(#append)
              v-btn(size="small" flat icon readonly)
                v-icon(
                  :icon="item.status === LessonStatus.active ? 'mdi-eye' : 'mdi-eye-off'"
                  :color="item.status === LessonStatus.active ? 'success' : 'dashed'")
          
        v-btn.mt-2(v-if="!isEdit"
          size="small"
          @click="openCreateLessonDialog") Add lesson
          v-icon mdi-plus

      LessonCreateDialog(v-if="course"
        v-model="visibleDialog"
        :courseId="course.id"
        @update="loadLessons")

    LessonEditor(v-else
      :lessonId="selectedLessonId"
      @back="clearSelectedLesson")

    CourseAccessDialog(v-if="course"
      v-model="visibleAccessDialog"
      :courseId="course.id")

</template>