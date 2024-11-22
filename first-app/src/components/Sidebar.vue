<template>
    <div class="d-flex justify-content-center">
        <div class="sidebar" :class="{ 'open': isOpen }">
            <button @click="toggleSidebar">Toggle Sidebar</button>
            <!-- Sidebar content goes here -->
            <ul>
                <li>Link 1</li>
                <li>Link 2</li>
                <li>Link 3</li>
            </ul>
        </div>
    </div>

</template>
<script>
export default {
    name: 'SideBar',
    data() {
        return {
            isOpen: false,
        };
    },
    methods: {
        toggleSidebar() {
            this.isOpen = !this.isOpen;
        },
    },
};
</script>
<style scoped>
.sidebar {
    width: 200px;
    height: 100%;
    position: fixed;
    left: 100px;
    top: 0;
    background-color: #e00000;
    transition: all 0.3s ease-in-out;
    transform: translateX(-200px);
    /* Initially hidden */
}

.sidebar.open {
    transform: translateX(0);
    /* Visible when open */
}
</style>