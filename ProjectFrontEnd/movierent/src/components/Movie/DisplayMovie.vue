<template>
    <MainLayout>
        <template #default>
            <div v-if="!isAdmin" class="content" style="background-color: black">
                <div class="content-top">
                    <div class="breadcrumbs d-flex justify-content-center align-items-center gap-2">
                        <RouterLink to="/" class="home">Home</RouterLink>
                        <p>></p>
                        <p>Movies All</p>
                    </div>
                    <div class="big-text d-flex justify-content-center">
                        <h1>Movies All</h1>
                    </div>
                </div>
                <div class="content-middle" style="width: 100%;">
                    <img src="../../Images/image-lines-header.jpg" alt="" width="100%">
                </div>
                <div class="container mt-5 d-flex flex-wrap flex-column flex-lg-row">
                    <div class="sideFilter-mapper mt-3">
                        <div class="sideFilterContent p-4"
                            style="background: #191919; color: white; border-radius: 20px;">
                            <input class="p-2 mt-2" type="text" placeholder="search" @input="search"
                                v-model="searchValue"
                                style="outline: none; border: none; background: black; color: white; border-radius: 20px;">
                            <hr />
                            <h5> Movie types</h5>
                            <div class="box-mapper my-4">
                                <input type="checkbox" name="action" id="action" class="me-3" @change="select"
                                    value="Action">
                                <label for="action">Action Movie</label>
                            </div>
                            <div class="box-mapper my-4">
                                <input type="checkbox" name="crime" id="crime" class="me-3" @change="select"
                                    value="Crime" />
                                <label for="crime">Crime Movie</label>
                            </div>
                            <div class="box-mapper my-4">
                                <input type="checkbox" name="drama" id="drama" class="me-3" @change="select"
                                    value="Drama" />
                                <label for="drama">Drama Movie</label>
                            </div>
                            <div class="box-mapper my-4">
                                <input type="checkbox" name="scifi" id="scifi" class="me-3" @change="select"
                                    value="Sci-Fi" />
                                <label for="scifi">Sci-Fi Movie</label>
                            </div>
                        </div>

                    </div>
                    <div class="container mt-3" style="flex: 1;">
                        <div class="row p-md-3">
                            <div class="cardContainer mb-3 col-12  col-sm-12 col-md-6 col-lg-4" v-for="movie in movies"
                                :key="movie.movieId" style="justify-items: center;">
                                <div class="card-div">
                                    <img :src=movie.coverImage class="card-img" alt="Movie Image">
                                    <div class="card-body">
                                        <p class="card-text m-0">{{ movie.genre }} / 180 Mins</p>
                                        <p class="card-title">{{ movie.title }}</p>
                                        <div class="btn-container">
                                            <button class="btn btn-primary"
                                                @click="toMoviePage(movie.movieId)">Rent</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="movie-pagination container mx-auto d-flex justify-content-center gap-3 mb-5">
                        <button class="pag-btn" @click="previous()">
                            <span class="material-icons">chevron_left</span>
                        </button>
                        <button class="pag-btn px-4">{{ this.page }}</button>
                        <button class="pag-btn" @click="next()">
                            <span class="material-icons">chevron_right</span>
                        </button>
                    </div>

                </div>

            </div>
            <div v-else>
                Admin Page
            </div>
        </template>
    </MainLayout>
</template>

<script>
import { getMovieWithPagination, movieFilter } from '@/script/MovieService';
import { jwtDecode } from 'jwt-decode';
import MainLayout from '../Layout/MainLayout.vue';

export default {
    name: "MovieDisplay",
    components: {
        MainLayout,
    },
    data() {
        return {
            isAdmin: false,
            page: 1,
            totalPage: 0,
            movies: [],
            searchValue: "",
            genre: "",
            isOptionSelected: false
        }
    },
    methods: {

        async search() {
            if (this.searchValue != "" || this.isOptionSelected) {
                const response = await movieFilter(this.searchValue, this.genre);
                this.movies = response.data;
                this.totalPage = Math.ceil(response.data.length / 5);
            }
            else this.fetchMovieWithPage();
        },

        async select(event) {
            this.isOptionSelected = !this.isOptionSelected
            if (this.isOptionSelected) {
                this.genre = event.target.value;
                const response = await movieFilter(this.searchValue, this.genre);
                this.movies = response.data;
                this.totalPage = Math.ceil(response.data.length / 5);
            }
            else if (!this.isOptionSelected && this.searchValue != "") {
                this.genre = "";
                const response = await movieFilter(this.searchValue, this.genre);
                this.movies = response.data;
                this.totalPage = Math.ceil(response.data.length / 5);
            }
            else {
                this.genre = "";
                this.fetchMovieWithPage();
            }
        },

        previous() {
            if (this.page > 1) {
                this.page--;
                this.fetchMovieWithPage();
                window.scrollTo({
                    top: 500,
                    left: 0,
                    behavior: 'smooth'
                });
            }
        },
        next() {
            if (this.page < this.totalPage) {
                this.page++;
                this.fetchMovieWithPage();
                window.scrollTo({
                    top: 500,
                    left: 0,
                    behavior: 'smooth'
                });
            }
        },
        async fetchMovieWithPage() {
            try {
                const response = await getMovieWithPagination(this.page, 5);
                if (response.status === 200) {
                    this.totalPage = response.data.totalPages;
                    this.movies = response.data.data;
                }
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
        },
        toMoviePage(id) {
            this.$router.push(`/movie/${id}`)
        }
    },
    mounted() {
        let isLogin = sessionStorage.getItem("token");
        if (isLogin) {
            const fetchMovie = async () => {
                var response = await getMovieWithPagination(1, 5);
                if (response.status == 200) {
                    this.totalPage = response.data.totalPages;
                    this.movies = response.data.data;
                    const token = sessionStorage.getItem('token')
                    const decode = jwtDecode(token);

                    const role = decode['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
                    if (role == 'Admin') this.isAdmin = true;
                }
            }
            fetchMovie();
        }
        else { this.$router.push('/login') }

    }
}
</script>

<style lang="scss" scoped>
/* Banner style start */
.content-top {
    width: 100%;
    height: 80vh;
    background-position: left;
    background-image:
        linear-gradient(to right, rgba(27, 27, 27, 0), rgb(0, 0, 0)),
        url('https://wallpapercave.com/wp/wp7349515.jpg');
    background-size: cover;
    color: white;
    position: relative;
}

.content-top img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.breadcrumbs {
    position: absolute;
    left: 0;
    right: 0;
    top: 50%;
    margin-left: auto;
    margin-right: auto;
}

.breadcrumbs p {
    margin: 0;
    padding: 0;
}

.home {
    text-decoration: none;
    color: white;
    transition: 0.4s ease-out;
}

.home:hover {
    color: orangered;
}

.big-text {
    position: absolute;
    left: 0;
    right: 0;
    top: 60%;
    margin-left: auto;
    margin-right: auto;
}

.big-text h1 {
    font-weight: bolder;
}

.banner,
.image-lines-header {
    width: 100%;
}

.banner-image,
.line-image {
    width: 100%;
    object-fit: contain;
}

/* Banner style end */

.card-div {
    position: relative;
    overflow: hidden;
    height: 100%;
    width: 90%;
    box-shadow: rgba(0, 0, 0, 0.3) 0px 19px 38px, rgba(0, 0, 0, 0.22) 0px 15px 12px;
    transition: .2s ease-out;
}


.card-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    filter: brightness(0.9);
    transition: .3s ease-out;
    filter: brightness(0.5);
}

.card-body {
    width: 100%;
    position: absolute;
    bottom: 0;
    padding: 20px;
    color: white;
    transition: bottom .8s ease-out;
}

.card-div:hover .card-img {
    transform: scale(1.1);
}

.card-div:hover {
    border: 3px solid orangered;
}

.card-div:hover .btn-container button {
    background: orangered;
    color: white;
}

.card-title {
    font-size: 1rem;
    font-weight: 600;
    font-family: 'Space Grotesk', sans-serif;
    margin-bottom: 20px;
}

.card-text {
    font-family: 'Space Grotesk', sans-serif;
    font-size: 13px;
    margin-bottom: 15px;
}

.btn-container {
    button {
        border: none;
        outline: none;
        border-radius: 0;
        background: whitesmoke;
        padding: 10px 30px;
        font-family: 'Space Grotesk', sans-serif;
        font-size: 13px;
        color: #737373;
        font-weight: bold;
        transition: 0.3s ease-out;
    }
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


.pag-btn {
    border: none;
    outline: none;
    padding: 10px 8px;
    transition: 0.3s ease-out;

    &:hover {
        background: red;

        .material-icons {
            color: wheat;
        }
    }
}
</style>
