import axios from "axios";
import { interceptors } from "../interceptors";

const lmsInstance = axios.create({
    baseURL: `${import.meta.env.VITE_LMS_API_URL}api/`,
});

interceptors(lmsInstance);

export default lmsInstance;