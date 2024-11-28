<script>
import MainLayout from './Layout/MainLayout.vue';


export default {
    name: 'NotFound',
    components: {
        MainLayout
    }
}
</script>

<template>
    <MainLayout>
        <template #default>
            <div class="content">
                <div class="content-top">
                    <div class="breadcrumbs d-flex justify-content-center align-items-center gap-2">
                        <RouterLink to="/" class="home">Home</RouterLink>
                        <p>></p>
                        <p>Unauthorize</p>
                    </div>
                    <div class="pageNotFound d-flex justify-content-center">
                        <h1>Unauthorize</h1>
                    </div>
                </div>
                <div class="content-middle" style="width: 100%;">
                    <img src="../Images/image-lines-header.jpg" alt="" width="100%">
                </div>
                <div class="content-buttom" style="margin-top: 100px;">
                    <img src="../Images/authorized1.jpg" alt="">
                    <!-- <h2 class="my-3" style="font-weight: bolder;">Sorry we can't find that page!</h2> -->
                    <h3 class="my-3" style="font-weight: 500; color: gray;">The page you are looking is not Authorized.
                    </h3>

                </div>
            </div>
        </template>
    </MainLayout>
</template>

<style scoped>
.content {
    width: 100%;
    /* height: 100vh; */
}

.content-top {
    height: 80vh;
    background-position: left;
    background-image:
        linear-gradient(to right, rgba(27, 27, 27, 0), rgb(0, 0, 0)),
        url('https://wallpapercave.com/wp/wp7349515.jpg');
    background-size: cover;
    color: white;
    position: relative;
}

.content-top img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.breadcrumbs {
    position: absolute;
    left: 0;
    right: 0;
    top: 50%;
    margin-left: auto;
    margin-right: auto;
}

.breadcrumbs p {
    margin: 0;
    padding: 0;
}

.pageNotFound {
    position: absolute;
    left: 0;
    right: 0;
    top: 60%;
    margin-left: auto;
    margin-right: auto;
}

.pageNotFound h1 {
    font-weight: bolder;
}

.breadcrumbs p {
    font-weight: bolder;
}

.home {
    text-decoration: none;
    color: white;
    transition: 1s;
}

.home:hover {
    color: red;
    cursor: pointer;
}

.content-buttom {
    text-align: center;
}
</style>