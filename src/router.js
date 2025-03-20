import { createMemoryHistory, createRouter } from 'vue-router'

import App from './App.vue'
import NewsWriting from './components/NewsWriting.vue'
import NewsPanel from './components/NewsPanel.vue'

const routes = [
    { path: '/', component: NewsPanel },
    { path: '/create', component: NewsWriting },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

export default router