<template>
  <div>
    <router-link :to="{name: 'Vault', params: {vaultId: vault.id}}">
      <div class="masonry-content card text-light elevation-4 selectable my-1" :title="vault.name">
        <img :src="vault.img" class="card-img" alt="vault-image">
        <div class="card-img-overlay p-0 d-flex flex-column justify-content-end">
          <div class="px-2 glass">
            <span class="card-title fs-5">{{vault.name}}</span>
          </div>
        </div>
      </div>
    </router-link>
  </div>
</template>



<script>
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {

    return {

    };
  },
};
</script>



<style>

</style>