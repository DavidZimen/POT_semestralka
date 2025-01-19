import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {PersonDto} from '../../dto/person-dto';
import {PersonService} from '../../services/person.service';
import {KeycloakService} from 'keycloak-angular';
import {ToastService} from '../../services/toast.service';
import {CountryService} from '../../services/country.service';
import {Button} from 'primeng/button';
import {DatePicker} from 'primeng/datepicker';
import {Dialog} from 'primeng/dialog';
import {DropdownModule} from 'primeng/dropdown';
import {FormsModule} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {NgIf} from '@angular/common';
import {PrimeTemplate} from 'primeng/api';
import {Textarea} from 'primeng/textarea';
import {Role} from '../../enum/role.enum';

@Component({
  selector: 'app-person-form',
  imports: [
    Button,
    DatePicker,
    Dialog,
    DropdownModule,
    FormsModule,
    InputText,
    NgIf,
    PrimeTemplate,
    Textarea
  ],
  templateUrl: './person-form.component.html',
  styleUrl: './person-form.component.scss'
})
export class PersonFormComponent implements OnInit {

  @Input()
  person: PersonDto | undefined

  @Output()
  personChanged = new EventEmitter<boolean>()

  // update form dialog
  openDialog = false
  id: string
  firstName: string
  lastName: string
  middleName: string | undefined
  bio: string | undefined;
  birthDate: Date;
  country: string

  // services
  countryService = inject(CountryService)
  personService = inject(PersonService)
  keycloakService = inject(KeycloakService)
  private toastService = inject(ToastService)

  ngOnInit(): void {
    if (this.person != null) {
      this.id = this.person.personId
      this.firstName = this.person.firstName
      this.middleName = this.person.middleName
      this.lastName = this.person.lastName
      this.bio = this.person.bio
      this.birthDate = new Date(this.person.birthDate)
      this.country = this.person.country
    }
  }

  createOrUpdateFilm() {
    if (!this.id) {
      this.createPerson()
    } else {
      this.updatePerson()
    }
  }

  private updatePerson() {
    this.personService.updatePerson(this.id!, {
      personId: this.id,
      firstName: this.firstName,
      middleName: this.middleName,
      lastName: this.lastName,
      bio: this.bio,
      birthDate: this.getDateString(this.birthDate),
      country: this.country
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Person updated successfully.')
        this.openDialog = false
        this.personChanged.emit(true)
      },
      error: () => this.toastService.showError('Error while updating person.')
    })
  }

  private createPerson() {
    this.personService.createPerson({
      firstName: this.firstName,
      middleName: this.middleName,
      lastName: this.lastName,
      bio: this.bio,
      birthDate: this.getDateString(this.birthDate),
      country: this.country
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Person created successfully.')
        this.openDialog = false
        this.personChanged.emit(true)
      },
      error: () => this.toastService.showError('Error while creating person.')
    })
  }

  private getDateString(date: Date): string {
    const index = date.toISOString().indexOf('T')
    return new Date(date.getTime() + (1000 * 60 * 60 * 24)).toISOString().slice(0, index)
  }

  protected readonly Role = Role;
}
