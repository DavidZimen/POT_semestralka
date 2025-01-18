import {Component, inject, Input, OnDestroy, OnInit} from '@angular/core';
import {FilmService} from '../../services/film.service';
import {FilmDto} from '../../dto/film-dto';

@Component({
  selector: 'app-film',
  imports: [],
  templateUrl: './film.component.html',
  styleUrl: './film.component.scss'
})
export class FilmComponent implements OnInit, OnDestroy {

  /**
   * FilmID to be found in the API.
   */
  @Input()
  filmId: string

  /**
   * Film to be displayed.
   */
  film: FilmDto

  /**
   * If the fetching of the film context is still running.
   */
  loading = true

  // services
  private filmService = inject(FilmService)

  ngOnInit(): void {
  }

  loadFilm(): void {
    this.filmService.getFilm(this.filmId).subscribe({
      next: film => this.film = film,
      error: err => console.error(err)
    })
  }

  ngOnDestroy(): void {
  }
}
