<template>
  <nav class="navbar navbar-expand-lg sticky-top">
    <div class="container-fluid">
      <img src="../Images/logo.webp" alt="" width="70px" style="cursor: pointer; ">
      <router-link to="/" class="navbar-brand text-light" style="flex:1; cursor: pointer;">MovieRent</router-link>
      <button class="navbar-toggler btn-primary" type="button" data-bs-toggle="collapse"
        data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false"
        aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNavDropdown">
        <ul class="navbar-nav ">
          <li class="nav-item ">
            <router-link to="/" class="nav-link active text-light" aria-current="page">Home</router-link>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle text-light" role="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              Movies
            </a>
            <ul class="dropdown-menu">
              <li>
                <router-link class="movie-drop-down-list" to="/movies-all">Movies All</router-link>
              </li>
              <li>
                <router-link class="movie-drop-down-list" to="/movies-all">Movies Comming Soon</router-link>
              </li>
              <li>
                <router-link class="movie-drop-down-list" to="/movies-all">Movies Now Playing</router-link>
              </li>
              <li>
                <router-link class="movie-drop-down-list" to="/movies-all">Movie Categeory</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle text-light" role="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              Catalog
            </a>
            <ul class="dropdown-menu">
              <li v-for="catalog in movieCatalog" :key="catalog">
                <a @click="fetchMovieByCategory(catalog)" class="dropdown-item">{{ catalog }}</a>
              </li>
            </ul>
          </li>
        </ul>
        <div class="profile-logo">
          <div class="dropdown">
            <router-link to="/login" v-if="!isLogin">
              <img
                src="https://static.vecteezy.com/system/resources/previews/005/544/718/original/profile-icon-design-free-vector.jpg"
                alt="" width="40px">
            </router-link>

            <div v-else>
              <img src="https://th.bing.com/th/id/OIP.8UqOTLl0knNXrmb8iSs8KwHaHw?w=860&h=900&rs=1&pid=ImgDetMain" alt=""
                width="40px" />

              <button style="font-size: 10px;" class="btn btn-sm dropdown-toggle" type="button"
                data-bs-toggle="dropdown" aria-expanded="false">
              </button>
              <ul class="dropdown-menu" style=" font-size: 12px; position: absolute; left: -100px; cursor: pointer;">
                <li>
                  <RouterLink class="dropdown-item" to="/dashboard">Dashboard</RouterLink>
                </li>
                <li><a class="dropdown-item" @click="logOut()">Log Out</a></li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import { getAllMovieCatalog } from '@/script/MovieService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
  name: 'NavBar',
  data() {
    return {
      isLogin: false,
      movieCatalog: [],
    };
  },
  mounted() {
    const token = sessionStorage.getItem("token");
    this.isLogin = token !== null;

    if (this.isLogin) {
      this.fetchMovieCatalog();
    }
  },
  methods: {
    reservation() {
      this.$router.push('/reservation');
    },
    logOut() {
      sessionStorage.removeItem('token');
      this.isLogin = false;
      this.showToast("Logout Successfully", 'success')
      this.$router.push('/login');
    },
    async fetchMovieCatalog() {
      try {
        const category = await getAllMovieCatalog();
        if (category.status === 200) {
          this.movieCatalog = category.data;
        } else {
          this.showToast('Failed to load movie catalog', 'error');
        }
      } catch (error) {
        this.showToast('An error occurred while fetching movie catalog', 'error');
      }
    },
    async fetchMovieByCategory(genre) {
      try {
        this.$router.push({
          name: 'GetMovieByCategory',
          query: { genre: genre }
        });
      } catch (error) {
        this.showToast('An error occurred while fetching movie catalog', 'error');
      }
    },
    showToast(message, type) {
      if (type === 'success') {
        toast.success(message, {
          rtl: true,
          limit: 2,
          position: toast.POSITION.TOP_CENTER,
        });
      } else {
        toast.error(message, {
          rtl: true,
          limit: 2,
          position: toast.POSITION.TOP_CENTER,
        });
      }
    }
  }
};
</script>

<style scoped>
.navbar {
  /* position: relative; */
  background: black;
  opacity: .9;
  width: 100%;
  position: fixed;
  min-height: 100px;
}

img {
  border-radius: 10px;
}

.profile-logo {
  justify-items: end;
  flex: 1;
}

.show {
  width: max-width;
}

.movie-drop-down-list {
  padding: 10px;
  text-decoration: none;
  font-size: small;
  color: black
}

li {
  cursor: pointer;
  transition: .5s;
}

li:hover {
  background-color: rgb(141, 140, 139);
}
</style>