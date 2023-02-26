import { createRouter, createWebHistory } from 'vue-router'

import LoginComponent from '@/components/LoginComponent.vue'
import RegisterComponent from '@/components/RegisterComponent.vue'
import ParentComponent from '@/components/ParentComponent.vue'

const routes = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/login',
    component: LoginComponent
  },
  {
    path: '/register',
    component: RegisterComponent
  },
  {
    path: '/home',
    component: ParentComponent,
    meta: { requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  console.log('beforeEach called with', to, from)
  const isAuthenticated = localStorage.getItem('token')
  let username = localStorage.getItem('username')

  if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated) {
    // If the user is not authenticated and the route requires authentication, redirect to the login page
    next('/login')
  } else if (isAuthenticated) {
    // If the user is authenticated, set the username in local storage
    localStorage.setItem('username', username)
  }
  next()
})







export default router
