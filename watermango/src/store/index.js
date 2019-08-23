import Vue from "vue";
import Vuex from "vuex";
import plantModule from './modules/plantModule';


Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    plantModule,
  },
  state: {},
  getters: {},
  mutations: {},
  actions: {},
});
