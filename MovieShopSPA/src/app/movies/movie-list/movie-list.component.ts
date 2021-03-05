import {Component, OnDestroy, OnInit} from '@angular/core';
import {MovieCard} from '../../shared/models/movie-card';
import {Subscription} from 'rxjs';
import {MovieService} from '../../core/services/movie.service';
import {PaginationResponse} from '../../shared/models/types';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit, OnDestroy {

  movies: MovieCard[] | undefined;
  subscription?: Subscription;

  constructor(protected movieService: MovieService) {
  }

  paginationResponse?: PaginationResponse<MovieCard>;

  updateState = (res: PaginationResponse<MovieCard>) => {
    this.paginationResponse = res;
    this.movies = res.data;
  }

  ngOnInit(): void {
    this.subscription = this.movieService.getAllMovie().subscribe(this.updateState);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  nextPage(): void {
    const {pageCount, pageNumber} = this.paginationResponse as PaginationResponse<MovieCard>;
    if (pageNumber < pageCount) {
      this.movies = undefined;
      this.subscription?.unsubscribe();
      this.subscription = this.movieService.getAllMovie(pageNumber + 1).subscribe(this.updateState);
    }
  }

  prevPage(): void {
    const {pageCount, pageNumber} = this.paginationResponse as PaginationResponse<MovieCard>;
    if (pageNumber > 1) {
      this.movies = undefined;
      this.subscription?.unsubscribe();
      this.subscription = this.movieService.getAllMovie(pageNumber - 1).subscribe(this.updateState);
    }
  }
}
