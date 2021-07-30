<template>
    <div class="paging-bar flex-align-center">
        <div class="counter">
            Hiển thị <span class="begin">{{ begin }}</span> -
            <span class="end">{{ end }}</span
            >/ <span class="total-person">{{ total }}</span> nhân viên
        </div>
        <div class="pagination flex-justify-space-around">
            <div class="firstpage-button icon-default" @click="firstpage"></div>
            <div class="prev-page-button icon-default" @click="prev"></div>
            <div class="index-bar">
                <ul
                    class="page-list flex-align-center"
                    :style="{ 'margin-left': margin + 'px' }"
                >
                    <li
                        v-for="index in totalPage"
                        :key="index"
                        :class="{ 'current-page': index == currentPage }"
                        @click="currentPage = index"
                    >
                        {{ index }}
                    </li>
                </ul>
            </div>
            <div class="next-page-button icon-default" @click="next"></div>
            <div class="lastpage-button icon-default" @click="lastpage"></div>
        </div>
        <div class="number-per-page">
            <span class="number-person">10</span> nhân viên/trang
        </div>
    </div>
</template>
<style scoped>
@import url("../../css/common/flex.css");
@import url("../../css/base/pagination.css");
</style>
<script>
import { mapGetters, mapMutations } from "vuex";
import { getEmployeeFilter } from "../../scripts/api.js";
import * as utils from "../../scripts/utils.js";
export default {
    name: "Pagination",
    data() {
        return {
            currentPage: 0,
            total: 0,
            totalPage: 0,
            filter: {
            pageSize: 10,
            pageNumber: 1,
            employeeFilter: "NV",
            departmentId: "",
            positionId: "",
        },
        };
    },
    computed: {
        ...mapGetters({
            employeeList: "getEmployee",
        }),
        begin() {
            return (this.currentPage - 1) * 10 + 1;
        },
        end() {
            return this.currentPage * 10 < this.total
                ? this.currentPage * 10
                : this.total;
        },
        margin() {
            return Math.floor((this.currentPage - 1) / 4) * -190;
        },
    },
    watch: {
        currentPage: async function () {
            this.$emit("currentPage", this.currentPage);
            this.filter = {...this.$store.getters.getFilter}
            this.filter.pageNumber = (this.currentPage - 1) * 10 + 1;
            this.setFilter(this.filter);
            let employee = await this.tranning();
            console.log(employee)
            this.total = employee.TotalRecord;
            this.totalPage = employee.TotalPage;
            this.setEmployee(employee.Data);
        },
    },
    methods: {
        ...mapMutations({
            setEmployee: "setEmployee",
            setFilter:"setFilter",
        }),
        firstpage: function () {
            this.currentPage = 1;
        },
        prev: function () {
            if (this.currentPage <= 1) {
                this.currentPage = this.totalPage;
            } else this.currentPage -= 1;
        },
        next: function () {
            if (this.currentPage >= this.totalPage) {
                this.currentPage = 1;
            } else this.currentPage += 1;
        },
        lastpage: function () {
            this.currentPage = this.totalPage;
        },
        async tranning() {
            // let pageNumber = (this.currentPage - 1) * 10 + 1;
            this.filter = {...this.$store.getters.getFilter};
            let employee = await getEmployeeFilter(this.filter);
            for (let i in employee.Data) {
                utils.trainingData(employee.Data[i]);
            }
            return employee;
        },
    },
};
</script>