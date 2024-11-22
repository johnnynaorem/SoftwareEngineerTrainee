<template>
    <div class="appOne">
        <AppSidebar />
        <router-view />
    </div>
</template>

<script setup>
import AppSidebar from './AppSidebar.vue';
</script>

<style lang="scss">
.appOne {
    display: flex;

    main {
        flex: 1 1 0;
        padding: 2rem;

        @media (max-width: 768px) {
            padding-left: 6rem;
        }
    }
}
</style>