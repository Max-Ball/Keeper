<template>
  <nav class="navbar navbar-expand-lg navbar-dark nav-bg px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }" title="Home">
      <div class="d-flex flex-column align-items-center">
        <img src="..\assets\img\vault-icon.png" alt="Keeper Logo" height="70">
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation" title="login menu">
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse justify-content-end" id="navbarText">
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>


</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-bg {
  background-color: #4A4E69;
}

img {
  filter: contrast(200%);
}
</style>
