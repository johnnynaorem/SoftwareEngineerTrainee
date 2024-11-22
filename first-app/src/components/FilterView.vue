<script setup>
import { computed, defineProps, ref } from 'vue'
import Search from './Search.vue';

const props = defineProps({
    items: {
        type: Array,
        required: true
    }
})

const searchFilter = ref('')
const isSort = ref(false)
const sortField = ref('')

const filteritems = computed(() => {
    let items = [...props.items]

    if (searchFilter.value !== '') {
        items = items.filter(item => item.first.includes(searchFilter.value) || item.last.includes(searchFilter.value));
    }

    if (isSort.value) {
        items = items.sort((a, b) => b.id - a.id);
    }

    return items;
})

const handleSearch = (search) => {
    searchFilter.value = search;
}

const sort = (para) => {
    console.log(para)
    sortField.value = para;
    isSort.value = !isSort.value
}
</script>

<template>
    <Search @search="handleSearch" />
    <div class="tableContainer container">
        <table class="table table">
            <thead>
                <tr>
                    <th scope=" col" @click="sort('id')">Id</th>
                    <th scope="col" @click="sort('first')">First</th>
                    <th scope="col" @click="sort('last')">Last</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in filteritems" :key="item.id">
                    <th scope="row">{{ item.id }}</th>
                    <th scope="row">{{ item.first }}</th>
                    <td colspan="2">{{ item.last }}</td>
                </tr>
            </tbody>
        </table>
    </div>

</template>

<style>
.tableContainer {
    padding: 20px;
    background-color: aquamarine;
}
</style>
