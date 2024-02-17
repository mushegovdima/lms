import { FieldType, Role } from "@/models";

export const fieldTypeItems = [
    { text: 'String', value: FieldType.string },
    { text: 'Text', value: FieldType.text },
    { text: 'Number', value: FieldType.number },
    { text: 'Video', value: FieldType.video },
]

export const roleItems = [
    { text: 'Admin', value: Role.admin },
    { text: 'Checker', value: Role.checker },
    { text: 'Student', value: Role.student },
]