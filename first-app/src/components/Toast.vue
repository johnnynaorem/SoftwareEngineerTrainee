<template>
    <b-toast v-if="isVisible" :variant="type" auto-hide-delay="3000" @hidden="onToastHidden"
        style="position: fixed; top: 0; right: 0; padding: 1rem; z-index: 1000;">
        {{ content }}
    </b-toast>
</template>

<script>
export default {
    name: 'ToastComponent',
    props: {
        show: {
            type: Boolean,
            required: true
        },
        type: {
            type: String,
            default: 'success'
        },
        content: {
            type: String,
            default: 'This is a default toast message.'
        }
    },
    data() {
        return {
            isVisible: this.show
        };
    },
    watch: {
        show(newVal) {
            this.isVisible = newVal;
        }
    },
    methods: {
        onToastHidden() {
            this.$emit('update:show', false); // Emit the updated show value to parent
        }
    }
};
</script>

<style scoped>
/* Style the toast message (optional) */
</style>