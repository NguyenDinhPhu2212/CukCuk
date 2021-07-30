<template>
    <!-- <div class="toast-message-container" v-show="toast.data.display"> -->
    <div class="toast-message">
        <div
            class="message-icon"
            :style="{ 'background-color': toast.data.color }"
        >
            <i :class="toast.data.iconClass"></i>
        </div>
        <div class="message-content" v-html="toast.data.content"></div>
        <div
            class="x-btn"
            :style="{ color: toast.data.color }"
            @click="clicked"
        >
            <i class="fal fa-times"></i>
        </div>
    </div>
    <!-- </div> -->
</template>
<style scoped>
@import url("../../css/base/message.css");
</style>
<script>
import {TIME} from "../../constants/time"
export default {
    name: "ToastMessage",
    props: {
        toast: { type: Object, default: () => {} },
    },
    data() {
        return {};
    },
    //tự động xoá toast khỏi mảng sau 3s
    mounted() {
        let vm= this;
        setTimeout(function () {
            vm.$store.commit("deleteToast", vm.toast.index);
        }, TIME.TOAST_DURATION);
    },
    methods: {
        //sự kiện khi nhấn tắt toast message
        clicked: function () {
            this.$store.commit("deleteToast", this.toast.index);
        },
    },
};
</script>