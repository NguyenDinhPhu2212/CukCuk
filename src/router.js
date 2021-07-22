import Vue from "vue";
import VueRouter from "vue-router";

import ThePage from "./components/layout/ThePage.vue";
Vue.use(VueRouter);

const router = new VueRouter({
    mode: "history",
    routes: [
        {
            path: "/general",
            name: "General",
            component: {
                render(h) {
                    return h("div", "Tổng quan");
                },
            },
        },
        {
            path: "/report",
            name: "Report",
            component: {
                render(h) {
                    return h("div", "Báo cáo");
                },
            },
        },
        {
            path: "/buy",
            name: "Buy",
            component: {
                render(h) {
                    return h("div", "Mua hàng");
                },
            },
        },
        {
            path: "/employee",
            name: "Employee",
            component: ThePage,
        },
        {
            path: "/settings",
            name: "Settings",
            component: {
                render(h) {
                    return h("div", "Thiết lập hệ thống");
                },
            },
        },
    ],
});
export default router;
