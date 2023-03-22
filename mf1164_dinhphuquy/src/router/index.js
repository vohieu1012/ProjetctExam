import { createRouter, createWebHistory } from 'vue-router'
const routes = [
    {
        path: '/emulation-title',
        name: 'emulation-title',
        component: () => import(/* webpackChunkName: "about" */ '../views/EmulationList.vue')
    },
    {
        path: '/',
        name: 'Home',
        component: () => import(/* webpackChunkName: "about" */ '../App.vue')
    },
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router