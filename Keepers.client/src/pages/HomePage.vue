<template>
  <div class="masonry">
    <div class="masonry-item" v-for="k in keeps" :key="k.id">
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

    function resizeMasonryItem(item) {
      console.log(item);
      let grid = document.getElementsByClassName('masonry')[0]
      let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'))
      let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
      console.log('rowGap', rowGap);
      console.log('rowHeight', rowHeight);



      let rowSpan = Math.ceil((item.querySelector('.masonry-content').getBoundingClientRect().height + rowGap) / (rowHeight + rowGap))

      console.log('rowSpan', rowSpan);
      item.style.gridRowEnd = 'span ' + rowSpan
      console.log('gridrowend', item.style.gridRowEnd);
    }

    function resizeAllMasonryItems() {
      let allItems = document.getElementsByClassName('masonry-item')
      console.log(allItems);
      for (let i = 0; i < allItems.length; i++) {
        resizeMasonryItem(allItems[i])
      }
    }

    function waitForImages() {
      let allItems =
        document.getElementsByClassName('masonry-item')
      for (let i = 0; i < allItems.length; i++) {
        imagesLoaded(allItems[i],
          function (instance) {
            let item = instance.elements[0]
            resizeMasonryItem(item)
          })
      }
    }

    let masonryEvents = ['load', 'resize'];
    masonryEvents.forEach(function (event) {
      window.addEventListener(event, resizeAllMasonryItems)
    })


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
      waitForImages()
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}


</script>

<style scoped lang="scss">
.masonry {
  display: grid;
  grid-gap: 10px;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  grid-auto-rows: 0;
}

.masonry-item {
  border-radius: 5px;
}

.masonry-item {
  background-color: #eee;
  border-radius: 5px;
  // overflow: hidden;
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
