import {Component, OnDestroy, OnInit} from '@angular/core';
import {MovieDetail} from '../../shared/models/types';
import {ActivatedRoute} from '@angular/router';
import {MovieService} from '../../core/services/movie.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit, OnDestroy {
  subscription?: Subscription;
  movie?: MovieDetail;

  constructor(private route: ActivatedRoute, protected movieService: MovieService) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id') as string;
    // tslint:disable-next-line:radix
    this.subscription = this.movieService.getMovieById(parseInt(id)).subscribe(res => this.movie = res);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
