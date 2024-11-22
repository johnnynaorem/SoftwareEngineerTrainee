<script>
import { login } from '@/script/UserService';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export default {
    name: 'LogIn',
    data() {
        return {
            email: '',
            password: '',
            isLogin: false,
            login: async () => {
                event.preventDefault();
                const data = await login(this.email, this.password);
                if (data.status == 200) {
                    this.isLoading = true;
                    this.email = '';
                    this.password = '';
                    sessionStorage.setItem("token", data.data.token);
                    this.isLogin = true;
                    this.isLoading = true,
                        toast.success(
                            "Login Successfull",
                            {
                                rtl: true,
                                limit: 2,
                                position: toast.POSITION.TOP_CENTER,
                            },
                        );
                    setTimeout(() => {
                        this.isLoading = false;
                        const redirectUrl = this.$route.query.redirect || '/';
                        this.$router.push(redirectUrl);
                    }, 3000);
                }
                else {
                    toast.error(
                        data.response.data.errorMessage,
                        {
                            rtl: true,
                            limit: 2,
                            position: toast.POSITION.TOP_CENTER,
                        },
                    );
                }
            }
        }
    },
    mounted() {
        const isToken = sessionStorage.getItem("token");
        if (isToken) {
            this.isLogin = true;
            this.$router.push('/')
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
                            <h4>Welcome Back to MovieRent! 🎬</h4>
                            <p class="mb-4">
                                Sign in to enjoy endless entertainment.</p>
                        </div>
                        <form>
                            <div class="form-group first">
                                <label hidden for="username">Email</label>
                                <input type="text" class="form-control" id="username" v-model="email"
                                    placeholder="email">

                            </div>
                            <div class="form-group last">
                                <label hidden for="password">Password</label>
                                <input type="password" class="form-control" id="password" v-model="password"
                                    placeholder="password">

                            </div>

                            <div class="d-flex mb-2 align-items-center justify-content-between">
                                <label class="control control--checkbox mb-0 d-flex align-items-center">
                                    <input type="checkbox" checked="checked" />
                                    <span class="caption ms-2">Remember me</span>
                                </label>
                                <span class="ml-auto"><a href="#" class="forgot-pass">Forgot Password</a></span>
                            </div>

                            <button type="submit" class="btn btn-block btn-primary w-100 p-3" @click="login()">Log
                                In</button>

                        </form>
                        <div><span>New to MovieRent?</span> <router-link to="/registration">Sign up now</router-link>
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
