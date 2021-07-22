<template>
    <div class="employee-list-header flex-align-center">
        <div class="logo-box flex-align-center">
            <div class="toggle-icon" @click="clickToggleBtn" ></div>
            <div class="logo"></div>
        </div>
        <div
            class="flex-justify-space-between"
            style="width: 100%; margin: auto"
        >
            <BaseCombobox
                class="mf-10px"
                dropdownId="restaurantDropdown"
                inputId="restaurant"
                :data="[{id: 0, text: 'Nhà hàng Biển Đông'}]"
                border="none"
            />

            <div class="user-box flex-align-center">
                <img
                    src="../../assets/resource/icon/avatar-default.png"
                    alt=""
                />
                <div class="username">Nguyễn Văn A</div>
                <img
                    src="../../assets/resource/icon/option.png"
                    alt=""
                    class="icon"
                />
            </div>
        </div>
    </div>
</template>
<style scoped>
@import url("../../css/layout/header.css");
@import url("../../css/common/flex.css");
</style>
<script>
export default {
    name: "TheHeader",
    methods: {
        clickToggleBtn: function () {
            this.$emit("clickToggleBtn");
        },
    },
};
</script>