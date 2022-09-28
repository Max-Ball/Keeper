<template>
  <a class="dropdown-item selectable" @click="addKeepToVault()" :title="vault.name">{{vault.name}}</a>
</template>



<script>
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import { computed } from '@vue/reactivity';
import Pop from '../utils/Pop';

export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup(props) {

    return {
      keep: computed(() => AppState.activeKeep),

      async addKeepToVault() {
        try {
          const yes = await Pop.confirm(`Are you sure you want to add ${AppState.activeKeep.name} to the ${props.vault.name} vault?`)
          if (!yes) {
            return
          }
          let newKeepToVault = {
            vaultId: props.vault.id,
            keepId: AppState.activeKeep.id
          }
          await keepsService.addKeepToVault(newKeepToVault)
          Pop.success(`${AppState.activeKeep.name} added to ${props.vault.name}`)
        } catch (error) {
          logger.error('[adding a keep to a vault]', error)
          Pop.error(error)
        }
      }
    };
  },
};
</script>



<style>

</style>