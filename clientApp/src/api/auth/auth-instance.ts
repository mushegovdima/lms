import axios from "axios";

const url = import.meta.env.LMS_AUTH_URL;

export const authInstance = axios.create({
    baseURL: `${url}api/`,
    headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
});
