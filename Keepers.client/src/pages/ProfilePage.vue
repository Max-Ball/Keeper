<template>
  <div class="row align-items-center my-3">
    <div class="col-md-1">
      <img class="img-fluid profile-pic" :src="profile.picture" alt="Profile-pic" :title="profile.name">
    </div>
    <div class="col-md-5">
      <div class="fs-1">{{profile.name}}</div>
      <div class="fs-3">Vaults: {{vaults.length}}</div>
      <div class="fs-3">Keeps: {{keeps.length}}</div>
    </div>
    <h3 class="mt-5">
      VAULTS <i v-if="profile.id == account.id" class="mdi mdi-plus selectable" data-bs-toggle="modal"
        data-bs-target="#create-vault-modal" title="Create Vault"></i>
    </h3>
    <VaultForm />
    <div class="masonry">
      <div v-for="v in vaults" :key="v.id" class="masonry-item">
        <VaultCard :vault="v" />
      </div>
    </div>
    <h3 class="mt-3">
      KEEPS <i v-if="profile.id == account.id" class="mdi mdi-plus selectable" data-bs-toggle="modal"
        data-bs-target="#create-keep-modal" title="Create Keep"></i>
    </h3>
    <KeepForm />
    <div class="masonry">
      <div id="keep-change" v-for="k in keeps" :key="k.id" class="masonry-item">
        <ProfileKeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { profilesService } from '../services/ProfilesService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import VaultForm from '../components/VaultForm.vue';
import KeepForm from '../components/KeepForm.vue';
import imagesloaded from 'imagesloaded';
import ProfileKeepCard from '../components/ProfileKeepCard.vue';
import { masonryService } from '../services/MasonryService';


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
      let allItems = document.getElementsByClassName('masonry-item')
      console.log(allItems);
      console.log(allItems.length);
      for (let i = 0; i < allItems.length; i++) {
        imagesloaded(allItems[i], function (instance) {
          let item = instance.elements[0]
          resizeMasonryItem(item)
        })
      }
    }

    let keepChange = document.getElementsByClassName('masonry-item');
    console.log(keepChange);
    let masonryEvents = ['load', 'resize'];
    // masonryEvents.forEach(function (event) {
    //   window.addEventListener(event, resizeAllMasonryItems)

    // })

    masonryEvents.forEach(function (event) {
      window.addEventListener(event, resizeAllMasonryItems)
      document.addEventListener(event, resizeAllMasonryItems)
    })




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
        waitForImages();
      }
      catch (error) {
        logger.error("[getting user vaults]", error);
        Pop.error(error);
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId);
        waitForImages();
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
  components: { VaultCard, KeepCard, VaultForm, KeepForm, ProfileKeepCard }
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

.bg-prof-card {
  background-color: #22223B;
  color: #F2E9E4;
}
</style>