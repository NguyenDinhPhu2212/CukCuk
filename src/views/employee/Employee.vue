<template>
    <div id="employee" class="GGSans-Regular flex-column">
        <TheHeader v-on:clickToggleBtn="clickToggleBtn" />
        <div style="display: flex; height: 100%; width: 100%">
            <TheMenu
                :style="{ width: width + 'px' }"
                :showMenuText="showMenuText"
            />
            <router-view
                :style="{ width: 'calc(100% - ' + width + 'px)' }"
            ></router-view>
        </div>
    </div>
</template>
 <style lang="css" scoped>
@import url("../../css/common/flex.css");
@import url("../../css/common/font.css");
@import url("https://pro.fontawesome.com/releases/v5.12.0/css/all.css");
#employee {
    height: 100vh;
    width: 100vw;
    position: relative;
    font-family: "Google Sans-Regular", sans-serif !important;
    -moz-user-select: none;
    -ms-user-select: none;
    -khtml-user-select: none;
    -webkit-user-select: none;
    -webkit-touch-callout: none;
}
</style>
 <script>
import TheMenu from "../../components/layout/TheMenu.vue";
import TheHeader from "../../components/layout/TheHeader.vue";

export default {
    name: "Employee",
    components: {
        TheHeader,
        TheMenu,
    },
    props: {},
    data() {
        return {
            width: 226,
            showMenuText: true,
        };
    },

    methods: {
        clickToggleBtn: function () {
            this.width = this.width == 60 ? 226 : 60;
            this.showMenuText = !this.showMenuText;
        },
    },
};
</script>