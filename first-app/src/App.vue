<template>
  <Slider />
</template>

<script>
import Slider from './components/Slider.vue';


export default {
  name: 'App',
  components: {
    Slider
    // FilterView
  },
  setup() {
    return {
      items: [
        {
          id: 1,
          first: 'johnny',
          last: 'naorem'
        },
        {
          id: 2,
          first: 'rohit',
          last: 'laishram'
        },
        {
          id: 3,
          first: 'disha',
          last: 'gupta'
        },
        {
          id: 4,
          first: 'niharika',
          last: 'grag'
        }
      ]
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  /* background-color: #2c3e50; */
}
</style>
