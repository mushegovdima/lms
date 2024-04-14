<script setup lang="ts">
import type { PropType } from 'vue';
import { rules } from '@/components/validation-rules';
import type { LessonField, VideoConfig } from '@/models';
import { computed } from 'vue';

const value = defineModel();
const props = defineProps({
    required: { type: Boolean, default: false },
    readonly: { type: Boolean, default: false },
    field: { type: Object as PropType<LessonField>, required: true },
});

const link = computed(() => {
    const config = props.field?.data as VideoConfig;
    console.log(config);
    if (!config?.url) return '';
    const pattern = /(?:\?|&)v=([^&#]+)/;
    const code = pattern.exec(config.url);
    if (!code) return '';
    return 'https://www.youtube.com/embed/' + code[1];
})

</script>

<template lang="pug">
    v-row.my-3.mx-1(v-if="link")
        iframe(:src="link"
            width="560"
            height="315"
            frameborder="0"
            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
            referrerpolicy="strict-origin-when-cross-origin"
            allowfullscreen)

</template>