<script lang="ts">
import { h, type PropType } from 'vue';
import { type LessonField, FieldType, type Lesson } from '@/models';
import VideoConfig from './configs/video-config.vue';


function getComponent(field: LessonField) {
    switch (field.type) {
        case FieldType.video: return VideoConfig;
        default: undefined;
    }
}

const DynamicFieldConfig = (props, context) => {
  const component = getComponent(props.field);
  if (!component) return;
  return h(component, context.attrs, context.slots)
}

export default DynamicFieldConfig;

</script>