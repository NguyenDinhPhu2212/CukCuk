import Vue from "vue";
import VTooltip from "v-tooltip";
// import App from './App.vue'
import Employee from "./views/employee/Employee.vue";
import BaseInput from "./components/base/BaseInput.vue";
import BaseCombobox from "./components/base/Combobox.vue";
import Loading from "./components/base/Loading.vue";
import ToastGroup from "./components/base/ToastGroup.vue";
import Popup from "./components/base/Popup.vue";
import Pagination from "./components/base/Pagination.vue";
import { store } from "./store/store";
import router from "./router";
Vue.config.productionTip = false;

Vue.component("BaseInput", BaseInput);
Vue.component("BaseCombobox", BaseCombobox);
Vue.component("Loading", Loading);
Vue.component("Pagination", Pagination);
Vue.component("ToastGroup", ToastGroup);
Vue.component("Popup", Popup);
Vue.use(VTooltip);
new Vue({
    router,
    store: store,
    render: (h) => h(Employee),
}).$mount("#app");
