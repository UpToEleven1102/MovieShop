import {Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {Observable} from 'rxjs';
import {MovieCard} from '../models/movie-card';
import {MovieDetail, PaginationResponse} from '../models/types';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(protected apiService: ApiService) {
  }

  getTopGrossingMovies(): Observable<MovieDetail[]> {
    return this.apiService.getAll<MovieDetail>('movies/toprevenue');
  }

  getAllMovie(pageNumber?: number, pageSize?: number): Observable<PaginationResponse<MovieCard>> {
    return this.apiService.getAllPagination<MovieCard>('movies', pageNumber, pageSize);
  }

  getMovieById(id: number): Observable<MovieDetail> {
    return this.apiService.getById<MovieDetail>(id, 'movies');
  }
}
