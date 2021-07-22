<template>
    <div class="toast-group">
        <transition-group name="list" tag="div">
            <ToastMessage
                v-for="(toast, index) in toastGroup"
                :key="index"
                :toast="{ data: toast, index: index }"
                class="list-item"
            ></ToastMessage>
        </transition-group>
    </div>
</template>
<style scoped>
.toast-group {
    position: absolute;
    top: 20px;
    left: calc(50% - 150px);
}
.list-item {
    margin: 5px auto;
    transition: all 1s;
}
.list-enter-active,
.list-leave-active {
    transition: all 1s ease;
}
.list-leave-active {
    position: absolute;
}
.list-enter,
.list-leave-to {
    opacity: 0;
    transform: translateY(-50px);
}
</style>
<script>
import ToastMessage from "./BaseToastMessage.vue";
export default {
    name: "ToastGroup",
    components: { ToastMessage },
    computed: {
        toastGroup: function () {
            return this.$store.getters.getToast;
        },
    },
};
</script>