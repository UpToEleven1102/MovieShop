import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {MovieListComponent} from './movies/movie-list/movie-list.component';
import {MovieDetailsComponent} from './movies/movie-details/movie-details.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'movies', component: MovieListComponent},
  {path: 'movies/:id', component: MovieDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
