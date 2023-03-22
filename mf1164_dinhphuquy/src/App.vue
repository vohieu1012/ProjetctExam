<template>
  <TheHeader />
  <TheNavBar />
  <TheContent />
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue';
import TheContent from './components/layout/TheContent.vue'
import TheNavBar from './components/layout/TheNavBar.vue'
export default {
  name: 'App',
  components: {
    TheHeader,
    TheNavBar,
    TheContent,
  }
}
</script>

<style>
@import url(./css/main.css);
</style>
