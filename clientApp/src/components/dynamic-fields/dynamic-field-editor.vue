<script lang="ts">
import { h, type PropType } from 'vue';
import { type LessonField, FieldType } from '@/models';
import NumberEditor from './editors/number-editor.vue';
import StringEditor from './editors/string-editor.vue';
import TextEditor from './editors/text-editor.vue';
import VideoEditor from './editors/video-editor.vue';


function getComponent(field: LessonField) {
    switch (field.type) {
        case FieldType.number: return NumberEditor;
        case FieldType.string: return StringEditor;
        case FieldType.text: return TextEditor;
        case FieldType.video: return VideoEditor;
    }
}

const DynamicFieldEditor = (props, context) => {
  return h(getComponent(props.field), context.attrs, context.slots)
}

export default DynamicFieldEditor;

</script>