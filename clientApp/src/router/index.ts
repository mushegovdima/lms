import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('@/components/HelloWorld.vue'),
      meta: { 
        nav: true,
        title: 'Home',
        icon: 'mdi-home'
      },
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('@/components/HelloWorld.vue'),
      meta: { 
        nav: true,
        title: 'About',
        icon: 'mdi-information'
      },
    },
    {
      path: '/signup',
      name: 'signup',
      component: () => import('@/components/security/signup.vue'),
      meta: {
        layout: 'blank',
      },
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/components/security/login.vue'),
      meta: {
        layout: 'blank',
      },
    },

  ]
})

export default router
