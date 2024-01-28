<script setup lang="ts">
  import { rules } from '@/components/validation-rules';
  import type { LoginForm } from '@/models';

  defineOptions({
    data() {
      return {
        model: {} as Partial<LoginForm>,
        isValid: false,
      }
    },
    methods: {
      async submit() {
        if (!this.isValid) return;
        this.$store.dispatch('auth/login', this.model);
      }
    },
  })

</script>

<template lang="pug">
  v-card.mx-auto.my-6(
    elevation="8"
    width="400px"
    rounded="lg")
    v-card-title
      .d-flex.justify-center
        h3 Welcome back
    v-card-text
      v-form(v-model="isValid")
        v-text-field(v-model="model.login"
          label="Login"
          :rules="[ rules.required ]")

        v-text-field(v-model="model.password"
          placeholder="Enter your password"
          prepend-inner-icon="mdi-lock-outline"
          label="Password"
          type="password"
          :rules="[ rules.required ]")

        v-btn(@click="submit"
          :disabled="!isValid"
          color="success"
          variant="elevated"
          block) Login

      .pt-2 You don't have account?
        router-link(:to="{ name: 'signup' }") SignUp!

</template>