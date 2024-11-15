<template>
    <Navbar />
    <div>
        <p>{{ JSON.stringify(reservations) }}</p>
    </div>
</template>

<script>
import { getReservationByCustomer } from '@/script/ReservationService';
import Navbar from './Navbar.vue';

export default {
    name: 'ReservationMovie',
    components: {
        Navbar
    },
    data() {
        return {
            reservations: [],
        }
    },
    mounted() {
        const fetching = async () => {
            const data = await getReservationByCustomer();
            if (data.status == 200) {
                this.reservations = data.data;
            }
        }
        fetching();
    }
}
</script>