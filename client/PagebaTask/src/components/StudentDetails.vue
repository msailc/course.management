<template>
  <div>
    <h2>Student Details</h2>
    <div v-if="student">
      <p>Student ID: {{ student.id }}</p>
      <p>Student Name: {{ student.name }}</p>
      <p>Student Surname: {{ student.surname }}</p>
      <p>Student Year: {{ student.year }}</p>
      <p>Student Index Number: {{ student.indexNo }}</p>
      <div>
        <h3>Courses</h3>
        <ul>
          <li v-for="course in courses" :key="course.id">{{ course.name }}</li>
        </ul>
      </div>
    </div>
    <div v-else>
      <p>No student selected</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  props: {
    studentId: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
      student: null,
      courses: []
    };
  },
  methods: {
    fetchStudentDetails() {
      axios.get(`http://localhost:5250/student/${this.studentId}`)
        .then(response => {
          this.student = response.data;
          this.fetchCourses();
        })
        .catch(error => {
          console.log(error);
        });
    },
    fetchCourses() {
      axios.get(`http://localhost:5250/student/${this.studentId}`)
        .then(response => {
          this.courses = response.data.courses;
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  mounted() {
    this.fetchStudentDetails();
  }
};
</script>