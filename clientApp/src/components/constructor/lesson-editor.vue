<script setup lang="ts">
  import { ref, watch } from 'vue';
  import type { Lesson } from '@/models';
  import { FieldType, LessonStatus } from '@/models';
  import FieldEditor from '@/components/constructor/lesson-field-editor.vue'

  import { lessonService } from '@/api/lms';

  const loading = ref(true);
  const lesson = ref<Lesson>();

  const props = defineProps({
    lessonId: { type: Number, required: true }
  })

  const emit = defineEmits(['back']);

  watch(() => props.lessonId, load, { immediate: true });

  function back() {
    emit('back');
  }

  function appendNew() {
    lesson.value?.fields.push({
      id: 0,
      title: '',
      description: '',
      required: true,
      type: FieldType.string,
      position: lesson.value?.fields.length + 1,
      lessonId: props.lessonId,
    })
  }

  async function load() {
    loading.value = true;
    lesson.value = await lessonService.getById(props.lessonId);
    loading.value = false;
  }

  async function update() {
    if (!lesson.value) return;
    await lessonService.update(lesson.value.id, lesson.value);
  }

</script>

<template lang="pug">
  v-row
    v-card.w-full(:loading="loading" flat)
      v-card-title
        .d-flex
          v-spacer
          v-btn.mx-2(@click="back") Back
            v-icon mdi-arrow-left
          v-btn.mx-2(color="success" @click="update") Save
            v-icon mdi-check-all

      v-card-text(v-if="lesson")
        v-text-field(v-model="lesson.title" label="Title")
        v-text-field(v-model="lesson.description" label="Description")
        v-switch(v-model="lesson.status"
          color="success"
          label="Active"
          :true-value="LessonStatus.active"
          :false-value="LessonStatus.disabled")

        b.my-2(v-for="(item, index) in lesson.fields")
          | Question: {{ index + 1 }}
          v-divider
          FieldEditor(v-model.sync="lesson.fields[index]")

        v-btn.mt-2(size="small"
          @click="appendNew") Append
          v-icon mdi-plus

</template>