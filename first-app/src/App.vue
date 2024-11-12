<template>
  <!-- <img alt="Vue logo" src="https://blog.logrocket.com/wp-content/uploads/2020/04/Vue-3.png"> -->
  <!-- <HelloWorld msg="Welcome to Your Vue.js App"/>
  <About msg="The Progressive
JavaScript Framework
An approachable, performant and versatile framework for building web user interfaces."/> -->
  <Navbar />
  <!-- <ProductList/> -->

</template>

<script>
// import HelloWorld from './components/HelloWorld.vue';
// import About from './components/About.vue';
import Navbar from './components/Navbar.vue';
// import ProductList from './components/ProductList.vue';

export default {
  name: 'App',
  components: {
    Navbar,
    // HelloWorld,
    // About,
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
