<template>
    <main>
        <h2>Rentals</h2>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" type="search" placeholder="Search by Rental ID or Status"
                    v-model="searchValue" @input="search" />
            </div>
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col" @click="sortBy('rentalId')">
                            Rental Id
                            <span v-if="sortKey === 'rentalId'">
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
                        <th scope="col" @click="sortBy('pStatus')">
                            Payment Status
                            <span v-if="sortKey === 'pStatus'">
                                {{ sortOrder === 'asc' ? '↑' : '↓' }}
                            </span>
                        </th>
                        <th scope="col">Rental Date</th>
                        <th scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(rental, i) in filteredRentals" :key="i">
                        <th>{{ rental.rentalId }}</th>
                        <td>{{ rental.customer.fullName }}</td>
                        <td>
                            <div class="d-flex table-movie-mapper">
                                <img :src="rental.movie.coverImage" alt="" width="30px">
                                <p>{{ rental.movie.title }}</p>
                            </div>
                        </td>
                        <td class="status">
                            <span class="text" :class="rental.status">
                                {{ rental.status }}
                            </span>
                        </td>
                        <td class="pStatus">
                            <span class="text" :class="rental.status !== 'Pending' ? 'Success' : 'Not-payment'">
                                {{ rental.status !== 'Pending' ? 'Success' : 'Not Payment' }}
                            </span>
                        </td>
                        <td>{{ new Date(rental.rentalDate).toDateString() }}</td>
                        <td>
                            <div>
                                <button class="btn btn-primary mx-1" :disabled="rental.status !== 'Confirmed'"
                                    @click="pickupMovie(rental.rentalId, rental.customer.customerId, rental.movie.movieId)">Pickup</button>
                                <button class="btn btn-warning text-dark mx-1"
                                    :disabled="rental.status == '!Active' || rental.status == 'Returned' || rental.status == 'Overdue'"
                                    @click="returnMovie(rental.rentalId, rental.customer.customerId)">Return</button>

                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>
</template>

<script setup>
import { getRentalByCustomer, pickupMovieByCustomer, returnMovieByCustomer } from '@/script/RentalService';
import { getCustomer, getUser } from '@/script/UserService';
import { jwtDecode } from 'jwt-decode';
import { onMounted, ref } from 'vue';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

const rentals = ref([]);
const filteredRentals = ref([]);
const searchValue = ref('');
const sortKey = ref('');
const sortOrder = ref('asc');

const search = () => {
    const query = searchValue.value.toLowerCase();
    filteredRentals.value = rentals.value.filter(rental => {
        const rentalIdMatch = rental.rentalId.toString().toLowerCase().includes(query);
        const statusMatch = rental.status.toLowerCase().includes(query);
        const movieMatch = rental.movie.title.toLowerCase().includes(query);
        const customerMatch = rental.customer.fullName.toLowerCase().includes(query)
        return rentalIdMatch || statusMatch || movieMatch || customerMatch;
    });
    sortRentals();
};

const pickupMovie = async (rentalId, movieId, customerId) => {
    console.log(rentalId, customerId, movieId);
    const response = await pickupMovieByCustomer(rentalId, customerId, movieId);
    if (response.status == 200) {
        toast.success("Movie is Successfully Pickup!....")
    }
}
const returnMovie = async (rentalId, customerId) => {
    console.log(rentalId, customerId);
    const response = await returnMovieByCustomer(rentalId, customerId);
    if (response.status == 200) {
        toast.success("Movie is Successfully Returned!....")
    }
}

const sortBy = (key) => {
    if (sortKey.value === key) {
        sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
    } else {
        sortKey.value = key;
        sortOrder.value = 'asc';
    }
    sortRentals();
};

const sortRentals = () => {
    filteredRentals.value = [...filteredRentals.value].sort((a, b) => {
        if (sortKey.value === 'rentalId') {
            return sortOrder.value === 'asc'
                ? a.rentalId - b.rentalId
                : b.rentalId - a.rentalId;
        } else if (sortKey.value === 'status') {
            return sortOrder.value === 'asc'
                ? a.status.localeCompare(b.status)
                : b.status.localeCompare(a.status);
        } else if (sortKey.value === 'movie') {
            return sortOrder.value === 'asc'
                ? a.movie.title.localeCompare(b.movie.title)
                : b.movie.title.localeCompare(a.movie.title);
        } else if (sortKey.value === 'pStatus') {
            return sortOrder.value === 'asc'
                ? a.status.localeCompare(b.status)
                : b.status.localeCompare(a.status);
        }
        return 0;
    });
};

onMounted(() => {
    const fetching = async () => {
        try {
            const token = sessionStorage.getItem('token');
            const decode = jwtDecode(token);
            const user = await getUser(decode.Email);
            const returnCustomer = await getCustomer(user.data.userId);
            const returnRentals = await getRentalByCustomer(returnCustomer.data.customerId);
            if (returnRentals.status === 200) {
                rentals.value = returnRentals.data;
                filteredRentals.value = returnRentals.data;
            }
        } catch (error) {
            console.error('Error fetching rental data:', error);
        }
    };
    fetching();
});
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
    font-weight: bolder;

    &.Returned {
        background-color: #ffc107;
        color: #ffffff;
    }

    &.Pending {
        background-color: #007bff;
        color: white;
    }

    &.Overdue {
        background-color: #dc3545;
        color: white;
    }

    &.Active {
        background-color: #6c757d;
        color: white;
    }

    &.Success {
        background-color: green;
        color: white;
    }

    &.Not-payment {
        background-color: gray;
        color: white;
    }
}
</style>
