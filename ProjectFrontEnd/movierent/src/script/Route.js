// import DisplayMovie from "@/components/Movie/DisplayMovie.vue";
import DisplayMovieByCategory from "@/components/Movie/DisplayMovieByCategory.vue";
import Login from "@/components/User/Login.vue";
import Reservation from "@/components/MovieReservation/Reservation.vue";
import ReservationDetail from "@/components/MovieReservation/ReservationDetail.vue";
import { createRouter, createWebHistory } from "vue-router";
import { isAdminAuthenticated, isAuthenticated } from "./UserService";
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
import AdminDashboard from "@/components/Admin/AdminDashboard.vue";
import ReservationList from "@/components/Admin/ReservationList.vue";
import PaymentList from "@/components/Admin/PaymentList.vue";
import RentalList from "@/components/Admin/RentalList.vue";
import MovieList from "@/components/Admin/MovieList.vue";
import UnAuthorize from "@/components/UnAuthorize.vue";
import WishlistMovie from "@/components/Wishlist/WishlistMovie.vue";

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
    path: "/unauthorized",
    component: UnAuthorize,
  },

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
      {
        path: "wishlist",
        component: WishlistMovie,
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

  //Admin path
  {
    path: "/admin/dashboard",
    component: AdminDashboard,
    children: [
      {
        path: "home",
        component: Dashboard,
      },
      {
        path: "movies",
        component: MovieList,
      },
      {
        path: "reservations",
        component: ReservationList,
      },
      {
        path: "payments",
        component: PaymentList,
      },
      {
        path: "rentals",
        component: RentalList,
      },
      {
        path: "reservation/:id",
        name: "reservationByCustomer",
        component: Dashboard,
      },
    ],
    meta: { requiresAdmin: true },
    beforeEnter: (to, from, next) => {
      if (to.matched.some((record) => record.meta.requiresAdmin)) {
        if (!isAuthenticated()) {
          next({ name: "Login", query: { redirect: to.fullPath } });
        } else {
          if (!isAdminAuthenticated()) {
            next({ path: "/unauthorized" });
          } else {
            next();
          }
        }
      } else {
        next();
      }
    },
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
