<template>
  <div class="parent-container">
    <div class="child-container">
      <student-list></student-list>
    </div>
    <div class="child-container">
      <course-list></course-list>
    </div>
  </div>
</template>

<script>
import StudentList from "./StudentList.vue";
import CourseList from "./CourseList.vue";

export default {
  name: "ParentComponent",
  components: {
    StudentList,
    CourseList,
  },
};
</script>

<style>
.parent-container {
  display: flex;
  justify-content: center;
}

.child-container {
  display: flex;
  align-items: center;
  flex-direction: column;
  height: 700px;
  width: 500px;
  margin: 10px;
  background-color: #ccc;
}
</style>
