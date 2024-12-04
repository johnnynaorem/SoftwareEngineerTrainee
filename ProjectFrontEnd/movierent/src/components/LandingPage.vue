<script>
import { jwtDecode } from 'jwt-decode';
import MainLayout from './Layout/MainLayout.vue';
import router from '@/script/Route';
import { getMovieById } from '@/script/MovieService';

export default {
    name: "LandingPage",
    components: {
        MainLayout,
    },
    data() {
        return {
            isAdmin: false,
            isClickOnTrailer: false,
            url: "",
            watchTrailer: (link) => {
                this.url = link
            },
            movies: [],
            movieALt: []
        }
    },
    async mounted() {
        const token = sessionStorage.getItem('token')
        const decode = jwtDecode(token);

        const role = decode['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if (role == 'Admin') {
            this.isAdmin = true;
            router.push('/admin/dashboard/home');
        }

        const movieOne = await getMovieById(18)
        this.movies.push(movieOne.data)
        const movieTwo = await getMovieById(19)
        this.movies.push(movieTwo.data)
        const movieThree = await getMovieById(20)
        this.movies.push(movieThree.data)

        const movieAltOne = await getMovieById(14)
        this.movieALt.push(movieAltOne.data)
        const movieAltTwo = await getMovieById(13)
        this.movieALt.push(movieAltTwo.data)
        const movieAltThree = await getMovieById(15)
        this.movieALt.push(movieAltThree.data)
        const movieAltFour = await getMovieById(12)
        this.movieALt.push(movieAltFour.data)
    }
}
</script>

<template>
    <MainLayout>
        <template #default>
            <div class="container-fluid p-0">
                <div class="banner">
                    <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <img src="../Images/banner-02.jpg" class="d-block w-100" alt="">
                            </div>
                            <div class=" carousel-item">
                                <img src="../Images/banner3.jpg" class="d-block w-100" alt="">
                            </div>
                        </div>
                    </div>
                </div>
                <div class=" my-2 image-lines-header">
                    <img class="line-image" src="../Images/image-lines-header.jpg" alt="">
                </div>
                <div class="content-container">
                    <img class="content-background-img" src="../Images/background.png" alt="">
                    <div class="blog container-fluid">
                        <div class="row">
                            <div class="col-12 col-md-6">
                                <div class="template">
                                    <img class="bg-image" src="../Images/bg-film-01.png" alt="">
                                    <div class="blog-container">
                                        <p>Join Now</p>
                                        <h3>Upcoming Film </h3>
                                        <h3> Festivals</h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-md-6">
                                <div class="template">
                                    <img class="bg-image" src="../Images/bg-film-01.png" alt="">
                                    <div class="blog-container">
                                        <p>Watch Trailer Now</p>
                                        <h3>Watch Film</h3>
                                        <h3> Awards</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="content" style="margin-top: 100px;">
                        <!-- <img src="https://clipart-library.com/newhp/kissclipart-cinematic-techniques-clipart-photographic-film-cin-2b1cbfa02047641b.png"
                            alt="" width="50px"> -->
                        <div class="content-top text-center">
                            <h2 style="font-size: large; font-weight: bold;"> Movies Now Playing</h2>
                        </div>
                        <div class="content-middle container">
                            <div class="row p-md-3">
                                <div class="cardContainer mb-3 col-6 col-md-4 col-lg-3" v-for="(movie, i) in movieALt"
                                    :key="i">
                                    <div class="card-div">
                                        <img :src="movie.coverImage" class="card-img" alt="Movie Image">
                                        <div class="card-body">
                                            <h6 class="card-title">{{ movie.title }}</h6>
                                            <p class="card-text">{{ movie.description.substring(0, 15) }}......</p>
                                            <a class="btn btn-primary"
                                                @click="this.$router.push(`/movie/${movie.movieId}`)">Reserve
                                                At {{ movie.rental_Price }} only/==</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content-bottom-container d-flex">
                    <div class="content-bottom container">
                        <img src="../Images/background.png" alt="" width="30px">
                        <p>Checkout Movies</p>
                        <h1>Top Features Movies</h1>

                        <div class="feature-movie-container mt-5">
                            <div class="row">
                                <div class="col-12 col-md-6 col-lg-4" v-for="(movie, i) in movies" :key="i">
                                    <div class="feature-movie-image-div position-relative">
                                        <img :src="movie.coverImage" alt="" width="100%">

                                        <div class="feature-movie-text position-absolute ps-4 py-4">
                                            <h5>{{ movie.title }}</h5>
                                            <div class="d-flex mb-2" style="width: 57%;">
                                                <div class="d-flex align-items-center">
                                                    <img src="https://th.bing.com/th/id/OIP.kQlRZLmD9xvTA1ijiTqqSAAAAA?rs=1&pid=ImgDetMain"
                                                        alt="">
                                                    <p>{{ movie.genre }}</p>
                                                </div>
                                                <div class="d-flex align-items-center">
                                                    <img src="https://th.bing.com/th/id/OIP.kQlRZLmD9xvTA1ijiTqqSAAAAA?rs=1&pid=ImgDetMain"
                                                        alt="">

                                                    <p>180 mins</p>
                                                </div>
                                            </div>
                                            <div class="feature-movie-button-div d-flex gap-3">
                                                <button type="button" class="btn" data-bs-toggle="modal"
                                                    data-bs-target="#exampleModal"
                                                    @click="watchTrailer(movie.trailerVideo)">Watch
                                                    Trailer</button>
                                                <button @click="this.$router.push(`/movie/${movie.movieId}`);">Rent
                                                    Movie</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal -->
                <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
                    aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content" style="background-color: black;">
                            <div class="modal-body">
                                <iframe width="100%" height="400px" :src='url'>
                                </iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </template>
    </MainLayout>
</template>

<style scoped>
.banner,
.image-lines-header {
    width: 100%;
}

.banner-image,
.line-image {
    width: 100%;
    object-fit: contain;
}

.content-container {
    position: relative;
}

.content-background-img {
    width: 100%;
    height: 100%;
    position: absolute;
    bottom: 0;
    object-fit: cover;
    opacity: .05;
}

.template {
    position: relative;
    background-color: black;
    height: 300px;
    width: 100%;
    transition: background-size 1s ease-in-out;
    overflow: hidden;
}

.bg-image {
    width: 100%;
    height: 100%;
    z-index: 1;
    transition: transform 1.5s ease-in-out;
    object-fit: cover;
    opacity: .3;
}

.blog-container {
    position: absolute;
    top: 50%;
    left: 20px;
    color: white;
    font-weight: bold;
    z-index: 2;
}

.blog-container p {
    font-weight: 400;
    font-style: italic;
}

.template:hover .bg-image {
    transform: scale(1.2);
}

.content {
    background-color: white;
}


.card-div {
    position: relative;
    overflow: hidden;
    border: 6px solid rgb(173, 63, 63);
    transition: 1s;
    height: 100%;
    width: 100%;
    box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px, rgba(0, 0, 0, 0.22) 0px 15px 12px;
}


.card-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    filter: brightness(0.6);
    transition: 1.2s;
}

.card-body {
    position: absolute;
    bottom: -200%;
    padding: 20px;
    background-color: rgba(0, 0, 0, 0.7);

    color: white;
    transition: bottom .8s ease-out;
}

.card-div:hover .card-body {
    bottom: 0;
}

.card-div:hover .card-img {
    transform: scale(1.2);
}

.card-div:hover {
    border: 6px solid rgb(45, 82, 247);
}

.card-title {
    font-size: 1.1rem;
}

.card-text {
    font-size: 0.9rem;
    margin-bottom: 15px;
}

.content-bottom-container {
    min-height: 150vh;
    background-color: #f3f3f3;
}

.content-bottom {
    margin-top: 160px;
}

.content-bottom p {
    font-size: medium;
    font-weight: bolder
}

.content-bottom h1 {
    font-size: 50px;
    font-weight: bolder
}

.feature-movie-image-div {
    height: 300px;
    transition: 1.5s;
    cursor: pointer;
}

.feature-movie-image-div img {
    object-fit: cover;
    height: 100%;
    width: 100%;
}


.feature-movie-text {
    background-color: rgb(255, 255, 255);
    /* background-color: transparent; */
    border: 1px solid rgb(255, 255, 255);
    bottom: -80px;
    width: 100%;
    /* text-align: center; */
    /* opacity: 1; */
    transition: 1s;
}

.feature-movie-text h5 {
    font-weight: bolder;
}

.feature-movie-text p {
    font-size: smaller;
    margin: 0;
}

.feature-movie-image-div:hover {
    transform: scale(1.1);

}

@media (max-width: 765px) {
    .content-bottom h1 {
        font-size: 30px;
        font-weight: bolder
    }

    .feature-movie-image-div {
        margin-bottom: 120px;
    }

    .feature-movie-image-div:hover {
        transform: scale(1.04);

    }

    .feature-movie-image-div img {
        height: 90%;
        width: 90%;
    }

    .feature-movie-text {
        width: 90%;
    }
}

@media (max-width: 991px) {
    .feature-movie-image-div {
        margin-bottom: 120px;
    }
}

.feature-movie-text img {
    width: 20%;
    object-fit: contain;
}


.feature-movie-button-div button {
    border: none;
    padding: 8px 15px;
    cursor: pointer;
    background-color: antiquewhite;
    font-size: small;
    font-weight: bolder;
    transition: background-color 1s;
}

.feature-movie-button-div button:hover {
    background-color: rgb(247, 34, 34);
    color: white
}



.feature-movie-image-div:hover .feature-movie-text {
    width: 80%;
}
</style>