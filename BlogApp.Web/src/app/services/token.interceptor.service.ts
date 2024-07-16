import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { AuthenticationService } from "./authentication.service";
import { inject } from "@angular/core";
import { AUTH_REQUEST } from "../consts/auth.request";

export class TokenInterceptorService implements HttpInterceptor{

    authService = inject(AuthenticationService);

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      if(!req.context.get(AUTH_REQUEST))            
          return next.handle(req);

      const token = this.authService.getToken();

      if(token !== null){
          req = req.clone({
              setHeaders: {
                Authorization: `Bearer ${token}`,
              },
          });
      }

      return next.handle(req).pipe(
          catchError((err) => {
            if (err.status === 401) {
              this.authService.logout();
            }

            const error = err.error.message || err.statusText;

            return throwError(() => error);
          })
        );    
    }
}