<script>
    export default{
        name: "ProductList",
        props: {
            products:Array,
        },
        data(){
            return{
                
                btnClick:() =>{
                    
                }
            }
        },
        mounted() {
    console.log('Received products:', this.products); // Verify that products are received correctly
  },
    }
</script>


<!-- <div v-for="product in products" :key="product.id" class="card" style="width: 18rem;">
    <img src="..." class="card-img-top" alt="...">
    <div class="card-body">
        <h5 class="card-title">{{ product.title }}</h5>
        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
    </div>
</div> -->

<template>
    <section>
        <div class="row gap-4 my-3">
            <div v-for="product in products" :key="product.id" class="card" style="width: 18rem;">
                <img :src= product.thumbnail class="card-img-top" alt="...">
                <div class="card-body">
        <h5 class="card-title">{{ product.title }}</h5>
        <p class="card-text">{{ product.description }}</p>
        <a href="#" class="btn btn-primary">Go somewhere</a>
    </div>
            </div>
        </div>
        
    </section>
</template>