<template>
  <router-link :to="{name: 'Vault', params: {vaultId: vault.id}}">
    <div class="masonry-content card text-light elevation-4 selectable my-1">
      <img :src="vault.img" class="card-img" alt="vault-image">
      <div class="card-img-overlay p-0 d-flex flex-column justify-content-end">
        <div class="d-flex justify-content-between align-items-center px-2">
          <span class="text-shadow card-title fs-6">{{vault.name}}</span>
        </div>
      </div>
    </div>
  </router-link>
</template>



<script>
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {

    return {};
  },
};
</script>



<style>

</style>