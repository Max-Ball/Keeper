<template>
  <div class="modal fade" id="create-keep-modal" tabindex="-1" aria-labelledby="create-keep-modal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="container-fluid">
          <div class="row">
            <div class="fs-3 my-3">
              Create A Keep
            </div>
            <form class="form-label" @submit.prevent="createKeep()">
              <label for="name">Name:</label>
              <input class="form-control" type="text" name="name" placeholder="Enter the name of your keep..." required
                v-model="editable.name">
              <label for="image" class="mt-3">Image:</label>
              <input class="form-control" type="text" name="image" placeholder="Enter an image for your keep..."
                required v-model="editable.img">
              <label for="description" class="mt-3">Description:</label>
              <textarea class="form-control" name="description" cols="30" rows="5"
                placeholder="Enter a description for your keep..." required v-model="editable.description"></textarea>
              <div class="text-end my-2">
                <button class="btn btn-primary" data-bs-dismiss="modal" title="Create Keep">
                  Create Keep
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
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

  setup() {
    const editable = ref({})
    return {
      editable,

      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Pop.success(`${editable.value.name} has been created!`)
          editable.value = {}
        } catch (error) {
          logger.error('[creating keep]', error)
          Pop.error(error)
        }
      }

    };
  },
};
</script>



<style>

</style>