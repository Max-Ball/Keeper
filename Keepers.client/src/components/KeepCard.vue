<template>
  <div class="card elevation-4 selectable my-1 masonry-content" data-bs-toggle="modal" data-bs-target="#keep-modal"
    @click="getKeepById()" :title="keep.name">
    <img :src="keep.img" class="card-img" alt="keep-image">
    <div class="card-img-overlay p-0 d-flex flex-column justify-content-end">
      <div class="d-flex justify-content-between align-items-center px-2 glass">
        <span class="card-title fs-5">{{keep.name}}</span>
        <img class="profile-pic elevation-4" :src="keep.creator.picture" alt="profile-pic" height="30"
          :title="keep.creator.name">
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



<style lang="scss">
.img-border {
  border-top-left-radius: 3%;
  border-top-right-radius: 3%;
}

.glass {
  background-color: rgb(154, 140, 152, 0.6);
  color: rgb(17, 15, 15);
  font-weight: bold;
  font-family: 'Trebuchet MS';
  backdrop-filter: blur(5px);
  width: 100%;
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
}
</style>