<script setup lang="ts">
  import type { User } from './models';
  import router from './router';

  defineOptions({
    computed: {
      routes() {
        return router.getRoutes()
          .filter(x => !!x.meta?.nav)
          .map(x => ({ path: x.path, title: x.meta.title, icon: x.meta.icon }));
      },
      visibleMenu() {
        return router.currentRoute.value.matched?.[0]?.meta?.layout !== 'blank';
      },
      userActive() {
        return this.$store.getters['auth/isActive'];
      },
      user(): User {
        return this.$store.state.auth.user;
      }
    },
    created(){
      if (this.userActive) this.$store.dispatch('auth/init');
      else this.$router.push({ name: 'login' })
    },
    methods: {
      logout() {
        this.$store.dispatch('auth/logout');
        this.$router.push({ name: 'login' })
      },
    },
  })

</script>

<template lang="pug">
  v-layout
    v-navigation-drawer(v-if="visibleMenu" expand-on-hover rail scrim permanent theme='dark')
      v-list
        v-list-item.my-1(
          prepend-icon="mdi-account"
          :title="user?.name"
          :subtitle="user?.email")
        v-divider

        v-list-item(v-for="route in routes" :key="route.path"
          :prepend-icon="route.icon"
          :title="route.title"
          :to="route.path")
      
      template(v-if="userActive" #append)
        div.pa-2
          v-btn(block @click="logout")
            v-icon mdi-logout

    v-main
      v-theme-provider(theme='light')
        RouterView
</template>