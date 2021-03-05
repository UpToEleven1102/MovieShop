import { Component, OnInit } from '@angular/core';
import {GenreService} from '../../../core/services/genre.service';
import {Subscription} from 'rxjs';
import {Genre} from '../../models/types';

@Component({
  selector: 'app-genres-dropdown',
  templateUrl: './genres-dropdown.component.html',
  styleUrls: ['./genres-dropdown.component.css']
})
export class GenresDropdownComponent implements OnInit {
  subscription?: Subscription;
  genres!: Array<Genre>;
  constructor(protected genreService: GenreService) { }

  ngOnInit(): void {
    this.subscription = this.genreService.getAllGenres().subscribe(res => {
      this.genres = res;
    });
  }
}
