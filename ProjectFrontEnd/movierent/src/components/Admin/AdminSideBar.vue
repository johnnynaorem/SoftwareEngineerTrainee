<template>
  <aside :class="`${is_expanded && 'is-expanded'}`">
    <div class="logo">
      <img src="../../Images/logo.webp" alt="logo" />
    </div>
    <div class="menu-toggle-wrap">
      <button class="menu-toggle" @click="ToggleMenu">
        <span class="material-icons"> keyboard_double_arrow_right </span>
      </button>
    </div>

    <h3>Menu</h3>
    <div class="menu">
      <router-link class="button" to="/admin/dashboard/home">
        <span class="material-icons">home</span>
        <span class="text">Home</span>
      </router-link>
      <router-link class="button" to="/admin/dashboard/movies">
        <span class="material-icons">videocam</span>
        <span class="text">Movie</span>
      </router-link>
      <router-link class="button" to="/admin/dashboard/reservations">
        <span class="material-icons">book_online</span>
        <span class="text">Reservation</span>
      </router-link>
      <router-link class="button" to="/admin/dashboard/payments">
        <span class="material-icons">payments</span>
        <span class="text">Payment</span>
      </router-link>
      <router-link class="button" to="/admin/dashboard/rentals">
        <span class="material-icons"> book </span>
        <span class="text">Rental</span>
      </router-link>
      <router-link class="button" @click="logout()" to="/">
        <span class="material-icons"> logout </span>
        <span class="text">Log Out</span>
      </router-link>
    </div>
    <!-- <div class="flex"></div>
        <router-link class="button" to="/setting">
            <span class="material-icons">
                settings
            </span>
            <span class="text">Setting</span>
        </router-link> -->
  </aside>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
const is_expanded = ref(false);
const router = useRouter()
const ToggleMenu = () => {
  is_expanded.value = !is_expanded.value;
};

const logout = () => {
  sessionStorage.removeItem('token');
  router.push('/login')
}

</script>

<style lang="scss" scoped>
aside {
  display: flex;
  flex-direction: column;
  width: calc(2rem + 2rem);
  overflow: hidden;
  min-height: 100vh;
  padding: 1rem;

  background-color: black;
  color: var(--light);

  transition: 0.2s ease-out;

  .flex {
    flex: 1 1 0;
  }

  button {
    outline: none;
    background: transparent;
    border: none;
  }

  .logo {
    margin-bottom: 1rem;

    img {
      width: 2.5rem;
    }
  }

  .menu-toggle-wrap {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 1rem;

    position: relative;
    top: 0;
    transition: 0.2s ease-out;

    .menu-toggle {
      transition: 0.2s ease-out;

      .material-icons {
        font-size: 2rem;
        color: var(--light);
      }

      &:hover {
        .material-icons {
          color: var(--primary);
          transform: translateX(0.5rem);
        }
      }
    }
  }

  h3,
  .button .text {
    opacity: 0;
    transition: 0.3s ease-out;
  }

  h3 {
    color: var(--gray);
    font-size: 0.875rem;
    margin-bottom: 0.5rem;
    text-transform: uppercase;
  }

  .menu {
    margin: 0 -1rem;

    .button {
      display: flex;
      align-items: center;
      text-decoration: none;

      padding: 0.5rem 1rem;
      transition: 0.2s ease-out;

      .material-icons {
        font-size: 2rem;
        color: var(--light);
        margin-right: 1rem;
        transition: 0.2s ease-out;
      }

      .text {
        color: var(--light);
        transition: 0.2s ease-out;
      }

      &:hover,
      &.router-link-active {
        background-color: var(--dark-alt);

        .material-icons,
        .text {
          color: var(--primary);
        }
      }

      &.router-link-active {
        border-right: 5px solid var(--primary);
      }
    }
  }

  &.is-expanded {
    width: 200px;

    .menu-toggle-wrap {
      top: -3rem;

      .menu-toggle {
        transform: rotate(-180deg);
      }
    }

    h3,
    .button .text {
      opacity: 1;
    }
  }

  @media (max-width: 768px) {
    position: fixed;
    z-index: 99;
  }
}
</style>
