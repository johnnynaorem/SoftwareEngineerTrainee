<script>
import { toast } from 'vue3-toastify';
import { registration } from '@/script/UserService';
import 'vue3-toastify/dist/index.css';

export default {
    name: 'RegisTration',
    data() {
        return {
            username: '',
            email: '',
            password: '',
            role: '',
            isLoading: false,

        }
    },
    methods: {
        async registration() {
            event.preventDefault();
            try {
                let convertRole = 1;
                if (this.role == 'Admin') {
                    convertRole = 0;
                }
                const response = await registration(this.username, this.email, this.password, convertRole)
                if (response.status == 200) {
                    this.isLoading = true;
                    toast.success("Registration Successfull");
                    setTimeout(() => {
                        this.isLoading = false;
                        this.$router.push('/login');
                    }, 2000);
                }
            } catch (error) {
                console.log(error)
            }
        }
    }
}
</script>

<template>
    <div class="container d-flex align-items-center">
        <div class="row my-center align-items-center position-relative">
            <div v-if="isLoading" class="position-absolute text-center">
                <img src="../../Images/loadingForRegistration.gif" alt="Loading">
            </div>
            <div class="col-md-6">
                <img src="../../Images/loginPageImage.svg" alt="Image" class="img-fluid">
            </div>
            <div class="col-md-6 contents">
                <div class="row justify-content-center">
                    <div class="col-md-8">
                        <div class="mb-4">
                            <h4>Welcome to MovieRent! 🎬</h4>
                            <p class="mb-4">
                                Sign Up to enjoy endless entertainment.</p>
                        </div>

                        <form>
                            <div class="form-group">
                                <label hidden for="username">Username</label>
                                <input type="text" class="form-control" id="username" v-model="username"
                                    placeholder="username" required minlength="5">

                            </div>
                            <div class="form-group">
                                <label hidden for="email">Email</label>
                                <input type="text" class="form-control" id="username" v-model="email"
                                    placeholder="email" required>

                            </div>
                            <div class="form-group">
                                <label hidden for="password">Password</label>
                                <input type="password" class="form-control" id="password" v-model="password"
                                    placeholder="create password" required>
                            </div>
                            <div class="form-group d-flex gap-3 align-items-center">
                                <label for="role">Role: </label>
                                <div class="flex-1 d-flex gap-4">
                                    <div class="d-flex gap-2 align-items-center">
                                        <input type="radio" id="admin" name="drone" value="Admin" checked
                                            v-model="role" />
                                        <label for="admin">Admin</label>
                                    </div>

                                    <div class="d-flex align-items-center gap-2">
                                        <input type="radio" id="user" name="drone" value="user" v-model="role" />
                                        <label for="user">User</label>
                                    </div>
                                </div>

                            </div>
                            <button type="submit" class="btn btn-block btn-primary w-100 p-3"
                                @click="registration()">Register</button>

                        </form>
                        <div><span>Sign in to MovieRent?</span> <router-link to="/login">Login now</router-link>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>

</template>
<style scoped>
.container {
    background-color: rgb(248 249 250);
    height: 100vh;
}

@media screen and (max-width: 672px) {
    .container {
        height: 150vh;
    }
}

.form-group {
    color: #edf2f5;
}

label {
    color: #b3b3b3;

}

input {
    background: #ced4da;
    height: 70px;
    font-size: 15px;
}

input::placeholder {
    color: rgb(100, 97, 97);
    opacity: .5;
}

p {
    color: #b3b3b3;
}
</style>
