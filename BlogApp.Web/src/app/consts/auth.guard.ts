import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthenticationService } from "../services/authentication.service";

export const AUTH_GUARD: CanActivateFn = (
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot
) : Observable<boolean | UrlTree> 
| Promise<boolean | UrlTree> 
| boolean 
| UrlTree=> {

return inject(AuthenticationService).authenticated()
  ? true
  : inject(Router).createUrlTree(['/login']);
};