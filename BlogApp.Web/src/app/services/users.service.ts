import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { UserProfileModel } from "../models/user.profile.model";
import { Observable } from "rxjs";
import { inject, Injectable } from "@angular/core";
import { UserRegisterModel } from "../models/user.register.model";

@Injectable()
export class UsersService{

    private httpClient = inject(HttpClient);
    private baseUrl: string = `${environment.url}users`

    getUserProfile(username: string) : Observable<UserProfileModel>{
        return this.httpClient.get<any>(`${this.baseUrl}/${username}`);
    }

    add(user: UserRegisterModel){
        return this.httpClient.post<UserRegisterModel>(`${this.baseUrl}`, user);
    }
}