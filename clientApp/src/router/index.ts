import { Role } from '@/models'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('@/components/home.vue'),
      meta: { 
        nav: true,
        title: 'Home',
        icon: 'mdi-home'
      },
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('@/components/home.vue'),
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
    {
      path: '/constructor',
      name: 'constructor',
      component: () => import('@/components/constructor/index.vue'),
      children: [
        {
          path: '/constructor',
          name: 'constructor-courses',
          component: () => import('@/components/constructor/courses.vue')
        },
        {
          path: '/constructor/:courseId',
          name: 'constructor-course',
          component: () => import('@/components/constructor/course-page.vue')
        },
      ],
      meta: { 
        nav: true,
        title: 'Constructor',
        icon: 'mdi-code-tags',
        role: Role.admin,
      },
    },
    {
      path: '/my-index',
      name: 'my-courses-index',
      component: () => import('@/components/student/index.vue'),
      redirect: { name: 'my-courses' },
      children: [
        {
          path: '/courses',
          name: 'my-courses',
          component: () => import('@/components/student/my-courses.vue')
        },
        {
          path: '/courses/:courseId',
          name: 'course-page',
          component: () => import('@/components/student/course-page.vue')
        },
        {
          path: '/courses/:courseId/lesson/:lessonId',
          name: 'lesson-page',
          component: () => import('@/components/student/lesson-page.vue')
        },
      ],
    },

  ]
})

export default router
