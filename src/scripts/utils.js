/**
 * Định dạng ngày thành chuỗi dạng "dd/mm/yyyy"
 * @param {any} date : chuỗi ngày cần định dạng
 * @returns chuổi string date đã được định dạng
 */
export function formatDate(date) {
    let formatedDate = "";
    let response = new Date(date);
    if (!date) {
        return "";
    }
    if (response.getDate() < 10) formatedDate += "0";
    formatedDate += response.getDate();
    if (response.getMonth() < 9) formatedDate += "/0";
    else formatedDate += "/";
    formatedDate += response.getMonth() + 1;
    formatedDate += "/" + response.getFullYear();
    return formatedDate;
}

export const convertGender = [
    { id: 0, text: "Nữ" },
    { id: 1, text: "Nam" },
    { id: 2, text: "Khác" },
];

export const convertWorkStatus = [
    { id: 0, text: "Đang làm việc" },
    { id: 1, text: "Đang thử việc" },
    { id: 2, text: "Đã nghỉ" },
    { id: 3, text: "Khác" },
];

export const departmentDictionary = {};
export const positionDictionary = {};
/**
 * Tranning dữ liệu nhận được từ api
 * @param {aby} object : object cần training
 * @returns object đã được train
 */
export function trainingData(object) {
    for (let attribute in object) {
        if (!object[attribute]) {
            object[attribute] = "";
        }
        switch (attribute) {
            case "JoinDate":
                object.JoinDate = formatDate(object.JoinDate);
                break;
            case "DateOfBirth":
                if (object.DateOfBirth)
                    object.DateOfBirth = formatDate(object.DateOfBirth);
                break;
            case "Gender":
                if (parseInt(object.Gender) >= 2) object.Gender = 2;
                if (parseInt(object.Gender) >= 0 && parseInt(object.Gender) <= 2)
                    object.GenderName = convertGender[parseInt(object.Gender)].text;
                break;
            case "WorkStatus":
                if (parseInt(object.WorkStatus) > 4) object.WorkStatus = 3;
                if (parseInt(object.WorkStatus) > -1 && parseInt(object.WorkStatus) < 4)
                    object.WorkStatus =
                        convertWorkStatus[parseInt(object.WorkStatus)].text;
                break;
            case "Salary":
                object.Salary = object.Salary.toLocaleString("it-IT");
                break;
            case "IdentityDate":
                object.IdentityDate = formatDate(object.IdentityDate);
                break;
        }
    }
    return object;
}
