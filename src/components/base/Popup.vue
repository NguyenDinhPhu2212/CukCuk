<template>
    <div class="popup" v-show="getPopup.display">
        <div class="popup-header">
            <div class="popup-title">
                <div class="title-content" v-html="getPopup.title"></div>
            </div>
            <div class="x-btn" @click="closePopup">
                <i class="fal fa-times"></i>
            </div>
        </div>
        <div class="popup-content">
            <div
                class="warning-icon"
                v-show="getPopup.showWarning"
                :style="{ color: getPopup.warningIconColor }"
            >
                <i class="fas fa-exclamation-triangle"></i>
            </div>
            <div
                class="popup-content-text"
                v-html="getPopup.popupContent"
            ></div>
        </div>
        <div class="popup-footer">
            <div
                class="close-popup-btn"
                v-show="getPopup.showCloseBtn"
                v-html="getPopup.closeBtn"
                @click="closePopup"
            ></div>
            <div
                class="confirm-btn"
                v-html="getPopup.confirmBtn"
                @click="clickConfirm"
                :style="{ 'background-color': getPopup.confirmColor }"
            ></div>
        </div>
    </div>
</template>
<style scoped>
@import url("../../css/base/popup.css");
</style>
<script>
import { api } from "../../mixins/api";
import { mapGetters, mapMutations } from "vuex";
export default {
    name: "Popup",
    mixins: [api],
    data() {
        return {};
    },
    created() {
        window.addEventListener("click", (event) => {
            if (!this.$el.contains(event.target)) {
                this.showOption = false;
            }
        });
    },
    computed: {
        ...mapGetters({
            getPopup: "getPopup",
        }),
    },
    methods: {
        ...mapMutations({
            setPopup: "setPopup",
            contextMenuPosition: "setContextMenu",
            setDialog: "setDialog",
        }),
        //sự kiện khi nhấn tắt popup
        closePopup: function () {
            this.setPopup({ display: false, type: {} });
        },
        //sự kiện khi nhấn confirm popup
        clickConfirm: async function () {
            let type = this.getPopup.type;
            //Kiểm tra là loại popup
            if (type != "exit") {
                //lấy dữ liệu cần được sử dụng
                let executeData = this.$store.getters.getExecuteData;
                if (type == "delete") {
                    await this.deleteEmployee(executeData);
                } else if (type == "update") {
                    await this.updateData(executeData);
                } else if (type == "create") {
                    await this.createNewEmployee(executeData);
                }
                await this.getEmployee();
            }
            //ẩn popup, dialog, contextmenu
            this.setDialog(false);
            this.setPopup({ display: false, type: {} });
            this.contextMenuPosition({
                display: false,
                top: 0,
                left: 0,
            });
        },
    },
};
</script>