<script setup lang="ts">
import type { LessonField } from '@/models';
import { rules } from '@/components/validation-rules';
import type { PropType } from 'vue';

const value = defineModel();
const props = defineProps({
    required: { type: Boolean, default: false },
    readonly: { type: Boolean, default: false },
    field: { type: Object as PropType<LessonField>, required: true },
});

</script>

<template lang="pug">
    v-text-field(v-model="value"
        :placeholder="props.field.title"
        type="number"
        :rules="required || props.required ? [ rules.required ] : []"
        :readonly="props.readonly"
        clearable)

</template>