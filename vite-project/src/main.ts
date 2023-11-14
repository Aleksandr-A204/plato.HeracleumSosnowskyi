//import * as exp from "@/types/cors/index.d";
import App from "./App.vue";
import router from "@/router/index";
import { createApp } from "vue";
import "@/style.css";

//import cors from "cors";

const app = createApp(App);

app.use(router);
// const exp = express();

// const express: exp.CorsOptions = {
//   origin: "http://localhost:5181",
//   credentials: true, //access-control-allow-credentials:true
//   maxAge: 100,
//   optionsSuccessStatus: 200
// };

// const corsOptions = {
//   origin: "http://localhost:5173",
//   credentials: true, //access-control-allow-credentials:true
//   optionSuccessStatus: 200
// };
// exp.use(cors(corsOptions));

app.mount("#app");
