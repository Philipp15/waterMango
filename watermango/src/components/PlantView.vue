<template>
  <v-card>
    <v-img
      :src="require('../assets/plantImages/'+ plant.ImagePath)"
      class="white--text"
      height="400px"
    ></v-img>
    <v-card-title class="fill-height align-end" v-text="plant.State" :class="{'warning': plant.State === plantStates.Dying}"></v-card-title>
    <v-card-actions>
      <v-btn icon @click="waterPlant(plant.Id)" :disabled="![plantStates.Watered,plantStates.Dying ].includes(plant.State) ">
        <v-icon>mdi-cup-water</v-icon>
      </v-btn>
      <v-btn :disabled="plant.State !== plantStates.InProgress"  icon @click="cancelWatering(plant.Id)">
        <v-icon>mdi-cancel</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import plantStates from "../lookups/PlantStates";

export default {
  props: {
    plant: {
      type: Object,
      default: null
    }
  },
  data() {
      return {
          plantStates
      }
  },
  methods:{
  async waterPlant(plantId) {
              await this.$store.dispatch("plantModule/waterPlant", plantId);
              this.$emit("checkwatered", true); //this will trigger the parent to refresh timeout and recaculate new earliest to die plant
     },
  async  cancelWatering(plantId){
       await  this.$store.dispatch("plantModule/cancelWatering", plantId);
     }
  },

};
</script>

<style >
.warning{
  color: red;
}
</style>