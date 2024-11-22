<template>
  <ToastifyContainer />
  <router-view />
</template>

<script>
import { ToastifyContainer } from 'vue3-toastify';

export default {
  name: 'App',
  components: {
    ToastifyContainer
  }
}
</script>

<style>
:root {
  --primary: #4ade80;
  --gray: #64748b;
  --dark: #1e293b;
  --dark-alt: #334155;
  --light: #f1f5f9;
  --sidebar-width: 300px;
}

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Fira sans', sans-serif;
}

/* body {
  background: var(--light);
} */
</style>
