<template>
    <div class="main-view flex-column">
        <div class="flex-column form-list h-100 flex-1">
            <div class="flex-column h-100 flex-1">
                <div class="flex-row title-box">
                    <div class="list-title flex">
                        {{ $t("EmulationText") }}
                    </div>
                </div>
                <div class="flex-row toolbar-box">
                    <div class="left-toolbar flex-row">
                        <div class="m-input ms-editor search-box m-r-10">
                            <div class="flex-row border flex align-items-center">
                                <input type="text" class="ms-editor ms-input-item flex-1 input-search"
                                    placeholder="Nhập mã hoặc tên danh hiệu" v-model="txtSearch"
                                    @keydown.enter="keydownSearch">
                                <div class="mi-24 mi mi-close m-r-6" @click="clearText" v-if="txtSearch"></div>
                                <div class="icon-search mi-24 mi positon-relative"></div>
                            </div>
                        </div>
                        <div class="filter-group">
                            <div class="flex-row">
                                <button class="ms-button btn btn-filter btn-secondary m-r-10" @click="btnFilterOnClick">

                                    <div class="mi mi-24 mi-filter positon-relative">
                                        <div class="access-filter" v-if="filterSuiteText"></div>
                                    </div>
                                    <div class="pl-0 btn-text">
                                        {{ $t("FilterText") }}
                                    </div>
                                </button>

                                <div class="flex align-items-center m-l-6 pointer" v-if="filterSuiteText"
                                    style="color: #2979ff" @click="unfiltered">Bỏ lọc</div>
                            </div>
                            <div class="filter-container" v-if="isShowFilterBox" @click="showFilterBox"></div>
                            <div class="filter-box" style="right: 23px; width: 360px;" v-if="isShowFilterBox">
                                <span class="custom-arrow" style="right: 2px;"></span>
                                <div class="box-header">
                                    <span class="box-header-title">
                                        <span class="box-header-title">Lọc danh hiệu</span>
                                    </span>
                                    <span class="mi-24 mi mi-close" @click="showFilterBox"></span>
                                </div>
                                <div class="box-content flex-column">
                                    <div class="form-group">
                                        <label>Đối tượng khen thưởng</label>
                                        <div class="flex-1 m-t-10">
                                            <div class="ms-combobox ms-editor input-editor">
                                                <div class="flex-row border combo-box-input">
                                                    <!-- v-model="filterSuite[0].ApplyObject[0]" -->
                                                    <Dropdown v-model="filterSuite[0]"
                                                        :options="enumeration.ApplyObjectFilter" option-label="Text" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Cấp khen thưởng </label>
                                        <div class="flex-1 m-t-10">
                                            <div class="ms-combobox ms-editor input-editor">
                                                <div class="flex-row border combo-box-input">
                                                    <Dropdown v-model="filterSuite[1]"
                                                        :options="enumeration.CommendationLevelFilter"
                                                        option-label="Text" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Loại phong trào</label>
                                        <div class="flex-1 m-t-10">
                                            <div class="ms-combobox ms-editor input-editor">
                                                <div class="flex-row border combo-box-input">
                                                    <!-- v-model="filterSuite[1].CommendationLevel[0]" -->
                                                    <Dropdown v-model="filterSuite[2]"
                                                        :options="enumeration.MovementTypeFilter" option-label="Text" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Trạng thái</label>
                                        <div class="flex-1 m-t-10">
                                            <div class="ms-combobox ms-editor input-editor">
                                                <div class="flex-row border combo-box-input">
                                                    <!-- v-model="filterSuite[3].Status[0]" -->
                                                    <Dropdown v-model="filterSuite[3]"
                                                        :options="enumeration.StatusFilter" option-label="Text" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <div class="right-content">
                                        <button class="ms-button btn m-r-10 btn-secondary" @click="showFilterBox">
                                            <div class="btn-text">
                                                {{ $t("CancelText") }}
                                            </div>
                                        </button>
                                        <button class="ms-button btn btn-primary pointer" @click="ApplyFilter">
                                            <div class="btn-text">
                                                {{ $t("ApplyText") }}
                                            </div>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="flex-1"></div>
                    <div class="right-toolbar flex-row">
                        <button class="btn btn-primary pointer" @click="btnAddEmulation(0)" v-if="!isShowCheckActions">
                            <div class="mi-24 mi icon-add-white positon-relative mi-add-white"></div>
                            <div class="btn-text pl-0">{{ $t("AddEmutaion") }}</div>
                        </button>
                        <div class="action-multi flex-row" v-if="isShowCheckActions">
                            <div class="selected-count">{{ $t("selectedText") }}
                                <strong>
                                    {{ totalSelected }}
                                </strong>
                            </div>
                            <div class="selected-all pointer de-selected m-l-20" @click="uncheckedAll">
                                {{ $t("uncheckedText") }}</div>
                            <div class="action-container flex">
                                <div class="btn action flex-row primary-outline pointer"
                                    @click="updateStatusFollowMode(1)">
                                    <div class="btn-text">
                                        {{ $t("UseText") }}
                                    </div>
                                </div>
                                <div class="btn action flex-row secondary-outline pointer"
                                    @click="updateStatusFollowMode(0)">
                                    <div class="btn-text">
                                        {{ $t("StopUsingText") }}
                                    </div>
                                </div>
                                <div class="btn action flex-row secondary-outline-red pointer"
                                    @click="deleteMultiple(emulation)">
                                    <div class="btn-text">{{ $t("DeleteText") }}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="flex-column flex-1 list-content box">
                    <div class="ms-grid-viewer flex-1 flex-box flex-column">
                        <div class="ms-content--table flex-1 scroller">
                            <table class="m-table" cellpadding="0" cellspacing="0">
                                <thead class="m-thead">
                                    <tr class="m-tr tr-1">
                                        <th class="ms-th multiple-cell ms-checkbox sticky" :class="{
                                            'checkmark-temp': EmulationIDPage[pageIndex].length !== 0 && !isSelectAll
                                        }">
                                            <input type="checkbox" class="ms-checkbox-control" v-model="isSelectAll"
                                                @change="changeCheckedAll">
                                            <span class="checkmark"></span>
                                        </th>
                                        <th class="ms-th" style="min-width: 310px; max-width: 310px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Tên danh hiệu thi đua">{{
                                                        $t("EmulationNameText")
                                                }}</div>
                                                <div class="mi mi-16 mi-arrow-dropdown ml-5" style="display:none">
                                                </div>
                                            </div>
                                        </th>
                                        <th class="ms-th" style="width: 160px; max-width: 160px; min-width: 160px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Mã danh hiệu">{{
                                                        $t("EmulationCodeText")
                                                }}</div>
                                                <div class="mi mi-16 mi-arrow-dropdown ml-5" style="display:none">
                                                </div>
                                            </div>
                                        </th>
                                        <th class="ms-th" style="width: 180px; max-width: 180px; min-width: 180px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Đối tượng khen thưởng">
                                                    {{
                                                            $t("ApplyObjectText")
                                                    }}
                                                </div>
                                            </div>
                                        </th>
                                        <th class="ms-th" style="width: 200px; max-width: 200px; min-width: 200px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Cấp khen thưởng">
                                                    {{ $t("CommendationLevelNameText") }}
                                                </div>
                                            </div>
                                        </th>
                                        <th class="ms-th" style="width: 200px; max-width: 200px; min-width: 200px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Loại phong trào">
                                                    {{ $t("MovementTypeText") }}
                                                </div>
                                            </div>
                                        </th>
                                        <th class="ms-th" style="width: 180px; max-width: 180px; min-width: 180px;">
                                            <div class="ms-th-content flex-row">
                                                <div class="ms-th-title" title="Trạng thái">{{ $t("StatusText") }}
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody class="m-tbody">
                                    <tr v-for="(emulation) in data" :key="emulation.EmulationID" class="ms-tr"
                                        :class="{ 'loading': isLoading }" v-on:mouseover="test"
                                        @mouseleave="moveleaveTr" @dblclick="btnAddEmulation(1, emulation)">
                                        <td @dblclick.stop="unDblClick" class="ms-td multiple-cell ms-checkbox sticky">
                                            <div class="flex-center">
                                                <!-- :value="emulation.EmulationID"  -->
                                                <input type="checkbox" class="ms-checkbox-control" selectedCheckbox
                                                    :id="emulation.EmulationID" v-model="EmulationIDPage[pageIndex]"
                                                    :value="emulation.EmulationID" @change="cbSingleStatus(emulation)">
                                                <span class="checkmark"></span>

                                            </div>
                                        </td>
                                        <td style="max-width: 310px;" class="ms-td colEmulationName"
                                            :title="emulation.EmulationName">
                                            <div class="ms-td-col flex-row">
                                                <div class="ms-td-title">
                                                    {{ emulation.EmulationName }}
                                                </div>
                                            </div>
                                        </td>
                                        <td style="max-width: 160px;" class="ms-td" :title="emulation.EmulationCode">
                                            <div class="ms-td-col flex-row">
                                                <div class="ms-td-title">
                                                    {{ emulation.EmulationCode }}
                                                </div>
                                            </div>
                                        </td>
                                        <td style="max-width: 180px;" class="ms-td" :title='
                                emulation.ApplyObject == enumeration.ApplyObject.Individual ?
                                    $t("i18nEnum.ApplyObject.Individual") : emulation.ApplyObject == enumeration.ApplyObject.Team ?
                                        $t("i18nEnum.ApplyObject.Team") : $t("i18nEnum.ApplyObject.Both")'>
                                            <div class="ms-td-col flex-row">
                                                <div class="ms-td-title">
                                                    {{ emulation.ApplyObject == enumeration.ApplyObject.Individual ?
                                                            $t("i18nEnum.ApplyObject.Individual") :
                                                            emulation.ApplyObject == enumeration.ApplyObject.Team ?
                                                                $t("i18nEnum.ApplyObject.Team") : $t("i18nEnum.ApplyObject.Both")
                                                    }}
                                                </div>
                                            </div>
                                        </td>
                                        <td style="max-width: 200px;" class="ms-td"
                                            :title="emulation.CommendationLevelName">
                                            <div class="ms-td-col flex-row">
                                                <div class="ms-td-title">
                                                    {{ emulation.CommendationLevelName }}
                                                </div>
                                            </div>
                                        </td>
                                        <td style="max-width: 200px;" class="ms-td" :title='emulation.MovementType == enumeration.MovementType.Batched ?
                                    $t("MovementType.Batched") :
                                    emulation.MovementType == enumeration.MovementType.Frequent ?
                                        $t("MovementType.Frequent") : $t("MovementType.FreAndBat")'>
                                            <div class="ms-td-col flex-row">
                                                <div class="ms-td-title">
                                                    {{ emulation.MovementType == enumeration.MovementType.Batched ?
                                                            $t("MovementType.Batched") :
                                                            emulation.MovementType == enumeration.MovementType.Frequent ?
                                                                $t("MovementType.Frequent") : $t("MovementType.FreAndBat")
                                                    }}
                                                </div>
                                            </div>
                                        </td>
                                        <!-- :title='emulation.Status == 1 ? "Sử dụng" : "Ngưng sử dụng"' -->
                                        <td style="max-width: 180px;" class="ms-td"
                                            :title='emulation.Status == enumeration.Status.Active ? $t("UseText") : $t("StopUsingText")'>
                                            <div class=" ms-td-col flex-row">
                                                <div class="inactive-icon flex align-items-center">
                                                    <div class="icon-image"
                                                        :class="{ 'icon-not-active': emulation.Status == enumeration.Status.Inactive, 'icon-active': emulation.Status == enumeration.Status.Active }">
                                                    </div>
                                                </div>
                                                <div class="ms-td-title">
                                                    {{ emulation.Status == enumeration.Status.Active ? $t("UseText") :
                                                            $t("StopUsingText")
                                                    }}
                                                </div>
                                            </div>
                                        </td>
                                        <div class="row-actions" @dblclick.stop="unDbl">
                                            <div class="row-actions-item item-edit"
                                                @click="btnAddEmulation(1, emulation)">
                                                <div class="flex-center">
                                                    <div class="mi mi-24 mi-edit"></div>
                                                </div>
                                                <span class="tooltip-text" id="top">{{ $t("EditText") }}</span>
                                            </div>
                                            <div class="row-actions-item item-more" @click="showRowAction">
                                                <div class="flex-center">
                                                    <div class="mi mi-24 mi-more"></div>
                                                </div>
                                                <span class="tooltip-text" id="more-po">Thêm nữa...</span>
                                            </div>
                                            <div class="positon-relative">
                                                <ul class="v-context" v-if="isShowRowAction">
                                                    <li class="li-active"
                                                        :disabled="(emulation.Status === enumeration.Status.Active)"
                                                        :class="{ 'disable': emulation.Status === enumeration.Status.Active }"
                                                        @click="activeStatus(enumeration.Status.Active, emulation)">
                                                        {{ $t("UseText") }}
                                                    </li>
                                                    <li @click="activeStatus(enumeration.Status.Inactive, emulation)"
                                                        :class="{ 'disable': emulation.Status === enumeration.Status.Inactive }">
                                                        {{
                                                                $t("StopUsingText")
                                                        }}</li>
                                                    <li class="text-red" @click="deleteEmulation(emulation)">
                                                        {{ $t("DeleteText") }}
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="ms-pagination flex-row">
                            <div class="h-20 p-r-12 flex">
                                <div class="min-page-caption">
                                    {{ $t("SumText") }}
                                </div>
                                <div class="total">
                                    <span>{{ TotalRecord }}</span>
                                </div>
                                <div class="min-page-caption">
                                    {{ $t("RecordText") }}
                                </div>
                            </div>
                            <div class="flex-1"></div>
                            <div class="flex align-items-center">
                                <span>{{ $t("recorInpage") }}</span>
                                <div class="page-size ms-editor">
                                    <div class="flex-row border combo-box-input">
                                        <Dropdown :options="options" v-model="pageSize" @change="changePageSize" />
                                    </div>
                                </div>
                                <div class="flex h-16">
                                    <!-- let start = (this.page - 1) * this.perPage;
                                    let end = (this.page - 1) * this.perPage + this.perPage; -->
                                    <div class="m-r-6"><b>{{ ((pageIndex - 1) * pageSize + 1) }}</b></div>
                                    <div class="m-r-6">-</div>
                                    <div class="m-r-6"><b>{{ ((pageIndex - 1) * pageSize) + data.length }}</b></div>
                                    <div>{{ $t("recordText") }}</div>
                                </div>
                                <div class="flex">
                                    <div class="move prev" :class="{ 'disabled': pageIndex === 1 }" @click="prevPage">
                                    </div>
                                    <div class="move next " :class="{ 'disabled': pageIndex === TotalPage }"
                                        @click="nextPage"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <EmulationDetail v-if="isShowEmulationDetail" @showEmulationDetail="showEmulationDetail" :EditMode="EditMode"
        :emulationSelected="emulationSelected" @loadData="loadData" @showToast="showToast" @showMessage="showMessage" />
    <TheToast v-if="isToast" :toastContent="toastContent" />
    <TheMessage v-if="isMessage" @showMessage="showMessage" :messageMode="messageMode" @loadData="loadData"
        :emulationSelected="emulationSelected" @showToast="showToast" @accesDeleteMultiple="accesDeleteMultiple"
        :totalSelected="totalSelected" @uncheckedAll="uncheckedAll" />
</template>

<script>
import TheMessage from '@/components/base/TheMessage.vue';
import TheToast from '@/components/base/TheToast.vue';
import { loadData, deleteEmulation, deleteMultiple, updateActiveStatus, updateInActiveStatus } from '@/api/link';
import Dropdown from 'primevue/dropdown';
import EmulationDetail from './EmulationDetail.vue';
import enumeration from '@/commons/enumeration';
export default {
    components: {
        EmulationDetail,
        Dropdown,
        TheToast,
        TheMessage
    },
    data() {
        return {
            enumeration: enumeration,
            txtSearch: '',
            isToast: false,
            toastContent: '',
            isMessage: false,
            messageMode: 0,
            isShowEmulationDetail: false,
            isShowRowAction: false,
            EditMode: 0,
            isLoading: false,
            isShowFilterBox: false,
            isShowCheckActions: false,
            emulationSelected: {},
            isSelectAll: false,
            isDelectAll: false,
            EmulationIDPage: [], // danh sách các id select theo trang
            EmulationIDs: [], //mảng danh sách id để xóa nhiều
            totalSelected: 0, //tổng số danh hiệu đã lựa chọn
            data: [],
            options: [10, 20, 50, 100],
            pageSize: 50,
            pageIndex: 1,
            TotalPage: 0,
            isSystem: false, //dữ liệu hệ thống
            TotalRecord: 0,
            filterSuite: [
                enumeration.ApplyObjectFilter[0],
                enumeration.CommendationLevelFilter[0],
                enumeration.MovementTypeFilter[0],
                enumeration.StatusFilter[0]
            ],
            filterSuiteText: '',
        }
    },
    methods: {

        /**
         * click nút sử dụng/ngưng sử dụng trên action checked row(click checkbox trên tr)
         * author:DPQuy (13/11/2022)
         * **/
        updateStatusFollowMode(mode, emu) {
            //nếu số lượng bản ghi đang chọn nhiều hơn 1
            //thì nối thành chuỗi cách nhau dấu phẩy
            try {
                var listID = "";
                if (this.totalSelected > 1) {
                    //ghép mảng từ dạng [{1,2,3},{4,5,6}] => [1,2,3,4,5,6]
                    for (const emu in this.EmulationIDPage) {
                        this.EmulationIDs.push(...this.EmulationIDPage[emu]);
                    }
                    //ghép mảng lưu trữ id cần xóa nhiều thành chuỗi cách nhau dấu ,
                    listID = this.EmulationIDs.join(",");
                }
                else if (this.totalSelected == 1) {
                    //nếu chỉ có 1 bản ghi thì ép thành chuỗi
                    listID = String(this.EmulationIDPage[1][0]);
                }

                //xảy ra khi click row actions khi hover trên từng tr
                else {
                    listID = String(emu.EmulationID);
                }
            } catch (error) {
                console.log(error);
            }

            try {
                var me = this;
                if (mode == 1) {
                    //gọi api cập nhật trạng thái các danh hiệu thành sử dụng
                    updateActiveStatus(listID).then(res => {
                        //xóa danh sách các bản ghi đã chọn
                        me.uncheckedAll();
                        //load lại dữ liệu
                        me.loadData();
                        //nội dung thông báo
                        this.showToast("Lưu thành công");
                    }).catch(error => {
                        console.log(error);
                    });
                }
                else {
                    //gọi api cập nhật trạng thái các danh hiệu thành ngưng sử dụng
                    updateInActiveStatus(listID).then(res => {
                        //xóa danh sách các bản ghi đã chọn
                        me.uncheckedAll();
                        //load lại dữ liệu
                        me.loadData();
                        //nội dung thông báo
                        this.showToast("Lưu thành công");
                    }).catch(error => {
                        console.log(error);
                    });
                }
            } catch (error) {
                console.log(error);
            }

        },

        /**
         * click nút áp dụng trên bộ lọc
         * author:DPQuy (13/11/2022)
         * **/
        ApplyFilter() {
            //kiểm tra xem đối tượng khen thưởng có đang ở giá trị mặc định ko
            //nếu có thì xóa khỏi object
            //nếu không thì thêm vào object
            try {
                var filterSuiteArr = [];
                // thêm bộ lọc đối tượng
                //kiểm tra xem có đang là giá trị mặc định ko
                if (this.filterSuite[0].Value != enumeration.ApplyObjectFilter[0].Value) {
                    filterSuiteArr.push(this.$tc("applyObjectFilter", { name: this.filterSuite[0].Value }));
                }
                //thêm bộ lọc cấp khen thưởng
                if (this.filterSuite[1].Value != enumeration.CommendationLevelFilter[0].Value) {
                    filterSuiteArr.push(this.$tc("commendationLevelFilter", { name: this.filterSuite[1].Value }))

                }
                //thêm bộ lọc loại phong trào
                if (this.filterSuite[2].Value != enumeration.MovementTypeFilter[0].Value) {
                    filterSuiteArr.push(this.$tc("movementTypeFilter", { name: this.filterSuite[2].Value }))

                }

                //thêm bộ lọc trạng thái
                if (this.filterSuite[3].Value != enumeration.StatusFilter[0].Value) {
                    filterSuiteArr.push(this.$tc("statusFilter", { name: this.filterSuite[3].Value }));

                }
                this.filterSuiteText = filterSuiteArr.join(' and ');
                this.loadData();
                this.showFilterBox();
            } catch (error) {
                console.log(error);
            }


        },

        /**
         * click nút lọc
         * author: DPQuy(13/11/2022)
         * **/
        btnFilterOnClick() {
            //gọi hàm show filterbox
            this.showFilterBox();
            //hủy các bản ghi đang được checked
            // this.uncheckedAll();
            console.log(this.EmulationIDPage);
        },

        /**
         * click nút bỏ lọc
         * **/
        unfiltered() {
            this.filterSuiteText = "";
            this.loadData();
        },


        /**
         * hàm toast thông báo
         * author:DPQuy (13/11/2022)
         * **/
        showToast(message) {
            try {
                clearTimeout();
                this.isToast = true;
                setTimeout(() => {
                    this.isToast = false
                }, 3000)
                //nội dung thông báo
                this.toastContent = message;
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * thay đổi số lượng bản ghi trên 1 trang
         * author:DPQuy (13/11/2022)
         * **/
        changePageSize() {
            this.pageIndex = 1;
            this.loadData();
        },
        /**
         * click nút chuyển trang kế tiếp
         * author:DPQuy (13/11/2022)
         * **/
        nextPage() {
            try {
                //kiểm tra mảng các checked theo từng trang có underfine ko
                if (!this.EmulationIDPage[this.pageIndex]) {
                    this.EmulationIDPage[this.pageIndex] = [];
                }

                //nếu pageIndex < tổng số trang thì thực hiện next trang
                if (this.pageIndex < this.TotalPage) {
                    this.pageIndex = this.pageIndex + 1;
                    //kiểm tra trạng thái select all
                    this.checkStatusSelectAll();
                    //load lại data theo trang
                    this.loadData();
                }
            } catch (error) {
                console.log(error);
            }
        },

        //bắt trạng thái select all cho từng trang
        checkStatusSelectAll() {
            try {
                //nếu mảng là underfine thì select all = false
                if (!this.EmulationIDPage[this.pageIndex]) {
                    this.isSelectAll = false;
                }
                else {
                    //Nếu số lượng phần tử đang chọn = tổng thì select all true
                    if (this.EmulationIDPage[this.pageIndex].length === this.data.length) {
                        this.isSelectAll = true;
                    }
                    else {
                        this.isSelectAll = false;
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },



        /**
         * click nút quay về trang trước đó
         * author:DPQuy (13/11/2022)
         * **/
        prevPage() {
            try {
                //kiểm tra mảng các checked theo từng trang có underfine ko
                if (!this.EmulationIDPage[this.pageIndex]) {
                    this.EmulationIDPage[this.pageIndex] = [];
                }
                if (this.pageIndex > 1) {
                    this.pageIndex = this.pageIndex - 1;
                    this.checkStatusSelectAll();
                    this.loadData();
                }
            } catch (error) {
                console.log(error);
            }
        },



        /**
         * Xóa danh hiệu
         * **/
        deleteEmulation(emulation) {
            try {
                //kiểm tra xem dữ liệu muốn xóa có phải dữ liệu hệ thống hay ko
                if (emulation.IsSystem == enumeration.IsSystem.isSystem) {
                    //hiển thị cảnh báo
                    this.showMessage(2, emulation)
                }
                else {
                    this.showMessage(1, emulation)
                }
            } catch (error) {
                console.log(error);
            }
            // this.showMessage(1, emulation);
        },
        /**
         * Hàm show dialog cảnh báo
         * author:QPQuy (13/11/2022)
         * **/
        showMessage(messMode, eml) {
            try {
                this.isMessage = !this.isMessage;
                this.messageMode = messMode;
                this.emulationSelected = eml;
            } catch (error) {
                console.log(error);
            }
        },


        /**
         * click nút sử dụng ,ngưng sử dụng
         * trên actions hover tr
         * 
         * **/
        activeStatus(mode, emulation) {
            try {
                var me = this;
                //nếu đang ở đúng trạng thái thì ko làm gì(ví dụ đang ở active thì ko làm gì)
                //nếu khác thì cập nhật
                if (mode != emulation.Status) {
                    //cập nhật trạng thái thành ngưng sử dụng
                    if (mode == this.enumeration.Status.Inactive) {
                        //gọi api cập nhật trạng thái các danh hiệu thành ngưng sử dụng
                        updateInActiveStatus(String(emulation.EmulationID)).then(res => {
                            me.showRowAction();
                            //load lại dữ liệu
                            me.loadData();
                            //nội dung thông báo
                            this.showToast(this.$t("SaveSucces"));
                        }).catch(error => {
                            console.log(error);
                        });
                    }
                    //cập nhật trạng thái thành  sử dụng
                    else if (mode == this.enumeration.Status.Active) {
                        updateActiveStatus(String(emulation.EmulationID)).then(res => {
                            me.showRowAction();
                            //load lại dữ liệu
                            me.loadData();
                            //nội dung thông báo
                            this.showToast(this.$t("SaveSucces"));
                        }).catch(error => {
                            console.log(error);
                        });
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * click nút bỏ chọn
         * author:DPQuy (13/11/2022)
         * **/
        uncheckedAll() {
            try {
                this.EmulationIDPage = [];
                this.isDelectAll = this.isSelectAll = this.isShowCheckActions = false;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * click nút xóa trên row checked
         * author:DPQuy (13/11/2022)
         * **/
        deleteMultiple() {
            try {
                //nếu số lượng check lớn hơn 1
                if (this.totalSelected > 1) {
                    this.showMessage(3);
                }
                else {
                    this.showMessage(1, this.emulationSelected);
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * click nút xóa để thực hiện xóa nhiều
         * author:DPQuy (13/11/2022)
         * **/
        async accesDeleteMultiple() {
            try {
                //ghép mảng từ dạng [{1,2,3},{4,5,6}] => [1,2,3,4,5,6]
                for (const emu in this.EmulationIDPage) {
                    this.EmulationIDs.push(...this.EmulationIDPage[emu]);
                }
                //ghép mảng lưu trữ id cần xóa nhiều thành chuỗi cách nhau dấu ,
                var listID = this.EmulationIDs.join(",");
            } catch (error) {
                console.log(error);
            }

            try {
                // gọi api thực hiện xóa nhiều
                deleteMultiple(listID).then(res => {
                    window.location.reload();
                    this.pageIndex = 1;

                    // setTimeout(function () {//load lại data
                    //     this.loadData();
                    // }, 400);
                    //hiển thị toast thông báo
                    this.showToast(this.$t("DeleteSucces"));
                    this.isDelectAll = this.isSelectAll = false;
                    //gắn mảng lưu trữ id về rỗng
                    this.EmulationIDs = [];
                    this.EmulationIDPage = [];
                    this.isShowCheckActions = false;
                }).catch(error => {
                    console.log(error);
                })
            } catch (error) {
                console.log(error);
            }
        },



        /**
         * khi nhập giá trị tìm kiếm
         * author:DPQuy (13/11/2022)
         * **/
        searchInput(mode) {
            try {
                //nếu ấn enter thì ko thực hiện hàm tự động search
                clearTimeout(this.debounce);
                this.debounce = setTimeout(() => {
                    //khi txtSearch rỗng
                    if (this.txtSearch == "") {
                        //load dữ liệu theo trang và số bản ghi theo filter
                        this.loadData();
                    }
                    else {
                        this.pageIndex = 1;
                        //khi filter rỗng thì lấy ra bản ghi theo số trang và số bản ghi
                        this.loadData();
                    }
                }, 750);

                console.log(mode);

            } catch (error) {
                console.log(error);
            }
        },

        /**
         * sự kiện nhấn nút enter thực hiện tìm kiếm
         * author:DPQuy (13/11/2022)
         * **/
        keydownSearch(event) {
            try {
                if (this.txtSearch == "") {
                    //load dữ liệu theo trang và số bản ghi theo filter
                    this.loadData();
                }
                else {
                    this.pageIndex = 1;
                    //khi filter rỗng thì lấy ra bản ghi theo số trang và số bản ghi
                    this.loadData();
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * click checkbox all
         * author:DPQuy (13/11/2022)
         * **/
        changeCheckedAll() {

            try {
                //khi đã tích chọn bản ghi trước đó
                //thực hiện hủy select toàn bộ
                if (this.EmulationIDPage[this.pageIndex].length !== 0) {
                    //ẩn icon hủy tất cả
                    this.isDelectAll = false;
                    this.isSelectAll = false;
                    this.isSystem = false;
                    //gán mảng về rỗng
                    this.EmulationIDPage[this.pageIndex] = [];
                    console.log(this.EmulationIDPage.length);
                    //kiểm tra xem trang khác còn bản ghi đang được chọn hay ko
                    this.sumChecked();
                    return;
                }

                //sét mảng về rỗng để thêm lại toàn bộ id trên trang
                this.EmulationIDPage[this.pageIndex] = [];
                this.isDelectAll = false;
                if (this.isSelectAll) {
                    for (const emu in this.data) {
                        //kiểm tra xem bản ghi có phải dữ liệu hệ thống ko
                        if (this.data[emu].IsSystem == enumeration.IsSystem.isSystem) {
                            this.isSystem = true;
                        }
                        this.EmulationIDPage[this.pageIndex].push(this.data[emu].EmulationID);
                    }
                }
                //lấy ra tổng số các danh hiệu đã chọn
                this.sumChecked();
            } catch (error) {
                console.log(error);
            }
        },
        /**
        * click checkbox trên từng tr
        * author: DPQuy (13/11/2022)
        * **/
        cbSingleStatus(emulation) {
            try {
                //lấy ra phần tử đã selected phục vụ cho xóa nhiều
                this.emulationSelected = emulation;
                //kiểm tra xem bản ghi có phải dữ liệu hệ thống ko
                if (emulation.IsSystem == enumeration.IsSystem.isSystem) {
                    this.isSystem = !this.isSystem;
                }
                //lấy ra tổng số các danh hiệu đã chọn
                this.sumChecked();
                //hiển thị các acitons khi checkbox
                //nếu đang mở thì kiểm tra xem số lượng checkbox
                //bằng 0 thì đóng, khác 0 thì giữ nguyên
                //chỉ thay đổi số lượng

                //nếu số lượng các checkbox = số bản ghi
                //1. checkboxall = true
                //2. checkboxall = false
                if (this.EmulationIDPage[this.pageIndex].length === this.data.length) {
                    this.isSelectAll = true;
                    this.isDelectAll = false;
                }
                else if (this.EmulationIDPage[this.pageIndex].length === 0) {
                    this.isDelectAll = false;
                    this.isSelectAll = false;
                }
                else {
                    this.isDelectAll = true;
                    this.isSelectAll = false;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Tính toán tổng các bản ghi đã lựa chọn
         * author:DPQuy (13/11/2022)
         * **/
        sumChecked() {
            try {
                if (this.EmulationIDPage.length !== 0) {
                    var total = 0;
                    for (const emu in this.EmulationIDPage) {
                        total += this.EmulationIDPage[emu].length;
                    }
                    this.totalSelected = total;


                    //kiểm tra xem số lượng bản ghi đang select
                    //nếu ko = 0 thì hiển thị actions cho các bản ghi
                    //nếu = thì ẩn
                    if (this.totalSelected === 0) {
                        this.isShowCheckActions = false;
                    }
                    else {
                        this.isShowCheckActions = true;
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm ẩn/ hiển menu bộ lọc
         * author: DPQuy(13/11/2022)
         * **/
        showFilterBox() {
            this.isShowFilterBox = !this.isShowFilterBox;
        },

        /**
         * click dấu x trên input search
         * thực hiện clear text
         * author:DPQuy (13/11/2022)
         * **/
        clearText() {
            this.txtSearch = '';
            this.pageIndex = 1;
            this.loadData();
        },
        /**
         * click nút thêm danh hiệu
         * author: DPQuy (13/11/2022)
         * **/
        btnAddEmulation(mode, emulation) {
            try {
                //0.thêm mới
                //1.Chỉnh sửa
                if (mode == enumeration.FormMode.EditMode) {
                    this.emulationSelected = emulation;
                }
                this.EditMode = mode;
                //gọi hàm thực hiện mở form chi tiết
                this.showEmulationDetail();
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * click icon more khi hover
         * từng tr
         * author: DPQuy (13/11/2022)
         * **/
        showRowAction() {
            this.isShowRowAction = !this.isShowRowAction;
        },
        /**
         * sự kiện khi ko hover ra ngoài
         * tr
         * ***/
        moveleaveTr() {
            //ẩn các action trên tr
            this.isShowRowAction = false;
        },
        /**
         * Hàm show popup chi tiết danh hiệu
         * author: DPQuy (13/11/2022)
         * ***/
        showEmulationDetail() {
            try {
                this.isShowEmulationDetail = !this.isShowEmulationDetail;
                //ẩn filter box nếu đang mở
                this.isShowFilterBox = false;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * load danh sách các danh hiệu
         * author:DPQuy(13/11/2022)
         * **/
        loadData() {
            try {
                var me = this;
                this.isLoading = true;
                loadData(this.pageIndex, this.pageSize, this.txtSearch, this.filterSuiteText).then(res => {
                    me.data = res.data;
                    //số trang dựa theo tổng số bản ghi trên 1 trang
                    me.TotalPage = res.TotalPage;
                    //tổng số bản ghi
                    me.TotalRecord = res.TotalRecord;
                    setTimeout(() => { this.isLoading = false }, 500);
                }).catch(error => console.log(error));
            } catch (error) {
                console.log(error);
            }


        }
    },

    created() {
        this.loadData();
        try {
            //kiểm tra mảng các checked theo từng trang có underfine ko
            if (!this.EmulationIDPage[this.pageIndex]) {
                this.EmulationIDPage[this.pageIndex] = [];
            }
        } catch (error) {
            console.log(error);
        }
    },
    beforeUpdate() {
        try {
            //kiểm tra mảng các checked theo từng trang có underfine ko
            if (!this.EmulationIDPage[this.pageIndex]) {
                this.EmulationIDPage[this.pageIndex] = [];
            }
        } catch (error) {
            console.log(error);
        }

    },
}
</script>


<style>
@import url(../css/views/emulationlist.css);
@import url(../css/base/table.css);
</style>