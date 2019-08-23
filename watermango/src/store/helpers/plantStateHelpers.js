export default {
  plantWait(seconds, plant) {
    let p = new Promise(resolve => {
      plant.cancel = resolve;
    });

    const waitPromise = new Promise(resolve => {
      plant.timerId = setTimeout(() => {
        resolve();
      }, seconds * 1000);
    });

    return Promise.race([p, waitPromise]);
  },

  plantCleanUp(plant, resetPreviousState) {
    // Make sure no memory leaks or previous states exist when cancelling Watering
    if (plant.timerId) {
      clearTimeout(plant.timerId);
      plant.timerId = null;
      delete plant.timerId;
    }

    if(plant.cancel){
      plant.cancel = null;
      delete plant.cancel;
    }

    if (resetPreviousState) {
      this.plantResetPreviousState(plant);
    }
  },

  plantResetPreviousState(plant){
    if(plant.PreviousState){
      plant.PreviousState = null;
      delete plant.PreviousState;
    }
  }

};
