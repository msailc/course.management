<template>
    <div>
      <h2>Course Details</h2>
      <div v-if="course">
        <p>Course ID: {{ course.id }}</p>
        <p>Course Name: {{ course.name }}</p>
        <div>
          <h3>Students</h3>
          <ul>
            <li v-for="student in students" :key="student.id">{{ student.name }}</li>
          </ul>
        </div>
      </div>
      <div v-else>
        <p>No course selected</p>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    props: {
      courseId: {
        type: Number,
        required: true
      }
    },
    data() {
      return {
        course: null,
        students: []
      };
    },
    methods: {
      fetchCourseDetails() {
        axios.get(`http://localhost:5250/course/${this.courseId}`)
          .then(response => {
            this.course = response.data;
            this.fetchStudents();
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchStudents() {
        axios.get(`http://localhost:5250/course/${this.courseId}`)
          .then(response => {
            this.students = response.data.students;
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    mounted() {
      this.fetchCourseDetails();
    }
  };
  </script>