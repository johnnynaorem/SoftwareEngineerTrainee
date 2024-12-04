<script setup>
import { onMounted, ref, watch } from 'vue';
import MainLayout from '../Layout/MainLayout.vue';
import { addMovieToWishlist, getMovieByCategory, getMovieById, getWishlistByMovieIdAndCustomerId, removeMovieFromWishlist } from '@/script/MovieService';
import { useRoute } from 'vue-router';
import { useRouter } from 'vue-router';
import { reservationSumbit } from '@/script/ReservationService';
import { jwtDecode } from 'jwt-decode';
import { getCustomer, getUser } from '@/script/UserService';
import { getRentalByCustomerIdAndMovieId } from '@/script/RentalService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const movie = ref();
const rentalStatus = ref(null)
const route = useRoute()
const router = useRouter()
const movieRelated = ref([])
const url = ref('')
const isMovieIsInWishlist = ref(false)
// const isReservedMovie = ref(false)
const reservationData = ref({
    fullName: '',
    email: '',
    phone: '',
    date: ''
});
const reservationCompleted = ref(false);

const watchTrailer = (link) => {
    url.value = link
}

const minDate = () => {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
};

const maxDate = () => {
    const now = new Date();
    now.setDate(now.getDate() + 7);

    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
};

const toMoviePage = (id) => {
    console.log()
    router.push(`/movie/${id}`)

}

const addToWishlist = async () => {
    const movieId = route.params.id;
    const token = sessionStorage.getItem('token');
    const decode = jwtDecode(token);
    const user = await getUser(decode.Email);
    const { data } = await getCustomer(user.data.userId);
    const addToWishlistResponse = await addMovieToWishlist(movieId, data.customerId);
    if (addToWishlistResponse.status == 200) {
        if (addToWishlistResponse.data == true) {
            toast.success("Movie Added to Wishlist")
            isMovieIsInWishlist.value = true;
        }
    }
}
const removeFromWishlist = async () => {
    const movieId = route.params.id;
    const token = sessionStorage.getItem('token');
    const decode = jwtDecode(token);
    const user = await getUser(decode.Email);
    const { data } = await getCustomer(user.data.userId);
    const removeFromWishlistResponse = await removeMovieFromWishlist(movieId, data.customerId);
    if (removeFromWishlistResponse.status == 200) {
        if (removeFromWishlistResponse.data == true) {
            toast.success("Movie Removed from Wishlist")
            isMovieIsInWishlist.value = false;
        }
    }
}
// @click="rentMovie(movie.movieId)"
// const rentMovie = (movieId) => {
//     console.log(movieId)
// }

const fetching = async () => {
    try {
        const id = route.params.id;
        const returnMovie = await getMovieById(id);

        let genre = '';
        if (returnMovie.status === 200) {
            movie.value = returnMovie.data;
            genre = returnMovie.data.genre;
        }

        const relativeMovie = await getMovieByCategory(genre);
        if (relativeMovie.status === 200) {
            let related = relativeMovie.data;
            related = related.filter(m => m.movieId !== movie.value.movieId);
            movieRelated.value = related;
        }
        const movieId = route.params.id;
        const token = sessionStorage.getItem('token');
        const decode = jwtDecode(token);
        const user = await getUser(decode.Email);
        const { data } = await getCustomer(user.data.userId);
        const response = await getRentalByCustomerIdAndMovieId(movieId, data.customerId)
        if (response.status == 200) {
            rentalStatus.value = response.data.status;
        }
    } catch (error) {
        console.error('Error fetching payment data:', error);
    }
}

const movieResevation = async (event) => {
    event.preventDefault();
    const movieId = route.params.id;
    const token = sessionStorage.getItem('token');
    const decode = jwtDecode(token);
    const user = await getUser(decode.Email);
    const { data } = await getCustomer(user.data.userId);
    const response = await reservationSumbit(data.customerId, movieId, reservationData.value.date);
    toast.pen
    if (response.status == 200) {
        toast.success("Reserved Successfull!")
        reservationCompleted.value = true;
    }

    reservationData.value = {
        fullName: '',
        email: '',
        phone: '',
        date: ''
    };
};

onMounted(() => {
    const getWishlist = async () => {
        const movieId = route.params.id;
        const token = sessionStorage.getItem('token');
        const decode = jwtDecode(token);
        const user = await getUser(decode.Email);
        const { data } = await getCustomer(user.data.userId);
        reservationData.value = {
            fullName: data.fullName,
            email: data.email,
            phone: data.phoneNumber,
        }
        const wishlist = await getWishlistByMovieIdAndCustomerId(movieId, data.customerId);
        if (wishlist.status == 200) {
            isMovieIsInWishlist.value = true;
        }
    }
    getWishlist();
    fetching();
});

watch(
    () => route.params.id,
    () => {
        fetching();
    }
);

</script>
<template>
    <MainLayout>
        <template #default>
            <div class="content-top">
                <div class="breadcrumbs d-flex justify-content-center align-items-center gap-2">
                    <RouterLink to="/" class="home">Home</RouterLink>
                    <p>></p>
                    <p>Movie</p>
                    <p>></p>
                    <p v-if="movie">{{ movie.title }}</p>
                </div>
                <div class="big-text d-flex justify-content-center">
                    <h1 v-if="movie">{{ movie.title }}</h1>
                </div>
            </div>
            <div class="content-middle" style="width: 100%;">
                <img src="../../Images/image-lines-header.jpg" alt="" width="100%">
            </div>
            <div v-if="movie" class="container-xl mt-5">
                <div class="title-mapper d-flex justify-content-between align-items-center">
                    <div class="title-mapper-left">
                        <h3 class="movie-title">{{ movie.title }}</h3>
                        <h6 class="movie-genre">{{ movie.genre }} / 130 Mins</h6>
                    </div>
                    <div class="title-mapper-right">
                        <div class="d-flex align-items-center gap-3">
                            <i v-if="!isMovieIsInWishlist" class="fa-regular fa-heart"
                                style="color: #f00f0f; font-size: 30px; cursor: pointer;" @click="addToWishlist()"></i>
                            <i v-if="isMovieIsInWishlist" class="fa-solid fa-heart"
                                style="color: #f00f0f; font-size: 30px; cursor: pointer;"
                                @click="removeFromWishlist()"></i>
                            <button v-if="rentalStatus == 3 || rentalStatus == null" type="button"
                                class="btn btn-primary reserve-btn" data-bs-toggle="modal"
                                data-bs-target="#rentModal">Reserve</button>
                            <button v-else-if="rentalStatus == 0" class="btn btn-primary reserve-btn"
                                @click="router.push('/dashboard/rental')">Payment Pending</button>
                            <button v-else-if="rentalStatus == 1" class="btn btn-primary reserve-btn"
                                @click="router.push('/dashboard/rental')">Pickup Movie</button>
                            <button v-else-if="rentalStatus == 2" class="btn btn-primary reserve-btn"
                                @click="router.push('/dashboard/rental')">Currently Renting</button>
                            <button v-else-if="rentalStatus == 4" class="btn btn-primary reserve-btn"
                                @click="router.push('/dashboard/rental')">Overdue! Return Now</button>
                        </div>
                    </div>
                </div>
                <div class="image-mapper d-flex">
                    <div class="image-mapper-left">
                        <img class="left-image" :src=movie.coverImage alt="Movie Image">
                    </div>
                    <div class="image-mapper-right position-relative">
                        <i class="fa-solid fa-circle-play fa-beat position-absolute btn" data-bs-toggle="modal"
                            data-bs-target="#trailerModal" @click="watchTrailer(movie.trailerVideo)"
                            style="color: #FFD43B;"></i>
                        <img class="left-right" :src=movie.coverImage alt="Another Image">
                    </div>
                </div>
                <div class="info-mapper mt-5">
                    <p><span class="info-title">Director: </span><span class="me-5 info-value">Chistine
                            Eve</span><span class="info-title">Preimier: </span><span class="info-value">06, March
                            2023</span></p>
                    <p><span class="info-title">Writer: </span><span class="me-5 info-value">Aleesha Rose</span>
                        <span class="info-title">Time: </span><span class="info-value">180 Mins</span>
                    </p>
                    <p><span class="info-title">Rating: </span><span class="info-value">8</span></p>
                </div>
                <hr class="my-5" />
                <div class="story-mapper">
                    <h3>Story Line</h3>
                    <p style="line-height: 1.8rem; color: gray;">
                        {{ movie.description }}
                    </p>
                </div>
                <hr class="my-5">
                <div class="more-relative-movie-mapper">
                    <h3>More Movie Like This</h3>
                    <div class="container mt-5">
                        <div class="row p-md-3">
                            <div class="cardContainer mb-3 col-12  col-sm-6 col-md-4 col-lg-3"
                                v-for="movie in movieRelated" :key="movie.movieId">
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
                </div>
            </div>
            <!-- Trailer Modal -->
            <div class="modal fade" id="trailerModal" tabindex="-1" aria-labelledby="TrailerModalLabel"
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
            <!-- Rent Modal -->
            <div class="modal fade" id="rentModal" tabindex="-1" aria-labelledby="rentModalLabel" aria-hidden="true">
                <div class="modal-mapper modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content modal-content-mapper">
                        <div class="modal-header">
                            <div>
                                <h4 class="modal-title" id="exampleModalLabel">Reserve your Movie today!</h4>
                                <p>Fill out the form below to reserve your movie. Complete the necessary details to
                                    ensure a smooth rental experience.</p>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"
                                aria-label="Close">X</button>
                        </div>
                        <div class="modal-body">
                            <!-- Conditional Success Message -->
                            <div v-if="reservationCompleted">
                                <p class="text-light">Congratulations! Your reservation is completed.</p>
                            </div>
                            <form v-else v-on:submit="movieResevation">
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div>
                                        <label>Rentee</label>
                                        <input v-model="reservationData.fullName" type="text" class="form-control"
                                            placeholder="Enter Full Name" required>
                                    </div>
                                    <div>
                                        <label>Email</label>
                                        <input v-model="reservationData.email" type="text" class="form-control"
                                            placeholder="Enter Email" required>
                                    </div>
                                    <div>
                                        <label>Phone</label>
                                        <input v-model="reservationData.phone" type="text" class="form-control"
                                            placeholder="Enter Phone Number" required>
                                    </div>
                                </div>
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <input v-model="reservationData.date" type="datetime-local" class="form-control"
                                        required :min="minDate()" :max="maxDate()">
                                </div>
                                <button type="submit" class="btn">Send</button>
                            </form>

                        </div>
                    </div>
                </div>
            </div>
        </template>
    </MainLayout>
</template>

<style lang="scss" scoped>
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

.title-mapper {
    .title-mapper-right {
        .reserve-btn {
            box-sizing: border-box;
            border: none;
            outline: none;
            padding: 10px 30px;
            background: orangered;
            transition: .7s ease-out;
            color: white;

            &:hover {
                background: white;
                color: orangered;
                // border: 1px solid orangered;
            }
        }


    }
}

// Image Mapper style start
.image-mapper {
    width: 100%;
    height: 500px;
    gap: 2rem;

    img {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }

    .image-mapper-left {
        width: 40%;
    }

    .image-mapper-right {
        width: 60%;

        i {
            left: 40%;
            top: 50%;
            font-size: 80px;
        }
    }

}

@media (max-width: 800px) {
    .image-mapper {
        flex-direction: column;

        .image-mapper-left {
            width: 100%;
            height: 100%;
        }

        .image-mapper-right {
            width: 100%;
            height: 100%;
        }
    }
}

.info-mapper {
    .info-title {
        font-weight: 700;
        font-family: 'Space Grotesk', sans-serif;
        font-size: 1.1rem;
    }

    .info-value {
        color: gray;
        font-weight: 500;
        font-family: 'Space Grotesk', sans-serif;
    }
}

//related-movie style start
.card-div {
    position: relative;
    overflow: hidden;
    height: 100%;
    width: 100%;
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

.modal-mapper {
    max-width: 60%;
    color: var(--light);

    .modal-content-mapper {
        background: #191919;
        padding: 20px;
        border-radius: 20px;
    }

    button {
        border: none;
        outline: none;
        padding: 10px 30px;
        background: black;
        border-radius: 20px;
        color: var(--light);
        transition: 0.2s ease-out;

        &:hover {
            background: rgb(134, 130, 130);
        }
    }

    .input-mapper {
        input {
            background: transparent;
            color: var(--light);

            &::placeholder {
                color: var(--light);
            }
        }

    }
}
</style>
