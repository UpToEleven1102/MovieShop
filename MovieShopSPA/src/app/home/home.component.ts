import {Component, OnDestroy, OnInit} from '@angular/core';
import {MovieCard} from '../core/models/movie-card';
import {MovieService} from '../core/services/movie.service';
import {Subscription} from 'rxjs';
import {PaginationResponse} from '../core/models/types';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
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
