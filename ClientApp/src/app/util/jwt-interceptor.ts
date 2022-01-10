import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../account.service';



@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let token = this.accountService.token();
    
    if (token) { 
        request = this.setHeader(request, token);
    }

    return next.handle(request);
  }

  private setHeader(request: HttpRequest<any>, token: string): HttpRequest<any> {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });

    return request;
  }
}