<template>
    <div class="loading" v-show="loading">
        <div class="loader"></div>
    </div>
</template>

<script>
export default {
    name: "Loading",
    computed: {
        loading: function () {
            return this.$store.getters.getLoading;
        },
    },
};
</script>
<style scoped>
.loading{
    width: 100%;
    height: 100%;
    position: absolute;
    top: 0;
    z-index: 10000;
    background-color: rgba(186, 186, 186, 0.3);
    display: flex;
    justify-content: center;
    align-items: center;
}
.loader {
    border: 8px solid #f3f3f3;
    border-top: 6px solid rgb(68, 68, 68);
    border-bottom: 6px solid rgb(68, 68, 68);
    border-radius: 50%;
    width: 40px;
    height: 40px;
    animation: spin 2s linear infinite;
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
</style>