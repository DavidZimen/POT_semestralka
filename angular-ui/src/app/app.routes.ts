import {Routes} from '@angular/router';
import {UiRoutes} from './constants/UiRoutes';


export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: UiRoutes.Home,
    title: 'Home Page',
    loadComponent: () => import('./components/home/home.component').then(x => x.HomeComponent)
  },
  {
    path: `${UiRoutes.Film}/:filmId`,
    title: 'Film',
    loadComponent: () => import('./components/film/film.component').then(x => x.FilmComponent)
  },
  {
    path: `${UiRoutes.Person}/:personId`,
    title: 'Person',
    loadComponent: () => import('./components/person/person.component').then(x => x.PersonComponent)
  },
  {
    path: UiRoutes.Error,
    title: 'Error',
    loadComponent: () => import('./components/error/error.component').then(x => x.ErrorComponent)
  }
]
