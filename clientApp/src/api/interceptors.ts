import store from "@/store";
import type { AxiosInstance } from "axios";

export function interceptors(instance: AxiosInstance) {
    instance.interceptors.request.use(function (config) {
        const token = store.getters['auth/token'];
        if (token) {
            config.headers["Authorization"] = "Bearer " + token;
        }
        return config;
    });

    instance.interceptors.response.use(function(response) {
        return response;
    }, async function(error) {
        const { response: { status } } = error;
        if (status === 401) {
            store.dispatch('auth/logout');
        } else {
            return Promise.reject(error);
        }
    });
}