<template>
  <div class="card text-light elevation-4 selectable my-1" data-bs-toggle="modal" data-bs-target="#keep-modal"
    @click="getKeepById()">
    <img :src="keep.img" class="card-img" alt="keep-image">
    <div class="card-img-overlay p-0 d-flex flex-column justify-content-end">
      <div class="glass elevation-5 d-flex justify-content-between align-items-center px-2">
        <span class="card-title fs-4">{{keep.name}}</span>
        <img class="profile-pic" :src="keep.creator.picture" alt="profile-pic" height="30">
      </div>
    </div>
  </div>
  <KeepModal />
</template>



<script>
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import KeepModal from './KeepModal.vue';

export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keep.id);
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
.glass {
  background-color: rgb(216, 239, 240, .5);
  color: rgb(16, 17, 17);
  border-bottom-left-radius: 4%;
  border-bottom-right-radius: 4%;
}
</style>