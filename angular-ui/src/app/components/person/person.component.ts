import {Component, inject, Input, OnChanges} from '@angular/core';
import {PersonDto} from '../../dto/person-dto';
import {PersonService} from '../../services/person.service';
import {ActorService} from '../../services/actor.service';
import {DirectorService} from '../../services/director.service';
import {ErrorService} from '../../services/error.service';
import {forkJoin, of} from 'rxjs';
import {LoadingAbstractComponent} from '../abstract/loading.abstract.component';
import {DirectorFilmDto, DirectorShowDto} from '../../dto/director-dto';
import {CharacterActorDto} from '../../dto/character-dto';
import {AsyncPipe, DatePipe} from '@angular/common';
import {LoadingComponent} from '../loading/loading.component';
import {CountryService} from '../../services/country.service';

@Component({
  selector: 'app-person',
  imports: [
    AsyncPipe,
    LoadingComponent,
    DatePipe
  ],
  templateUrl: './person.component.html',
  styleUrl: './person.component.scss'
})
export class PersonComponent extends LoadingAbstractComponent implements OnChanges {
  /**
   * FilmID to be found in the API.
   */
  @Input()
  personId: string

  /**
   * Detailed information about person
   */
  person: PersonDto

  /**
   * Shows that person directed.
   */
  directedShows: DirectorShowDto[] = []

  /**
   * Films that the person directed.
   */
  directedFilms: DirectorFilmDto[] = []

  /**
   * Shows and films, that person played in.
   */
  actedMedia: CharacterActorDto[] = []

  // services
  personService = inject(PersonService)
  actorService = inject(ActorService)
  directorService = inject(DirectorService)
  countryService = inject(CountryService)
  private errorService = inject(ErrorService)

  ngOnChanges(): void {
    this.init()
  }

  private init(): void {
    this.startLoading();
    this.loadPerson()
  }

  private loadPerson(): void {
    this.personService.getPerson(this.personId).subscribe({
      next: person => {
        this.person = person
        console.log(person)
        this.loadCareerContent()
      },
      error: err => this.errorService.emitError(err.error.message, err.status)
    })
  }

  private loadCareerContent(): void {
    const shows$ = this.person.directorId ? this.directorService.getDirectorShows(this.person.directorId) : of([])
    const films$ = this.person.directorId ? this.directorService.getDirectorFilms(this.person.directorId) : of([])
    const acted$ = this.person.actorId ? this.actorService.getActorMedias(this.person.actorId) : of([])

    forkJoin([shows$, films$, acted$]).subscribe({
      next: ([shows, films, acted]) => {
        this.directedShows = shows
        this.directedFilms = films
        this.actedMedia = acted

        this.completeLoading()
      },
      error: err => this.errorService.emitError(err.error.message, err.status)
    })
  }

  getPersonName(): string {
    const { firstName, middleName, lastName } = this.person
    return `${firstName} ${middleName ? middleName + ' ' : ''}${lastName}`
  }

  getPersonRoles(): string {
    const roles = [];
    if (this.person.actorId) {
      roles.push('Actor');
    }
    if (this.person.directorId) {
      roles.push('Director');
    }
    return roles.join(' - ');
  }
}
