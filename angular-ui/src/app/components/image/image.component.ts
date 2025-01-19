import {Component, inject, Input} from '@angular/core';
import {ImageService} from '../../services/image.service';
import {environment} from '../../../environments/environment';
import {KeycloakService} from 'keycloak-angular';
import {Image} from 'primeng/image';
import {Button} from 'primeng/button';
import {Role} from '../../enum/role.enum';
import {ConfirmService} from '../../services/confirm.service';
import {ConfirmType} from '../../enum/confirm-type.enum';
import {ToastService} from '../../services/toast.service';
import {ImageUploadComponent} from './image-upload/image-upload.component';

@Component({
  selector: 'app-image',
  imports: [
    Image,
    Button,
    ImageUploadComponent
  ],
  templateUrl: './image.component.html',
  styleUrl: './image.component.scss'
})
export class ImageComponent {

  @Input()
  imageId: string | undefined

  @Input()
  height: number = 480

  @Input()
  width: number = 320

  @Input()
  useStock = true

  @Input()
  filmId: string | undefined

  @Input()
  showId: string | undefined

  @Input()
  episodeId: string | undefined

  @Input()
  personId: string | undefined

  @Input()
  allowOperations = true

  // services
  keycloakService = inject(KeycloakService)
  private imageService = inject(ImageService)
  private confirmService = inject(ConfirmService)
  private toastService = inject(ToastService)

  getImageUrl(): string | undefined {
    let url: string | undefined
    if (this.imageId) {
      url = `${environment.apiUrl}/image/${this.imageId}`
    } else if (this.useStock) {
      url = environment.stockFilm
    }
    return url
  }

  initRemoveImage(): void {
    this.confirmService.confirm({
      header: 'Image deletion',
      message: 'Are you sure you want to delete image ?',
      type: ConfirmType.DELETE,
      accept: () => this.removeImage()
    })
  }

  private removeImage(): void {
    this.imageService.deleteImage(this.imageId!).subscribe({
      next: () => {
        this.imageId = undefined
        this.toastService.showSuccess('Image deleted successfully.')
      },
      error: err => this.toastService.showError(err.error.message)
    })
  }

  protected readonly Role = Role;
}
