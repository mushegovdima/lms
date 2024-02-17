<script setup lang="ts">
import type { CourseRole, CourseRolePostRequest, User } from '@/models';
import { courseRoleService } from '@/api/lms';
import { ref, watch } from 'vue';
import { rules } from '@/components/validation-rules';
import { roleItems } from '../helpers';
import { userService } from '@/api/auth';

const isVisible = defineModel({ default: false });
const loading = ref(true);
const items = ref([] as unknown[]);
const isValid = ref(false);
const model = ref({} as Partial<CourseRolePostRequest>);
const selectedUser = ref<User>();

const props = defineProps({
    courseId: { type: Number, required: true }
})

const headers = [
    { title: 'User', value: 'user' },
    { title: 'Role', value: 'role' },
    { title: 'Date', value: 'createdAt' },
    { title: '', value: 'actions', width: '2em' }
]

watch(() => isVisible, load, { immediate: true });
watch(() => model.value.email, updateUser, { immediate: true });

async function load() {
  if (!isVisible) return;

  loading.value = true;
  const response = await courseRoleService.getByCourseId(props.courseId);
  const users = await userService.getByIdRange(response.map(x => x.userId));
  items.value = response.map(x => ({
    ...x,
    user: users.find(u => u.id === x.userId)?.name || '',
    role: roleItems.find(r => r.value == x.role)?.text,
    createdAt : new Date(x.createdAt).toLocaleString(),
  }));
  loading.value = false;
}

async function updateUser() {
  if (!isVisible || !model.value.email || rules.email(model.value.email) !== true) return;

  loading.value = true;
  const user = await userService.getByEmail(model.value.email);
  selectedUser.value = user;
  model.value.userId = user.id || undefined;
  loading.value = false;
}

async function submit() {
  await courseRoleService.create(
    {
      ...model.value,
      courseId: props.courseId,
    } as CourseRolePostRequest);
    await load();
}

async function remove(id: number) {
  await courseRoleService.remove(id);
  await load();
}

function close() {
  isVisible.value = false;
  model.value = {} as CourseRolePostRequest;
}

</script>

<template lang="pug">
  v-dialog(v-model="isVisible" persistent width="70%" height="70%" @close="close")
    v-card
      v-card-title
        .d-flex.justify-center
          h3 Course members
          v-spacer
          v-btn(@click="close" variant="text" color="error" icon)
            v-icon mdi-close
      v-card-text
        v-data-table(
          :headers="headers"
          :items="items"
          item-key="name"
          items-per-page="5")

          template(#item.actions="{ item }")
            v-btn(@click="remove(item.id)" variant="text" color="error" size="small" icon)
              v-icon mdi-delete

      v-card-actions
        v-form.d-flex.w-full.align-items-top(v-model="isValid")
          v-text-field(v-model="model.email"
            label="Email"
            variant="outlined"
            density="compact"
            :rules="[ rules.required, rules.email ]"
            hide-details
            no-gutters)
            template(#append)
              v-tooltip(v-if="selectedUser" :text="selectedUser?.name")
                template(v-slot:activator="{ props }")
                  v-icon(v-if="selectedUser" v-bind="props") mdi-account

          v-autocomplete.mx-2(v-model="model.role"
            :items="roleItems"
            item-title="text"
            item-value="value"
            label="Role"
            variant="outlined"
            density="compact"
            :rules="[ rules.required ]"
            hide-details
            no-gutters)

        v-btn.mx-2(@click="submit"
          :disabled="!isValid || !model.userId"
          color="success"
          variant="outlined") Add access
          v-icon mdi-plus

</template>