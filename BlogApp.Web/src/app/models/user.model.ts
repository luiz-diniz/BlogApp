import { SafeResourceUrl } from "@angular/platform-browser";

export interface UserModel{
    id: number;
    username: number;
    profileImageContent: string;
    profileImageContentSafe: SafeResourceUrl;
}