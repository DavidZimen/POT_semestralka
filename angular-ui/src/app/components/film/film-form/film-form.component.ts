import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {FilmService} from '../../../services/film.service';
import {PersonService} from '../../../services/person.service';
import {PersonDto} from '../../../dto/person-dto';
import {FilmDto} from '../../../dto/film-dto';
import {Button} from 'primeng/button';
import {Dialog} from 'primeng/dialog';
import {DropdownModule} from 'primeng/dropdown';
import {FormsModule} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {NgIf} from '@angular/common';
import {PrimeTemplate} from 'primeng/api';
import {Textarea} from 'primeng/textarea';
import {DatePicker} from 'primeng/datepicker';
import {InputNumber} from 'primeng/inputnumber';
import {KeycloakService} from 'keycloak-angular';
import {Role} from '../../../enum/role.enum';
import {ToastService} from '../../../services/toast.service';

@Component({
  selector: 'app-film-form',
  imports: [
    Button,
    Dialog,
    DropdownModule,
    FormsModule,
    InputText,
    NgIf,
    PrimeTemplate,
    Textarea,
    DatePicker,
    InputNumber
  ],
  templateUrl: './film-form.component.html',
  styleUrl: './film-form.component.scss'
})
export class FilmFormComponent implements OnInit {

  @Input()
  film: FilmDto | undefined

  @Input()
  director: PersonDto | undefined

  @Output()
  filmChanged = new EventEmitter<boolean>()

  // update form dialog
  openDialog = false
  id: string
  title: string
  description: string;
  releaseDate: Date;
  duration: number;
  person: PersonDto
  dropdownPersons: PersonDto[] = []

  // services
  filmService = inject(FilmService)
  personService = inject(PersonService)
  keycloakService = inject(KeycloakService)
  private toastService = inject(ToastService)

  ngOnInit(): void {
    this.loadPeople()

    if (this.film != null) {
      this.id = this.film.filmId
      this.title = this.film.title
      this.description = this.film.description
      this.releaseDate = new Date(this.film.releaseDate)
      this.duration = this.film.duration
      this.person = Object.assign({}, this.director)
    }
  }

  private loadPeople(): void {
    this.personService.getPersons().subscribe({
      next: people => this.dropdownPersons = people
    })
  }

  createOrUpdateFilm() {
    if (!this.id) {
      this.createFilm()
    } else {
      this.updateFilm()
    }
  }

  private updateFilm() {
    this.filmService.updateFilm(this.id!, {
      filmId: this.id!,
      title: this.title,
      description: this.description,
      duration: this.duration,
      releaseDate: this.getDateString(this.releaseDate),
      directorPersonId: this.person!.personId
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Film updated successfully.')
        this.openDialog = false
        this.filmChanged.emit(true)
      },
      error: () => this.toastService.showError('Error while updating film.')
    })
  }

  private createFilm() {
    this.filmService.createFilm({
      title: this.title,
      description: this.description,
      duration: this.duration,
      releaseDate: this.getDateString(this.releaseDate),
      directorPersonId: this.person!.personId
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Film created successfully.')
        this.openDialog = false
        this.filmChanged.emit(true)
      },
      error: () => this.toastService.showError('Error while creating film.')
    })
  }

  private getDateString(date: Date): string {
    const index = date.toISOString().indexOf('T')
    return new Date(date.getTime() + (1000 * 60 * 60 * 24)).toISOString().slice(0, index)
  }

  protected readonly Role = Role;
}
