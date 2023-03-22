<template>
    <div class="m-message">
        <div class="ms-mesage-box">
            <div class="close-button mi mi-24 mi-close" @click="closeMessage"></div>
            <div class="message-header">
                <div class="content">
                    <div class="icon-message">
                        <div class="mi mi-52 mi-exclamation-error-48-2"></div>
                    </div>
                    <div class="message-content p-l-16">
                        <!-- messageMode == $t("enumeration.Message.DuplicateCode") ? -->
                        <!-- 'helo' : $t("DeleteMessageSingle") -->
                        <!-- {{ $t("i18nEnum.CommendationLevel.GovernmentLevel") }} -->
                        <div class="message-title">{{
                                messageMode == 0 ? $t("NameProject") : messageMode == 1 ? $t("DeleteMessageSingle") :
                                    "Thông báo"
                        }}
                        </div>

                        <span class="message-main" id="idMessage">
                            {{
                                    messageMode == enumeration.Message.DuplicateCode ? $t("EmulationCodeDuplicate", {
                                        name: emulationSelected.EmulationCode
                                    })
                                        : messageMode == enumeration.Message.MessageSingle ? $t('messageDeleteSingle', {
                                            name:
                                                emulationSelected.EmulationCode
                                        }) :
                            
                                            messageMode == enumeration.Message.MessageSystem ? $t("MessageSytemDelete") :
                                                $t('messageDeleteMultiple', {
                                                    name:
                                                        totalSelected
                                                })
                            }}
                        </span>
                    </div>
                </div>
            </div>
            <div class="mess-footer" v-if="messageMode == 1 || messageMode == 3">
                <div class="float-right mess-footer-box ">
                    <button class="btn w-100 btn-danger pointer" @click="acceptDeleteEmulation">
                        <div class="btn-text">Xóa danh hiệu</div>
                    </button>
                </div>
                <div class="float-right mess-footer-box">
                    <button class="ms-button btn btn-secondary" @click="closeMessage">Không</button>
                </div>
            </div>
            <div class="mess-footer" v-else>
                <div class="float-right mess-footer-box ">
                    <button class="btn w-100 btn-danger pointer" @click="closeMessage">
                        <div class="btn-text">Đóng</div>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import enumeration from '@/commons/enumeration';
import { loadData, deleteEmulation } from '@/api/link';
import i18nEnum from '@/i18ns/i18nEnum';
export default {
    props: {
        emulationSelected: {
            type: Object
        },
        messageMode: {
            type: Number
        },
        totalSelected: {
            type: Number,
        }
    },
    data() {
        return {
            enumeration: enumeration
        }
    },
    methods: {
        /**
         * Đóng form message
         * author:DPQuy (13/11/2022)
         * **/
        closeMessage() {
            this.$emit("showMessage", 0, this.emulationSelected)
        },
        /**
         * click nút xóa danh hiệu
         * author:DPQuy (13/11/2022)
         * **/
        acceptDeleteEmulation() {
            var me = this;

            //kiểm tra xem số lượng phần tử muốn xóa
            //xóa một
            if (this.messageMode == enumeration.Message.MessageSingle) {
                deleteEmulation(this.emulationSelected.EmulationID).then(res => {
                    window.location.reload();
                    // me.$emit("loadData");
                    me.$emit("showToast", 'Xóa thành công');
                    me.$emit("showMessage");
                    me.$emit("uncheckedAll");
                    console.log(res);
                }).catch(error => {
                    console.log(error);
                });
            }
            //xóa nhiều
            else if (this.messageMode == enumeration.Message.MessageMultiple) {
                //gọi hàm gọi api để xóa nhiều
                this.$emit("accesDeleteMultiple");
                //đóng form
                me.$emit("showMessage");
            }

        }
    }
}
</script>
<style>
@import url(../../css/base/message.css);
</style>