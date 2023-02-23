<template>
    <div class="register-container">
      <h2>Register</h2>
      <form @submit.prevent="registerUser">
        <label>
          Username:
          <input type="text" v-model="username" />
        </label>
        <label>
          Password:
          <input type="password" v-model="password" />
        </label>
        <button type="submit">Register</button>
      </form>
      <div v-if="errorMessage" class="error-message">
        {{ errorMessage }}
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: 'RegisterComponent',
    data() {
      return {
        username: '',
        password: '',
        errorMessage: '',
      };
    },
    methods: {
      registerUser() {
        axios
          .post('http://localhost:5250/auth/register', {
            username: this.username,
            password: this.password,
          })
          .then(() => {
            this.$router.push('/');
          })
          .catch((error) => {
            this.errorMessage = error.response.data.message;
          });
      },
    },
  };
  </script>
  
  <style>
  .register-container {
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
  