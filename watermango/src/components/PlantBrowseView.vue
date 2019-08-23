<template>
  <v-container>
    <v-layout row justify-center>
      <v-flex md2 mx-2 v-for="plant in plants"  :key="plant.id">
        <PlantView :plant="plant"  @checkwatered="recursiveCheck()"></PlantView>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import PlantView from "./PlantView";
import ApiPlants from "../api/ApiPlants";

function wait(sec) {
  return new Promise(r => setTimeout(r, sec * 1000));
}

export default {
  components: {
    PlantView
  },
  data: function() {
    return {
      plants: this.$store.getters["plantModule/getPlants"]
    };
  },
  methods: {
    async recursiveCheck() {
      let secondsRecheck = await ApiPlants.getEarliestCheckUpOnPlants(); // get the earliest time we must re-update the store
      //console.log(secondsRecheck);
      if (secondsRecheck == -1) {
       
        return; // All plants are in dying state no need to recheck until user waters at least one
      }
      await wait(secondsRecheck); // wait for this time
      await this.$store.dispatch("plantModule/loadPlants"); //some plant must have elapsed 6 hour period -> reload the store
      this.$forceUpdate();
      this.recursiveCheck(); //
    },
  },
  async created() {
    await this.$store.dispatch("plantModule/loadPlants");
    await this.recursiveCheck();
  }
};
</script>

<style>
</style>