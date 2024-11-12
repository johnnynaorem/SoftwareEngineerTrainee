
<script>
import ProductList from './ProductList.vue';

    export default {
    name: 'NavBar',
    components: {
        ProductList,
    },
    data(){
            return{
                products: [],
                Categories: [],
                updateProduct:(slug) =>{ 
                    fetch(`https://dummyjson.com/products/category/${slug}`)
                    .then(res => res.json())
                    .then(data => {
                        this.products = data.products
                    });
                }
            }
        },
        mounted(){
            fetch('https://dummyjson.com/products/categories')
                .then(res => res.json())
                .then(data => {
                    this.Categories = data;
                    console.log(data)
                });

                fetch('https://dummyjson.com/products')
                    .then(res => res.json())
                    .then(data => this.products = data.products);
        }
  }
</script>
<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
  <div class="container-fluid">
    <a class="navbar-brand" href="#">Navbar</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavDropdown">
      <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="#">Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">Features</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">Pricing</a>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Catalog
          </a>
          <ul class="dropdown-menu">
            <li v-for="category in Categories" :key="category.slug"><a class="dropdown-item" @click="updateProduct(category.slug)">{{ category.name }}</a></li>
          </ul>
        </li>
      </ul>
    </div>
  </div>
</nav>
<ProductList :products="products" />
</template>