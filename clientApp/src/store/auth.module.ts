import { authService } from "@/api/auth/auth";
import type { AuthState } from "./types";
import { createStore, type Commit } from "vuex";
import type { LoginForm, RegisterForm, UserTokenResponse } from "@/models";
import { userService } from "@/api/auth";

const initialState : AuthState = {
    token: localStorage.getItem('token') || '',
    refreshToken: localStorage.getItem('refresh-token') || '',
    expireDate: localStorage.getItem('token-expire') || '',
    user: null,
    userId: null,
}

function onAuthSuccess(commit: Commit, data: UserTokenResponse) {
    localStorage.setItem('token', data.token);
    localStorage.setItem('refresh-token', data.refreshToken);
    localStorage.setItem('token-expire', data.expireDate);

    commit('loginSuccess', data);
}
export default createStore<AuthState>({
    state: initialState,
    getters: {
        isActive: (state: AuthState) => () => !!state.token,
        refreshToken: (state: AuthState ) => () => state.refreshToken,
    },
    actions: {
        async login({ commit }, data: LoginForm) {
            try {
                const res = await authService.login(data);
                onAuthSuccess(commit, res);
                return res;
            }
            catch {
                commit('loginFailure')
            }
        },
        async register({ commit }, data: RegisterForm) {
            try {
                const res = await authService.register(data);
                onAuthSuccess(commit, res);
                return res;
            }
            catch {
                commit('loginFailure')
            }
        },
        async logout({ commit }) {
            await authService.logout();
            commit('logout');
        },
    },
    mutations: {
        async loginSuccess(state: AuthState, res: UserTokenResponse) {
            //state.user = user; load currentUser info
            state.userId = res.userId;
            state.user = await userService.getById(res.userId);
        },
        loginFailure(state: AuthState) {
            state = {} as AuthState;
        },
        logout(state: AuthState) {
            state = {} as AuthState;
        },
    },
});
