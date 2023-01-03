import {Injectable} from '@angular/core';
import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import {catchError, Observable, of} from 'rxjs';
import {Router} from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.has('X-Skip-Interceptor')) {
      const headers = req.headers.delete('X-Skip-Interceptor');
      return next.handle(req.clone({headers}));
    }
    return next.handle(req).pipe(
      catchError(error => {
        switch (error.status) {
          case 401:
            this.router.navigate(['/login'])
            break;
          case 403:
            this.router.navigate(['/login'])
            break;
          case 404:
            this.router.navigate(['/not-found'])
            break;
        }
        return of(error);
      })
    )
  }
}
