<template>
    <main>
        <h2>Reservation</h2>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" type="search" placeholder="Search by Reservation ID or Status"
                    v-model="searchValue" @input="search" />
            </div>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col" @click="sortBy('reservationId')">
                            Reservation Id
                            <span v-if="sortKey === 'reservationId'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th>
                        <th scope="col" @click="sortBy('customer')">
                            Customer
                            <span v-if="sortKey === 'customer'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th>
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
                        <th>{{ reservation.reservationId }}</th>
                        <td>{{ reservation.customerFullName }}</td>
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
                            <!-- <button class="btn btn-primary" @click="viewMore(reservation.reservationId)">View</button> -->
                            <button style="border: none; outline: none; background-color: transparent;"
                                @click="() => console.log(reservation)">
                                <span class="material-icons">edit</span>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>
    <router-view />
</template>

<script>
// import { getCustomer, getUser } from '@/script/UserService';
// import { jwtDecode } from 'jwt-decode';
import { getAllReservation } from '@/script/ReservationService';

export default {
    name: 'ReservationMovie',
    components: {},
    data() {
        return {
            searchValue: '',
            reservations: [],
            filteredReservations: [],
            sortKey: '',
            sortOrder: 'asc'
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
                name: 'reservationByCustomer',
                params: { id: reservationId }
            });
        },
        search() {
            const query = this.searchValue.toLowerCase();
            this.filteredReservations = this.reservations.filter(reservation => {
                const reservationIdMatch = reservation.reservationId.toString().toLowerCase().includes(query);
                const statusMatch = this.statusText(reservation.status).toLowerCase().includes(query);
                const movieMatch = reservation.movie.title.toLowerCase().includes(query);
                const customerMatch = reservation.customerFullName.toLowerCase().includes(query);
                return reservationIdMatch || statusMatch || movieMatch || customerMatch;
            });

            // this.sortReservations();
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
                else if (this.sortKey === 'customer') {
                    return this.sortOrder === 'asc'
                        ? a.customerFullName.localeCompare(b.customerFullName)
                        : b.customerFullName.localeCompare(a.customerFullName)
                }
                return 0;
            });

            this.filteredReservations = sortedReservations;
        }
    },
    mounted() {
        const fetching = async () => {
            // const token = sessionStorage.getItem('token');
            // const decode = jwtDecode(token);
            // const user = await getUser(decode.Email);
            // const customer = await getCustomer(user.data.userId);
            const reservations = await getAllReservation();
            if (reservations.status == 200) {
                this.reservations = reservations.data;
                this.filteredReservations = reservations.data;
            }
        };
        fetching();
    }
};
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
        background-color: #abe018;
        color: black;
    }
}
</style>
