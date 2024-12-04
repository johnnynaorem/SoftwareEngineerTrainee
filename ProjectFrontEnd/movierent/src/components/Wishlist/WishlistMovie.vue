<template>
    <main>
        <h2>Wishlists</h2>
        <div v-if="wishlists.length > 0" class="container mt-3" style="flex: 1;">
            <div class="row p-md-3">
                <div class="cardContainer mb-3 col-12  col-sm-12 col-md-6 col-lg-4" v-for="w in wishlists"
                    :key="w.movieId" style="justify-items: center;">
                    <div class="card-div">
                        <img :src=w.movie.coverImage class="card-img" alt="Movie Image">
                        <div class="card-body">
                            <p class="card-text m-0">{{ w.movie.genre }} / 180 Mins</p>
                            <p class="card-title">{{ w.movie.title }}</p>
                            <div class="btn-container">
                                <button class="btn btn-primary" @click="toMoviePage(w.movie.movieId)">Rent</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-else class="container mt-3">
            No Wishlists
        </div>
    </main>
</template>

<script setup>
import { getWishlistsCustomerId } from '@/script/MovieService';
import router from '@/script/Route';
import { getCustomer, getUser } from '@/script/UserService';
import { jwtDecode } from 'jwt-decode';
import { onMounted, ref } from 'vue';

const wishlists = ref([])

const toMoviePage = (id) => {
    router.push(`/movie/${id}`)
}

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

<style scoped>
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

.btn-container button {
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
</style>
