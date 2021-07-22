import axios from "axios";
import * as utils from "../scripts/utils.js";
import { mapMutations } from "vuex";
export const api = {
    data() {
        return {
            apiEmployee: "http://cukcuk.manhnv.net/v1/Employees",
            apiDepartment: "http://cukcuk.manhnv.net/api/Department",
            apiPosition: "http://cukcuk.manhnv.net/v1/Positions",
        };
    },
    methods: {
        ...mapMutations({
            setDataLength: "setDataLength",
            setEmployee: "setEmployee",
            addToast: "addToast",
        }),
        async getEmployee() {
            await axios.get(this.apiEmployee).then((response) => {
                response.data.forEach((value) => {
                    utils.trainingData(value);
                });
                this.setEmployee(response.data);
            });
        },
        async getDepartment() {
            await axios.get(this.apiDepartment).then((response) => {
                for (let item of response.data) {
                    let tmp = {
                        id: item.DepartmentId,
                        text: item.DepartmentName,
                    };
                    this.dataDepartment.push(tmp);
                }
            });
        },
        async getPosition() {
            await axios.get(this.apiPosition).then((response) => {
                for (let item of response.data) {
                    let tmp = { id: item.PositionId, text: item.PositionName };
                    this.dataPosition.push(tmp);
                }
            });
        },
        async deleteEmployee(employee) {
            await axios
                .delete(this.apiEmployee + "/" + employee.EmployeeId)
                .then(() => {
                    this.addToast({
                        iconClass: "far fa-check",
                        content: "Thành công",
                        color: "#01B075",
                    });
                });
        },
        async updateData(employee) {
            try {
                await axios.put(
                    `http://cukcuk.manhnv.net/v1/Employees/${employee.EmployeeId}`,
                    employee
                );
            } catch (error) {
                alert(JSON.stringify(error));
                console.log(error);
            }
        },
        async createNewEmployee(employee) {
            try {
                await axios.post(
                    "http://cukcuk.manhnv.net/v1/Employees",
                    employee
                );
            } catch (error) {
                alert(JSON.stringify(error));
                console.log(error);
            }
        },
    },
};
