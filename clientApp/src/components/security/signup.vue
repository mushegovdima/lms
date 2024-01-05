<script setup lang="ts">
  import { rules } from '@/components/validation-rules';
  import type { RegisterForm } from '@/models';

  defineOptions({
    data() {
      return {
        model: {} as Partial<RegisterForm>,
        isValid: false,
      }
    },
    methods: {
      async submit() {
        console.log(this.isValid);
        if (!this.isValid) return;
        this.$store.dispatch('auth/register', this.model);
      }
    },
    computed: {
      isComplete() {
        return this.$store.getters.isActive;
      }
    },
    watch: {
      isComplete(value: boolean) {
        if(value) this.$router.push({ name: 'home' } )
      }
    }
  })

</script>

<template lang="pug">
  v-card.mx-auto.my-6(
    elevation="8"
    width="400px"
    rounded="lg")
    v-card-title
      .d-flex.justify-center
        h3 Registration
    v-card-text
      v-form(v-model="isValid")
        v-text-field(v-model="model.login"
          label="Login"
          hint="Login must be unique"
          :rules="[ rules.required ]")

        v-text-field(v-model="model.email"
          label="Email"
          type="email"
          :rules="[ rules.required, rules.email ]")

        v-text-field(v-model="model.name"
          label="Name"
          :rules="[ rules.required ]")

        v-text-field(v-model="model.phone"
          label="Phone"
          type="phone"
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
          block) Submit

</template>