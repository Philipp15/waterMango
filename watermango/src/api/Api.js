import axios from 'axios';

var Singleton = (function () {
    var instance;
 
    function createInstance() {
        var axiosInstance = axios.create({ baseURL: 'http://localhost:28403/api/', });
        return axiosInstance;
    }
 
    return {
        getInstance: function () {
            if (!instance) {
                instance = createInstance();
            }
            return instance;
        }
    };
})();
 
 export default Singleton.getInstance();

