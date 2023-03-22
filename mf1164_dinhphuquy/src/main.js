import { createApp } from 'vue'
import PrimeVue from 'primevue/config';
import App from './App.vue'
import router from './router'
import resource from './Resource/MisaReource'
const app = createApp(App);
app.use(PrimeVue);
app.use(resource)
app.use(router).mount('#app')

