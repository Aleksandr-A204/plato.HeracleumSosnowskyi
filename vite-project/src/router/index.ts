import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "",
    name: "main",
    component: () => import("@/view/TheMain.vue")
  },
  {
    path: "/about",
    name: "about",
    component: () => import("@/view/TheAbout.vue")
  }
];

const router = createRouter({
  routes,
  history: createWebHistory()
});

export default router;
