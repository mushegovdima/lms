import type { User } from "@/models";

export interface AuthState {
    token: string,
    refreshToken: string,
    expireDate: string,
    user: User | null,
    userId: number | null,
}
