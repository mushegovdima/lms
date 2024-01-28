<script setup lang="ts">
import { fieldTypeItems } from '../helpers';
import { rules } from '../validation-rules';

const field = defineModel();

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

</template>