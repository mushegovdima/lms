<script setup lang="ts">
import type { PropType } from 'vue';
import { rules } from '@/components/validation-rules';
import type { LessonField } from '@/models';

const value = defineModel();
const props = defineProps({
    required: { type: Boolean, default: false },
    readonly: { type: Boolean, default: false },
    field: { type: Object as PropType<LessonField>, required: true },
});

</script>

<template lang="pug">
    v-text-field(v-model="value"
        :label="props.field.title"
        :hint="props.field.description"
        :rules="required || props.required ? [ rules.required ] : []"
        :readonly="props.readonly"
        clearable)

</template>