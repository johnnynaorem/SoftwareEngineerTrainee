import DisplayMovie from "@/components/DisplayMovie.vue";
import DisplayMovieByCategory from "@/components/DisplayMovieByCategory.vue";
import Login from "@/components/Login.vue";
import Reservation from "@/components/Reservation.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
  { path: "/", name: "Home", component: DisplayMovie },
  { path: "/login", name: "Login", component: Login },
  { path: "/Reservation", name: "Reservation", component: Reservation },
  {
    path: "/Movie/GetMovieByCategory",
    name: "GetMovieByCategory",
    component: DisplayMovieByCategory,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
