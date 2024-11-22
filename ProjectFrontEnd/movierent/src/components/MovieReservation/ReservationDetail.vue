<script>
import { getReservationByReservationId } from '@/script/ReservationService';

export default {
    name: 'ReservationDetail',

    data() {
        return {
            reservation: [],
            movie: [],
            customer: [],
            coverImage: "",
        }
    },
    methods: {
        async fetching() {
            try {

                const reservationId = this.$route.params.id;
                const { data } = await getReservationByReservationId(reservationId);
                this.reservation = data.reservation;
                this.movie = data.reservation.movie
                this.customer = data.reservation.customer
                this.coverImage = data.reservation.movie.coverImage
            } catch (error) {
                console.log(error);
            }
        }
    },

    mounted() {
        this.fetching();
    }
}
</script>

<template>
    <main>
        <div class="container reservation-container mt-5 d-flex flex-column gap-3">
            <div class="top">
                <div class="top-upper">
                    <h3>Reservation ID: {{ reservation.reservationId }}</h3>
                </div>
                <div class="top-down d-flex align-items-center gap-5">
                    <p class="reservationDate-text">{{ new Date(reservation.reservationDate).toDateString() }}
                    </p>
                    <p class="status-text">{{ reservation.status }}</p>
                </div>
            </div>
            <hr />
            <div class="middle">
                <div class="middleContent d-flex gap-4 align-items-center">
                    <div class="imageContainer">
                        <img :src=coverImage alt="movie Image">
                    </div>
                    <div class="movie-details flex-1">
                        <p>{{ movie.title }}</p>
                        <p>{{ movie.genre }}</p>
                    </div>
                </div>
            </div>
            <hr />
            <div class=" bottom d-flex align-items-center justify-content-between">
                <div class="bottom-left">
                    <p class="title">Reservation By:- </p>
                    <div class="customer-details">
                        <p class="value">{{ customer.fullName }}</p>
                        <p class="value">{{ customer.email }}</p>
                        <p class="value">{{ customer.phoneNumber }}</p>
                    </div>
                </div>
                <div class="bottom-right">
                    <p class="title">Address</p>
                    <p class="value">{{ customer.address }}</p>
                </div>
            </div>
        </div>
    </main>
</template>

<style scoped>
.reservation-container {
    padding: 20px;
    border-radius: 20px;
    background-color: rgb(255, 253, 253);
    width: 40%;
    cursor: pointer;
}

@media screen and (min-width: 901px) and (max-width: 1100px) {
    .reservation-container {
        width: 50%;
    }
}

@media screen and (min-width: 610px) and (max-width: 900px) {
    .reservation-container {
        width: 60%;
    }
}

@media screen and (min-width: 610px) and (max-width: 700px) {
    .reservation-container {
        width: 70%;
    }
}

@media screen and (max-width: 609px) {
    .reservation-container {
        width: 100%;
    }
}

.reservation-container p {
    margin: 0;
    padding: 0;
}

.reservationDate-text,
.status-text {
    font-size: 13px;
}

.imageContainer {
    width: 100px;
    height: 100px;
    border-radius: 20px;
    padding: 15px;
    background-color: antiquewhite;
    transition: 1.2s;
}

.imageContainer:hover {
    background-color: rgb(207, 191, 191);
}

img {
    object-fit: cover;
    object-position: top;
    height: 100%;
    width: 100%;
    border-radius: 10px;
}

.movie-details p {
    font-weight: bold;
}

.title {
    font-size: 14px;
    font-weight: bold;
    color: rgb(153, 160, 167);
}

.value {
    font-size: 13px;
}
</style>