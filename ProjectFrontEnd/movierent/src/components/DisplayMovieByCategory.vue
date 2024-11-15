<template>
    <Navbar />


    <div class="container-xl movie-container mt-5">
        <div v-if="loading" class="spinnerContainer d-flex gap-3 justify-content-center">
            <div class="spinner-grow spinner-grow-md" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
            <div class="spinner-grow spinner-grow-md" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
            <div class="spinner-grow spinner-grow-md" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
        <div v-else-if="movies.length" class="row shadow-lg rounded p-md-3">
            <div class="cardContainer mb-3 col-12 col-sm-6 col-md-4 col-lg-3" v-for="movie in movies"
                :key="movie.movieId">
                <div class="card">
                    <div class="imgContainer">
                        <img :src="movie.coverImage" class="card-img-top" alt="Movie Image" height="100%" width="100%">
                    </div>
                    <div class="card-body">
                        <h6 class="card-title">{{ movie.title }}</h6>
                        <p class="card-text">{{ movie.description.substring(0, 50) }}.....</p>
                        <a class="btn btn-primary">Reserve At {{ movie.rental_Price }} only/==</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { getMovieByCategory } from '@/script/MovieService';
import { ref, watch, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import Navbar from './Navbar.vue';

export default {
    name: "MovieByCategory",
    components: {
        Navbar
    },
    setup() {
        const movies = ref([]);
        const route = useRoute();
        const loading = ref(true);

        const fetchMovies = (category) => {
            loading.value = true;
            if (category != null) {
                getMovieByCategory(category)
                    .then(data => {
                        movies.value = data.data;
                    })
                    .catch(error => {
                        console.error('Error fetching movies:', error);
                    })
                    .finally(() => {
                        setTimeout(() => {
                            loading.value = false;
                        }, 1000)
                    });
            }
        };

        onMounted(() => {
            const category = route.query.genre;
            fetchMovies(category);
        });

        watch(() => route.query.genre, (newCategory, oldCategory) => {
            if (newCategory !== oldCategory) {
                fetchMovies(newCategory);
            }
        });

        return { movies, loading };
    }
};
</script>

<style>
.movie-container {
    min-height: 100vh;
}
</style>
