import { createApp } from "vue";
import App from "./App.vue";
import router from "./script/router";

// Import Bootstrap 5 and BootstrapVue 3
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue-3/dist/bootstrap-vue-3.css";
import { BootstrapVue3 } from "bootstrap-vue-3";
// import Toast from "./components/Toast.vue";

// Make BootstrapVue available throughout your project
// Vue.use(BootstrapVue)

createApp(App)
  .use(router)
  .use(BootstrapVue3)
  //   .component("toast", Toast)
  .mount("#app");
