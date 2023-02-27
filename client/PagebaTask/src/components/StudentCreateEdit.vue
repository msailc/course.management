<template>
  <div>
    <h2 style="margin-top: 1rem; font-size: 20px; font-weight: 600;">{{ mode === 'create' ? 'Create' : 'Edit' }} student</h2>
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
        <label for="statusId">Status ID:</label>
        <select required style="width: 100%;" id="statusId" v-model="formData.statusId">
          <option v-for="status in statusList" :value="status.id">{{ status.status }}</option>
        </select>
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
      },
      statusList: []
    };
  },
  created() {
    axios.get('http://localhost:5250/studentstatus')
      .then(response => {
        this.statusList = response.data;
      })
      .catch(error => {
        console.log(error);
      });
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
      data: jsonData
    })
      .then(response => {
        this.$emit('saved', response.data);
        this.formData = {}; // Clear the form data after saving
        console.log(response.data);
      })
      .catch(error => {
        console.log(error);
      });
  },
  cancel() {
    this.formData = {};
    this.$emit('cancel');
  }
  },
  watch: {
  student: {
    handler(newVal, oldVal) {
      this.formData = { ...newVal };
    },
    immediate: true,
    deep: true
  },
  mode(newVal, oldVal) {
    if (newVal === 'create') {
      this.formData = {};
    }
  }
}
};
</script>

