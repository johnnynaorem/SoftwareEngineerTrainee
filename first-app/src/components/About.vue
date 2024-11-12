<template>
    <div class="hello">
      <h3>{{ msg }}</h3>
    </div>
  </template>

<script>
export default {
  name: 'AboutVue',
  props: {
  products: {
    type: Array,
    required: true
  }
}
}
</script>