<template>
  <div class="row my-3">
    <div class="col-md-12 d-flex align-items-center">
      <img class="img-fluid me-3 profile-pic" :src="profile.picture" alt="">
      <div>
        <div class="fs-1">{{profile.name}}</div>
        <div class="fs-3">Vaults: {{vaults.length}}</div>
        <div class="fs-3">Keeps: {{keeps.length}}</div>
      </div>
    </div>
    <h3 class="mt-5">
      VAULTS <i v-if="profile.id == account.id" class="mdi mdi-plus selectable" data-bs-toggle="modal"
        data-bs-target="#create-vault-modal"></i>
    </h3>
    <VaultForm />
    <div v-for="v in vaults" :key="v.id" class="col-md-1">
      <VaultCard :vault="v" />
    </div>
    <h3>
      KEEPS <i v-if="profile.id == account.id" class="mdi mdi-plus selectable" data-bs-toggle="modal"
        data-bs-target="#create-keep-modal"></i>
    </h3>
    <KeepForm />
    <div v-for="k in keeps" :key="k.id" class="col-md-1">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>



<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { profilesService } from '../services/ProfilesService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import VaultForm from '../components/VaultForm.vue';
import KeepForm from '../components/KeepForm.vue';

export default {
  setup() {
    const route = useRoute();
    async function getProfile() {
      try {
        await profilesService.getProfile(route.params.profileId);
        getProfileVaults();
        getProfileKeeps();
      }
      catch (error) {
        logger.error("[getting profile]", error);
        Pop.error(error);
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.params.profileId);
      }
      catch (error) {
        logger.error("[getting user vaults]", error);
        Pop.error(error);
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId);
      }
      catch (error) {
        logger.error("[getting user keeps]", error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getProfile();
    });
    return {
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    };
  },
  components: { VaultCard, KeepCard, VaultForm, KeepForm }
};
</script>



<style>

</style>