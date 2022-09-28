<template>
  <div v-if="account.id" class="modal fade" id="keep-modal" tabindex="-1">
    <div class="modal-dialog modal-xl">
      <div class="modal-content modal-bg">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-4 p-1">
              <img :src="keep.img" class="img-fluid rounded" alt="keep-image" :title="keep.name">
            </div>
            <div class="col-md-8 d-flex flex-column justify-content-between">
              <div class="row align-items-center">
                <div class="col-md-12 d-flex justify-content-center my-2">
                  <span class="mx-2 user-select-none" title="Keep Views">
                    <i class="mdi mdi-eye"></i>{{keep.views}}
                  </span>
                  <span class="mx-2 user-select-none" title="In vaults">
                    <i class="mdi mdi-heart"></i>{{keep.kept}}
                  </span>
                  <span class="mx-2 user-select-none" title="Shared">
                    <i class="mdi mdi-share-variant"></i>{{keep.shares}}
                  </span>
                </div>
                <div class="text-center header-color fs-1 mt-3 pb-3 user-select-none">
                  {{keep.name}}
                </div>
                <div class="d-flex justify-content-center">
                  <div class="custom-border"></div>
                </div>
                <span class="my-3">
                  {{keep.description}}
                </span>
              </div>
              <div class="row my-2">
                <div class="col-md-12 d-flex justify-content-between">
                  <router-link v-if="keep?.creator" :to="{name: 'Profile', params: {profileId: keep.creator.id}}">
                    <div data-bs-dismiss="modal" title="Go to profile">
                      <span class="me-2">
                        <img class="profile-pic" :src="keep.creator?.picture" alt="profile-pic" height="40" />
                      </span>
                      <span class="text-dark">
                        {{keep.creator?.name}}
                      </span>
                    </div>
                  </router-link>
                  <div v-if="!vaultKeep || activeVault.creatorId != account.id">
                    <div class="dropdown">
                      <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                        title="Add to Vault">
                        Add To Vault
                      </button>
                      <ul class="dropdown-menu">
                        <li v-for="v in vaults" :key="v.id">
                          <VaultList :vault="v" />
                        </li>
                      </ul>
                    </div>
                  </div>
                  <div v-else>
                    <button class="btn btn-warning" @click="removeKeepFromVault()" data-bs-dismiss="modal"
                      title="Remove From Vault">
                      Remove From Vault
                    </button>
                  </div>
                  <div>
                    <i v-if="keep.creatorId == account.id" class="mdi mdi-delete fs-3 selectable" @click="deleteKeep()"
                      data-bs-dismiss="modal" title="Delete Keep"></i>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>



<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';


export default {

  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.accountVaults),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault),
      vaultKeep: computed(() => AppState.vaultKeeps.find(vk => vk.id == AppState.activeKeep.id)),

      async deleteKeep() {
        try {
          const yes = await Pop.confirm("Are you sure you want to delete this keep?")
          if (!yes) {
            return
          }
          await keepsService.deleteKeep(this.keep.id)
          Pop.success(`${AppState.activeKeep.name} has been deleted!`)
        } catch (error) {
          logger.error('[deleting keep]', error)
          Pop.error(error)
        }
      },

      async removeKeepFromVault() {
        try {
          const yes = await Pop.confirm("Are you sure you want to remove this keep from your vault?")
          if (!yes) {
            return
          }
          console.log(this.vaultKeep);
          await keepsService.removeKeepFromVault(this.vaultKeep.vaultKeepId)
          Pop.success(`${this.keep.name} has been removed from ${this.activeVault.name}`)
        } catch (error) {
          logger.error('removing keep from vault', error)
          Pop.error(error)
        }
      }
    };
  },
};
</script>



<style>
.dropdown-menu {
  height: 200px;
  overflow-y: scroll;
}

.custom-border {
  position: relative;
  height: 10px;
  border-bottom: 2px solid #22223B;
  width: 50%;
}

.modal-bg {
  background-color: #F2E9E4;
}

.header-color {
  color: #22223B;
}
</style>