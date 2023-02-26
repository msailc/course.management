<template>
  <div class="navbar">
    <div class="navbar-left">Page.ba Task</div>
    <div class="navbar-right">
      <div class="user-info" v-if="$isAuthenticated">
        Logged in as {{ currentUser }}
        <button @click="logout">Logout</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Navbar',
  computed: {
    currentUser() {
      const username = localStorage.getItem('username')
      console.log('username:', username)
      return username
    }
  },
  methods: {
    logout() {
      // Clear the token and username from local storage
      localStorage.removeItem('token')
      localStorage.removeItem('username')
      // Redirect to the login page
      this.$router.push('/login')
    }
  }
}
</script>

<style>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 50px;
  padding: 0 20px;
  background-color: #333;
  color: #fff;
}

.navbar-left {
  font-size: 24px;
  font-weight: bold;
}

.navbar-right {
  font-size: 16px;
}

.user-info {
  display: flex;
  align-items: center;
}

.user-info button {
  margin-left: 10px;
}
</style>
