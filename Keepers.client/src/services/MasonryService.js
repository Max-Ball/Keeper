import imagesloaded from 'imagesloaded';
import { vaultsService } from './VaultsService';

class MasonryService {

  resizeMasonryItem(item) {
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

  resizeAllMasonryItems() {
    let allItems = document.getElementsByClassName('masonry-item')
    console.log(allItems);
    for (let i = 0; i < allItems.length; i++) {
      masonryService.resizeMasonryItem(allItems[i])
    }
  }


  waitForImages() {

    let allItems =
      document.getElementsByClassName('masonry-item')
    console.log(allItems.length);
    for (let i = 0; i < allItems.length; i++) {
      console.log(allItems.length);
      imagesloaded(allItems[i], function (instance) {
        let item = instance.elements[0]
        masonryService.resizeMasonryItem(item)
      })
    }

  }

  eventList() {

    let masonryEvents = ['load', 'resize'];
    masonryEvents.forEach(function (event) {
      window.addEventListener(event, masonryService.resizeAllMasonryItems)

      // keepChange.addEventListener(event, resizeAllMasonryItems)
      // NOTE I need to add another event to listen for when keeps/vaults are being added
    })
  }




}

export const masonryService = new MasonryService();