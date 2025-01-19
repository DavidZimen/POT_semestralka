import {Component, inject, Input} from '@angular/core';
import {CharacterMediaDto} from '../../dto/character-dto';
import {MediaType} from '../../enum/media-type.enum';
import {LowerCasePipe} from '@angular/common';
import {PersonMinimalDto} from '../../dto/person-dto';
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

@Component({
  selector: 'app-media-characters',
  imports: [
    LowerCasePipe,
    Avatar
  ],
  templateUrl: './media-characters.component.html',
  styleUrl: './media-characters.component.scss'
})
export class MediaCharactersComponent {

  @Input()
  characters: CharacterMediaDto[] = []

  @Input()
  mediaType: MediaType

  // services
  keycloakService = inject(KeycloakService)
  private characterService = inject(CharacterService)
  private toastService = inject(ToastService)
  private confirmService = inject(ConfirmService)
  private router = inject(Router)

  getImageUrl(person: PersonMinimalDto): string {
    return person.imageId ? `${environment.apiUrl}/image/${person.imageId}` : 'assets/avatar-stock.jpg'
  }

  showPerson(personId: string) {
    this.router.navigate([UiRoutes.Person, personId])
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

  protected readonly Role = Role;
}
