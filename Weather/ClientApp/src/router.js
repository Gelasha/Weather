import Vue from "vue";
import Router from "vue-router";
import bootstrap from "bootstrap-vue";
import Home from "./views/Home.vue";
import History from "./views/History.vue";

Vue.use(Router);
Vue.use(bootstrap);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/history",
      name: "history",
      component: History
    }
  ]
});
