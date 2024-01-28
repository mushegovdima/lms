<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import type { Course } from '@/models';
  import CourseCreateDialog from './course-create-dialog.vue';
  import { courseService } from '@/api/lms';
  import store from "@/store";

  const loading = ref(true);
  const visibleDialog = ref(false);
  const items = ref([] as Course[])

  onMounted(async () => {
    items.value = await courseService.getByAuthorId(store.state.auth.userId);
    loading.value = false;
  })

  function onCreateClick() {
    if (visibleDialog.value) return;
    visibleDialog.value = true;
}
</script>

<template lang="pug">
  v-card.w-full(:loading="loading" flat)
    v-card-title
      .d-flex.align-center
        h2 My courses
        v-spacer
        v-btn(color="primary" @click="onCreateClick") New
          v-icon mdi-plus

    v-card-text
      v-list
        v-list-item(v-for="item in items"
          prepend-icon="mdi-minus"
          :title="item.title"
          :to="{ name: 'constructor-course', params: { courseId: item.id } }"
          :subtitle="item.description")
          template(#append)
            div {{ item.lessonsCount}} lessons

    CourseCreateDialog(v-model="visibleDialog")
</template>