<script setup lang="ts">
import type { LessonField } from '@/models';
import { fieldTypeItems } from '../helpers';
import { rules } from '../validation-rules';
import DynamicFieldConfig from '@/components/dynamic-fields/dynamic-field-config.vue'

const field = defineModel<LessonField>();

function updateData(data: any) {
  if (!field.value) return;
  field.value.data = data;
}

</script>

<template lang="pug">
  v-col
    v-row(justify="space-between")
      v-text-field(v-model="field.title" :rules="[ rules.required ]" placeholder="Title" variant="underlined")
    v-row
      v-text-field(v-model="field.description" placeholder="Description" variant="underlined")
    v-row
      v-autocomplete(v-model="field.type"
        :items="fieldTypeItems"
        item-title="text"
        item-value="value"
        label="Type"
        :rules="[ rules.required ]")
      v-switch.mx-2(v-model="field.required" color="primary" label="Required")
    v-row
      DynamicFieldConfig(:field="field" @updated="updateData")

</template>