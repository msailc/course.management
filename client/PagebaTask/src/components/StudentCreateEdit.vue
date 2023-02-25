<template>
  <div>
    <h2>{{ mode === 'create' ? 'Create' : 'Edit' }} student</h2>
    <form>
      <div>
        <label for="name">Name:</label>
        <input required style="width: 100%;" type="text" id="name" v-model="formData.name" />
      </div>
      <div>
        <label for="surname">Surname:</label>
        <input required style="width: 100%;" type="text" id="surname" v-model="formData.surname" />
      </div>
      <div>
        <label for="year">Year:</label>
        <input required style="width: 100%;" type="text" id="year" v-model="formData.year" />
      </div>
      <div>
        <label for="indexNo">Index Number:</label>
        <input required style="width: 100%;" type="text" id="indexNo" v-model="formData.indexNo" />
      </div>
      <div>
        <label for="statusid">Status id</label>
        <input required style="width: 100%;" type="text" id="statusid" v-model="formData.statusId" />
      </div>
      <div style="margin-top: 1em;">
        <button @click.prevent="save">Save</button>
        <button style="margin-left: 5px" @click.prevent="cancel">Cancel</button>
      </div>
    </form>
  </div>
</template>


<script>
import axios from 'axios';

export default {
  props: {
    student: {
      type: Object,
      default: null
    },
    mode: {
      type: String,
      required: true
    },
    selectedStudent: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      formData: {
        name: '',
        surname: '',
        year: '',
        indexNo: '',
        statusId: ''
      }
    };
  },
  methods: {
    save() {
  const url = this.mode === 'create' ? 'http://localhost:5250/student' : `http://localhost:5250/student/${this.student.id}`;
  const method = this.mode === 'create' ? 'post' : 'put';
  const jsonData = JSON.stringify(this.formData); // Convert form data to JSON object
  axios({
    method: method,
    url: url,
    headers: {
      'Content-Type': 'application/json' // Set request header to indicate JSON format
    },
    data: jsonData // Send JSON object as request body
  })
    .then(response => {
      this.$emit('save-student');
    })
    .catch(error => {
      console.log(error);
    });
},
    cancel() {
      this.$emit('cancel');
    }
  },
  watch: {
    selectedStudent: {
      handler() {
        if (this.selectedStudent) {
          this.formData = Object.assign({}, this.selectedStudent);
        } else {
          this.formData = {
            name: '',
            surname: '',
            year: '',
            indexNo: '',
            statusId: ''
          };
        }
      },
      immediate: true
    },
    student: {
      handler() {
        if (this.student) {
          this.formData = Object.assign({}, this.student);
        } else {
          this.formData = {
            name: '',
            surname: '',
            year: '',
            indexNo: '',
            statusId: ''
          };
        }
      },
      immediate: true
    }
  }
};
</script>
