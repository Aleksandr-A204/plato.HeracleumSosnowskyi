import _ from "lodash";

import axios, { type AxiosRequestConfig, type InternalAxiosRequestConfig } from "axios";
declare module "axios" {
  export interface AxiosRequestConfig {
    redirect?: boolean;
  }
}

import router from "@/router/index";

// import AuthenticationClient from "~/API/AuthenticationClient";
// import constants from "@/utils/constants";

let jwtCache: any;

const LoginRedirectHttpClient = axios.create({
  headers: {
    "Content-Type": "application/json"
  }
});

const DefaultHttpClient = axios.create({
  redirect: true,
  headers: {
    "Content-Type": "application/json"
  }
});

DefaultHttpClient.interceptors.request.use(request => requestInterceptor(request));
DefaultHttpClient.interceptors.response.use(response => response, error => responseInterceptor(error));

LoginRedirectHttpClient.interceptors.request.use(request => requestInterceptor(request));
LoginRedirectHttpClient.interceptors.response.use(response => response, error => redirectInterceptor(error));

const setJwtCache = (newCache:any) => jwtCache = _.cloneDeep(newCache);

let isRefreshing = false;

const requestInterceptor = async (request: InternalAxiosRequestConfig<any>) => {
  request.headers.Authorization = `Bearer ${jwtCache?.token}`;
  return request;
};

const responseInterceptor = async (error: { response: { config: AxiosRequestConfig<any>; }; }) => {
  if (_.get(error, "response.status") === 401) {
    if (isRefreshing) {
      while (isRefreshing) {
        await new Promise(resolve => setTimeout(resolve, 250));
      }

      return LoginRedirectHttpClient.request(error.response.config);
    }

    isRefreshing = true;

    try {
      return LoginRedirectHttpClient.request(error.response.config);
    }
    catch (_error) {
      if (error.response.config.redirect) {
        const reason = _.get(_error,
          "inner.response.data.reason");

        router.push({ path: "/login", query: { reason: reason } }).catch(() => {});
      }
    }
    finally {
      isRefreshing = false;
    }
  }

  throw error;
};

const redirectInterceptor = async (error: any) => {
  if (_.get(error, "response.status") === 401) {
    const reason = _.get(error,
      "inner.response.data.reason");

    router.push({ path: "/login", query: { reason: reason } }).catch(() => {});
  }

  throw error;
};

export { DefaultHttpClient as default, setJwtCache };
