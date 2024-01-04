import type { Entity } from "./common";

export interface UserTokenResponse {
    userId: number;
    token: string;
    refreshToken: string;
    expireDate: string;
}

export interface RegisterForm {
    login: string;
    password: string;
    name: string;
    email: string;
    phone: string;
}

export interface LoginForm {
    login: string;
    password: string;
}

export interface User extends Entity {
    email: string;
    phone: string;
    name: string;
    login: string;
    createdAt: string;
    isAdmin: boolean;
}