import {Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {MovieCard} from '../models/movie-card';
import {PaginationResponse} from '../models/types';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(protected apiService: ApiService<MovieCard>) {
  }

  getAllMovie(pageNumber?: number, pageSize?: number): Observable<PaginationResponse<MovieCard>> {
    return this.apiService.getAllPagination('movies', pageNumber, pageSize);
  }
}
