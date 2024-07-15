import { inject, Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { LoginModel } from "../models/login.model";
import { JwtHelperService } from "@auth0/angular-jwt";
import { catchError, map, throwError } from "rxjs";

@Injectable()
export class AuthenticationService{

    httpClient = inject(HttpClient);

    usernameSignal = signal<string | null>(null);
    roleSignal = signal<number | null>(null);

    private baseUrl: string = `${environment.url}authentication`;
    private jwtModule: JwtHelperService;

    constructor(){
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
            this.roleSignal.set(this.getUserRole());
        }));            
    }

    logout(){
        localStorage.removeItem('sessionToken');
        this.usernameSignal.set(null);
        this.roleSignal.set(null);
    }

    authenticated(): boolean{
        return !this.jwtModule.isTokenExpired(localStorage.getItem('sessionToken'));
    }

    getToken() : string | null{
        const token = localStorage.getItem('sessionToken');
        return token !== null ? token.toString() : null;
    }

    getUsername(): string{
        return this.jwtModule.decodeToken(localStorage.getItem('sessionToken')!).Username;
    }

    getUserId(): number{
        return +this.jwtModule.decodeToken(localStorage.getItem('sessionToken')!).userId;
    }

    getUserRole(): number{
        return +this.jwtModule.decodeToken(localStorage.getItem('sessionToken')!).userRoleId;
    }

    setSignals(){
        if(this.authenticated()){
            this.usernameSignal.set(this.getUsername());
            this.roleSignal.set(this.getUserRole());
        }
        else{
            this.usernameSignal.set(null);
            this.roleSignal.set(null);
        }
    }
}