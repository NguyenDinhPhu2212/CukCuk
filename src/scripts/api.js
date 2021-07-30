import { API } from "../constants/api";
import axios from "axios";
export async function newCode() {
    let newCode = null;
    await axios.get(API.NEW_EMPLOYEE_CODE).then((response) => {
        newCode = response.data;
    });
    return newCode;
}
export async function getEmployeeFilter({
    pageSize,
    pageNumber,
    employeeFilter,
    departmentId,
    positionId,
} = {}) {
    let employee = {};
    let filterPath = "";
    if (pageSize) filterPath += "pageSize=" + pageSize;
    if (pageNumber) filterPath += "&pageNumber=" + pageNumber;
    if (employeeFilter) filterPath += "&employeeFilter=" + employeeFilter;
    if (departmentId) filterPath += "&departmentId=" + departmentId;
    if (positionId) filterPath += "&positionId=" + positionId;
    try {
        employee = await axios.get(API.EMPLOYEE_FILTER + filterPath);
    } catch (error) {
        console.log(error);
    }
    return employee.data;
}
export async function getDepartment() {
    let dataDepartment = {};
    await axios.get(API.DEPARTMENT).then((response) => {
        dataDepartment = { ...response };
    });
    return dataDepartment;
}
export async function getPosition() {
    let dataPosition = {};
    await axios.get(API.POSITION).then((response) => {
        dataPosition = { ...response };
    });
    return dataPosition;
}
export async function deleteEmployee(employee) {
    let response = {};
    try {
        response = await axios.delete(API.EMPLOYEE + "/" + employee.EmployeeId);
    } catch (error) {
        response = error;
    }
    return response;
}
export async function updateData(employee) {
    let response = {};
    try {
        response = await axios.put(
            API.EMPLOYEE + "/" + employee.EmployeeId,
            employee
        );
    } catch (error) {
        response = error;
    }
    return response;
}
export async function createNewEmployee(employee) {
    let response = {};
    try {
        response = await axios.post(API.EMPLOYEE, employee);
    } catch (error) {
        response = error;
    }
    return response;
}
