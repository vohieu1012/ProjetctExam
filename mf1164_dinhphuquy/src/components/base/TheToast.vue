<template>
    <div class="m-toast-box">
        <div class="m-toast flex align-items-center w-100 h-100">
            <div class="mi-cego mi-16 mi-checkdone"></div>
            <div class="toast-content p-l-8">{{ toastContent }}</div>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        toastContent: {
            type: String
        }
    }
}
</script>
<style>
@import url(../../css/base/toast.css);
</style>