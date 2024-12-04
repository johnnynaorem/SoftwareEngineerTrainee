<template>
    <main>
        <h2>Payment</h2>
        <div class="container">
            <div class="search my-4">
                <input class="search-input" @input="search" v-model="searchValue" type="search"
                    placeholder="Search (YYYY-MM-DD)" />
            </div>
            <div class="table-mapper">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th scope="col">Sl</th>
                            <th scope="col" @click="sortBy('customerName')">
                                Paid By
                                <span v-if="sortKey === 'customerName'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                            </th>
                            <th scope="col" @click="sortBy('rentalId')">
                                Rental Id
                                <span v-if="sortKey === 'rentalId'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                            </th>
                            <th scope="col" @click="sortBy('amount')">
                                Total Amount
                                <span v-if="sortKey === 'amount'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                            </th>
                            <th scope="col" @click="sortBy('paymentType')">
                                Payment Method
                                <span v-if="sortKey === 'paymentType'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                            </th>
                            <th scope="col" @click="sortBy('paymentDate')">
                                Payment Date
                                <span v-if="sortKey === 'paymentDate'">{{ sortOrder === 'asc' ? '↑' : '↓' }}</span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(payment, i) in paginatedPayments" :key="i">
                            <th>{{ i + 1 }}</th>
                            <td>{{ payment.customer.fullName }}</td>
                            <td>{{ payment.rentalId }}</td>
                            <td>{{ payment.amount }}</td>
                            <td>{{ payment.paymentType }}</td>
                            <td>{{ new Date(payment.paymentDate).toDateString() }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- Pagination Controls -->
            <div v-if="totalPage" class="pagination-controls d-flex align-items-center justify-content-center">
                <i :class="currentPage == 1 ? 'fa-sharp-duotone fa-solid fa-backward' : 'fa-solid fa-backward fa-fade'"
                    @click="previousPage()" :disabled="currentPage == 1" :style="{
                        '--fa-primary-opacity': currentPage == 1 ? 0.5 : 0.9,
                        'font-size': '1.5rem',
                        'cursor': currentPage == 1 ? 'not-allowed' : 'pointer'
                    }"></i>

                <span class="mx-3">Page {{ currentPage }} of {{ totalPage }}</span>

                <i :class="currentPage == totalPage ? 'fa-sharp-duotone fa-solid fa-forward' : 'fa-solid fa-forward fa-fade'"
                    @click="nextPage()" :disabled="currentPage == totalPage" :style="{
                        '--fa-primary-opacity': currentPage == 1 ? 0.5 : 0.9,
                        'font-size': '1.5rem',
                        'cursor': currentPage == totalPage ? 'not-allowed' : 'pointer'
                    }"></i>
            </div>

        </div>
    </main>
</template>

<script setup>
import { getAllPayments } from '@/script/PaymentService';
import { onMounted, ref, computed, watch } from 'vue';

const payments = ref([]);
const searchValue = ref("");
const sortKey = ref("");
const sortOrder = ref("asc");
const totalPage = ref(0)
const currentPage = ref(1)

const filteredPayments = computed(() => {
    const query = searchValue.value.toLowerCase();
    const filtered = payments.value.filter(payment => {
        return (
            payment.amount.toString().toLowerCase().includes(query) ||
            payment.rentalId.toString().toLowerCase().includes(query) ||
            payment.paymentDate.toLowerCase().includes(query) ||
            payment.paymentType.toLowerCase().includes(query) ||
            payment.customer.fullName.toLowerCase().includes(query)
        );
    });
    return filtered;
});

const sortPayments = (paymentsToSort) => {
    if (!sortKey.value) return paymentsToSort;

    return paymentsToSort.sort((a, b) => {
        const aValue = a[sortKey.value];
        const bValue = b[sortKey.value];

        if (sortKey.value === 'paymentDate') {
            const dateA = new Date(aValue).getTime();
            const dateB = new Date(bValue).getTime();
            return sortOrder.value === 'asc' ? dateA - dateB : dateB - dateA;
        }

        if (sortKey.value == 'customerName') {
            return sortOrder.value === 'asc' ? a.customer.fullName.localeCompare(b.customer.fullName) : b.customer.fullName.localeCompare(a.customer.fullName)
        }

        if (typeof aValue === 'string') {
            const stringA = aValue.toLowerCase();
            const stringB = bValue.toLowerCase();
            return sortOrder.value === 'asc' ? stringA.localeCompare(stringB) : stringB.localeCompare(stringA);
        }

        return sortOrder.value === 'asc' ? aValue - bValue : bValue - aValue;
    });
};

const sortBy = (key) => {
    if (sortKey.value === key) {
        sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
    } else {
        sortKey.value = key;
        sortOrder.value = 'asc';
    }
    sortPayments(payments.value)
};

const paginatedPayments = computed(() => {
    const start = (currentPage.value - 1) * 5;
    const end = start + 5;
    return filteredPayments.value.slice(start, end);
})

const nextPage = () => {
    if (currentPage.value < totalPage.value) {
        currentPage.value++;
    }
}

const previousPage = async () => {
    if (currentPage.value !== 1) {
        currentPage.value--;
    }
}

watch(filteredPayments, (filtered) => {
    currentPage.value = 1;
    totalPage.value = Math.ceil(filtered.length / 5);  // Recalculate total pages based on filtered data
});

onMounted(() => {
    const fetching = async () => {
        try {
            const returnPayment = await getAllPayments();
            if (returnPayment.status === 200) {
                payments.value = returnPayment.data;
                totalPage.value = Math.ceil(returnPayment.data.length / 5);
            }
        } catch (error) {
            console.error('Error fetching payment data:', error);
        }
    };

    fetching();
});
</script>

<style lang="scss" scoped>
.search {
    input {
        padding: 10px 30px;
        border: none;
        outline: none;
        border-radius: 10px;
        background: var(--light);
    }
}

.table-mapper {
    min-height: 430px;
}

.table {
    th {
        cursor: pointer;
    }
}
</style>
