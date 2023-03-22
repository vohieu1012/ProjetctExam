<template>
    <div class="m-navbar">
        <div class="navbar-container">
            <div class="navbar-menu">
                <div class="navbar-item nav-dashboard">
                    <div class="navbar-item-content">Tổng quan</div>
                </div>
                <div class="navbar-item nav-movement">
                    <div class="navbar-item-content">Thi đua</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-item nav-reward">
                    <div class="navbar-item-content">Khen thưởng</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-item nav-decision">
                    <div class="navbar-item-content">Quyết định</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-item nav-profile">
                    <div class="navbar-item-content">Hồ sơ</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-item nav-search">
                    <div class="navbar-item-content">Tra cứu</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-item nav-report">
                    <div class="navbar-item-content">Báo cáo</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
                <div class="navbar-line"></div>
                <router-link to="/emulation-title">
                    <div class="navbar-item nav-category">
                        <div class="navbar-item-content">Danh mục</div>
                        <div class="navbar-item-arrow-right"></div>
                    </div>
                </router-link>
                <router-link to="/">
                    <div class="navbar-item nav-system">
                        <div class="navbar-item-content">Thiết lập</div>
                        <div class="navbar-item-arrow-right"></div>
                    </div>
                </router-link>
                <div class="navbar-line"></div>
                <div class="navbar-item nav-guide">
                    <div class="navbar-item-content">Hướng dẫn</div>
                    <div class="navbar-item-arrow-right"></div>
                </div>
            </div>
            <div class="navbar-footer flex align-items-center">
                <div class="navbar-footer-icon">
                    <div class="mi-24 mi mi-toggle m-l-16"></div>
                </div>
                <div class="navbar-footer-title">Thu gọn</div>
            </div>
        </div>
    </div>
</template>



<style>
@import url('../../css/layout/navbar.css')
</style>