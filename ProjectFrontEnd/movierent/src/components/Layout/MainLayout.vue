<template>
    <div>
        <Navbar />
        <main>
            <slot></slot>
        </main>
        <Footer />
    </div>
</template>

<script>
import Footer from '../Footer.vue';
import Navbar from '../Navbar.vue';


export default {
    name: 'MainLayout',
    components: {
        Navbar,
        Footer,
    },
};
</script>
