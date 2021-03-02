import {Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {Genre} from '../models/types';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(protected apiService: ApiService) {
  }

  getAllGenres = () => {
    return this.apiService.getAll<Genre>('genres');
  }
}
