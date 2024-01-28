<script lang="ts">
import { PropType } from 'vue'
import { LessonField, FieldType } from '../../models/lms';
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

export default {
    functional: true,
    props: {
        value: { required: true },
        required: { type: Boolean, default: false },
        readonly: { type: Boolean, default: false },
        field: { type: Object as PropType<LessonField>, required: true },
    },
    render(h, ctx) {
      return h(
        getComponent(ctx.props.field), 
        {
            props: { 
                ...ctx.props,
                ...ctx.data.attr
            },
            on: ctx.listeners,
        })
    }
}
</script>