import plantStates from "../../lookups/PlantStates";
import plantStateHelper from "../helpers/plantStateHelpers";
import ApiPlants from "../../api/ApiPlants";

export default {
  namespaced: true,
  state: {
    plants: [],
  },
  mutations: {
    init: (state, payload) => {
      state.plants.splice(0, payload.plants.length, ...payload.plants);
    },
    setState: (state, payload) => {
      let plant = payload.plant;

      const currentState = plant.State;
      plant.State = payload.newState;

      if(payload.rememberPreviousState){
        plant.PreviousState = currentState;
      }

      state.plants.splice(payload.plantIndex, 1, plant);
    },
  },
  getters: {
    getPlants: state => {
      return state.plants;
    },
    getPlantIndexById: state => plantId => {
      let plantIndex = state.plants.findIndex(pl => pl.Id === plantId);
      return plantIndex;
    },
  },
  actions: {
    async loadPlants({ commit }) {
      const plants = await ApiPlants.getPlants();
      commit("init", { plants });
    },

    async cancelWatering({ state, getters, commit }, plantId) {
      const plantIndex = getters.getPlantIndexById(plantId);
      let plant = state.plants[plantIndex];

      if (plant.State === plantStates.InProgress) {
        plant.cancel();
        plant.Cancelled = true;
      }

      commit("setState", {
        plant,
        plantIndex: plantIndex,
        newState: plant.PreviousState,
      });
    },

    async waterPlant({ getters, state, commit }, plantId) {
      const plantIndex = getters.getPlantIndexById(plantId);
      const plant = state.plants[plantIndex];

      if (plant.State === plantStates.InProgress) {
        //console.log("Already watering this plant");
        return;
      }

      try {
        commit("setState", { plantIndex, plant, newState: plantStates.InProgress, rememberPreviousState: true });
        await plantStateHelper.plantWait(10, plant);
        if (plant.Cancelled) {
          plant.Cancelled = false;
          plantStateHelper.plantCleanUp(plant, true);
          return;
        }
        plant.WateredTime = new Date().toJSON();

        commit("setState", { plantIndex, plant, newState: plantStates.Resting });
        plantStateHelper.plantCleanUp(plant, true);
        await ApiPlants.updatePlant(plant); // Update Resting State

        await plantStateHelper.plantWait(30, plant);
        commit("setState", { plantIndex, plant, newState: plantStates.Watered });  // Update Watered State

        await ApiPlants.updatePlant(plant);
      } catch (e) {
        //console.log(e);
      }
    },
  },
};
