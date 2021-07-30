import Vue from "vue";
import Vuex from "vuex";
Vue.use(Vuex);
import * as utils from "../scripts/utils";

import { getEmployeeFilter } from "../scripts/api.js";
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
        loading: false,
        filter: {
            pageSize: 10,
            pageNumber: 0,
            employeeFilter: "NV",
            departmentId: "",
            positionId: "",
        },
    },
    getters: {
        getEmployee: (state) => {
            return state.employeeList;
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
        getLoading: (state) => {
            return state.loading;
        },
        getFilter: (state) => {
            return state.filter;
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
        setLoading: (state, bool) => {
            state.loading = bool;
        },
        setFilter: (state, obj) => {
            state.filter = { ...obj };
        },
    },
    //this.$store.dispatch("name", para)
    actions: {
        employeeFilter: async (context) => {
            let filter = {...context.getters.getFilter};
            console.log(filter)
            let employee = await getEmployeeFilter(filter);
            for (let i in employee.Data) {
                utils.trainingData(employee.Data[i]);
            }
            context.commit("setEmployee", employee.Data);
        },
    },
});
