import { Injectable } from '@angular/core';
import { HttpHandler, HttpRequest, HttpInterceptor, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

//import { ToastrService } from 'ngx-toastr';

const NO_RESPONDE = 'Il y a un problème de communication, vérifiez votre connexion internet et réessayez plus tard';
const ERROR_500 = "Une erreur interne s'est produite, veuillez réessayer plus tard ou contacter le support technique";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  //constructor(private toastr: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
      catchError(error => {

        let errorMessage = 'Une erreur est survenue ';
        if (error instanceof ErrorEvent) {
          // client-side error
          errorMessage = NO_RESPONDE;
        }
        else {
          // backend error
          if (error instanceof HttpErrorResponse) {
            switch (error.status) {
              case 0:
                errorMessage = NO_RESPONDE;
                break;
              case 400:
                errorMessage = errorMessage + ': ' + error.message;
                break;
              case 401:
                errorMessage = error.message;
                break;
              case 403:
                if (error.error.apiError) {
                  errorMessage = error.message;
                } else {
                  errorMessage = "Le rôle de l'utilisateur n'a pas accès à cette option";
                }
                break;
              case 406:
                errorMessage = error.message;
                break;
              case 500:
                errorMessage = ERROR_500;
                break;
            }
          } else {
            errorMessage = error.message;
          }
        }

        console.log(errorMessage);
        
        //this.toastr.error(errorMessage);

        return throwError(() => errorMessage);
      })
    );
  }

}


