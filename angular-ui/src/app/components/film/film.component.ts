import {Component, inject, Input, OnChanges} from '@angular/core';
import {FilmService} from '../../services/film.service';
import {FilmDto} from '../../dto/film-dto';
import {forkJoin} from 'rxjs';
import {CharacterMediaDto} from '../../dto/character-dto';
import {KeycloakService} from 'keycloak-angular';
import {PersonDto} from '../../dto/person-dto';
import {PersonService} from '../../services/person.service';
import {HttpErrorResponse} from '@angular/common/http';
import {ErrorService} from '../../services/error.service';
import {LoadingComponent} from '../loading/loading.component';
import {LoadingAbstractComponent} from '../abstract/loading.abstract.component';
import {AsyncPipe, DatePipe} from '@angular/common';
import {MediaGenresComponent} from '../media-genres/media-genres.component';
import {MediaType} from '../../enum/media-type.enum';
import {MediaCharactersComponent} from '../media-characters/media-characters.component';
import {DurationPipe} from '../../pipes/duration.pipe';
import {Router} from '@angular/router';
import {UiRoutes} from '../../constants/UiRoutes';
import {ImageComponent} from '../image/image.component';
import {RatingComponent} from '../rating/rating.component';
import {Panel} from 'primeng/panel';
import {DropdownModule} from 'primeng/dropdown';
import {FormsModule} from '@angular/forms';
import {FilmFormComponent} from './film-form/film-form.component';
import {Role} from '../../enum/role.enum';

@Component({
  selector: 'app-film',
  imports: [
    LoadingComponent,
    AsyncPipe,
    MediaGenresComponent,
    MediaCharactersComponent,
    DatePipe,
    DurationPipe,
    ImageComponent,
    RatingComponent,
    Panel,
    DropdownModule,
    FormsModule,
    FilmFormComponent
  ],
  templateUrl: './film.component.html',
  styleUrl: './film.component.scss'
})
export class FilmComponent extends LoadingAbstractComponent implements OnChanges {

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
   * Director of the film.
   */
  director: PersonDto

  /**
   * Characters in the film.
   */
  characters: CharacterMediaDto[] = []

  // services
  filmService = inject(FilmService)
  personService = inject(PersonService)
  keycloakService = inject(KeycloakService)
  private errorService = inject(ErrorService)
  private router = inject(Router)

  ngOnChanges(): void {
    this.init()
  }

  init(): void {
    this.startLoading()
    const film$ = this.filmService.getFilm(this.filmId)
    const characters$ =  this.filmService.getFilmCharacters(this.filmId)

    forkJoin([film$, characters$]).subscribe({
        next: ([film, actors]) => {
          this.film = film
          this.characters = actors

          // load director
          this.personService.getPerson(this.film.directorPersonId).subscribe({
            next: director => {
              this.director = director
              this.completeLoading()
            },
            error: err => this.errorService.emitError(err.error.message, err.status),
          })
        }, error: (err: HttpErrorResponse) => this.errorService.emitError(err.error.message, err.status)
    })
  }

  showDirector(personId: string): void {
    this.router.navigate([UiRoutes.Person, personId])
  }

  loadCharacters(): void {
    this.filmService.getFilmCharacters(this.filmId).subscribe({
      next: characters => this.characters = characters
    })
  }

  protected readonly MediaType = MediaType;
  protected readonly UiRoutes = UiRoutes;
  protected readonly Role = Role;
}
