import type { User } from "@/models";
import authInstance from "./auth-instance";

const controllerName = 'user';
export const userService = {
    getById: (id: number) : Promise<User> => {
        return authInstance.get(`${controllerName}/${id}`).then(x => x.data);
    },
    getMe: () : Promise<User> => {
        return authInstance.get(`${controllerName}/getMe`).then(x => x.data);
    },
}
