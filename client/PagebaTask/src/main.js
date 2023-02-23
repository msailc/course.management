import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import './assets/main.css'

const app = createApp(App)

// Check if there is a token in the local storage
const isAuthenticated = localStorage.getItem('token') !== null

// Set a global state variable indicating whether the user is authenticated
app.config.globalProperties.$isAuthenticated = isAuthenticated

// Mount the app
app.use(router).mount('#app')
