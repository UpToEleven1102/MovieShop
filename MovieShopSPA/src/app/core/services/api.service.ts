import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpParamsOptions } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { catchError, map } from 'rxjs/operators';
import { PaginationResponse } from '../../shared/models/types';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  header: HttpHeaders;
  constructor(protected http: HttpClient) {
    this.header = new HttpHeaders();
    this.header.append('Content-Type', 'application/json');
  }

  getAllPagination<T>(path: string, pageNumber?: number, pageSize?: number): Observable<PaginationResponse<T>> {
    let params = new HttpParams();
    if (pageNumber) {
      params = params.append('pageNumber', `${pageNumber}`);
    }
    if (pageSize) {
      params = params.append('pageSize', `${pageSize}`);
    }
    return this.http.get(`${environment.apiUrl}${path}`, { params }).pipe(map((res) => res as PaginationResponse<T>));
  }

  getAll<T>(path: string): Observable<T[]> {
    return this.http.get(`${environment.apiUrl}${path}`).pipe(map((res) => res as T[]));
  }

  getById<T>(id: number, path: string): Observable<T> {
    return this.http.get(`${environment.apiUrl}${path}/` + id).pipe(map((res) => res as T));
  }

  postData<T>(path: string, data: object, options: HttpParamsOptions = {}): Observable<T> {
    return this.http.post(`${environment.apiUrl}${path}/`, data, { headers: this.header, ...options }).pipe(
      map((res) => res as T),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.log(error.error.errorMessage);
      console.error(`Backend returned code ${error.status}, ` + `body was: ${error.message}`);
    }
    // return an observable with a user-facing error message
    return throwError(error.error.errorMessage);
  }
}
