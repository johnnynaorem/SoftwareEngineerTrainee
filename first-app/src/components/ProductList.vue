<script>
export default {
    name: "ProductList",
    props: {
        products: Array,
        isLoading:Boolean
    },
    data() {
        return {

            btnClick: () => {

            }
        }
    },
    mounted() {
        console.log('Received products:', this.products); // Verify that products are received correctly
    },
}
</script>
<template>
    <section>
        <div class="row gap-4 my-3 mx-auto justify-content-center" style="cursor: pointer;">
            <div v-for="product in products" :key="product.id" class="card" style="width: 18rem;">
                <img :src=product.thumbnail class="card-img-top" alt="..."  v-if="!isLoading">
                <img src="https://static.vecteezy.com/system/resources/thumbnails/008/202/382/original/loading-circle-icon-loading-gif-loading-screen-gif-loading-spinner-gif-loading-animation-loading-free-video.jpg" alt="Default Image" v-else />
                <p ><span class="product-cate">{{ product.category }}</span></p>
                <div class="card-body">
                    <h5 class="card-title">{{ product.title }}</h5>
                    <p class="card-text">{{ product.description.substring(0, 50) }}....</p>
                    <a class="btn btn-primary">Buy at {{ product.price }}$</a>
                </div>
            </div>
        </div>

    </section>
</template>

<style scoped>
img {
    transition: 2s;
}

img:hover {
    /* width: 300px;
    height: 300px; */
    /* border: 20px solid rgb(9, 214, 250); */
    /* animation: flip 2s infinite linear; */
    transform: scaleX(-1);
}

.product-cate {
    color: white;
    background-color: blue;
    border-radius: 18px;
    font-size: 13px;
    font-weight: bold;
    cursor: pointer;
    padding: 6px;
}
</style>