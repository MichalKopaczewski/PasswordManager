import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(
                catchError(error => {
                    if (error instanceof HttpErrorResponse) {
                        if (error.status === 401) {
                            return throwError('Brak dostępu');
                        }
                        const applicationError = error.error;
                        if (applicationError) {
                            console.error(applicationError);
                            return throwError(applicationError);
                        }
                    }
                    const serverError = error.error;
                    let modalStatesErrors = '';
                    if (serverError && typeof serverError === 'object') {
                        for (const key in serverError) {
                            if (serverError[key]) {
                                modalStatesErrors += serverError[key] + '\n';
                            }
                        }
                    }
                    console.log(JSON.stringify(error));
                    return throwError(modalStatesErrors + serverError + 'Server error');
                }));
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
}