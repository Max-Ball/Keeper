<template>
  <div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="keep-modal" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-4 p-1">
              <img :src="keep.img" class="img-fluid rounded" alt="keep-image">
            </div>
            <div class="col-md-8 d-flex flex-column justify-content-between">
              <div class="row align-items-center">
                <div class="col-md-12 d-flex justify-content-center my-2">
                  <span class="mx-2">
                    <i class="mdi mdi-eye"></i>{{keep.views}}
                  </span>
                  <span class="mx-2">

                    <i class="mdi mdi-heart selectable"></i>{{keep.kept}}
                  </span>
                  <span class="mx-2">
                    <i class="mdi mdi-share-variant"></i>{{keep.share}}
                  </span>
                </div>
              </div>
              <div>
                <div class="text-center fs-1">
                  {{keep.name}}
                </div>
                <span class="my-3">
                  {{keep.description}}
                </span>
              </div>
              <div class="row justify-content-center my-2">
                <div class="col-md-8">
                  <span class="me-2">
                    <img class="profile-pic" :src="keep.creator?.picture" alt="profile-pic" height="40">
                  </span>
                  <span class="text-center">
                    {{keep.creator?.name}}
                  </span>
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


export default {

  setup() {

    return {
      keep: computed(() => AppState.activeKeep)
    };
  },
};
</script>



<style>

</style>