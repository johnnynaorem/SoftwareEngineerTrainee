<template>
    <Navbar />
    <div class="container-xl mt-5">
        <div class="row shadow-lg rounded p-md-3">
            <div class="cardContainer mb-3 col-12  col-sm-6 col-md-4 col-lg-3" v-for="movie in movies"
                :key="movie.movieId">
                <div class="card">
                    <div class="imgContainer">
                        <img :src=movie.coverImage class="card-img-top" alt="Movie Image" height="100%" width="100%">
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
import { getAllMovie } from '@/script/MovieService';
import Navbar from './Navbar.vue';

export default {
    name: "MovieDisplay",
    components: {
        Navbar
    },
    data() {
        return {
            movies: [],
        }
    },
    mounted() {
        let isLogin = sessionStorage.getItem("token");
        if (isLogin) {
            const fetchMovie = async () => {
                var response = await getAllMovie();
                console.log(response);
                if (response.status == 200) {
                    console.log(response.data)
                    this.movies = response.data;
                }
            }
            fetchMovie();
        }
        else { this.$router.push('/login') }

    }
}
</script>

<style scoped>
.cardContainer {
    cursor: pointer;
    justify-items: center;
}

.card {
    width: 16rem;
}

@media screen and (min-width:991px) and (max-width: 1092px) {
    .card {
        width: 14rem;
    }
}

@media screen and (min-width: 940px) and (max-width: 990px) {
    .card {
        width: 18rem;
    }
}

@media screen and (min-width:873px) and (max-width: 940px) {
    .card {
        width: 17rem;
    }
}

@media screen and (min-width: 811px) and (max-width: 840px) {
    .card {
        width: 16rem;
    }
}

@media screen and (min-width: 767px) and (max-width: 810px) {
    .card {
        width: 15rem;
    }
}

@media screen and (min-width: 710px) and (max-width: 767px) {
    .card {
        width: 22rem;
    }
}

@media screen and (min-width: 689px) and (max-width: 709px) {
    .card {
        width: 21rem;
    }
}

@media screen and (min-width: 652px) and (max-width: 688px) {
    .card {
        width: 20rem;
    }
}

@media screen and (min-width: 618px) and (max-width: 651px) {
    .card {
        width: 19rem;
    }
}

@media screen and (max-width: 575px) {
    .card {
        width: 24rem;
    }
}

.imgContainer {
    width: 100%;
    height: 300px;
}

img {
    object-fit: cover;
    object-position: top;
}

.card-title {
    font-weight: 700;
}

.card-text {
    font-size: 14px;
}
</style>
