<template>
    <main class="px-3 p-0">
        <div class="content-top">
            <div class="breadcrumbs d-flex justify-content-center align-items-center gap-2">
                <RouterLink to="/" class="home">Home</RouterLink>
                <p>></p>
                <RouterLink to="/dashboard" class="home">Dashboard</RouterLink>
                <p>></p>
                <p>Profile</p>
            </div>
            <div class="big-text d-flex justify-content-center">
                <h1 v-if="customer">{{ customer.fullName }}</h1>
            </div>
        </div>
        <div class="content-middle" style="width: 100%;">
            <img src="../../Images/image-lines-header.jpg" alt="" width="100%">
        </div>
        <div class="container-fluid justify-content-center">
            <div class="content d-flex gap-2 flex-column flex-lg-row  justify-content-center align-items-center">
                <div class="content-mapper">
                    <button class="edit-button" data-bs-toggle="modal" data-bs-target="#profileEditModal"
                        @click="setForUpdating()">Edit
                        Profile</button>
                    <div class="image-container">
                        <img src='../../Images/banner3.jpg' alt="Profile Image">
                    </div>
                    <div v-bind="customer" class="profile-detail">
                        <p class="customerName">{{ customer.fullName }}</p>
                        <p class="customerAddress">{{ customer.address }}</p>
                        <p class="customerEmail">{{ customer.email }}</p>
                        <p class="customerPhone">{{ customer.phoneNumber }}</p>
                    </div>
                </div>
                <!-- <div class="content-mapper" style="min-height: 400px;">
                    <div class="d-flex gap-2 justify-content-center flex-wrap">
                        <div class="total-reservation" style="height: 210px; width: 210px; background: white;">
                            <h4>Reservation</h4>
                            <p>10</p>
                        </div>
                        <div class="total-rent" style="height: 210px; width: 210px; background: aqua;">
                            <h4>Rent</h4>
                            <p>10</p>
                        </div>
                    </div>
                </div> -->
            </div>
        </div>
        <!-- Update Profile  Modal -->
        <div class="modal fade" id="profileEditModal" tabindex="-1" aria-labelledby="profileEditModalLabel"
            aria-hidden="true">
            <div class="modal-mapper modal-dialog modal-dialog-centered modal-dialog-scrollable">
                <div class="modal-content modal-content-mapper">
                    <div class="modal-header">
                        <div>
                            <h4 class="modal-title" id="profileEditModalLabel">Update Profile!</h4>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">X</button>
                    </div>
                    <div class="modal-body">
                        <!-- Conditional Success Message -->
                        <!-- <div v-if="isMovieAddSuccess">
                            <p class="text-white">Profile is Sucessfully Updated.</p>
                        </div> -->
                        <form v-on:submit="updateProfileMethod">
                            <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                <div>
                                    <label for="name">Full Name</label>
                                    <input type="text" class="form-control" placeholder="Full Name" required
                                        v-model="customerUpdate.fullName">
                                </div>
                                <div>
                                    <label for="email">Email</label>
                                    <input type="text" class="form-control" placeholder="Email" required
                                        :value="customerUpdate.email" disabled>
                                </div>
                                <div>
                                    <label for="phone">Phone</label>
                                    <input type="text" class="form-control" required placeholder="Enter Phone Number"
                                        v-model="customerUpdate.phoneNumber">
                                </div>
                            </div>
                            <div class="input-group mb-3 d-flex gap-2 input-mapper">
                                <div>
                                    <p class="m-0">Address</p>
                                    <textarea cols="40" placeholder="Address" required
                                        v-model="customerUpdate.address" />
                                </div>
                            </div>
                            <button type="submit" class="btn">Send</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script setup>
import { getCustomer, getUser, profileUpdate } from '@/script/UserService';
import { jwtDecode } from 'jwt-decode';
import { onMounted, ref } from 'vue';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const customer = ref({
    customerId: null,
    fullName: '',
    email: '',
    phoneNumber: '',
    address: '',
    profileImage: ''
});

const customerUpdate = ref({
    fullName: '',
    email: '',
    phoneNumber: '',
    address: '',
    profileImage: ''
});

const setForUpdating = () => {
    customerUpdate.value.fullName = customer.value.fullName;
    customerUpdate.value.address = customer.value.address;
    customerUpdate.value.phoneNumber = customer.value.phoneNumber;
    customerUpdate.value.email = customer.value.email;
}

const updateProfileMethod = async (event) => {
    event.preventDefault();
    toast.info("Updating");
    const response = await profileUpdate(customerUpdate.value.fullName, customerUpdate.value.phoneNumber, customerUpdate.value.address, customer.value.customerId);
    if (response.status == 200) {
        toast.success("Profile Updated");
        fetching();
    }
}

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

onMounted(() => {
    fetching();
});
</script>

<style lang="scss" scoped>
.content-top {
    width: 100%;
    height: 30vh;
    background-position: left;
    background-image:
        linear-gradient(to right, rgba(27, 27, 27, 0), rgb(0, 0, 0)),
        url('https://wallpapercave.com/wp/wp7349515.jpg');
    background-size: cover;
    color: white;
    position: relative;
}

.content-top img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.breadcrumbs {
    position: absolute;
    left: 0;
    right: 0;
    top: 30%;
    margin-left: auto;
    margin-right: auto;
}

.breadcrumbs p {
    margin: 0;
    padding: 0;
}

.home {
    text-decoration: none;
    color: white;
    transition: 0.4s ease-out;
}

.home:hover {
    color: orangered;
}

.big-text {
    position: absolute;
    left: 0;
    right: 0;
    top: 40%;
    margin-left: auto;
    margin-right: auto;
}

.big-text h1 {
    font-weight: bolder;
}

.banner,
.image-lines-header {
    width: 100%;
}

.banner-image,
.line-image {
    width: 100%;
    object-fit: contain;
}

/* Banner style end */
.container-fluid {
    background: black;

    .content-mapper {
        margin-bottom: 50px;
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-top: 20px;
        width: 40%;
        // height: 400px;
        border-radius: 20px;
        background: rgb(88, 86, 86);
        color: white;
        padding: 20px;

        .profile-detail {
            display: flex;
            flex-direction: column;
            align-items: center;

            p {
                margin: 0;
                padding: 0;
            }

            .customerName {
                font-size: 2rem;
                font-weight: bold;
            }

            .customerAddress {
                font-size: 15px;
                // font-weight: bold;
                margin-bottom: 40px;
            }

            .customerEmail,
            .customerPhone {
                font-size: 1rem;
            }
        }

    }

    .edit-button {
        align-self: end;
        outline: none;
        border: none;
        border-radius: 25px;
        padding: 10px 15px;
        background-color: orangered;
        color: white;
        font-size: medium;
        transition: 0.5s ease-out;

        &:hover {
            background-color: black;
            color: white;
        }
    }

    .image-container {
        img {
            width: 150px;
            height: 150px;
            object-fit: cover;
            object-position: left;
            border-radius: 50%;
        }
    }
}

@media (max-width: 480px) {
    .container-fluid {

        .content-mapper {
            width: 90%;
            margin-bottom: 20px;
            margin-left: 35px;

            .profile-detail {

                .customerName {
                    font-size: 1.4rem;
                    font-weight: bold;
                }
            }
        }
    }
}

@media (min-width: 481px) and (max-width: 987px) {
    .container-fluid {

        .content-mapper {
            width: 70%;
            margin-bottom: 20px;
        }
    }
}


.modal-mapper {
    max-width: 60%;
    color: var(--light);

    .modal-content-mapper {
        background: #191919;
        padding: 20px;
        border-radius: 20px;
    }

    button {
        border: none;
        outline: none;
        padding: 10px 30px;
        background: black;
        border-radius: 20px;
        color: var(--light);
        transition: 0.2s ease-out;

        &:hover {
            background: rgb(134, 130, 130);
        }
    }

    .input-mapper {
        input {
            background: transparent;
            color: var(--light);

            &::placeholder {
                color: var(--light);
            }
        }

        textarea {
            background: transparent;
            color: var(--light);

            &::placeholder {
                color: var(--light);
            }
        }

    }

    .select-mapper {
        border: 2px solid white;
        border-radius: 10px;
        padding: 20px;

        select {
            cursor: pointer;
            border: none;
            border: 2px solid rgb(20, 1, 1);
            border-radius: 10px;
            padding: 10px;
            outline: none;
            background-color: transparent;
            color: white;

            option {
                background: red;
            }
        }
    }
}
</style>