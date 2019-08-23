<template>
  <v-container>
      <v-layout justify-center my-5>
          <h1 > Welcome to WaterMango! </h1>
      </v-layout>
      <v-layout>
      <PlantBrowseView></PlantBrowseView>
      </v-layout>
  </v-container>
</template>

<script>
import PlantBrowseView from '../components/PlantBrowseView';
export default {
    components: {
        PlantBrowseView,
    },
};
</script>
