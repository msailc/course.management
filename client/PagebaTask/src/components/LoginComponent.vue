<template>
    <div class="login-container">
      <h2>Login</h2>
      <form @submit.prevent="login">
        <label>
          Username:
          <input type="text" v-model="username" required>
        </label>
        <br>
        <label>
          Password:
          <input type="password" v-model="password" required>
        </label>
        <br>
        <button type="submit">Login</button>
      </form>
      <p>Don't have an account? <router-link to="/register">Register</router-link></p>
    </div>
  </template>
  
  <script>
export default {
  name: 'LoginComponent',
  data() {
    return {
      username: '',
      password: '',
      error: ''
    }
  },
  methods: {
    async login() {
  try {
    const token = localStorage.getItem('token')

    if (token) {
      // Token exists, redirect to ParentComponent
      this.$router.push('/home')
      return
    }

    const response = await fetch('http://localhost:5250/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        username: this.username,
        password: this.password
      })
    })

    const data = await response.json()

    if (data.token) {
      localStorage.setItem('token', data.token)
      localStorage.setItem('username', this.username)
      this.$router.push('/home')
    } else {
      this.error = data.message
    }
  } catch (err) {
    this.error = err.message
  }
}

  }
}
</script>

<style>
.login-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 20px;
}

label {
  margin-top: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

input[type='text'],
input[type='password'] {
  margin-left: 5px;
  padding: 5px;
  border-radius: 3px;
  border: 1px solid #ccc;
}

button[type='submit'] {
  margin-top: 10px;
  padding: 5px;
  border-radius: 3px;
  border: none;
  background-color: #4caf50;
  color: white;
  cursor: pointer;
}

.error-message {
  margin-top: 10px;
  color: red;
  font-weight: bold;
  text-align: center;
}
</style>
