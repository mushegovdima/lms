<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { courseService } from '@/api/lms';
  import { userService } from '@/api/auth';

  const loading = ref(true);
  const items = ref([] as unknown[])

  onMounted(async () => {
    const resp = await courseService.getMy();
    const users = await userService.getByIdRange(resp.map(x => x.authorId));
    items.value = resp.map(x => ({
        ...x,
        author: users.find(u => u.id === x.authorId)?.name,
    }))

    loading.value = false;
  })

</script>

<template lang="pug">
  v-card.w-full(:loading="loading" flat)
    v-card-title
      .d-flex.align-center
        h2 My courses

    v-row(align="center")
      v-col.ma-4(v-for="item in items" :key="item.id" cols="auto")
        v-card.mx-auto(width="350" color="indigo" hover variant="tonal")
          v-card-item
            div
              .text-overline.mt-1 {{ item.author }}
              .text-h6.mb-1 {{ item.title }}
              .text-caption {{ item.description }}
          v-card-actions
            .mx-1 Lessons: {{ item.lessonsCount }}
            v-spacer
            v-btn(:to="{ name: 'course-page', params: { courseId: item.id } }") Open

</template>