<template>
    <div class="m-message">
        <div class="ms-mesage-box">
            <div class="close-button mi mi-24 mi-close"></div>
            <div class="message-header">
                <div class="content">
                    <div class="icon-message">
                        <div class="mi mi-52 mi-exclamation-error-48-2"></div>
                    </div>
                    <div class="message-content p-l-16">
                        <div class="message-title">
                            {{ $t("NameProject") }}
                        </div>

                        <span class="message-main">
                            Dữ liệu đã thay đổi, bạn có chắc chắn muốn đóng ko?
                        </span>
                    </div>
                </div>
            </div>
            <div class="mess-footer">
                <div class="float-right mess-footer-box ">
                    <button class="btn w-100 btn-danger pointer" @click="closeForm">
                        <div class="btn-text">Đóng</div>
                    </button>
                </div>
                <div class="float-right mess-footer-box">
                    <button class="ms-button btn btn-secondary" @click="closeWarning">Không</button>
                </div>
            </div>

        </div>
    </div>
</template>
<script>

export default {
    methods: {
        //đóng toàn bộ form
        closeForm() {
            this.$emit("closeDetail");
        },
        //đóng form cảnh báo
        closeWarning() {
            this.$emit("showWarning");
            //focus lại ô đầu tiên
            this.$emit("focusFirst");
        }


    }
}
</script>
<style>
@import url(../../css/base/message.css);
</style>