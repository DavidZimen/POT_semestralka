import {HttpEvent, HttpHandlerFn, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';

export function apiUrlInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const apiReq = req.clone({
    url: `${environment.apiUrl}${environment.apiUrl.endsWith('/') || req.url.endsWith('/') ? '' : '/'}${req.url}` }
  )
  return next(apiReq)
}
