<template>
    <div class="default-size">
        <label v-if="label"
            >{{ label }}{{ " "
            }}<span v-if="required"
                >(<span style="color: red">*</span>)</span
            ></label
        >
        <input
            v-bind="$attrs"
            :required="required"
            :type="type"
            :class="{
                'notice-border': error,
                'right-lign': format == 'currency',
            }"
            :tabindex="tabindex"
            :value="inputValue"
            :fieldName="fieldName"
            @input="updateValue($event.target.value)"
            @keypress="keyPress"
            @blur="onBlur"
            autocomplete="off"
            ref="BaseInput"
            v-tooltip="{
                content: msg,
                show: error,
                trigger: 'manual',
            }"
        />
        <div v-show="format == 'currency'" class="currency">(VND)</div>
    </div>
</template>

<script>
export default {
    inheritAttrs: false,
    name: "BaseInput",
    props: {
        type: { type: String, default: "text" },
        required: { type: Boolean, default: false },
        label: String,
        value: { type: String, default: "" },
        tabindex: Number,
        format: { type: String, default: "text" },
        fieldName: String,
    },
    data() {
        return {
            error: false,
            msg: "",
            inputValue: "",
        };
    },
    watch: {
        value: function () {
            if (this.format === "currency")
                this.inputValue = this.formatCurrency(this.value);
            this.inputValue = this.value;
        },
    },
    methods: {
        updateValue(value) {
            if (this.format === "currency") {
                value = this.formatCurrency(value);
            }
            this.$emit("input", value);
            this.inputValue = value;
        },
        onBlur(event) {
            let value = event.target.value;
            let type = this.fieldName;
            if (this.required && value == "") {
                this.msg = "Vui lòng nhập thông tin.";
                this.error = true;
                return;
            }
            if (type == "Email" && !this.validateEmail(value)) {
                this.msg = "Vui lòng nhập email chính xác.";
                this.error = true;
                return;
            } else if (
                type == "PhoneNumber" &&
                !this.validatePhoneNumber(value)
            ) {
                this.msg = "Vui lòng nhập chính xác số điện thoại.";
                this.error = true;
                return;
            } else if (type == "Identity" && !this.validateIdentity(value)) {
                this.msg = "Vui lòng nhập chính xác số CMND/Căn cước.";
                this.error = true;
                return;
            }
            this.error = false;
        },
        keyPress(e) {
            if (this.format === "currency" || this.format === "number") {
                if (e.key < "0" || e.key > "9") {
                    e.preventDefault();
                }
            }
        },
        validateEmail(email) {
            const regex = new RegExp(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
            return regex.test(email);
        },
        validatePhoneNumber(phoneNumber) {
            const regex = new RegExp(
                /^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$/
            );
            return regex.test(phoneNumber);
        },
        validateIdentity(identity) {
            return identity.length == 12 || identity.length == 9;
        },
        formatCurrency(str) {
            if (str === "") return "";
            return Intl.NumberFormat()
                .format(Number(str?.replaceAll(".", "")?.replaceAll(",", "")))
                .toString();
        },
    },
};
</script>
<style scoped>
@import url("../../css/common/tooltip.css");

.default-size {
    width: 48% !important;
    margin-bottom: 16px;
    position: relative;
}

.border-focus {
    border: 1px solid #01b075 !important;
    border-radius: 6px;
}
.notice-border {
    border: 1px solid red !important;
    border-radius: 4px;
}
input {
    border: 1px solid #bbbbbb;
    outline: none;
    box-shadow: none;
    width: calc(100% - 16px);
    height: 40px !important;
    padding-left: 16px;
    border-radius: 6px;
    margin-top: 5px;
}
.right-lign {
    padding-right: 50px;
    text-align: right;
    width: calc(100% - 67px);
}
.currency {
    font-size: 13px;
    position: absolute;
    top: 38px;
    right: 5px;
    font-style: italic !important;
    color: grey;
}
</style>