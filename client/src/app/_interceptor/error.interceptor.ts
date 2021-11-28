import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr:MatSnackBar) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                throw modalStateErrors.flat();
              } else {
                this.showSnackBar(error.statusText + '  '+error.status);
              }
              break;
            case 401:
              this.showSnackBar(error.statusText + '  '+error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              //const navigationExtras: NavigationExtras = {state: {error: error.error}}
              //this.router.navigateByUrl('/server-error', navigationExtras);
              this.showSnackBar('server error');
              break;
            default:
              this.toastr.open('Something unexpected went wrong', '',{duration:2500});
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }

  showSnackBar(txt:string){
    this.toastr.open(txt, '', {duration:2500, panelClass:['red-snackbar']})
  }
}