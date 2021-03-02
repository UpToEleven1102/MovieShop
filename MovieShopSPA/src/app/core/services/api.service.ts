import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {map} from 'rxjs/operators';
import {PaginationResponse} from '../models/types';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(protected http: HttpClient) {
  }

  getAllPagination<T>(path: string, pageNumber?: number, pageSize?: number): Observable<PaginationResponse<T>> {
    let params = new HttpParams();
    if (pageNumber) {
      params = params.append('pageNumber', `${pageNumber}`);
    }
    if (pageSize) {
      params = params.append('pageSize', `${pageSize}`);
    }
    return this.http.get(`${environment.apiUrl}${path}`, { params }).pipe(
      map(res => res as PaginationResponse<T>)
    );
  }

  getAll<T>(path: string): Observable<T[]> {
    return this.http.get(`${environment.apiUrl}${path}`).pipe(
      map(res => res as T[])
    );
  }

  getById<T>(id: number, path: string): Observable<T> {
    return this.http.get(`${environment.apiUrl}${path}/` + id).pipe(
      map(res => res as T)
    );
  }
}
