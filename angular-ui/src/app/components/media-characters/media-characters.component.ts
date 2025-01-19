import {Component, EventEmitter, inject, Input, OnInit, Output} from '@angular/core';
import {CharacterMediaDto} from '../../dto/character-dto';
import {MediaType} from '../../enum/media-type.enum';
import {LowerCasePipe, NgIf} from '@angular/common';
import {PersonDto, PersonMinimalDto} from '../../dto/person-dto';
import {environment} from '../../../environments/environment';
import {Avatar} from 'primeng/avatar';
import {Router} from '@angular/router';
import {CharacterService} from '../../services/character.service';
import {UiRoutes} from '../../constants/UiRoutes';
import {KeycloakService} from 'keycloak-angular';
import {Role} from '../../enum/role.enum';
import {ConfirmService} from '../../services/confirm.service';
import {ConfirmType} from '../../enum/confirm-type.enum';
import {ToastService} from '../../services/toast.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Button} from 'primeng/button';
import {Dialog} from 'primeng/dialog';
import {InputText} from 'primeng/inputtext';
import {PersonService} from '../../services/person.service';
import {DropdownModule} from 'primeng/dropdown';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-media-characters',
  imports: [
    LowerCasePipe,
    Avatar,
    Button,
    Dialog,
    InputText,
    DropdownModule,
    FormsModule,
    NgIf
  ],
  templateUrl: './media-characters.component.html',
  styleUrl: './media-characters.component.scss'
})
export class MediaCharactersComponent implements OnInit {
  @Input()
  characters: CharacterMediaDto[] = []

  @Input()
  mediaType: MediaType

  @Input()
  mediaId: string

  @Output()
  characterChanged = new EventEmitter<boolean>()

  openDialog = false
  characterName: string
  person: PersonDto
  dropdownPersons: PersonDto[] = []

  // services
  keycloakService = inject(KeycloakService)
  private personService = inject(PersonService)
  private characterService = inject(CharacterService)
  private toastService = inject(ToastService)
  private confirmService = inject(ConfirmService)
  private router = inject(Router)

  ngOnInit(): void {
    this.loadPeople()
  }

  getImageUrl(person: PersonMinimalDto): string {
    return person.imageId ? `${environment.apiUrl}/image/${person.imageId}` : 'assets/avatar-stock.jpg'
  }

  showPerson(personId: string) {
    this.router.navigate([UiRoutes.Person, personId])
  }

  createCharacter(): void {
    this.characterService.createCharacter({
      characterName: this.characterName,
      actorPersonId: this.person.personId,
      filmId: this.mediaType === MediaType.FILM ? this.mediaId : undefined,
      showId: this.mediaType === MediaType.SHOW ? this.mediaId : undefined
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Character created successfully.')
        this.openDialog = false
        this.characterChanged.emit(true)
      },
      error: () => this.toastService.showError('Error while creating new character.')
    })
  }

  initRemoveCharacter(character: CharacterMediaDto): void {
    this.confirmService.confirm({
      header: 'Character deletion',
      message: `Are you sure you want to delete character ${character.characterName} ?`,
      type: ConfirmType.DELETE,
      accept: () => this.removeCharacter(character)
    })
  }

  private removeCharacter(character: CharacterMediaDto): void {
    this.characterService.deleteCharacter(character.characterId).subscribe({
      next: () => {
        this.toastService.showSuccess('Character removed successfully.')

        const index = this.characters.indexOf(character)
        if (index != -1) {
          this.characters = this.characters.slice(index, index + 1)
        }
      },
      error: (err: HttpErrorResponse) => this.toastService.showError(err.error.message)
    })
  }

  private loadPeople(): void {
    this.personService.getPersons().subscribe({
      next: people => this.dropdownPersons = people
    })
  }

  protected readonly Role = Role;
}
