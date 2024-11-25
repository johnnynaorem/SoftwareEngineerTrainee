<template>
    <div class="appOne">
        <AdminSideBar />
        <router-view />
    </div>
</template>
<script>
import AdminSideBar from './AdminSideBar.vue';


export default {
    name: "AdminDashboard",
    components: {
        AdminSideBar
    }
}
</script>
<style lang="scss"></style>