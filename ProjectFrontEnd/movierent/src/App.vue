<template>
  <!-- <Navbar /> -->
  <ToastifyContainer />
  <router-view/>
</template>

<script>
import { ToastifyContainer } from 'vue3-toastify';
// import Navbar from './components/Navbar.vue';

export default {
  name: 'App',
  components: {
    // Navbar,
    ToastifyContainer
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center; */
  color: #2c3e50;
  background-color: rgb(248 249 250);
}
</style>
