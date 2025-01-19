import {Component, inject} from '@angular/core';
import {KeycloakService} from 'keycloak-angular';
import {Router} from '@angular/router';
import {ToastService} from '../../services/toast.service';
import {ConfirmService} from '../../services/confirm.service';
import {PersonService} from '../../services/person.service';
import {AsyncPipe, DatePipe, NgTemplateOutlet} from '@angular/common';
import {Button} from 'primeng/button';
import {Card} from 'primeng/card';
import {ImageComponent} from '../image/image.component';
import {MaxLengthPipe} from '../../pipes/max-length.pipe';
import {Role} from '../../enum/role.enum';
import {UiRoutes} from '../../constants/UiRoutes';
import {ConfirmType} from '../../enum/confirm-type.enum';
import {PersonDto} from '../../dto/person-dto';
import {PersonFormComponent} from '../person-form/person-form.component';

@Component({
  selector: 'app-people',
  imports: [
    AsyncPipe,
    Button,
    Card,
    DatePipe,
    ImageComponent,
    MaxLengthPipe,
    NgTemplateOutlet,
    PersonFormComponent
  ],
  templateUrl: './people.component.html',
  styleUrl: './people.component.scss'
})
export class PeopleComponent {

  // services
  personService = inject(PersonService)
  keycloakService = inject(KeycloakService)
  router = inject(Router)
  private toastService = inject(ToastService)
  private confirmService = inject(ConfirmService)

  people$ = this.personService.getPersons()

  initRemovePerson(person: PersonDto): void {
    this.confirmService.confirm({
      header: 'Film deletion',
      message: `Are you sure you want to delete film ${person.firstName} ${person.lastName} ?`,
      type: ConfirmType.DELETE,
      accept: () => this.removePerson(person)
    })
  }

  private removePerson(person: PersonDto): void {
    this.personService.deletePerson(person.personId).subscribe({
      next: () => {
        this.toastService.showSuccess(`Person ${person.firstName} ${person.lastName} deleted successfully.`)
        this.people$ = this.personService.getPersons()
      },
      error: () => this.toastService.showError(`Error while deleting person ${person.firstName} ${person.lastName}.`)
    })
  }

  protected readonly Role = Role;
  protected readonly UiRoutes = UiRoutes;
}
