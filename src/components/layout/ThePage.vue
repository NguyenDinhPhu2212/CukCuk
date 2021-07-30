<template>
    <div>
        <div class="page-content">
            <div class="flex-justify-space-between" style="margin: 0 16px">
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
                            v-model="keyWord"
                        />
                    </div>
                    <BaseCombobox
                        dropdownId="ftDepartmentDropdown"
                        inputId="ftDepartment"
                        :data="dataDepartment"
                        placeholder="Tất cả phòng ban"
                        @result="(result) => filter(result, 'department')"
                    />
                    <BaseCombobox
                        dropdownId="ftPositionDropdown"
                        inputId="ftPosition"
                        :data="dataPosition"
                        placeholder="Tất cả chức vụ"
                        @result="(result) => filter(result, 'position')"
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
                    <tbody ref="tbody">
                        <tr
                            class="row"
                            v-for="(employee, index) in employeeList"
                            :key="index"
                            @contextmenu="clickRow($event, employee)"
                            @dblclick="dbclickRow(employee)"
                            :class="{ grey: index % 2 == 1 }"
                        >
                            <td v-tooltip.top-center="employee.EmployeeCode">
                                {{ employee.EmployeeCode }}
                            </td>
                            <td v-tooltip.top-center="employee.FullName">
                                {{ employee.FullName }}
                            </td>
                            <td v-tooltip.top-center="employee.GenderName">
                                {{ employee.GenderName }}
                            </td>
                            <td v-tooltip.top-center="employee.DateOfBirth">
                                {{ employee.DateOfBirth }}
                            </td>
                            <td v-tooltip.top-center="employee.PhoneNumber">
                                {{ employee.PhoneNumber }}
                            </td>
                            <td v-tooltip.top-center="employee.Email">
                                {{ employee.Email }}
                            </td>
                            <td v-tooltip.top-center="employee.PositionName">
                                {{ employee.PositionName }}
                            </td>
                            <td v-tooltip.top-center="employee.DepartmentName">
                                {{ employee.DepartmentName }}
                            </td>
                            <td v-tooltip.top-center="employee.Salary">
                                {{ employee.Salary
                                }}<span
                                    style="font-style: italic; color: #454545"
                                >
                                    (VND)</span
                                >
                            </td>
                            <td v-tooltip.top-center="employee.WorkStatus">
                                {{ employee.WorkStatus }}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <Pagination
                ref="pagination"
                @currentPage="(index) => (current = index)"
            ></Pagination>
            <Loading></Loading>
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
        <Popup @clickConfirm="clickConfirm"></Popup>
    </div>
</template>

<script>
import BaseButton from "../base/Button.vue";
import TheEmployeeDetail from "./TheEmployeeDetail.vue";
import ContextMenu from "../base/ContextMenu.vue";
import * as utils from "../../scripts/utils.js";
import { api } from "../../mixins/api";
import { mapGetters, mapMutations } from "vuex";
import Pagination from "../base/Pagination.vue";
// import { getEmployeeFilter } from "../../scripts/api.js";
export default {
    name: "ThePage",
    mixins: [api],
    components: {
        BaseButton,
        TheEmployeeDetail,
        ContextMenu,
        Pagination,
    },
    data() {
        return {
            data: {},
            dataDepartment: [],
            dataPosition: [],
            dataWorkStatus: utils.convertWorkStatus,
            dataGender: utils.convertGender,
            current: 1,
            keyWord: "",
            filterData: {
                pageSize: 10,
                pageNumber: 0,
                employeeFilter: "NV",
                departmentId: "",
                positionId: "",
            },
        };
    },
    /**
     * Lấy dữ liệu từ api department và position
     */
    async created() {
        await this.getDepartment();
        this.dataDepartment = [...this.dataDepartment];
        await this.getPosition();
        this.dataPosition = [...this.dataPosition];
        this.$refs.pagination.currentPage = 1;
        await this.getData(1);
    },
    computed: {
        ...mapGetters({
            employeeList: "getEmployee",
            getPopup: "getPopup",
        }),
    },
    watch: {
        filterData: async function () {
            console.log(this.filterData);
            this.$store.commit("setFilter", this.filterData);
            this.$refs.pagination.currentPage = 1;
            await this.getData(1);
        },
        keyWord: function () {
            this.filterData.employeeFilter = this.keyWord;
            this.filterData = { ...this.filterData };
        },
    },
    methods: {
        ...mapMutations({
            setEmployee: "setEmployee",
            executeData: "setExecuteData",
            dialog: "setDialog",
            contextMenuPosition: "setContextMenu",
            setLoading: "setLoading",
            setPopup: "setPopup",
        }),
        clickConfirm: async function () {
            let type = this.getPopup.type;
            //Kiểm tra là loại popup
            if (type != "exit") {
                //lấy dữ liệu cần được sử dụng
                let executeData = this.$store.getters.getExecuteData;
                await this.execute(type, executeData);
            }
            //ẩn popup, dialog, contextmenu
            this.dialog(false);
            this.setPopup({ display: false });
            this.contextMenuPosition({
                display: false,
            });
            if (type != "exit") {
                await this.getData(this.current);
            }
        },
        //sự kiện khi nhấn nút thêm nhân viên
        clickAddBtn: function () {
            this.dialog(true);
            this.data = {};
        },
        //sự kiện khi nhấn nút refresh
        clickRefreshBtn: async function () {
            this.setLoading(true);
            let vm = this;
            setTimeout(async function () {
                if (vm.$refs.pagination.currentPage == 1) await vm.getData(1);
                else vm.$refs.pagination.currentPage = 1;
                vm.setLoading(false);
            }, 1000);
        },
        //sự kiện khi right click
        clickRow: function (event, employee) {
            event.preventDefault();

            this.$refs.tbody.children.forEach((element) => {
                element.classList.remove("row-clicked");
            });
            event.currentTarget.classList.add("row-clicked");
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
        async getData(index) {
            this.setLoading(true);
            let filter = { ...this.$store.getters.getFilter };
            filter.pageNumber = (index - 1) * 10 + 1;
            this.$store.commit("setFilter", filter);
            await this.$store.dispatch("employeeFilter");
            this.setLoading(false);
        },
        filter(result, type) {
            if (type == "department") {
                this.filterData.departmentId = result.id;
            } else this.filterData.positionId = result.id;
            this.filterData = { ...this.filterData };
        },
    },
};
</script><style scoped>
@import url("../../css/layout/page.css");
@import url("../../css/common/tooltip.css");
.page-content {
    position: relative;
}
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
.grey:hover {
    background-color: #bbbbbb;
}
</style>