<template>
    <div
        class="employee-information-form"
        v-show="this.$store.getters.getDialog"
    >
        <div class="employee-detail">
            <div class="form-header">
                <div class="form-header-title">THÔNG TIN NHÂN VIÊN</div>
                <div class="exit-button" @click="clickExitBtn">
                    <i class="far fa-times"></i>
                </div>
            </div>
            <div class="employee-detail-content">
                <div class="avatar">
                    <img
                        src="../../assets/resource/img/default-avatar.jpg"
                        alt=""
                        class="avatar-default"
                    />
                    <div class="note">
                        (Vui lòng chọn ảnh có định dạng .jpg .jpeg .png .gif)
                    </div>
                </div>

                <div class="employee-information">
                    <div class="general-information">
                        <div class="form-title">A.THÔNG TIN CHUNG</div>
                        <form class="general-information-form flex-column">
                            <div
                                class="
                                    employee-id
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <BaseInput
                                    label="Mã nhân viên:"
                                    :required="true"
                                    :tabindex="1"
                                    v-model="currentEmployee.EmployeeCode"
                                    ref="check-1"
                                />
                                <BaseInput
                                    label="Họ và tên:"
                                    :required="true"
                                    :tabindex="2"
                                    v-model="currentEmployee.FullName"
                                    ref="check-2"
                                />
                            </div>

                            <div class="flex-justify-space-between flex-wrap">
                                <BaseInput
                                    type="date"
                                    label="Ngày sinh:"
                                    :tabindex="3"
                                    v-model="currentEmployee.DateOfBirth"
                                />
                                <div class="flex-column">
                                    <label for="gender"
                                        >Giới tính (<span style="color: red"
                                            >*</span
                                        >)</label
                                    >
                                    <BaseCombobox
                                        dropdownId="genderDropdown"
                                        inputId="gender"
                                        :data="dataGender"
                                        :choosed="{
                                            ...{
                                                id: currentEmployee.Gender,
                                                text: currentEmployee.GenderName,
                                            },
                                        }"
                                        :tabindex="4"
                                        @result="
                                            (result) =>
                                                getData(result, 'gender')
                                        "
                                        required
                                        ref="check-3"
                                    />
                                </div>
                            </div>
                            <div
                                class="
                                    identity-information
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <BaseInput
                                    label="Số CMTND/ Căn cước:"
                                    :required="true"
                                    :tabindex="5"
                                    format="number"
                                    v-model="currentEmployee.IdentityNumber"
                                    ref="check-4"
                                />

                                <BaseInput
                                    label="Nơi cấp:"
                                    :tabindex="6"
                                    v-model="currentEmployee.IdentityPlace"
                                />

                                <BaseInput
                                    type="date"
                                    label="Ngày cấp:"
                                    :tabindex="7"
                                    v-model="currentEmployee.IdentityDate"
                                />
                            </div>

                            <div
                                class="
                                    contact-method
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <BaseInput
                                    label="Email:"
                                    type="email"
                                    :required="true"
                                    :tabindex="8"
                                    v-model="currentEmployee.Email"
                                    ref="check-5"
                                />
                                <BaseInput
                                    label="Số điện thoại:"
                                    type="tel"
                                    :required="true"
                                    :tabindex="9"
                                    format="number"
                                    v-model="currentEmployee.PhoneNumber"
                                    ref="check-6"
                                />
                            </div>
                        </form>
                    </div>

                    <div class="job-information">
                        <div class="form-title">B.THÔNG TIN CÔNG VIỆC</div>
                        <form class="job-information-form flex-column">
                            <div
                                class="
                                    office
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <div class="flex-column">
                                    <label for="position">Vị trí</label>
                                    <BaseCombobox
                                        dropdownId="positionDropdown"
                                        inputId="position"
                                        :data="dataPosition"
                                        :choosed="{
                                            ...{
                                                id: currentEmployee.PositionId,
                                                text: currentEmployee.PositionName,
                                            },
                                        }"
                                        :tabindex="10"
                                        @result="
                                            (result) =>
                                                getData(result, 'position')
                                        "
                                    />
                                </div>
                                <div class="flex-column">
                                    <label for="department">Phòng ban</label>
                                    <BaseCombobox
                                        dropdownId="departmentDropdown"
                                        inputId="department"
                                        :data="dataDepartment"
                                        :choosed="{
                                            ...{
                                                id: currentEmployee.DepartmentId,
                                                text: currentEmployee.DepartmentName,
                                            },
                                        }"
                                        :tabindex="11"
                                        @result="
                                            (result) =>
                                                getData(result, 'department')
                                        "
                                    />
                                </div>
                            </div>
                            <div
                                class="
                                    salary-information
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <BaseInput
                                    label="Mã số thuế cá nhân:"
                                    type="number"
                                    :tabindex="12"
                                    format="number"
                                    v-model="currentEmployee.PersonalTaxCode"
                                />
                                <BaseInput
                                    type="text"
                                    label="Mức lương cơ bản:"
                                    :tabindex="13"
                                    format="currency"
                                    v-model="currentEmployee.Salary"
                                />
                            </div>
                            <div
                                class="
                                    working-status
                                    flex-justify-space-between flex-wrap
                                "
                            >
                                <BaseInput
                                    label="Ngày gia nhập công ty:"
                                    type="date"
                                    :tabindex="14"
                                    v-model="currentEmployee.JoiningDate"
                                />

                                <div class="flex-column">
                                    <label for="job-status"
                                        >Tình trạng công việc</label
                                    >
                                    <BaseCombobox
                                        dropdownId="workStatusDropdown"
                                        inputId="work-status"
                                        :data="dataWorkStatus"
                                        :choosed="{
                                            ...workStatus,
                                        }"
                                        :tabindex="15"
                                        @result="
                                            (result) =>
                                                getData(result, 'workStatus')
                                        "
                                    />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="footer">
                <div class="destroy-button" @click="clickExitBtn">Hủy</div>
                <div class="button save-button" @click="clickSaveBtn">
                    <div class="save-button-icon"></div>
                    <div>Lưu</div>
                </div>
            </div>
        </div>
    </div>
</template>
<style scoped>
@import url("../../css/common/flex.css");
@import url("../../css/layout/detail.css");
</style>

<script>
export default {
    name: "TheEmployeeDetail",
    props: {
        data: Object,
        dataDepartment: Array,
        dataPosition: Array,
        dataWorkStatus: Array,
        dataGender: Array,
    },
    data() {
        return {
            exitPopup: {
                display: true,
                type: "exit",
                title: "Thoát form thông tin",
                showWarning: true,
                warningIconColor: "#ffbb00",
                popupContent: "Bạn có chắc muốn thoát hay không?",
                showCloseBtn: true,
                closeBtn: "Tiếp tục nhập",
                confirmBtn: "Thoát",
                confirmColor: "#ffbb00",
            },
            savePopup: {
                display: true,
                type: "update",
                title: "Lưu bản ghi",
                showWarning: true,
                warningIconColor: "#01B075",
                popupContent: "Bạn có chắc muốn lưu bản ghi không?",
                showCloseBtn: true,
                closeBtn: "Tiếp tục nhập",
                confirmBtn: "Lưu",
                confirmColor: "#01B075",
            },
            executeData: {},
        };
    },
    computed: {
        //lấy ra work status của nhân viên hiện tại
        workStatus: function () {
            return { ...this.createWS(this.data.WorkStatus) };
        },
        //format lại dateString
        currentEmployee: function () {
            return {
                ...this.data,
                Gender: this.data?.Gender?.toString(),
                DateOfBirth: this.formatDate(this.data?.DateOfBirth),
                IdentityDate: this.formatDate(this.data?.IdentityDate),
                JoinDate: this.formatDate(this.data?.JoinDate),
                Salary: this.data?.Salary?.toString(),
            };
        },
    },
    methods: {
        //lấy ra object work status
        createWS(workStatus) {
            return this.dataWorkStatus.find((element) => {
                return element.text == workStatus;
            });
        },
        //sự kiện khi nhấn nút thoát dialog
        clickExitBtn: function () {
            this.$store.commit("setPopup", this.exitPopup);
            Object.entries(this.$refs).forEach((component) => {
                component[1].error = false;
            });
        },
        //sự kiện khi nhấn nút lưu
        clickSaveBtn: function () {
            let errors = [];
            Object.entries(this.$refs).forEach((component, index) => {
                component[1].$refs.BaseInput.focus();
                component[1].$refs.BaseInput.blur();
                if (component[1].error) {
                    errors.push(index);
                }
            });
            if (!errors.length) {
                //kiểm tra xem người dùng đang update hay tạo mới
                if (!Object.keys(this.data).length)
                    this.savePopup.type = "create";
                else this.savePopup.type = "update";

                let currentEmployee = this.currentEmployee;
                //lấy dữ liệu cuối cùng
                this.update(this.executeData, currentEmployee);
                console.log(this.executeData);
                this.$store.commit("setExecuteData", this.executeData);
                this.$store.commit("setPopup", this.savePopup);
            }
        },
        //format dateString theo định dạng yyyy-mm-dd
        formatDate: function (dateString) {
            if (dateString) {
                return dateString?.split("/").reverse().join("-");
            }
        },
        //cập nhật data
        getData(result, type) {
            switch (type) {
                case "gender":
                    this.executeData.Gender = result.id;
                    this.executeData.GenderName = result.text;
                    break;
                case "position":
                    this.executeData.PositionId = result.id;
                    this.executeData.PositionName = result.text;
                    break;
                case "department":
                    this.executeData.DepartmentId = result.id;
                    this.executeData.DepartmentName = result.text;
                    break;
                case "workStatus":
                    this.executeData.WorkStatus = result.id;
                    break;
            }
            this.executeData = { ...this.executeData };
        },
        update(dest, source) {
            for (let key in source) {
                if (
                    key !== "Gender" &&
                    key !== "GenderName" &&
                    key !== "PositionId" &&
                    key !== "PositionName" &&
                    key !== "DepartmentId" &&
                    key !== "DepartmentName" &&
                    key !== "WorkStatus"
                ) {
                    if (key == "Salary") {
                        dest[key] = Number(source[key]?.split(".").join(""));
                    } else dest[key] = source[key];
                }
            }
        },
    },
};
</script>