import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './core/layout/header/header.component';
import { HomeComponent } from './home/home.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { HttpClientModule } from '@angular/common/http';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';
import { SpinnerComponent } from './shared/components/spinner/spinner.component';
import { GenresDropdownComponent } from './shared/components/genres-dropdown/genres-dropdown.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { AuthModule } from './auth/auth.module';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    MovieListComponent,
    MovieCardComponent,
    SpinnerComponent,
    GenresDropdownComponent,
    MovieDetailsComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, NgbModule, HttpClientModule, AuthModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
