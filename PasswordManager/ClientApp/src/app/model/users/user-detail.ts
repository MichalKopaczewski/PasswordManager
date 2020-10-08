import { UserRole } from "./user-role";

export interface UserDetail {
    username: string;
    password: string;
    userRoles: UserRole[];
}