export interface UserRegisterModel{
    username: string;
    password: string;
    passwordConfirmation: string;
    email: string;
    description?: string;
    profileImageContent?: string;
}