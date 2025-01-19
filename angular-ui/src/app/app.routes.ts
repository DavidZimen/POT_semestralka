import {Routes} from '@angular/router';
import {UiRoutes} from './constants/UiRoutes';


export const routes: Routes = [
  { path: '', redirectTo: UiRoutes.Film, pathMatch: 'full' },
  {
    path: UiRoutes.Person,
    title: 'People',
    loadComponent: () => import('./components/people/people.component').then(x => x.PeopleComponent)
  },
  {
    path: `${UiRoutes.Film}`,
    title: 'Films',
    loadComponent: () => import('./components/films/films.component').then(x => x.FilmsComponent)
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
