<template>
  <div>
    <div style="display: flex; align-items: center;">
      <h1 style="margin-right: 1em;">Course list</h1>
      <button style="font-size: 1em; padding: 0.2em 0.5em; border-radius: 50%; background-color: black; color: white;" @click="createCourse">+</button>
    </div>
    <table>
      <thead>
        <tr>
          <th>Course name</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="course in courses" :key="course.id">
          <td style="padding-left: 10px; padding-right: 10px">{{ course.name }}</td>
          <td style="padding-left: 10px; padding-right: 10px">
            <button @click="showDetails(course.id)">Details</button>
            <button style="margin-left: 5px" @click="deleteCourse(course.id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
    <CourseDetails v-if="selectedCourseId" :key="selectedCourseId" :courseId="selectedCourseId" @close-details="closeDetails" />
    <CourseCreate v-if="createCourseModal" @create-course="closeCreateCourseModal" />
    
  </div>
</template>

<script>
import CourseDetails from './CourseDetails.vue';
import CourseCreate from './CourseCreate.vue';
import axios from 'axios';

export default {
  components: {
    CourseDetails,
    CourseCreate
  },
  data() {
    return {
      courses: [],
      selectedCourseId: null,
      createCourseModal: false
    };
  },
  methods: {
    fetchCourses() {
      axios.get('http://localhost:5250/course')
        .then(response => {
          this.courses = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    createCourse() {
      this.createCourseModal = true;
      this.closeDetails();
    },
    closeCreateCourseModal() {
      this.createCourseModal = false;
      this.fetchCourses();
    },
    showDetails(courseId) {
      this.selectedCourseId = courseId;
      this.createCourseModal = false;
    },
    closeDetails() {
      this.selectedCourseId = null;
      this.fetchCourses();
    },
    deleteCourse(courseId) {
      if (confirm('Are you sure you want to delete this course?')) {
        axios.delete(`http://localhost:5250/course/${courseId}`)
          .then(response => {
            this.fetchCourses();
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  },
  mounted() {
    this.fetchCourses();
  }
};
</script>