import { HttpContextToken } from "@angular/common/http";

export const AUTH_REQUEST = new HttpContextToken<boolean>(() => false);