// import DisplayMovie from "@/components/Movie/DisplayMovie.vue";
import DisplayMovieByCategory from "@/components/Movie/DisplayMovieByCategory.vue";
import Login from "@/components/User/Login.vue";
import Reservation from "@/components/MovieReservation/Reservation.vue";
import ReservationDetail from "@/components/MovieReservation/ReservationDetail.vue";
import { createRouter, createWebHistory } from "vue-router";
import { isAuthenticated } from "./UserService";
import Registration from "@/components/User/Registration.vue";
import LandingPage from "@/components/LandingPage.vue";
import NotFoundPage from "@/components/NotFound/NotFoundPage.vue";
import DashboardOne from "@/components/Dashboard/DashboardOne.vue";
import UserProfile from "@/components/User/UserProfile.vue";
import UserPayment from "@/components/Payment/UserPayment.vue";
import Dashboard from "@/components/Dashboard/Dashboard.vue";
import Rental from "@/components/Rental/UserRental.vue";
import DisplayMovie from "@/components/Movie/DisplayMovie.vue";
import MovieDetail from "@/components/Movie/MovieDetail.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: LandingPage,
    meta: { requiresAuth: true },
  },

  { path: "/login", name: "Login", component: Login },

  {
    path: "/movies-all",
    name: "Movies",
    component: DisplayMovie,
    meta: { requiresAuth: true },
  },

  { path: "/:pathMatch(.*)", name: "NotFound", component: NotFoundPage },

  {
    path: "/dashboard",
    name: "Dashboard",
    meta: { requiresAuth: true },
    component: DashboardOne,
    children: [
      {
        path: "",
        component: Dashboard,
      },
      {
        path: "profile",
        component: UserProfile,
      },
      {
        path: "payment",
        component: UserPayment,
      },
      {
        path: "reservation",
        component: Reservation,
      },
      {
        path: "reservation/:id",
        name: "reservationDetail",
        component: ReservationDetail,
      },
      {
        path: "rental",
        component: Rental,
      },
    ],
  },

  { path: "/registration", name: "Registration", component: Registration },

  {
    path: "/movies",
    name: "GetMovieByCategory",
    component: DisplayMovieByCategory,
    meta: { requiresAuth: true },
  },

  {
    path: "/movie/:id",
    name: "MovieDetail",
    component: MovieDetail,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.afterEach(() => {
  window.scrollTo({
    top: 0,
    left: 0,
    behavior: "smooth",
  });
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!isAuthenticated()) {
      next({ name: "Login", query: { redirect: to.fullPath } });
    } else {
      next();
    }
  } else {
    next();
  }
});
export default router;
