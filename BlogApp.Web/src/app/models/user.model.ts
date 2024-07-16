import { SafeResourceUrl } from "@angular/platform-browser";

export interface UserModel{
    id: number;
    username: string;
    profileImageContent: string;
    profileImageContentSafe: SafeResourceUrl;
}