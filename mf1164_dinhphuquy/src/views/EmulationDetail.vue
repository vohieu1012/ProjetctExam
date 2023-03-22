<template>
    <div class="detail-popup">
        <!-- :style="{top: topDrop +'px', left: leftDrop +'px'}" -->
        <div class="popup-modal">
            <div class="ms-popup">
                <div class="buttons">
                    <div class="ms-popup-button">
                        <div class="btn-pop">
                            <div class="mi mi-24 mi-help"></div>
                            <span class="tooltip-text" id="top-2">
                                {{ $t("HelpText") }}
                            </span>
                        </div>
                    </div>
                    <div class="ms-popup-button">
                        <div class="btn-pop icon-close-popup" @click="closeEmulationDetail">
                            <span class="tooltip-text" id="top-3">
                                {{ $t("CloseText") }}
                            </span>
                        </div>
                    </div>
                </div>
                <div class="popup-header">{{ EditMode == 0 ? $t("AddEmulationText") : $t("EditEmulationText") }}</div>
                <div class="popup-content">
                    <div class="w-100 h-100 flex-1 flex-column">
                        <div class="form-group slide-detail">
                            <label class="form-group-label flex">
                                {{ $t("EmulationNameText") }}
                                <span class="text-red">&nbsp; *</span>
                            </label>

                            <div class="flex-1">
                                <div class="ms-input ms-editor w-100">
                                    <div class="flex-row border align-items-center"
                                        :class="{ 'error-border': errorList.EmulationName, 'disabled': emulation.IsSystem == enumeration.IsSystem.isSystem }">
                                        <input type="text" class="ms-input-item flex-1 EmulationName"
                                            ref="refEmulationName" v-model="emulation.EmulationName"
                                            @input="inputEmulationName" :disabled="emulation.IsSystem == 1">

                                        <div class="mi-24 mi mi-close m-r-6" @click="clearText(0)"
                                            v-if="emulation.EmulationName && emulation.EmulationName != emulationSelected.EmulationName">
                                        </div>
                                    </div>
                                    <div class="error-text" v-if="errorList.EmulationName">{{ $t("EmulationNameEmpty")
                                    }}</div>
                                </div>

                            </div>
                        </div>

                        <div class="form-group flex slide-detail">
                            <div class="flex-1 m-r-10">
                                <label class="form-group-label flex">
                                    {{ $t("EmulationCodeText") }}
                                    <span class="text-red">&nbsp; *</span>
                                </label>
                                <div class="flex-1">
                                    <div class="ms-input ms-editor w-100">
                                        <div class="flex-row border align-items-center "
                                            :class="{ 'error-border': errorList.EmulationCode, 'disabled': emulation.IsSystem == enumeration.IsSystem.isSystem }">
                                            <input type="text" class="ms-input-item flex-1" ref="refEmulationCode"
                                                v-model="emulation.EmulationCode" @input="inputEmulationCode"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)">
                                            <div class="mi-24 mi mi-close m-r-6" @click="clearText(1)"
                                                v-if="(emulation.EmulationCode && emulation.EmulationCode != emulationSelected.EmulationCode && emulation.IsSystem == enumeration.IsSystem.noSystem)">
                                            </div>
                                        </div>
                                        <div class="error-text" v-if="errorList.EmulationCode">{{
                                                $t("EmulationCodeEmpty")
                                        }}</div>
                                    </div>
                                </div>
                            </div>
                            <div class="flex-1 m-r-10">
                                <label class="form-group-label flex">
                                    {{ $t("ApplyObjectText") }}
                                    <span class="text-red">&nbsp; *</span>
                                </label>
                                <div class="flex-row flex-center checkbox-group">
                                    <div class="flex-1">
                                        <label class="text-black ms-checkbox ms-editor">
                                            <input type="checkbox" class="ms-checkbox-control" value="1"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                v-model="applyObject" @change="changeApplyObject">
                                            <span class="checkmark"></span>
                                            <span class="ms-checkbox--text text-black">
                                                {{ $t("IndividualText") }}
                                            </span>
                                        </label>
                                    </div>
                                    <div class="flex-1">
                                        <label class="text-black ms-checkbox ms-editor">
                                            <input type="checkbox" class="ms-checkbox-control" value="2"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                v-model="applyObject" @change="changeApplyObject">
                                            <span class="checkmark"></span>
                                            <span class="ms-checkbox--text text-black">
                                                {{ $t("GroupText") }}
                                            </span>
                                        </label>
                                    </div>
                                </div>
                                <div class="error-text" v-if="errorList.ApplyObject"> {{ $t("ApplyObjectEmpty") }}</div>
                            </div>
                        </div>
                        <div class="form-group flex slide-detail">
                            <div class="flex-1 m-r-10">
                                <label class="form-group-label flex">
                                    {{ $t("CommendationLevelNameText") }}
                                    <span class="text-red">&nbsp; *</span>
                                </label>
                                <div class="flex-1">
                                    <div class="ms-input ms-editor w-100">
                                        <div class="flex-row border "
                                            :class="{ 'error-border': errorList.CommendationLevelName, 'disabled': emulation.IsSystem == enumeration.IsSystem.isSystem }">
                                            <!-- :input-props="{ tabindex: '5' }" -->
                                            <Dropdown :editable="true"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                placeholder="Chọn hiện vật khen thưởng" autoOptionFocus
                                                v-model="emulation.CommendationLevelID" :options="options"
                                                option-label="CommendationLevelName" panelClass="combo-dropdown-panel"
                                                @change="changeCommendationLevelName"
                                                option-value="CommendationLevelID" />
                                        </div>
                                    </div>
                                    <div class="error-text" v-if="errorList.CommendationLevelName">
                                        {{ $t("CommendationLevelNameEmpty") }} </div>
                                </div>
                            </div>
                            <div class="flex-1 m-r-10">
                                <label class="form-group-label flex">
                                    {{ $t("MovementTypeTextApply") }}
                                    <span class="text-red">&nbsp; *</span>
                                </label>
                                <div class="flex-row flex-center checkbox-group">
                                    <div class="flex-1">
                                        <label class="text-black ms-checkbox ms-editor">
                                            <input type="checkbox" class="ms-checkbox-control"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                v-model="movementType" value="2" @change="changeMovementType">
                                            <span class="checkmark"></span>
                                            <span class="ms-checkbox--text text-black">
                                                {{ $t("FrequentText") }}
                                            </span>
                                        </label>
                                    </div>
                                    <div class="flex-1">
                                        <label class="text-black ms-checkbox ms-editor">
                                            <input type="checkbox" class="ms-checkbox-control"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                v-model="movementType" value="1" @change="changeMovementType">
                                            <span class="checkmark"></span>
                                            <span class="ms-checkbox--text text-black">
                                                {{ $t("BatchedText") }}
                                            </span>
                                        </label>
                                    </div>
                                </div>
                                <div class="error-text" v-if="errorList.MovementType"> {{ $t("MovementTypeEmpty") }}
                                </div>
                            </div>
                        </div>
                        <div class="form-group flex slide-detail">
                            <div class="flex-1">
                                <label class="form-group-label flex">
                                    {{ $t("NoteText") }}
                                </label>
                                <div class="flex-1">
                                    <div class="ms-textarea ms-editor">
                                        <div class="flex-row border"
                                            :class="{ 'disabled': emulation.IsSystem == enumeration.IsSystem.isSystem }">
                                            <textarea rows="4" placeholder="Nhập ghi chú"
                                                :disabled="(emulation.IsSystem == enumeration.IsSystem.isSystem)"
                                                class="ms-textarea-item flex-1" v-model="emulation.Note"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group slide-detail" v-if="EditMode == 1">
                            <label class="form-group-label flex">
                                {{ $t("StatusText") }}
                            </label>
                            <div class="flex-row">
                                <div class="flex-1">
                                    <label class="ms-radio">
                                        <input type="radio" name="MsRadio_Inactive" v-model="emulation.Status" value=1
                                            @change="changeStatus">
                                        <span class="checkmark"></span>
                                        <span class="ms-radio--text text-black pointer">
                                            {{ $t("UseText") }}
                                        </span>
                                    </label>
                                </div>
                                <div class="flex-1">
                                    <label class="ms-radio">
                                        <input type="radio" name="MsRadio_Inactive" v-model="emulation.Status" value=0
                                            @change="changeStatus">
                                        <span class="checkmark"></span>
                                        <span class="ms-radio--text text-black pointer">
                                            {{ $t("StopUsingText") }}
                                        </span>
                                    </label>
                                </div>
                                <div class="flex-1"></div>
                                <div class="flex-1"></div>
                                <div class="flex-1"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup-footer">
                    <div class="flex-row">
                        <div class="flex-1"></div>
                        <div class="flex-rtl" v-if="EditMode === 0">
                            <div class=" positon-relative">
                                <button class="ms-button btn btn-primary pointer"
                                    @keydown.ctrl.s.prevent.stop="alert(1)" @click="btnAddOnClick(0)">
                                    {{ $t("SaveText") }}
                                </button>
                                <span class="tooltip-text" id="top-5">Ctrl + S</span>
                            </div>
                            <div class=" positon-relative">
                                <button class="ms-button btn primary-outline m-r-10 pointer" @click="btnAddOnClick(1)">
                                    <div class="btn-text">
                                        {{ $t("SaveAndAddText") }}
                                    </div>
                                </button>
                                <span class="tooltip-text" id="top-4">Ctrl + Shift + S</span>
                            </div>
                            <div class=" positon-relative">
                                <button class="ms-button btn m-r-10 btn-secondary" @keydown.tab="lastTabIndex"
                                    @click="closeEmulationDetail">
                                    <div class="btn-text">
                                        {{ $t("CancelText") }}
                                    </div>
                                </button>

                            </div>
                        </div>
                        <div class="flex-rtl" v-else>
                            <div class=" positon-relative">
                                <button class="ms-button btn btn-primary pointer" @click="btnAddOnClick(0)">
                                    {{ $t("SaveText") }}
                                </button>
                                <span class="tooltip-text" id="top-5">Ctrl + S</span>
                            </div>
                            <div class=" positon-relative">
                                <button class="ms-button btn m-r-10 btn-secondary" @click="closeEmulationDetail">
                                    <div class="btn-text">
                                        {{ $t("CancelText") }}
                                    </div>
                                </button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <TheWarning v-if="isWarning" @showWarning="showWarning" @focusFirst="focusFirst" @closeDetail="closeDetail" />
</template>

<script>
import { getCommendationLevel, insertEmulation, getNewEmulationCode, updateEmulation } from '@/api/link';
import Dropdown from 'primevue/dropdown';
import enumeration from '@/commons/enumeration';
import TheWarning from '@/components/base/TheWarning.vue';
export default {
    components: {
        Dropdown,
        TheWarning
    },
    data() {
        return {
            enumeration: enumeration,
            topDrop: 0,
            leftDrop: 0,
            isDown: false,
            top: '',
            left: '',
            isWarning: false,
            isMessage: false,
            applyObject: [],
            movementType: [],
            emulation: {
                EmulationName: null,
                EmulationCode: '',
            },
            generateCode: 0,
            isClearText: false,
            options: [],
            errorList: {},
        }
    },
    props: {
        EditMode: {
            type: Number,
            default: 0
        },
        emulationSelected: {
            type: Object,
        }
    },
    methods: {
        /**
         * click icon đóng trên form
         * author: DPQuy (13/11/2022)
         * **/
        closeEmulationDetail() {
            //kiểm tra xem dữ liệu có thay đổi ko
            if (JSON.stringify(this.emulationSelected) != JSON.stringify(this.emulation)) {
                this.showWarning();
            }
            else {
                this.closeDetail();
            }
        },
        /**
         * hiển thị cảnh báo khi form thay đổi
         * dữ liệu
         * create by: DPQuy (13/11/2022)
         * **/
        showWarning() {
            this.isWarning = !this.isWarning;
        },
        //đóng form chi tiết
        closeDetail() {
            //gọi hàm đóng form ở EmulationList
            this.$emit("showEmulationDetail");
            this.isWarning = false;
        },


        /**
         * sự kiện checked checkbox Đối tượng khen thưởng
         * 1.Nếu ko có đối tượng nào được check thì hiển thị 
         * cảnh báo kèm border cảnh báo
         * 2.Nếu có thì ẩn
         * author:DPQuy(13/11/2022)
         * **/
        changeApplyObject() {
            this.changeEventCommon(this.applyObject.length, "ApplyObject")
        },
        /**
         *hàm chung cho sự kiện thay đổi giá trị
         *checkbox
         *author:DPQuy(13/11/2022)
         * **/
        changeEventCommon(lengthProp, nameError) {
            try {
                if (lengthProp === enumeration.FormMode.AddMode) {
                    this.errorList[nameError] = true;
                }
                else {
                    this.errorList[nameError] = false;
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * sự kiện thay đổi giá trị Loại phong trào
         * 1.Nếu ko có thì hiển cảnh báo
         * 2.Nếu có thì ẩn
         * **/
        changeMovementType() {
            this.changeEventCommon(this.movementType.length, "MovementType");
        },
        /**
         * hàm clear text khi nhấn vào icon 
         * x trên input
         * author:DPQuy(13/11/2022)
         * **/
        clearText(mode) {
            try {
                if (mode == enumeration.FormMode.AddMode) {
                    this.emulation.EmulationName = '';
                    //hiện cảnh báo khi text rỗng
                    this.inputEmulationName();

                }
                else {
                    this.emulation.EmulationCode = '';
                    //hiện cảnh báo khi text rỗng
                    this.inputEmulationCode();
                    //cho phép sinh mã theo tên
                    this.generateCode = enumeration.GenerateCode.notallow;
                }
                this.isClearText = false;
            } catch (error) {
                console.log(error);
            }
        },


        /**
         * bắt sự kiện nhập input 
         * tên danh hiệu
         * author:DPQuy (13/11/2022)
         * **/
        inputEmulationName() {
            try {
                //hiện thị icon clear text
                this.isClearText = true;
                this.inputEventCommon(this.emulation.EmulationName, "EmulationName")
                this.generateEmulationCode(this.emulation.EmulationName, this.generateCode);

                this.inputEventCommon(this.emulation.EmulationCode, "EmulationCode");
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Hàm sinh mã danh hiệu theo tên danh hiệu
         * author:DPQuy (13/11/2022)
         * **/
        generateEmulationCode(name, mode) {
            //cho phép sinh mã theo tên
            try {
                if (mode == enumeration.FormMode.AddMode) {
                    //xóa khoảng trắng thừa trong chuỗi VD: Đinh     Phú Quý => Đinh Phú QUý
                    var stringSplit = String(name).replace(/\s+/g, ' ').trim().split(' ');
                    this.emulation.EmulationCode = '';
                    //lấy ra chữ cái đầu tiên từng phần tử
                    for (let i = 0; i < stringSplit.length; i++) {
                        this.emulation.EmulationCode += stringSplit[i].toUpperCase().charAt(0);
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * hàm chung cho sự kiện nhập input
         * author:DPQuy(13/11/2022)
         * **/
        inputEventCommon(namePro, nameError) {
            try {
                //kiểm tra xem có underfine ko
                if (!namePro.trim()) {
                    this.errorList[nameError] = true;
                }
                else {
                    this.errorList[nameError] = false;
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * sự kiện nhập input mã danh hiệu
         * author:DPQuy(13/11/2022)
         * **/
        inputEmulationCode() {
            try {
                //hiển thị dấu x clear text
                this.isClearText = true;
                this.inputEventCommon(this.emulation.EmulationCode, "EmulationCode");
                if (!this.emulation.EmulationCode) {
                    //ko tự động generate theo tên 
                    this.generateCode = enumeration.GenerateCode.notallow;
                }
                else {
                    //tự động sinh mã theo tên
                    this.generateCode = enumeration.GenerateCode.allow
                }
            } catch (error) {
                console.log(error);
            }
        },
        changeCommendationLevelName() {
            this.errorList["CommendationLevelName"] = false;
        },


        /***
         * hàm validate dữ liệu
         * author:DPQuy(13/11/2022)
        */
        validateData() {
            try {
                //kiểm tra xem tên có trống ko
                if (!this.emulation.EmulationName) {
                    this.errorList["EmulationName"] = true;
                    //focus vào ô tên đang lỗi
                    this.$refs.refEmulationName.focus();
                    return false;
                }
                else {
                    this.errorList["EmulationName"] = false;
                }
                //kiểm tra xem mã có trống ko
                if (!this.emulation.EmulationCode) {
                    //focus vào ô mã đang lỗi
                    this.$refs.refEmulationCode.focus();
                    this.errorList["EmulationCode"] = true;
                    return false;
                }
                else {
                    this.errorList["EmulationCode"] = false;
                }

                //kiểm tra đối tượng
                if (this.applyObject.length === 0) {
                    this.errorList["ApplyObject"] = true;
                    return false;
                }
                else {
                    this.errorList["ApplyObject"] = false;
                }

                //kiểm tra cấp khen thuỏng
                if (!this.emulation.CommendationLevelID.trim()) {
                    this.errorList["CommendationLevelName"] = true;
                    return false;
                }
                else {
                    this.errorList["CommendationLevelName"] = false;
                }
                return true;
            } catch (error) {
                console.log(error);
            }

        },

        /**
         * hiển thị thông báo lỗi
         * author:DPQuy (13/11/2022)
         * **/
        showMessage() {
            this.isMessage = !this.isMessage;
        },
        /**
         * khi tabindex vào ô cuối cùng thì focus lên ô đầu
         * **/
        lastTabIndex(event) {
            event.preventDefault();
            this.$nextTick(() => {
                this.$refs.refEmulationName.focus();
            })
        },

        /**
         * Sự kiện thay đổi giá trị status
         * author:DPQuy (13/11/2022)
         * **/
        changeStatus(event) {
            this.emulation.Status = Number(event.target.value);
        },

        setData() {
            try {
                //lấy dữ liệu ra từ form và thực hiện gán cho object emulation
                if (this.applyObject.length === 2) {
                    this.emulation.ApplyObject = enumeration.ApplyObject.Both;
                }
                else {
                    this.emulation.ApplyObject = Number(this.applyObject[0]);
                }
                //kiểm tra số lượng loại phong trào
                if (this.movementType.length === enumeration.MovementType.Team) {
                    this.emulation.MovementType = enumeration.MovementType.Both;
                }
                else {
                    this.emulation.MovementType = Number(this.movementType[0]);
                }
            } catch (error) {
                console.log(error);
            }

        },
        /**
         * sét form khi thêm mới
         * **/
        async setForm() {
            try {
                var me = this;
                //gọi api lấy mã nhân viên mới
                var res = await getNewEmulationCode();
                me.emulation = { ...res };

                this.applyObject.push(enumeration.ApplyObject.Individual);

                this.emulation.IsSystem = enumeration.IsSystem.noSystem;
                //sét giá trị mặc định cho cấp khen thưởng
                this.emulation.CommendationLevelID = this.options[0].CommendationLevelID;
                //sét giá trị mặc định cho loại phong trào
                this.movementType.push(enumeration.MovementType.Frequent);
            } catch (error) {
                console.log(error);
            }


        },

        /**
         * ctrl + S lưu form
         * author: DPQuy (13/11/2022)
         * **/
        doSave(e) {
            try {
                if (!(e.keyCode === 83 && e.ctrlKey)) {
                    return;
                }
                e.preventDefault();
                this.btnAddOnClick(enumeration.FormMode.AddMode);
            } catch (error) {
                console.log(error);
            }

        },
        doSaveAndAdd(e) {
            try {
                if (!(e.keyCode === 83 && e.ctrlKey && e.keyCode === 16)) {
                    return;
                }
                e.preventDefault();
                this.btnAddOnClick(enumeration.FormMode.AddMode);
            } catch (error) {
                console.log(error);
            }
        },
        async getData() {
            /**
        * gọi api lấy danh sách các cấp khen thưởng
        * **/
            var me = this;
            await getCommendationLevel().then(res => {
                me.options = res;
            });
            // axios.get(API.getCommendationLevel()).then(res => {
            //     me.options = res.data;
            // })

            //Kiểm tra xem form đang ở chế độ chỉnh
            //sửa hay thêm mới
            //1.chỉnh sửa
            //0.thêm mới
            if (this.EditMode == enumeration.FormMode.AddMode) {
                try {

                    //gọi hàm set giá trị khi thêm mới
                    //gọi api lấy mã nhân viên mới
                    var res = await getNewEmulationCode();
                    me.emulation = { ...res };

                    me.applyObject.push(enumeration.ApplyObject.Individual);

                    me.emulation.IsSystem = enumeration.IsSystem.noSystem;
                    //sét giá trị mặc định cho cấp khen thưởng
                    me.emulation.CommendationLevelID = me.options[0].CommendationLevelID;
                    //sét giá trị mặc định cho loại phong trào
                    me.movementType.push(enumeration.MovementType.Frequent);
                } catch (error) {
                    console.log(error);
                }

            }
            else {
                try {
                    //lấy ra object cần chỉnh sửa
                    this.emulation = { ...this.emulationSelected };
                    //kiểm tra giá trị đối tượng và gán lên form
                    //1.Cá nhân
                    //2.Tập thể
                    //3.Tập thể và cá nhân
                    if (this.emulation.ApplyObject == enumeration.ApplyObject.Both) {
                        this.applyObject.push(enumeration.ApplyObject.Team, enumeration.ApplyObject.Individual);
                    }
                    else {
                        this.applyObject.push(this.emulation.ApplyObject)
                    }

                    //kiểm tra giá trị loại phong trào
                    //1.Theo đợt
                    //2.Thường xuyên
                    //3.Thường xuyên, theo đợt
                    if (this.emulation.MovementType == enumeration.MovementType.Both) {
                        this.movementType.push(enumeration.MovementType.Batched, enumeration.MovementType.Frequent);
                    }
                    else {
                        this.movementType.push(this.emulation.MovementType);
                    }
                } catch (error) {
                    console.log(error);
                }

            }
        },

        /**
         * hàm gọi api thêm mới
         * created by:DPQuy (13/11/2022)
         * **/
        insertNew(mode) {
            try {
                var me = this;
                insertEmulation(me.emulation).then(res => {
                    //kiểm tra khi click nút lưu
                    if (mode == enumeration.FormMode.AddMode) {
                        //đóng form
                        this.$emit("showEmulationDetail");
                    }
                    //load lại dữ liệu
                    this.$emit("loadData");
                    //nội dung thông báo
                    this.$emit("showToast", "Thêm thành công")
                }).catch(error => {
                    console.log(error);
                    //kiểm tra xem lỗi request có phải lỗi gửi dữ liệu
                    if (error.response.data.errorCode == enumeration.ErrorCode.BadRequest) {
                        this.$emit('showMessage', enumeration.ErrorCode.BadRequest, me.emulation);
                    }
                    //lỗi server
                    else if (error.response.data.errorCode == enumeration.ErrorCode.ServerError) {
                        this.$emit('showMessage', enumeration.ErrorCode.ServerError, me.emulation);
                    }

                });
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * click vào nút Lưu 
         * author:DPQuy (13/11/2022)
         * **/
        btnAddOnClick(mode) {
            try {
                if (this.validateData()) {
                    this.setData();
                    var me = this;
                    //thực hiện thêm mới và đóng form khi nhấn nút lưu
                    if (mode == enumeration.FormMode.AddMode) {
                        //nếu form đang ở chế độ thêm mới thì gọi api thêm mới
                        if (this.EditMode === enumeration.FormMode.AddMode) {
                            this.emulation.Status = enumeration.Status.Active;
                            //gán giá trị mặc định
                            this.emulation.IsSystem = enumeration.IsSystem.noSystem;
                            this.insertNew(0);
                        }
                        else {

                            //kiểm tra xem dữ liệu có thay đổi ko
                            if (JSON.stringify(this.emulationSelected) == JSON.stringify(this.emulation)) {
                                //ko có sự thay đổi dữ liệu
                                //gọi hàm đóng form
                                this.$emit("showEmulationDetail");
                            }
                            else {
                                //gọi api cập nhật danh hiệu
                                updateEmulation(me.emulation).then(res => {
                                    //đóng form
                                    this.$emit("showEmulationDetail");
                                    //load lại dữ liệu
                                    this.$emit("loadData");
                                    //nội dung thông báo
                                    this.$emit("showToast", "Lưu thành công")
                                    console.log(res);
                                }).catch(error => {
                                    console.log(error);
                                    //kiểm tra xem lỗi request có phải lỗi gửi dữ liệu
                                    if (error.response.data.errorCode == enumeration.ErrorCode.BadRequest) {
                                        this.$emit('showMessage', enumeration.ErrorCode.BadRequest, me.emulation);
                                    }
                                    //lỗi server
                                    else if (error.response.data.errorCode == enumeration.ErrorCode.ServerError) {
                                        this.$emit('showMessage', enumeration.ErrorCode.ServerError, me.emulation);
                                    }
                                });
                            }


                        }
                    }
                    else {
                        //thực hiện thêm mới và reset form để thêm mới
                        this.insertNew(enumeration.FormMode.EditMode);
                        //reset lại form thực hiện thêm mới
                        this.setForm();
                    }
                }
            } catch (error) {
                console.log(error);
            }
        }
    },

    async created() {

        this.getData();




        try {
            /**
           * Thêm các sự kiện từ bàn phím
           * author: DPQuy (6/7/2022)
           * **/
            window.addEventListener('keydown', (e) => {
                //nhấn esc để đóng form
                if (e.key == 'Escape') {
                    e.preventDefault();
                    this.closeEmulationDetail();
                }
            });
        } catch (error) {
            console.log(error);
        }

        // //focus vào ô nhập liệu đầu tiên
        this.$nextTick(() => {
            this.$refs.refEmulationName.focus();
        })
    },
    mounted() {
        //gắn sự kiện cho form khi tải xong dom
        document.addEventListener("keydown", this.doSave);
        document.addEventListener("keydown", this.doSaveAndAdd);
    }
}
</script>

<style>
@import url(../css/views/emulationdetail.css);
</style>