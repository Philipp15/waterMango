import Api from './Api';

export default {

    async getPlants() {
        const  { data } = await Api.get('plant/Get');
        return data;
    },

    async updatePlant(plant){
        await Api.post('plant/UpdatePlant', plant) ;
    },

    async getEarliestCheckUpOnPlants(){
        const  { data } = await Api.get('plant/GetEarliestCheckOnPlantsTime');
        return data;
    }

}