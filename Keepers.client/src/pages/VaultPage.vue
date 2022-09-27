<template>
  <div class="row">
    <div class="col-md-9 my-3">
      <div>
        <span class="fs-1">
          {{vault.name}}
        </span><br>
        <span class="fs-4">
          {{vault.description}}
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
    <div class="masonry">
      <div v-for="k in keeps" :key="k.id" class="masonry-item">
        <KeepCard :keep="k" />
      </div>
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
import imagesloaded from 'imagesloaded';


export default {

  setup() {

    function resizeMasonryItem(item) {
      // console.log(item);
      let grid = document.getElementsByClassName('masonry')[0]
      let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'))
      let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
      // console.log('rowGap', rowGap);
      // console.log('rowHeight', rowHeight);



      let rowSpan = Math.ceil((item.querySelector('.masonry-content').getBoundingClientRect().height + rowGap) / (rowHeight + rowGap))

      // console.log('rowSpan', rowSpan);
      item.style.gridRowEnd = 'span ' + rowSpan
      // console.log('gridrowend', item.style.gridRowEnd);
    }

    function resizeAllMasonryItems() {
      let allItems = document.getElementsByClassName('masonry-item')
      // console.log(allItems);
      for (let i = 0; i < allItems.length; i++) {
        resizeMasonryItem(allItems[i])
      }
    }


    function waitForImages() {
      let allItems =
        document.getElementsByClassName('masonry-item')
      for (let i = 0; i < allItems.length; i++) {
        console.log('getting here?');
        imagesloaded(allItems[i], function (instance) {
          let item = instance.elements[0]
          resizeMasonryItem(item)
        })
      }
    }


    let masonryEvents = ['load', 'resize'];
    masonryEvents.forEach(function (event) {
      window.addEventListener(event, resizeAllMasonryItems)
    })




    const route = useRoute()
    const router = useRouter()

    async function getVault() {
      try {
        await vaultsService.getVault(route.params.vaultId)
        getKeepsByVaultId()
      } catch (error) {
        logger.error('[getting vault]', error)
        Pop.error("That vault is either private or does not exist")
        router.push({ name: 'Home' })
      }
    }

    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId)
        waitForImages();
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
      keeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),

      async deleteVault() {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this vault?')
          if (!yes) {
            return
          }
          await vaultsService.deleteVault(route.params.vaultId)
          Pop.success(`${AppState.activeVault.name} has been deleted`)
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
.masonry {
  display: grid;
  grid-gap: 10px;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  grid-auto-rows: 0;
}


.masonry-item {
  border-radius: 5px;
  overflow: hidden;
  position: relative;
}

.masonry-item:after {
  font-weight: bold;
  background-color: rgba(0, 0, 0, .5);
  counter-increment: masonry;
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  transition: all .1s ease-in;
}

.masonry-item:hover:after {
  font-size: 30px;
  background-color: rgba(0, 0, 0, .75);
}
</style>