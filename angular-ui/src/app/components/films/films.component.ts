import {Component, inject} from '@angular/core';
import {FilmService} from '../../services/film.service';
import {AsyncPipe, DatePipe, NgTemplateOutlet} from '@angular/common';
import {Card} from 'primeng/card';
import {ImageComponent} from '../image/image.component';
import {MaxLengthPipe} from '../../pipes/max-length.pipe';
import {Router} from '@angular/router';
import {UiRoutes} from '../../constants/UiRoutes';
import {FilmFormComponent} from '../film/film-form/film-form.component';
import {KeycloakService} from 'keycloak-angular';
import {Role} from '../../enum/role.enum';
import {Button} from 'primeng/button';
import {ConfirmService} from '../../services/confirm.service';
import {ConfirmType} from '../../enum/confirm-type.enum';
import {FilmDto} from '../../dto/film-dto';
import {ToastService} from '../../services/toast.service';

@Component({
  selector: 'app-films',
  imports: [
    AsyncPipe,
    NgTemplateOutlet,
    Card,
    ImageComponent,
    DatePipe,
    MaxLengthPipe,
    FilmFormComponent,
    Button
  ],
  templateUrl: './films.component.html',
  styleUrl: './films.component.scss'
})
export class FilmsComponent {

  // services
  filmService = inject(FilmService)
  keycloakService = inject(KeycloakService)
  router = inject(Router)
  private toastService = inject(ToastService)
  private confirmService = inject(ConfirmService)

  films$ = this.filmService.getFilms()

  initRemoveFilm(film: FilmDto): void {
    this.confirmService.confirm({
      header: 'Film deletion',
      message: `Are you sure you want to delete film ${film.title} ?`,
      type: ConfirmType.DELETE,
      accept: () => this.removeFilm(film)
    })
  }

  private removeFilm(film: FilmDto): void {
    this.filmService.deleteFilm(film.filmId).subscribe({
      next: () => {
        this.toastService.showSuccess(`Film ${film.title} deleted successfully.`)
        this.films$ = this.filmService.getFilms()
      },
      error: err => this.toastService.showError(err.error.message)
    })
  }

  protected readonly UiRoutes = UiRoutes;
  protected readonly Role = Role;
}
