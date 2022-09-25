<template>
  <div class="row">
    <div v-for="k in keeps" :key="k.id" class="col-md-2">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>

<script>
import { onMounted } from 'vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService'
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import KeepCard from '../components/KeepCard.vue';

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error("[getting all keeps]", error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}


</script>

<style scoped lang="scss">

</style>
