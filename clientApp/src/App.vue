<template lang="pug">
  v-layout
    v-navigation-drawer(expand-on-hover rail permanent theme="dark")
      v-list
        v-list-item.my-1(
          prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg"
          title="Sandra Adams"
          subtitle="sandra_a88@gmailcom")
        v-divider

        v-list-item(v-for="route in routes" :key="route.path"
          :prepend-icon="route.icon"
          :title="route.title"
          :to="route.path")
      
      template(v-if="userActive" #append)
        div.pa-2
          v-btn(block)
            v-icon mdi-logout

    RouterView
</template>

<script setup lang="ts">
import router from './router';

defineOptions({
  computed: {
    routes() {
      return router.getRoutes()
        .filter(x => !!x.meta?.nav)
        .map(x => ({ path: x.path, title: x.meta.title, icon: x.meta.icon }));
    },
    userActive() {
      return this.$store.getters.isActive;
    },
  }
})
</script>
