import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { UserProfileModel } from "../models/user.profile.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class UsersService{

    private baseUrl: string = `${environment.url}users`

    constructor(private httpClient: HttpClient){
    }

    getUserProfile(username: string) : Observable<UserProfileModel>{
        return this.httpClient.get<any>(`${this.baseUrl}/${username}`);
    }
}