import Vue from "vue";
import Vuex from "vuex";
Vue.use(Vuex);

export const store = new Vuex.Store({
    strict: true,
    state: {
        employeeList: [],
        executeData: {},
        dialog: false,
        contextMenu: {
            display: false,
            top: 0,
            left: 0,
        },
        popup: {
            display: false,
            type: {},
        },
        toastGroup: [],
    },
    getters: {
        getEmployee: (state) => {
            return state.employeeList;
        },
        getDataLength: (state) => {
            return Math.ceil(state.employeeList.length / 10);
        },
        getExecuteData: (state) => {
            return state.executeData;
        },
        getContextMenu: (state) => {
            return state.contextMenu;
        },
        getPopup: (state) => {
            return state.popup;
        },
        getToast: (state) => {
            return state.toastGroup;
        },
        getDialog: (state) => {
            return state.dialog;
        },
    },
    mutations: {
        setEmployee: (state, array) => {
            state.employeeList = [...array];
        },
        setExecuteData: (state, obj) => {
            state.executeData = { ...obj };
        },
        setContextMenu: (state, obj) => {
            state.contextMenu = { ...obj };
        },
        setPopup: (state, obj) => {
            state.popup = { ...obj };
        },
        addToast: (state, obj) => {
            state.toastGroup.push(obj);
        },
        setDialog: (state, bool) => {
            state.dialog = bool;
        },
        deleteToast(state, index) {
            state.toastGroup.splice(index, 1);
            state.toastGroup = [...state.toastGroup];
        },
    },
    //this.$store.dispatch("name", para)
    actions: {},
});
