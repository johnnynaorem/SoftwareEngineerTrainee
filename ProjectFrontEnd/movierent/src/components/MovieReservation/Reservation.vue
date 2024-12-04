<template>
    <main>
        <h2>Reservation</h2>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" type="search" placeholder="Search by Reservation ID or Status"
                    v-model="searchValue" @input="search" />
            </div>
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col">
                            Sl
                        </th>
                        <!-- <th scope="col" @click="sortBy('reservationId')">
                            Reservation Id
                            <span v-if="sortKey === 'reservationId'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th> -->
                        <th scope="col" @click="sortBy('movie')">
                            Movie
                            <span v-if="sortKey === 'movie'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th>
                        <th scope="col" @click="sortBy('status')">
                            Status
                            <span v-if="sortKey === 'status'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th>
                        <th scope="col">Reservation Date</th>
                        <th scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(reservation, i) in filteredReservations" :key="i">
                        <!-- <th>{{ reservation.reservationId }}</th> -->
                        <th>{{ i + 1 }}</th>
                        <td>
                            <div class="d-flex table-movie-mapper">
                                <img :src=reservation.movie.coverImage alt="" width="30px">
                                <p>{{ reservation.movie.title }}</p>
                            </div>
                        </td>
                        <td class="status">
                            <span class="text" :class="getStatusClass(reservation.status)">
                                {{ statusText(reservation.status) }}
                            </span>
                        </td>
                        <td>{{ new Date(reservation.reservationDate).toDateString() }}</td>
                        <td>
                            <button class="btn btn-primary mx-1"
                                @click="viewMore(reservation.reservationId)">View</button>
                            <button type="button" class="btn btn-warning mx-1 rent-btn" data-bs-toggle="modal"
                                data-bs-target="#rentMovieModal" :disabled="reservation.status !== 0"
                                @click="rentMovieSetup(reservation.movie.movieId, reservation.customerId, reservation.movie.title, reservation.customerFullName, reservation.movie.rentalPrice)">Rent</button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <!-- Rental Movie Modal -->
            <div class="modal fade" id="rentMovieModal" tabindex="-1" aria-labelledby="rentMovieModalLabel"
                aria-hidden="true">
                <div class="modal-mapper modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content modal-content-mapper">
                        <div class="modal-header">
                            <div>
                                <h4 class="modal-title" id="reservationUpdateModalLabel">
                                    Rent "{{ movieToBeRent.currentMovie }}"
                                </h4>
                                <p>
                                    Get ready to enjoy "{{ movieToBeRent.currentMovie }}"! Simply confirm your rental to
                                    start watching
                                    this exciting film. Whether it's an action-packed adventure, a heartfelt drama, or a
                                    comedy, you’re just
                                    a few clicks away from streaming this movie on your device. Don't miss out – rent
                                    now and enjoy instantly.
                                </p>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"
                                aria-label="Close">X</button>
                        </div>
                        <div class="modal-body">
                            <!-- Conditional Success Message -->
                            <div v-if="isPaymentTime && !isPaymentSuccessfull">
                                <h4>Complete Your Payment Process for the Rental!</h4>
                                <div>
                                    <h2>Pay for your order</h2>
                                    <div id="card-element"></div> <!-- Card Element -->
                                    <button @click="handleSubmit" :disabled="isProcessing">Pay Now</button>
                                </div>
                            </div>

                            <div v-if="isPaymentSuccessfull">
                                <p class="text-white">{{ movieToBeRent.currentMovie }} successfully rented!!</p>
                            </div>

                            <!-- Show form only if neither payment time nor success is true -->
                            <form v-if="!isPaymentTime && !isPaymentSuccessfull" @submit="rentMovieMethod">
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div class="movie-input-mapper">
                                        <label for="movie">Movie Name</label>
                                        <input id="movie" type="text" class="form-control"
                                            v-model="movieToBeRent.currentMovie" disabled>
                                    </div>
                                    <div class="rantee-input-mapper">
                                        <label for="rantee">Rantee</label>
                                        <input id="rantee" type="text" class="form-control"
                                            v-model="movieToBeRent.customerName" disabled>
                                    </div>
                                    <div class="rentalPrice-input-mapper">
                                        <label for="price">Price in ₹</label>
                                        <input type="number" class="form-control" v-model="movieToBeRent.rentalPrice"
                                            disabled>
                                    </div>
                                </div>
                                <button type="submit" class="btn">Rent</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </main>
    <router-view />
</template>

<script>
import { getCustomer, getUser } from '@/script/UserService';
import { jwtDecode } from 'jwt-decode';
import { getReservationByCustomer } from '@/script/ReservationService';
import { loadStripe } from "@stripe/stripe-js";
import { CardElement } from "@stripe/react-stripe-js";
import { rentMovie } from '@/script/RentalService';
import { makePayment } from '@/script/PaymentService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
    name: 'ReservationMovie',
    components: {},
    data() {
        return {
            movieToBeRent: {
                currentMovie: '',
                customerName: '',
                customerId: null,
                movieId: null,
                rentalPrice: 0,


            },
            stripe: null,
            elements: null,
            clientSecret: null,
            isProcessing: false,
            isPaymentTime: false,
            isPaymentSuccessfull: false,
            searchValue: '',
            reservations: [],
            filteredReservations: [],
            sortKey: '',
            sortOrder: 'asc',
            rentalId: null
        };
    },
    methods: {
        statusText(status) {
            switch (Number(status)) {
                case 0:
                    return 'Active';
                case 1:
                    return 'Pending';
                case 2:
                    return 'Completed';
                case 3:
                    return 'Cancelled';
                case 4:
                    return 'Expired';
                default:
                    return 'Not Available';
            }
        },
        getStatusClass(status) {
            switch (Number(status)) {
                case 0:
                    return 'status-active';
                case 1:
                    return 'status-pending';
                case 2:
                    return 'status-completed';
                case 3:
                    return 'status-cancelled';
                case 4:
                    return 'status-expired';
                default:
                    return 'status-not-available';
            }
        },
        viewMore(reservationId) {
            this.$router.push({
                name: 'reservationDetail',
                params: { id: reservationId }
            });
        },
        search() {
            const query = this.searchValue.toLowerCase();
            this.filteredReservations = this.reservations.filter(reservation => {
                const reservationIdMatch = reservation.reservationId.toString().toLowerCase().includes(query);
                const statusMatch = this.statusText(reservation.status).toLowerCase().includes(query);
                const movieMatch = reservation.movie.title.toLowerCase().includes(query);
                return reservationIdMatch || statusMatch || movieMatch;
            });

            this.sortReservations();
        },
        sortBy(key) {
            if (this.sortKey === key) {
                this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
            } else {
                this.sortKey = key;
                this.sortOrder = 'asc';
            }
            this.sortReservations();
        },
        sortReservations() {
            const sortedReservations = [...this.filteredReservations].sort((a, b) => {
                if (this.sortKey === 'reservationId') {
                    return this.sortOrder === 'asc'
                        ? a.reservationId - b.reservationId
                        : b.reservationId - a.reservationId;
                } else if (this.sortKey === 'status') {
                    const statusA = this.statusText(a.status).toLowerCase();
                    const statusB = this.statusText(b.status).toLowerCase();
                    return this.sortOrder === 'asc'
                        ? statusA.localeCompare(statusB)
                        : statusB.localeCompare(statusA);
                } else if (this.sortKey === 'movie') {
                    const movieA = a.movie.title.toLowerCase();
                    const movieB = b.movie.title.toLowerCase();
                    return this.sortOrder === 'asc'
                        ? movieA.localeCompare(movieB)
                        : movieB.localeCompare(movieA)
                }
                return 0;
            });

            this.filteredReservations = sortedReservations;
        },

        rentMovieSetup(movieId, customerId, movieTitle, customerName, rentalPrice) {
            console.log(movieId, customerId, movieTitle)
            this.movieToBeRent.currentMovie = movieTitle
            this.movieToBeRent.customerName = customerName
            this.movieToBeRent.customerId = customerId
            this.movieToBeRent.movieId = movieId
            this.movieToBeRent.rentalPrice = rentalPrice
        },
        async rentMovieMethod(event) {
            event.preventDefault();
            toast.info("Processing");
            const responseRental = await rentMovie(this.movieToBeRent.customerId, this.movieToBeRent.movieId);
            if (responseRental.status === 200) {
                toast.info("Complete Payment For Futher Process");
                this.isPaymentTime = true;
                this.rentalId = responseRental.data;
                this.stripe = await loadStripe("pk_test_51OjhOhSFOS5YpWUi0nBFqisLpS52YZNSozPPJZafVCSyDrridXyMOqcgJNI2ortDLFrigd5Gv2UzvXd4oFA1p6iy00D3vAcpCg");
                this.elements = this.stripe.elements();

                // Get the client secret from your server for Stripe payment
                const response = await fetch("https://localhost:7203/api/Payment/create-payment-intent", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        amount: this.movieToBeRent.rentalPrice * 100,
                        currency: "usd",
                        description: `Movie rental payment for ${this.movieToBeRent.currentMovie}`,
                        customerName: this.movieToBeRent.customerName,
                        customerAddress: {
                            line1: "123 Main St",
                            city: "Mumbai",
                            state: "MH",
                            country: "IN",
                            PostalCode: "400001"
                        }
                    }),
                });
                const data = await response.json();
                this.clientSecret = data.clientSecret;

                // Mount the card element in the form
                const cardElement = this.elements.create("card");
                cardElement.mount("#card-element");
                this.isPaymentTime = true;
            }
        },
        async handleSubmit() {
            if (this.isProcessing || !this.clientSecret) return;
            toast.info("Processing");
            this.isProcessing = true;

            const { error, paymentIntent } = await this.stripe.confirmCardPayment(
                this.clientSecret,
                {
                    payment_method: {
                        card: this.elements.getElement(CardElement),
                    },
                }
            );

            if (error) {
                console.error(error);
                toast.error("Payment failed: " + error.message);
            } else if (paymentIntent.status === "succeeded") {
                this.isPaymentSuccessfull = true;
                const paymentResponse = await makePayment(this.rentalId, this.movieToBeRent.customerId, 'card');
                if (paymentResponse.status == 200) {
                    toast.success("Payment successful!");
                    this.fetching();
                }
            }

            this.isProcessing = false;
        },
        async paymentMethod(e, price) {
            e.preventDefault();
            console.log(price);
            this.isPaymentSuccessfull = true;
        },
        async fetching() {
            const token = sessionStorage.getItem('token');
            const decode = jwtDecode(token);
            const user = await getUser(decode.Email);
            const customer = await getCustomer(user.data.userId);
            const reservations = await getReservationByCustomer(customer.data.customerId);
            if (reservations.status == 200) {
                this.reservations = reservations.data;
                this.filteredReservations = reservations.data;
            }
        }
    },
    mounted() {
        this.fetching();
    }
}
</script>

<style lang="scss" scoped>
.search {
    input {
        padding: 10px 30px;
        border: none;
        outline: none;
        border-radius: 10px;
        background: var(--light);
    }
}

.table {
    th {
        cursor: pointer;
    }
}

.table-movie-mapper {

    align-items: center;
    gap: 10px;

    p {
        padding: 0;
        margin: 0;
    }

    img {
        height: 30px;
        object-fit: cover;
        border-radius: 20px;
    }
}

.text {
    padding: 10px 6px;
    border-radius: 10px;
    font-size: small;

    &.status-active {
        background-color: #28a745;
        color: white;
    }

    &.status-pending {
        background-color: #ffc107;
        color: black;
    }

    &.status-completed {
        background-color: #007bff;
        color: white;
    }

    &.status-cancelled {
        background-color: #dc3545;
        color: white;
    }

    &.status-expired {
        background-color: #6c757d;
        color: white;
    }

    &.status-not-available {
        font-size: x-small;
        background-color: #abe018;
        color: black;
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

    .select-mapper {
        border: 2px solid white;
        border-radius: 10px;
        padding: 20px;

        select {
            cursor: pointer;
            border: none;
            border: 2px solid rgb(20, 1, 1);
            border-radius: 10px;
            padding: 10px;
            outline: none;
            background-color: transparent;
            color: white;

            option {
                background: red;
            }
        }
    }
}
</style>
