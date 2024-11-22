<template>
    <main>
        <h2>Profile</h2>
        <div class="container">
            <button class="edit-button">Edit Profile</button>
            <div class="image-container">
                <img src='../../Images/banner3.jpg' alt="Profile Image">
            </div>
            <div class="profile-detail">
                <form>
                    <div class="fullName d-flex align-items-center">
                        <label for="">Full Name: </label>
                        <input type="text" v-model="customer.fullName" placeholder="Full Name">
                    </div>
                    <div class="email d-flex align-items-center">
                        <label for="">Email: </label>
                        <input type="text" v-model="customer.email" placeholder="Email">
                    </div>
                    <div class="contact d-flex align-items-center">
                        <label for="">Contact: </label>
                        <input v-model="customer.phoneNumber" placeholder="Contact">
                    </div>
                    <div class="address d-flex align-items-center">
                        <label for="">Address: </label>
                        <input type="text" v-model="customer.address" placeholder="Address">
                    </div>
                </form>
            </div>
        </div>
    </main>
</template>

<script setup>
import { getCustomer, getUser } from '@/script/UserService';
import { jwtDecode } from 'jwt-decode';
import { onMounted, ref } from 'vue';

const customer = ref({
    fullName: '',
    email: '',
    phoneNumber: '',
    address: '',
    profileImage: ''
});

onMounted(() => {
    const fetching = async () => {
        try {
            const token = sessionStorage.getItem('token');
            const decode = jwtDecode(token);
            const user = await getUser(decode.Email);
            const returnCustomer = await getCustomer(user.data.userId);

            if (returnCustomer.status === 200) {
                customer.value = returnCustomer.data;
            }
        } catch (error) {
            console.error('Error fetching customer data:', error);
        }
    };

    fetching();
});
</script>

<style lang="scss" scoped>
.container {
    height: 100vh;
    // background: black;

    .edit-button {
        outline: none;
        border: none;
        padding: 10px 20px;
        background-color: aliceblue;
        transition: 0.5s ease-out;

        &:hover {
            border-radius: 8px;
            background-color: rgb(229, 5, 236);
            color: white;
        }
    }

    .image-container {
        display: flex;
        justify-content: center;
        align-items: center;
        margin-top: 1rem;
        width: 100%;
        height: 40%;

        img {
            width: 250px;
            height: 250px;
            object-fit: cover;
            object-position: left;
            border-radius: 50%;
        }
    }

    .profile-detail {
        height: 60%;
        // background: yellow;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;

        form {
            // width: 80%;
            // display: flex;
            // flex-direction: row;

            input {
                padding: 10px;
                font-size: 16px;
                border-radius: 5px;
                border: 1px solid #ccc;
            }
        }
    }
}
</style>