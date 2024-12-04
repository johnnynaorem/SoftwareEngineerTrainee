<template>
    <main>
        <h2>Rentals</h2>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" type="search" placeholder="Search by Rental ID or Status"
                    v-model="searchValue" @input="search" />
            </div>
            <div class="table-mapper">
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
                            <th scope="col">Return Date</th>
                            <!-- <th scope="col">Action</th> -->
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(rental, i) in paginatedRentals" :key="i">
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

                            <td v-if="rental.returnDate == null">NULL</td>
                            <td v-else>{{ new Date(rental.returnDate).toDateString() }}</td>
                            <!-- <td>
                            <button class="btn btn-primary" @click="viewMore(rental.rentalId)">View</button>
                        </td> -->
                        </tr>
                    </tbody>
                </table>
            </div>


            <!-- Pagination Controls -->
            <div v-if="totalPage" class="pagination-controls d-flex align-items-center justify-content-center">
                <i :class="currentPage == 1 ? 'fa-sharp-duotone fa-solid fa-backward' : 'fa-solid fa-backward fa-fade'"
                    @click="previousPage()" :disabled="currentPage == 1" :style="{
                        '--fa-primary-opacity': currentPage == 1 ? 0.5 : 0.9,
                        'font-size': '1.5rem',
                        'cursor': currentPage == 1 ? 'not-allowed' : 'pointer'
                    }"></i>

                <span class="mx-3">Page {{ currentPage }} of {{ totalPage }}</span>

                <i :class="currentPage == totalPage ? 'fa-sharp-duotone fa-solid fa-forward' : 'fa-solid fa-forward fa-fade'"
                    @click="nextPage()" :disabled="currentPage == totalPage" :style="{
                        '--fa-primary-opacity': currentPage == 1 ? 0.5 : 0.9,
                        'font-size': '1.5rem',
                        'cursor': currentPage == totalPage ? 'not-allowed' : 'pointer'
                    }"></i>
            </div>
        </div>
    </main>
</template>

<script setup>
import { getAllRetals } from '@/script/RentalService';
import { computed, watch } from 'vue';
import { onMounted, ref } from 'vue';

const rentals = ref([]);
const filteredRentals = ref([]);
const searchValue = ref('');
const sortKey = ref('');
const sortOrder = ref('asc');
const totalPage = ref(0)
const currentPage = ref(1)

const search = () => {
    const query = searchValue.value.toLowerCase();
    filteredRentals.value = rentals.value.filter(rental => {
        const rentalIdMatch = rental.rentalId.toString().toLowerCase().includes(query);
        const statusMatch = rental.status.toLowerCase().includes(query);
        const movieMatch = rental.movie.title.toLowerCase().includes(query);
        const customerMatch = rental.customer.fullName.toLowerCase().includes(query)
        return rentalIdMatch || statusMatch || movieMatch || customerMatch;
    });
};

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
        else if (sortKey.value === 'customer') {
            return sortOrder.value === 'asc'
                ? a.customer.fullName.localeCompare(b.customer.fullName)
                : b.status.localeCompare(a.customer.fullName);
        }
        return 0;
    });
};

const paginatedRentals = computed(() => {
    const start = (currentPage.value - 1) * 5;
    const end = start + 5;
    return filteredRentals.value.slice(start, end);
});

const nextPage = () => {
    if (currentPage.value < totalPage.value) {
        currentPage.value++;
    }
}

const previousPage = async () => {
    if (currentPage.value !== 1) {
        currentPage.value--;
    }
}

watch(filteredRentals, (filtered) => {
    currentPage.value = 1;
    totalPage.value = Math.ceil(filtered.length / 5);
});

onMounted(() => {
    const fetching = async () => {
        try {
            const returnRentals = await getAllRetals();
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

.table-mapper {
    min-height: 430px;
}

.table {
    min-height: 390px;

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
