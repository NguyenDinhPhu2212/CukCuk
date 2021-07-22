<template>
    <div class="navbar">
        <router-link
            to="/general"
            class="navbar-item flex-align-center"
            @click.native="clickItem"
            :class="{ clickItem: index == 1 }"
        >
            <div class="navbar-item-icon navbar-item-dashboard-icon"></div>
            <div class="navbar-item-text" v-show="showMenuText">
                Tổng quan
            </div></router-link
        >

        <router-link
            to="/report"
            class="navbar-item flex-align-center"
            @click.native="clickItem"
            :class="{ clickItem: index == 2 }"
        >
            <div class="navbar-item-icon navbar-item-report-icon"></div>
            <div class="navbar-item-text" v-show="showMenuText">
                Báo cáo
            </div></router-link
        >

        <router-link
            to="/buy"
            class="navbar-item flex-align-center"
            @click.native="clickItem"
            :class="{ clickItem: index == 3 }"
        >
            <div class="navbar-item-icon navbar-item-dashboard-icon"></div>
            <div class="navbar-item-text" v-show="showMenuText">
                Mua hàng
            </div></router-link
        >
        <router-link
            to="/employee"
            class="navbar-item flex-align-center"
            @click.native="clickItem"
            :class="{ clickItem: index == 4 }"
        >
            <div class="navbar-item-icon navbar-item-dic-employee-icon"></div>
            <div class="navbar-item-text" v-show="showMenuText">
                Danh mục nhân viên
            </div></router-link
        >
        <router-link
            to="/settings"
            class="navbar-item flex-align-center"
            @click.native="clickItem"
            :class="{ clickItem: index == 5 }"
        >
            <div class="navbar-item-icon navbar-item-setting-icon"></div>
            <div class="navbar-item-text" v-show="showMenuText">
                Thiết lập hệ thống
            </div></router-link
        >
    </div>
</template>
<style scoped>
@import url("../../css/layout/navbar.css");
</style>
<script>
import $ from "jquery";
export default {
    name: "TheMenu",
    props: {
        showMenuText: {
            type: Boolean,
            default: true,
        },
    },
    data() {
        return {
            index: 4,
        };
    },
    created() {
        this.$router.push("/employee").catch(() => {});
    },
    methods: {
        clickItem: function (event) {
            let current = event.currentTarget;
            this.index = $(current.parentNode).children().index(current) + 1;
        },
    },
};
</script>