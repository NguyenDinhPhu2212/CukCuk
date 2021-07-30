
// import * as utils from "../scripts/utils.js";
import { mapMutations } from "vuex";

import { TOAST } from "../constants/toast";
import * as ApiFunction from "../scripts/api.js";
export const api = {
    data() {
        return {};
    },
    methods: {
        ...mapMutations({
            setEmployee: "setEmployee",
            addToast: "addToast",
            setLoading: "setLoading",
        }),
        async getDepartment() {
            const response = await ApiFunction.getDepartment();
            if (response.status == 200) {
                for (let item of response.data) {
                    let tmp = {
                        id: item.DepartmentId,
                        text: item.DepartmentName,
                    };
                    this.dataDepartment.push(tmp);
                }
            }
        },
        async getPosition() {
            const response = await ApiFunction.getPosition();
            if (response.status == 200) {
                for (let item of response.data) {
                    let tmp = {
                        id: item.PositionId,
                        text: item.PositionName,
                    };
                    this.dataPosition.push(tmp);
                }
            }
        },
        async deleteEmployee(employee) {
            const response = await ApiFunction.deleteEmployee(employee);
            if (response.status == 200) {
                this.addToast(TOAST.SUCCESS_DELETE);
            } else {
                this.addToast(TOAST.DENY);
            }
        },
        async updateData(employee) {
            const response = await ApiFunction.updateData(employee);
            if (response.status == 200) {
                this.addToast(TOAST.SUCCESS_DELETE);
            } else {
                this.addToast(TOAST.DENY);
            }
        },
        async createNewEmployee(employee) {
            const response = await ApiFunction.createNewEmployee(employee);
            if (response.status == 200) {
                this.addToast(TOAST.SUCCESS_DELETE);
            } else {
                this.addToast(TOAST.DENY);
            }
        },
        async execute(type, data) {
            if (type == "update") {
                await this.updateData(data);
            } else if (type == "delete") {
                await this.deleteEmployee(data);
            } else if (type == "create") {
                await this.createNewEmployee(data);
            }
        },
    },
};
