<template>
    <div
        class="row-service"
        v-show="getInfor.display"
        :style="{ top: getInfor.top + 'px', left: getInfor.left + 'px' }"
    >
        <div>
            <div class="update" @click="clickUpdate">Update</div>
            <div class="delete" @click="clickDelete">Delete</div>
        </div>
    </div>
</template>
<style scoped>
.row-service {
    position: absolute;
}
.row-service > div {
    display: flex;
    position: absolute;
    font-size: 13px;
    height: 40px;
    width: 160px;
    border-radius: 6px;
}
.row-service > div > * {
    color: white;
    text-align: center;
    cursor: pointer;
    height: 100%;
    width: 50%;
    border-radius: 6px;
    display: flex;
    justify-content: center;
    align-items: center;
}
.update {
    background-color: #2fbe8e;
}
.delete {
    background-color: #f65454;
}
.update:hover {
    background-color: #019160;
}
.delete:hover {
    background-color: #fa3636;
}
</style>
<script>
import { api } from "../../mixins/api";
export default {
    name: "ContextMenu",
    mixins: [api],
    created() {
        //sự kiện khi nhấn ra ngoiaf context menu
        window.addEventListener("click", (event) => {
            if (!this.$el.contains(event.target)) {
                this.$store.commit("setContextMenu", {
                    display: false,
                    top: 0,
                    left: 0,
                });
            }
        });
    },
    data() {
        return {
            popup: {
                display: true,
                type: "delete",
                title: "Xoá bản ghi",
                showWarning: true,
                warningIconColor: "#F65454",
                popupContent: "Bạn có chắc muốn xoá bản ghi này?",
                showCloseBtn: true,
                closeBtn: "Huỷ",
                confirmBtn: "Xoá",
                confirmColor: "#F65454",
            },
        };
    },
    computed: {
        getInfor: function () {
            return this.$store.getters.getContextMenu;
        },
    },
    methods: {
        clickUpdate: function () {
            this.$store.commit("setDialog", true);
        },
        clickDelete: function () {
            this.$store.commit("setPopup", this.popup);
        },
    },
};
</script>