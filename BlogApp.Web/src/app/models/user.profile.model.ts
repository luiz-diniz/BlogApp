import { UserModel } from "./user.model";

export interface UserProfileModel extends UserModel{
    description?: string;
}