import {Component, inject, Input} from '@angular/core';
import {MediaType} from '../../enum/media-type.enum';
import {GenreDto} from '../../dto/genre-dto';
import {GenreService} from '../../services/genre.service';
import {FilmService} from '../../services/film.service';
import {ShowService} from '../../services/show.service';
import {KeycloakService} from 'keycloak-angular';
import {Role} from '../../enum/role.enum';
import {LowerCasePipe} from '@angular/common';
import {Chip} from 'primeng/chip';
import {ToastService} from '../../services/toast.service';
import {HttpErrorResponse} from '@angular/common/http';
import {ConfirmService} from '../../services/confirm.service';
import {ConfirmType} from '../../enum/confirm-type.enum';

@Component({
  selector: 'app-media-genres',
  imports: [
    LowerCasePipe,
    Chip
  ],
  templateUrl: './media-genres.component.html',
  styleUrl: './media-genres.component.scss'
})
export class MediaGenresComponent {

  @Input()
  mediaType: MediaType

  @Input()
  mediaId: string

  @Input()
  genres: GenreDto[] = []

  // services
  private genreService = inject(GenreService)
  private filmService = inject(FilmService)
  private showService = inject(ShowService)
  private keycloakService = inject(KeycloakService)
  private toastService = inject(ToastService)
  private confirmService = inject(ConfirmService)

  canRemoveGenre(): boolean {
    return this.keycloakService.isLoggedIn() && this.keycloakService.isUserInRole(Role.ADMIN.toString())
  }

  initiateRemoveGenre(genre: GenreDto): void {
    this.genres = [...this.genres, genre]

    this.confirmService.confirm({
      header: 'Unassigning genre',
      message: `Are you sure you want to unassign genre ${genre.name} from ${this.mediaType.toLowerCase()} ?`,
      type: ConfirmType.DELETE,
      accept: () => this.removeGenre(genre.genreId),
    })
  }

  private removeGenre(genreId: string): void {
    const removeGenre$ = MediaType.FILM === this.mediaType
      ? this.filmService.removeGenreFromFilm(this.mediaId, genreId)
      : this.showService.removeGenreFromShow(this.mediaId, genreId)

    removeGenre$.subscribe({
      next: () => {
        this.toastService.showSuccess('Genre unassigned successfully.')
        // remove from array
        this.genres.forEach((genre, i) => {
          if (genre.genreId == genreId)
            this.genres.slice(i, 1)
        })
      }, error: (err: HttpErrorResponse) => this.toastService.showError(err.error.message)
    })
  }
}
