import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Login } from '../../shared/models/login';
import { Observable } from 'rxjs';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private apiService: ApiService) {}

  login(userLogin: Login): Observable<boolean> {
    return this.apiService.postData('account/login', userLogin).pipe(
      map((res) => {
        return true;
      })
    );
  }
}
