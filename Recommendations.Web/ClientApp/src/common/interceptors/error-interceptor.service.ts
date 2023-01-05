import {Injectable} from '@angular/core';
import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import {catchError, Observable, of} from 'rxjs';
import {Router} from "@angular/router";
import {UserService} from "../services/user/user.service";
import {state} from "@angular/animations";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private userService: UserService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.has('X-Skip-Interceptor')) {
      const headers = req.headers.delete('X-Skip-Interceptor');
      return next.handle(req.clone({headers}));
    }
    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401) {
          this.router.navigate(['/login'])
        }
        if (error.status === 403) {
          this.userService.isAuthenticated = false
          this.router.navigate(['/access-denied'])
        }
        if (error.status === 404) {
          this.router.navigate(['/not-found'])
        }
        return of(error);
      })
    )
  }
}
