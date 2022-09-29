<template>
  <div class="modal fade" id="create-vault-modal" tabindex="-1" aria-labelledby="create-vault-modal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="container-fluid">
          <div class="row">
            <div class="fs-3 my-3">
              Create A Vault
            </div>
            <form @submit.prevent="createVault()">
              <label for="name">Name:</label>
              <input class="form-control" type="text" name="name" placeholder="Enter the name of your vault..." required
                v-model="editable.name" title="Enter a name">
              <label for="image" class="mt-3">Image:</label>
              <input class="form-control" type="text" name="image" placeholder="Enter an image for your vault..."
                required v-model="editable.img" title="Enter an image">
              <label for="description" class="mt-3">Description:</label>
              <textarea class="form-control" name="description" cols="30" rows="5"
                placeholder="Enter a description for your vault..." required v-model="editable.description"
                title="Enter a description"></textarea>
              <div class="form-check mt-3">
                <input class="form-check-input selectable" type="checkbox" name="private" v-model="editable.isPrivate"
                  title="Check if private">
                <label class="form-check-label" for="private">
                  Private
                </label>
                <p class="form-text text-muted p-0 m-0">
                  (If checked this vault will not be seen by other users)
                </p>
              </div>
              <div class="text-end my-2">
                <button class="btn" title="Create Vault">
                  Create Vault
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>



<script>
import { ref } from 'vue';
import { masonryService } from '../services/MasonryService';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          await vaultsService.createVault(editable.value)
          Pop.success(`Your ${editable.value.name} vault has been created!`)
          masonryService.waitForImages()
          editable.value = {}
        } catch (error) {
          logger.error('[creating vault]', error)
          Pop.error(error)
        }
      }
    };
  },
};
</script>



<style>

</style>