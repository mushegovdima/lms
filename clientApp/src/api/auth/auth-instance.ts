import axios from "axios";
import { interceptors } from "../interceptors";

const authInstance = axios.create({
    baseURL: `${import.meta.env.VITE_LMS_AUTH_URL}api/`,
});

interceptors(authInstance);

export default authInstance;