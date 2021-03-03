import {Component, OnDestroy, OnInit} from '@angular/core';
import {MovieDetail} from '../core/models/types';
import {MovieService} from '../core/services/movie.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  movies?: Array<MovieDetail>;
  subscription?: Subscription;

  constructor(protected movieService: MovieService) {
  }

  ngOnInit(): void {
    this.subscription = this.movieService.getTopGrossingMovies().subscribe(
      res => this.movies = res
    );
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
