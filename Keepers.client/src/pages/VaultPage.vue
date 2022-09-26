<template>
  <div class="row">
    <div class="col-md-9 my-3">
      <div>
        <span class="fs-1">
          {{vault.name}}
        </span><br>
        <span>
          Keeps: {{keeps.length}}
        </span>
      </div>
    </div>
    <div class="col-md-3 text-end my-3">
      <button v-if="vault.creator?.id == account.id" class="btn btn-danger" @click="deleteVault()">
        DELETE VAULT
      </button>
    </div>
    <div v-for="k in keeps" :key="k.id" class="col-md-2">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>



<script>
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

  setup() {
    const route = useRoute()
    const router = useRouter()

    async function getVault() {
      try {
        await vaultsService.getVault(route.params.vaultId)
        getKeepsByVaultId()
      } catch (error) {
        logger.error('[getting vault]', error)
        Pop.error(error)
      }
    }

    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId)
      } catch (error) {
        logger.error('[getting keeps by vault id]', error)
        Pop.error(error)
      }
    }

    onMounted(() => {
      getVault();
    })
    return {
      route,
      router,
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),

      async deleteVault() {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this vault?')
          if (!yes) {
            return
          }
          await vaultsService.deleteVault(route.params.vaultId)
          router.push({ name: 'Home' })
        } catch (error) {
          logger.error('[deleting vault]', error)
          Pop.error(error)
        }
      }
    };
  },
};
</script>



<style>

</style>