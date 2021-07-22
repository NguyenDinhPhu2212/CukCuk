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
                'border-focus': isTrue,
                'right-lign': format == 'currency',
            }"
            :value="formatValue"
            :tabindex="tabindex"
            @input="updateValue($event)"
            @blur="onBlur($event, $event.target.value)"
            @keypress="keyPress($event)"
            autocomplete="off"
            ref="BaseInput"
        />
        <div v-show="format == 'currency'" class="currency">(VND)</div>
    </div>
</template>
<style scoped>
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
}
.right-lign {
    padding-right: 50px;
    text-align: right;
    width: calc(100% - 67px);
}
.currency {
    font-size: 13px;
    position: absolute;
    top: 32px;
    right: 5px;
    font-style: italic !important;
    color: grey;
}
</style>
<script>
export default {
    inheritAttrs: false,
    name: "BaseInput",
    props: {
        type: { type: String, default: "text" },
        required: { type: Boolean, default: false },
        label: String,
        value: String,
        tabindex: Number,
        format: { type: String, default: "text" },
    },
    data() {
        return {
            isTrue: false,
            error: false,
        };
    },
    created() {
        window.addEventListener("click", (event) => {
            if (!this.$el.contains(event.target)) {
                this.isTrue = false;
            }
        });
    },
    computed: {
        formatValue: function () {
            return this.value;
        },
    },
    methods: {
        updateValue: function (event) {
            let value = event.target.value;
            if (this.format === "currency") {
                value = this.formatCurrency(value);
            }
            event.target.value = value;
            this.$emit("input", value);
        },
        onBlur(event, val) {
            if (this.required && val === "") {
                this.error = true;
            }
            if (event.currentTarget.type === "email") {
                if (this.validateEmail(val)) {
                    this.isTrue = false;
                } else {
                    this.error = true;
                }
            }
            this.isTrue = false;
        },
        keyPress(event) {
            event = event || window.event;
            if (this.format === "currency" || this.format === "number") {
                var key = event.which ? event.which : event.keyCode;
                if (key > 31 && (key < 48 || key > 57)) {
                    event.preventDefault();

                    return false;
                }
                return true;
            }
        },
        validateEmail(email) {
            const regex = new RegExp(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
            return regex.test(email);
        },
        formatCurrency(str) {
            return Intl.NumberFormat()
                .format(Number(str?.replaceAll(".", "")?.replaceAll(",", "")))
                .toString();
        },
    },
};
</script>