import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { LoginModel } from "../models/login.model";
import { Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { catchError, map, throwError } from "rxjs";

@Injectable()
export class AuthenticationService{

    usernameSignal = signal<any | undefined | null>(undefined);

    private baseUrl: string = `${environment.url}authentication`;
    private jwtModule: JwtHelperService;

    constructor(private httpClient: HttpClient, private router: Router){
        this.jwtModule = new JwtHelperService;
    }

    login(loginInfo: LoginModel){
        return this.httpClient.post<any>(`${this.baseUrl}`, loginInfo)
        .pipe(catchError(error => {
            return throwError(() => error)
        }),
        map(result => {
            localStorage.setItem('sessionToken', result.token);
            this.usernameSignal.set(this.getUsername());
        }));            
    }

    logout(){
        localStorage.removeItem('sessionToken');
        this.usernameSignal.set(null);
    }

    authenticated() : boolean{
        return !this.jwtModule.isTokenExpired(localStorage.getItem('sessionToken'));
    }

    getUsername(): any{
        return this.jwtModule.decodeToken(localStorage.getItem('sessionToken')!).Username;
    }

    setTokenSignal(){
        if(this.authenticated())
            this.usernameSignal.set(this.getUsername());
        else
            this.usernameSignal.set(null);
    }
}