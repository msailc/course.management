<template>
  <div>
    <h2>Create Course</h2>
    <form @submit.prevent="createCourse">
      <label for="course-name" style="text-align: left; display: block;">Course Name:</label>
      <input type="text" id="course-name" v-model="courseName" required style="width: 100%;">
      <div>
        <label for="students" style="text-align: left; display: block;">Select Students:</label>
        <select id="students" v-model="selectedStudent" style="width: 100%;">
          <option v-for="student in students" :key="student.id" :value="student">{{ student.name }}</option>
        </select>
        <button type="button" @click="addStudent">Add Student</button>
      </div>
      <div>
        <label for="selected-students">Selected Students:</label>
        <div id="selected-students">
          <span v-for="student in selectedStudents" :key="student.id">{{ student.name }} <button type="button" @click="removeStudent(student)">x</button></span>
        </div>
      </div>
      <button>Create</button>
    </form>
  </div>
</template>


  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        courseName: '',
        students: [],
        selectedStudent: null,
        selectedStudents: []
      };
    },
    methods: {
      createCourse() {
        axios.post('http://localhost:5250/course', { name: this.courseName })
          .then(response => {
            const courseId = response.data.id;
            this.selectedStudents.forEach(student => {
              axios.post(`http://localhost:5250/course/${courseId}/students/${student.id}`)
                .then(() => console.log(`Added student ${student.name} to course ${this.courseName}`))
                .catch(error => console.log(error));
            });
            this.$emit('created', response.data);
            this.courseName = '';
            this.selectedStudent = null;
            this.selectedStudents = [];
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchStudents() {
        axios.get('http://localhost:5250/student')
          .then(response => {
            this.students = response.data;
          })
          .catch(error => {
            console.log(error);
          });
      },
      addStudent() {
        if (this.selectedStudent) {
          this.selectedStudents.push(this.selectedStudent);
          this.students = this.students.filter(student => student.id !== this.selectedStudent.id);
          this.selectedStudent = null;
        }
      },
      removeStudent(student) {
        this.selectedStudents = this.selectedStudents.filter(s => s.id !== student.id);
        this.students.push(student);
      }
    },
    mounted() {
      this.fetchStudents();
    }
  };
  </script>

  <style>
label {
  text-align: left;
  display: block;
  margin-bottom: 5px;
}

</style>