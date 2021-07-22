import Vue from "vue";
import jQuery from "jquery";
global.$ = jQuery;
// import App from './App.vue'
import Employee from "./views/employee/Employee.vue";
import BaseInput from "./components/base/BaseInput.vue";
import BaseCombobox from "./components/base/BaseCombobox.vue";
import { store } from "./store/store";
import router from "./router";
Vue.config.productionTip = false;

Vue.component("BaseInput", BaseInput);
Vue.component("BaseCombobox", BaseCombobox);
new Vue({
    router,
    store: store,
    render: (h) => h(Employee),
}).$mount("#app");
