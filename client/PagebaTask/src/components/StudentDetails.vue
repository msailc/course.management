<template>
  <div>
    <h2 style="margin-top: 1rem; padding-bottom: 1rem; font-size: 20px; font-weight: 600;">Student Details</h2>
    <div v-if="student">
      <p>Student ID: {{ student.id }}</p>
      <p>Student Name: {{ student.name }}</p>
      <p>Student Surname: {{ student.surname }}</p>
      <p>Student Year: {{ student.year }}</p>
      <p>Student Index Number: {{ student.indexNo }}</p>
      <p>Student Status: {{ statusName }}</p>
      <div>
        <h3 style="margin-top: 0.5rem; padding-bottom: 0.5rem; font-size: 20px; font-weight: 600;">Courses</h3>
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
      courses: [],
      statuses: []
    };
  },
  methods: {
    fetchStudentDetails() {
      axios.get(`http://localhost:5250/student/${this.studentId}`)
        .then(response => {
          this.student = response.data;
          this.fetchCourses();
          this.fetchStatuses();
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
    },
    fetchStatuses() {
      axios.get('http://localhost:5250/studentstatus')
        .then(response => {
          this.statuses = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  computed: {
    statusName() {
      const status = this.statuses.find(s => s.id === this.student.statusId);
      return status ? status.status : '';
    }
  },
  mounted() {
    this.fetchStudentDetails();
  }
};
</script>