<template>
    <div>
        <div class="display">
            <div class="flex-justify-space-between" style="margin: 10px 16px">
                <h3>Danh sách nhân viên</h3>
                <BaseButton
                    v-on:clicked="clickAddBtn"
                    class="add-employee-button"
                    btnContent="Thêm nhân viên"
                    :showIcon="true"
                />
            </div>
            <div class="filter-bar flex-justify-space-between">
                <form class="filter-form">
                    <div class="input-information">
                        <input
                            class="input-text icon-search"
                            id="ftText"
                            placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
                        />
                    </div>
                    <BaseCombobox
                        dropdownId="ftDepartmentDropdown"
                        inputId="ftDepartment"
                        :data="dataDepartment"
                    />

                    <BaseCombobox
                        dropdownId="ftPositionDropdown"
                        inputId="ftPosition"
                        :data="dataPosition"
                    />
                </form>
                <div id="refresh" class="refresh-btn" @click="clickRefreshBtn">
                    <img src="../../assets/resource/icon/refresh.png" alt="" />
                </div>
            </div>
            <div class="employee-list">
                <table class="table" id="employee-table" cellspacing="0">
                    <colgroup>
                        <col style="width: 120px" />
                        <col style="width: 200px" />
                        <col style="width: 100px" />
                        <col style="width: 120px" />
                        <col style="width: 120px" />
                        <col style="width: 250px" />
                        <col style="width: 200px" />
                        <col style="width: 180px" />
                        <col style="width: 160px" />
                        <col style="width: 200px" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th fieldname="EmployeeCode">Mã nhân viên</th>
                            <th fieldname="FullName">Họ và tên</th>
                            <th fieldname="GenderName">Giới tính</th>
                            <th fieldname="DateOfBirth">Ngày sinh</th>
                            <th fieldname="PhoneNumber">Điện thoại</th>
                            <th fieldname="Email">Email</th>
                            <th fieldname="PositionName">Chức vụ</th>
                            <th fieldname="DepartmentName">Phòng ban</th>
                            <th fieldname="Salary">Mức lương cơ bản</th>
                            <th fieldname="WorkStatus">Tình trạng công việc</th>
                        </tr>
                    </thead>
                    <tbody
                        v-for="(employee, index) in employeeList"
                        :key="index"
                    >
                        <tr
                            class="row"
                            @contextmenu="clickRow($event, employee)"
                            @dblclick="dbclickRow(employee)"
                            :class="{grey: index %2 == 1}"
                        >
                            <td>{{ employee.EmployeeCode }}</td>
                            <td>{{ employee.FullName }}</td>
                            <td>{{ employee.GenderName }}</td>
                            <td>{{ employee.DateOfBirth }}</td>
                            <td>{{ employee.PhoneNumber }}</td>
                            <td>{{ employee.Email }}</td>
                            <td>{{ employee.PositionName }}</td>
                            <td>{{ employee.DepartmentName }}</td>
                            <td>
                                {{ employee.Salary
                                }}<span
                                    style="font-style: italic; color: #454545"
                                >
                                    (VND)</span
                                >
                            </td>
                            <td>{{ employee.WorkStatus }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="paging-bar flex-align-center">
                <div class="counter">
                    Hiển thị <span class="begin">1</span>-
                    <span class="end">10</span>/
                    <span class="total-person">1000</span> nhân viên
                </div>
                <div class="pagination flex-justify-space-around">
                    <div class="firstpage-button icon-default"></div>
                    <div class="prev-page-button icon-default"></div>
                    <div class="index-bar">
                        <ul class="page-list flex-align-center">
                            <li v-for="index in dataLength" :key="index">
                                {{ index }}
                            </li>
                        </ul>
                    </div>
                    <div class="next-page-button icon-default"></div>
                    <div class="lastpage-button icon-default"></div>
                </div>
                <div class="number-per-page">
                    <span class="number-person">10</span> nhân viên/trang
                </div>
            </div>
        </div>
        <TheEmployeeDetail
            :data="data"
            :dataDepartment="dataDepartment"
            :dataPosition="dataPosition"
            :dataWorkStatus="dataWorkStatus"
            :dataGender="dataGender"
        />
        <ContextMenu ref="contextMenu" />
        <ToastGroup></ToastGroup>
        <Popup></Popup>
    </div>
</template>
<style scoped>
@import url("../../css/layout/page.css");
::-webkit-scrollbar {
    height: 5px;
    width: 5px;
}

::-webkit-scrollbar-track {
    box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
}

::-webkit-scrollbar-thumb {
    background-color: rgb(66, 66, 66);
}
.grey {
    background-color: #e5e5e5;
}
.grey:hover{
    background-color: #bbbbbb;
}
</style>
<script>
import BaseButton from "../base/BaseButton.vue";
import TheEmployeeDetail from "./TheEmployeeDetail.vue";
import ContextMenu from "../base/ContextMenu.vue";
import ToastGroup from "../base/ToastGroup.vue";
import Popup from "../base/Popup.vue";
import * as utils from "../../scripts/utils.js";
import { api } from "../../mixins/api";
import $ from "jquery";
import { mapGetters, mapMutations } from "vuex";
export default {
    name: "ThePage",
    mixins: [api],
    components: {
        BaseButton,
        TheEmployeeDetail,
        ContextMenu,
        ToastGroup,
        Popup,
    },
    data() {
        return {
            data: {},
            dataDepartment: [],
            dataPosition: [],
            dataWorkStatus: utils.convertWorkStatus,
            dataGender: utils.convertGender,
        };
    },
    /**
     * Lấy dữ liệu từ api department và position
     */
    async created() {
        await this.getEmployee();
        await this.getDepartment();
        this.dataDepartment = [...this.dataDepartment];
        await this.getPosition();
        this.dataPosition = [...this.dataPosition];
    },
    computed: {
        ...mapGetters({
            employeeList: "getEmployee",
            dataLength: "getDataLength",
        }),
    },
    methods: {
        ...mapMutations({
            executeData: "setExecuteData",
            dialog: "setDialog",
            contextMenuPosition: "setContextMenu",
        }),
        //sự kiện khi nhấn nút thêm nhân viên
        clickAddBtn: function () {
            this.dialog(true);
            this.data = {};
        },
        //sự kiện khi nhấn nút refresh
        clickRefreshBtn: async function () {
            await this.getEmployee();
        },
        //sự kiện khi right click
        clickRow: function (event, employee) {
            event.preventDefault();
            $(".row").removeClass("row-clicked");
            $(event.currentTarget).addClass("row-clicked");
            this.executeData(employee);
            this.data = { ...employee };
            this.contextMenuPosition({
                display: true,
                top: event.clientY - 20,
                left: event.clientX,
            });
        },
        //sự kiện khi nhấn đúp
        dbclickRow: function (employee) {
            this.dialog(true);
            this.data = { ...employee };
        },
    },
};
</script>