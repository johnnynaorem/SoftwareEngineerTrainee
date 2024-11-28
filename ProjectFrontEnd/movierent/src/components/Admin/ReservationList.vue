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
                            <button type="button" class="btn btn-primary reserve-btn" data-bs-toggle="modal"
                                data-bs-target="#reservationUpdateModal"
                                style="border: none; outline: none; background-color: transparent;">
                                <span class="material-icons" style="color:black"
                                    @click="() => reservationToBeEdited(reservation.reservationId, reservation.status)">edit</span>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <!-- Rerservation Status Update Modal -->
            <div class="modal fade" id="reservationUpdateModal" tabindex="-1"
                aria-labelledby="reservationUpdateModalLabel" aria-hidden="true">
                <div class="modal-mapper modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content modal-content-mapper">
                        <div class="modal-header">
                            <div>
                                <h4 class="modal-title" id="reservationUpdateModalLabel">Update Reservation Status!</h4>
                                <p>To ensure accurate tracking, please select the current status of the reservation.
                                    This will update the reservation's progress in the system. Choose from the following
                                    options to reflect the appropriate stage of the rental process.</p>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Conditional Success Message -->
                            <div v-if="reservationStatusUpdateCompleted">
                                <p class="text-success">Reservation Status Update is completed.</p>
                            </div>
                            <!-- <form v-else v-on:submit="movieResevation">
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <input v-model="reservationData.fullName" type="text" class="form-control"
                                        placeholder="Enter Full Name" required>
                                    <input v-model="reservationData.email" type="text" class="form-control"
                                        placeholder="Enter Email" required>
                                    <input v-model="reservationData.phone" type="text" class="form-control"
                                        placeholder="Enter Phone Number" required>
                                </div>
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <input v-model="reservationData.date" type="datetime-local" class="form-control"
                                        required>
                                </div>
                                <button type="submit" class="btn">Send</button>
                            </form> -->
                            <div v-else class="select-mapper">
                                <select v-model="updateStatus" name="" id="">
                                    <option value="" disabled selected>{{ currentReservationStatus }}</option>
                                    <option value="active">Active</option>
                                    <option value="completed">Completed</option>
                                    <option value="cancelled">Cancelled</option>
                                    <option value="expired">Expired</option>
                                    <option value="notAvailable">Not Available</option>
                                </select>
                                <button type="submit" class="btn mx-3" @click="updateStatusMethod()">Send</button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <router-view />
</template>

<script>
// import { getCustomer, getUser } from '@/script/UserService';
// import { jwtDecode } from 'jwt-decode';
import { getAllReservation, updateReservationStatus } from '@/script/ReservationService';

export default {
    name: 'ReservationMovie',
    components: {},
    data() {
        return {
            searchValue: '',
            updateStatus: '',
            currentReservationStatus: '',
            reservationStatusUpdateCompleted: false,
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
        },
        reservationToBeEdited(id, status) {
            if (status == 0) status = 'Active'
            else if (status == 1) status = 'Pending'
            else if (status == 2) status = 'Completed'
            else if (status == 3) status = 'Cancelled'
            else if (status == 4) status = 'Expired'
            else if (status == 5) status = 'Not Available'
            this.currentReservationStatus = status;
            sessionStorage.setItem("reservationId", id)
        },
        async updateStatusMethod() {
            console.log(this.currentReservationStatus, this.updateStatus)
            if (this.currentReservationStatus.toLowerCase() !== this.updateStatus.toLowerCase()) {
                let status = 0;
                if (this.updateStatus == 'completed') status = 2;
                if (this.updateStatus == 'expired') status = 4;
                if (this.updateStatus == 'cancelled') status = 3;
                if (this.updateStatus == 'notAvailable') status = 5;
                const reservationId = sessionStorage.getItem('reservationId')
                const { data } = await updateReservationStatus(reservationId, status);
                if (data) {
                    console.log(data);
                    this.updateStatus = '';
                    this.reservationStatusUpdateCompleted = true;
                }
            }
            else alert("Default Status");
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
    padding: 10px;
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
        background: red;
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
