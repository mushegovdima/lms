import type { LoginForm, RegisterForm, UserTokenResponse } from "@/models";
import authInstance from "./auth-instance";

export const authService = {
    register: (data: RegisterForm) : Promise<UserTokenResponse> => {
        return authInstance.post('auth/register', data).then(x => x.data);
    },

    login: (data: LoginForm) : Promise<UserTokenResponse> => {
        return authInstance.post('auth/login', data).then(x => x.data)
    },

    refreshToken: (refreshToken: string) : Promise<UserTokenResponse> => {
        return authInstance.post('auth/refresh-token', refreshToken).then(x => x.data)
    },

    logout: () => {
        return authInstance.post('auth/logout').then(x => x.data)
    },
}
