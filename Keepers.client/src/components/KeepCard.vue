<template>
  <div class="rounded text-light elevation-4 selectable my-1 masonry-content" data-bs-toggle="modal"
    data-bs-target="#keep-modal" @click="getKeepById()">
    <img :src="keep.img" class="img-fluid rounded" alt="keep-image">
    <div class="p-0 d-flex flex-column justify-content-end">
      <div class="d-flex justify-content-between align-items-center px-2">
        <span class="text-shadow fs-4">{{keep.name}}</span>
        <img class="profile-pic elevation-4" :src="keep.creator.picture" alt="profile-pic" height="30">
      </div>
    </div>
  </div>
  <KeepModal />
</template>



<script>
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import KeepModal from './KeepModal.vue';

export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      async getVaultsByAccount() {
        try {
          await vaultsService.getVaultsByAccount()
        } catch (error) {
          logger.error('[getting account vaults]')
          Pop.error(error)
        }
      },

      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keep.id);
          this.getVaultsByAccount()
        }
        catch (error) {
          logger.error("[getting one keep]", error);
          Pop.error(error);
        }
      }
    };
  },
  components: { KeepModal }
};
</script>



<style scoped lang="scss">

</style>