import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if(err) {
        if(err?.error?.status === 401 || err?.status === 401) {
          document.location.href = '/login';
        }
      }
      return throwError(() => err)
    })
  );
};
