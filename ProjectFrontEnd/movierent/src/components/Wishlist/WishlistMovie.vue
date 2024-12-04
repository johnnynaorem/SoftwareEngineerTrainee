<template>
    <main>
        <div class="container mt-3" style="flex: 1;">
            <div class="row p-md-3">
                <div class="cardContainer mb-3 col-12  col-sm-12 col-md-6 col-lg-4" v-for="movie in wishlists"
                    :key="movie.movieId" style="justify-items: center;">
                    <div class="card-div">
                        <img :src=movie.coverImage class="card-img" alt="Movie Image">
                        <div class="card-body">
                            <p class="card-text m-0">{{ movie.genre }} / 180 Mins</p>
                            <p class="card-title">{{ movie.title }}</p>
                            <div class="btn-container">
                                <button class="btn btn-primary" @click="toMoviePage(movie.movieId)">Rent</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script setup>
import { getWishlistsCustomerId } from '@/script/MovieService';
import { jwtDecode } from 'jwt-decode';
import { onMounted } from 'vue';

const wishlists = ref([])


const fetching = async () => {
    try {
        const token = sessionStorage.getItem('token');
        const decode = jwtDecode(token);
        const user = await getUser(decode.Email);
        const { data } = await getCustomer(user.data.userId);
        const returnWishlist = await getWishlistsCustomerId(data.customerId)
        if (returnWishlist.status === 200) {
            wishlists.value = returnWishlist.data;
        }
    } catch (error) {
        console.error('Error fetching customer data:', error);
    }
};

onMounted(() => {
    fetching();
});
</script>
