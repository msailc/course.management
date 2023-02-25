<template>
    <div class="navbar">
      <div class="navbar-left">Page.ba Task</div>
      <div class="navbar-right">Logged in as {{ currentUser }}</div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'Navbar',
    computed: {
      currentUser() {
        return localStorage.getItem('username')
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
  </style>
  