import { authService } from "@/api/auth/auth";
import type { AuthState } from "./types";
import { type Commit } from "vuex";
import type { LoginForm, RegisterForm, User, UserTokenResponse } from "@/models";
import { userService } from "@/api/auth";
import router from "@/router";

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

function clearStorage(state: AuthState) {
    localStorage.removeItem('token');
    localStorage.removeItem('refresh-token');
    localStorage.removeItem('token-expire');
    state = {} as AuthState;
}

export const auth = {
    namespaced: true,
    state: initialState,
    getters: {
        token: (state: AuthState) => state.token,
        isActive: (state: AuthState) => !!state.token,
        refreshToken: (state: AuthState) => state.refreshToken,
    },
    actions: {
        async init({ commit, getters }) {
            try {
                if (!getters.isActive) return;
                const res = await userService.getMe();
                commit('init', res);
                return res;
            }
            catch {
                commit('logout')
            }
        },

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
        init(state: AuthState, res: User) {
            console.debug('init')
            state.token = initialState.token;
            state.expireDate = initialState.expireDate;
            state.refreshToken = initialState.refreshToken;
            state.userId = res.id;
            state.user = res;
        },
        async loginSuccess(state: AuthState, res: UserTokenResponse) {
            console.debug('login success')
            state.token = res.token;
            state.expireDate = res.expireDate;
            state.refreshToken = res.refreshToken;
            if (state.userId !== res.userId || !state.user) {
                state.user = await userService.getById(res.userId);
            }
            state.userId = res.userId;
            router.push({ name: 'home' } )
        },
        loginFailure(state: AuthState) {
            console.debug('login failure')
            clearStorage(state);
            router.push({ name: 'login' });
        },
        logout(state: AuthState) {
            console.debug('logout')
            clearStorage(state);
            router.push({ name: 'login' });
        },
    },
};
