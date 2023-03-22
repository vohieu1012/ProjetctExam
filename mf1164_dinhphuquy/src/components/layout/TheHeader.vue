<template>
    <div class="m-header">
        <div class="header-container flex align-items-center">
            <div class="header-left flex align-items-center">
                <div class="header-left-icon flex-center">
                    <div class="mi-24 mi mi-app"></div>
                </div>
                <div class="header-left-logo flex align-items-center">
                    <div class="header-logo m-r-12 flex-center">
                        <div class="mi-32 mi-logo"></div>
                    </div>
                    <div class="logo-title">MISA CeGov</div>
                </div>
            </div>
            <div class="flex-1"></div>
            <div class="header-right flex align-items-center h-100">
                <div class="right-btt-action flex-center positon-relative">
                    <div class="mi mi-24 mi-checkmark-temp"></div>
                    <span class="tooltip-text" id="bottom">Thông báo</span>
                </div>
                <div class="right-btt-action flex-center positon-relative">
                    <div class="mi mi-24 mi-help"></div>
                    <span class="tooltip-text" id="bottom">Hướng dẫn</span>
                </div>
                <div class="right-btt-action flex-center positon-relative">
                    <div class="mi mi-24 mi-document"></div>
                    <span class="tooltip-text" id="bottom">Văn bản quy định</span>
                </div>
                <div class="right-btt-action flex-center positon-relative">
                    <div class="mi mi-24 mi-setting"></div>
                    <span class="tooltip-text" id="bottom">Thiết lập hệ thống</span>
                </div>
                <div class="m-user flex-center">
                    <div class="mi-32">
                        <img src="../../assets/icons/getavatar.png" alt="" class="avatar">
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
@import url(../../css/layout/header.css);
</style>