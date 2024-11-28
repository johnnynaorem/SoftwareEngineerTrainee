<template>
    <main>
        <div class="d-flex justify-content-between">
            <h2>Movies</h2>
            <button type="button" class="add-new-movie-btn" data-bs-toggle="modal"
                data-bs-target="#addNewMovieModal">Add New Movie</button>
        </div>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" @input="search" v-model="searchValue" type="search" placeholder="Search" />
            </div>
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col">Sl</th>
                        <th scope="col" @click="sortBy('movie')">
                            Movie
                            <span v-if="sortKey === 'movie'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                        </th>
                        <th scope="col" @click="sortBy('genre')">
                            Genre
                            <span v-if="sortKey === 'genre'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                        </th>
                        <th scope="col" @click="sortBy('rentalPrice')">
                            RentalPrice
                            <span v-if="sortKey === 'rentalPrice'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                        </th>
                        <th scope="col" @click="sortBy('availableCopies')">
                            Available Copy
                            <span v-if="sortKey === 'availableCopies'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                        </th>
                        <th scope="col" @click="sortBy('releaseDate')">
                            Release Date
                            <span v-if="sortKey === 'releaseDate'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                        </th>
                        <th scope="col">
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(movie, i) in movies" :key="i">
                        <th>{{ i + 1 }}</th>
                        <td>
                            <div class="d-flex table-movie-mapper">
                                <img :src=movie.coverImage alt="" width="30px">
                                <p>{{ movie.title }}</p>
                            </div>
                        </td>
                        <td>{{ movie.genre }}</td>
                        <td>{{ movie.rental_Price }}</td>
                        <td>{{ movie.availableCopies }}</td>
                        <td>{{ new Date(movie.releaseDate).toDateString() }}</td>
                        <td>
                            <button type="button" class="btn btn-primary reserve-btn" data-bs-toggle="modal"
                                data-bs-target="#reservationUpdateModal"
                                style="border: none; outline: none; background-color: transparent;">
                                <span class="material-icons" style="color:black">edit</span>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <!-- Add New Movie Modal -->
            <div class="modal fade" id="addNewMovieModal" tabindex="-1" aria-labelledby="addNewMovieModalLabel"
                aria-hidden="true">
                <div class="modal-mapper modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content modal-content-mapper">
                        <div class="modal-header">
                            <div>
                                <h4 class="modal-title" id="addNewMovieModalLabel">Add new Movie!</h4>
                                <p>To add a new movie, please fill in the required details and select the appropriate
                                    status. This will help us accurately track the movie in the system and manage its
                                    progress.</p>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Conditional Success Message -->
                            <div v-if="isMovieAddSuccess">
                                <p class="text-success">Movie is Sucessfully Added.</p>
                            </div>
                            <form v-else v-on:submit="addNewMovieMethod">
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div>
                                        <label for="movie">Movie Name</label>
                                        <input type="text" class="form-control" placeholder="Title" required
                                            v-model="movieToBeAdd.title">
                                    </div>
                                    <div>
                                        <label for="movie">Genre</label>
                                        <input type="text" class="form-control" placeholder="Genre" required
                                            v-model="movieToBeAdd.genre">
                                    </div>
                                    <div>
                                        <label for="movie">Rental Price</label>
                                        <input type="number" class="form-control" required placeholder="Enter Price"
                                            v-model="movieToBeAdd.rental_Price">
                                    </div>
                                </div>
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div>
                                        <label for="movie">Movie Cover Image</label>
                                        <input type="text" class="form-control" placeholder="CoverImage" required
                                            v-model="movieToBeAdd.coverImage">
                                    </div>
                                    <div>
                                        <label for="movie">Rating</label>
                                        <input type="number" class="form-control" required placeholder="Enter Rating"
                                            v-model="movieToBeAdd.rating">
                                    </div>
                                    <div>
                                        <label for="movie">Release Date</label>
                                        <input type="datetime-local" class="form-control" required
                                            v-model="movieToBeAdd.releaseDate">
                                    </div>
                                </div>
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div>
                                        <label for="movie">Available Copies</label>
                                        <input type="number" class="form-control" required placeholder="No of Copies"
                                            v-model="movieToBeAdd.availableCopies">
                                    </div>

                                </div>
                                <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                    <div>
                                        <label for="movie">Description</label>
                                        <textarea cols="80" rows="4" class="form-control" required
                                            placeholder="......about movie" v-model="movieToBeAdd.description" />
                                    </div>

                                </div>
                                <button type="submit" class="btn">Send</button>
                            </form>
                            <!-- <div v-else class="select-mapper">
                                <select v-model="updateStatus" name="" id="">
                                    <option value="" disabled selected>{{ currentReservationStatus }}</option>
                                    <option value="active">Active</option>
                                    <option value="completed">Completed</option>
                                    <option value="cancelled">Cancelled</option>
                                    <option value="expired">Expired</option>
                                    <option value="notAvailable">Not Available</option>
                                </select>
                                <button type="submit" class="btn mx-3" @click="updateStatusMethod()">Send</button>
                            </div> -->

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script setup>
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { addMovie, getAllMovie } from '@/script/MovieService';
import { onMounted, ref } from 'vue';

const movies = ref([]);
const searchValue = ref("");
const sortKey = ref("");
const sortOrder = ref("asc");
const movieToBeAdd = ref({
    title: "",
    genre: "",
    rental_Price: 0,
    coverImage: "",
    rating: 0,
    description: "",
    availableCopies: 0,
    releaseDate: ""
})
const isMovieAddSuccess = ref(false);

// const filteredMovie = computed(() => {
//     const query = searchValue.value.toLowerCase();
//     const filtered = movies.value.filter(movie => {
//         return (
//             movie.amount.toString().toLowerCase().includes(query) ||
//             movie.rentalId.toString().toLowerCase().includes(query) ||
//             movie.paymentDate.toLowerCase().includes(query) ||
//             movie.paymentType.toLowerCase().includes(query) ||
//             movie.customer.fullName.toLowerCase().includes(query)
//         );
//     });
//     return sortMovies(filtered);
// });

// const sortMovies = (paymentsToSort) => {
//     if (!sortKey.value) return paymentsToSort;

//     return paymentsToSort.sort((a, b) => {
//         const aValue = a[sortKey.value];
//         const bValue = b[sortKey.value];

//         if (sortKey.value === 'paymentDate') {
//             const dateA = new Date(aValue).getTime();
//             const dateB = new Date(bValue).getTime();
//             return sortOrder.value === 'asc' ? dateA - dateB : dateB - dateA;
//         }

//         if (sortKey.value == 'customerName') {
//             return sortOrder.value === 'asc' ? a.customer.fullName.localeCompare(b.customer.fullName) : b.customer.fullName.localeCompare(a.customer.fullName)
//         }

//         if (typeof aValue === 'string') {
//             const stringA = aValue.toLowerCase();
//             const stringB = bValue.toLowerCase();
//             return sortOrder.value === 'asc' ? stringA.localeCompare(stringB) : stringB.localeCompare(stringA);
//         }

//         return sortOrder.value === 'asc' ? aValue - bValue : bValue - aValue;
//     });
// };

// const sortBy = (key) => {
//     if (sortKey.value === key) {
//         sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
//     } else {
//         sortKey.value = key;
//         sortOrder.value = 'asc';
//     }
// };

const addNewMovieMethod = async (event) => {
    event.preventDefault();
    console.log(movieToBeAdd.value)
    const returnMovie = await addMovie(movieToBeAdd.value.title,
        movieToBeAdd.value.genre,
        movieToBeAdd.value.rental_Price,
        movieToBeAdd.value.coverImage,
        movieToBeAdd.value.rating,
        movieToBeAdd.value.description,
        movieToBeAdd.value.availableCopies,
        movieToBeAdd.value.releaseDate);
    if (returnMovie.status == 200) {
        toast.success("Movie is added successfully!!");
    }
}

onMounted(() => {
    const fetching = async () => {
        try {
            // const returnMovie = await getMovieWithPagination(1, 5);
            const returnMovie = await getAllMovie();
            if (returnMovie.status === 200) {
                movies.value = returnMovie.data;
            }
        } catch (error) {
            console.error('Error fetching movie data:', error);
        }
    };

    fetching();
});
</script>

<style lang="scss" scoped>
.add-new-movie-btn {
    padding: 10px 20px;
    outline: none;
    border: none;
    border-radius: 20px;
    background: rgba(0, 0, 0, 0.705);
    color: white;
    transition: 0.4s ease-out;

    &:hover {
        background: rgba(248, 244, 244, 0.705);
        color: rgb(26, 25, 25);
    }
}

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

.table {
    th {
        cursor: pointer;
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

        textarea {
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
