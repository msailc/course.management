<template>
  <div>
    <h2>Student List</h2>
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Surname</th>
          <th>Actions</th>
          <th>    <button @click="createStudent">Create Student</button></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="student in students" :key="student.id">
          <td>{{ student.name }}</td>
          <td>{{ student.surname }}</td>
          <td>
            <button @click="showDetails(student.id)">Details</button>
            <button @click="showEdit(student)">Edit</button>
            <button @click="showConfirmation(student.id)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
    <StudentDetails v-if="selectedStudentId" :key="selectedStudentId" :studentId="selectedStudentId" @close-details="closeDetails" />

<StudentCreateEdit v-if="showCreateModal || showEditModal" :mode="mode" :student="student" @save-student="saveStudent" @cancel="closeModals" />

  </div>
</template>

<script>
import StudentDetails from "./StudentDetails.vue";
import StudentCreateEdit from "./StudentCreateEdit.vue";
import axios from "axios";

export default {
  components: {
    StudentDetails,
    StudentCreateEdit,
  },
  data() {
    return {
      students: [],
      selectedStudentId: null,
      showCreateModal: false,
      showEditModal: false,
      student: null,
      mode: "",
    };
  },
  methods: {
    fetchStudents() {
      axios
        .get("http://localhost:5250/student")
        .then((response) => {
          this.students = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    createStudent() {
      this.mode = "create";
      this.showCreateModal = true;
      this.closeDetails();
    },
    showDetails(studentId) {
      this.selectedStudentId = studentId;
      this.closeModals();
    },
    showEdit(student) {
      this.mode = "edit";
      this.student = { ...student };
      this.showEditModal = true;
      this.closeDetails();
    },
    closeDetails() {
      this.selectedStudentId = null;
    },
    closeModals() {
      this.showCreateModal = false;
      this.showEditModal = false;
      this.student = null;
    },
    saveStudent(newStudent) {
      if (this.mode === "create") {
        axios
          .post("http://localhost:5250/student", newStudent)
          .then((response) => {
            this.closeModals();
            this.fetchStudents();
          })
          .catch((error) => {
            console.log(error);
          });
      } else if (this.mode === "edit") {
        axios
          .put(`http://localhost:5250/student/${this.student.id}`, newStudent)
          .then((response) => {
            this.closeModals();
            this.fetchStudents();
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    showConfirmation(studentId) {
  if (confirm("Are you sure you want to delete this student?")) {
    axios
      .delete(`http://localhost:5250/student/${studentId}`)
      .then((response) => {
        this.fetchStudents();
      })
      .catch((error) => {
        console.log(error);
      });
  }
},
  },
  mounted() {
    this.fetchStudents();
  },
};
</script>

