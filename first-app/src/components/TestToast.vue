<template>
    <div>
        <!-- Button to trigger the toast -->
        <button @click="triggerToast">Show Toast</button>

        <!-- Toast component with dynamic properties -->
        <Toast :show="showToast" type="success" content="This is a toast from Another Component!" />
    </div>
</template>

<script>
export default {
    name: 'AnotherComponent',
    data() {
        return {
            showToast: false, // Control the visibility of the toast
        };
    },
    methods: {
        triggerToast() {
            this.showToast = true; // Trigger the toast visibility
        }
    }
};
</script>

<style scoped>
/* Add your styles here */
</style>