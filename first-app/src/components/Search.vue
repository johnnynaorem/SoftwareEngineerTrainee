<script setup>
import { defineEmits } from 'vue';
const emit = defineEmits(['search']);
const search = (e) => {
    emit('search', e.target.value);
}




</script>

<template>
    <input class="search-input" type="search" placeholder="search" @input="search">
</template>

<style>
.search-input {
    padding: 10px 5px;
    border: 1px solid red;
    font-size: medium;
}
</style>