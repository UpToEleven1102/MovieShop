import {Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {Genre} from '../models/types';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(protected apiService: ApiService<Genre>) {
  }

  getAllGenres = () => {
    return this.apiService.getAll('genres');
  }
}
