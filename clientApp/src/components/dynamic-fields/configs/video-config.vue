<script setup lang="ts">
import type { PropType } from 'vue';
import { ref, watch } from 'vue';
import { rules } from '@/components/validation-rules';
import type { LessonField, VideoConfig } from '@/models';

const value = ref<VideoConfig>({ url: ''});

const emit = defineEmits(['updated']);
const props = defineProps({
    readonly: { type: Boolean, default: false },
    field: { type: Object as PropType<LessonField>, required: true },
});

watch(() => props.field.data, () => {
    if (value.value === props.field.data) return;
    value.value = props.field.data || {};
}, { immediate: true })

watch(() => value.value, () => {
    if (value.value === props.field.data) return;
    emit('updated', value.value);
}, { immediate: true })


</script>

<template lang="pug">
    v-text-field(v-model="value.url"
        label="Youtube url"
        placeholder="https://www.youtube.com/watch?v=GsVpr41EagI"
        :rules=" [ rules.required ]"
        :readonly="props.readonly"
        clearable)

</template>