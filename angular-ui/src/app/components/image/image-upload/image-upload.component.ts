import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {FileUpload, FileUploadErrorEvent, FileUploadEvent} from 'primeng/fileupload';
import {ToastService} from '../../../services/toast.service';
import {HttpResponse} from '@angular/common/http';

@Component({
  selector: 'app-image-upload',
  imports: [
    FileUpload,
  ],
  templateUrl: './image-upload.component.html',
  styleUrl: './image-upload.component.scss'
})
export class ImageUploadComponent {

  @Input()
  filmId: string | undefined

  @Input()
  showId: string | undefined

  @Input()
  episodeId: string | undefined

  @Input()
  personId: string | undefined

  @Output()
  imageUploaded = new EventEmitter<string>()

  // services
  private toastService = inject(ToastService)

  onUpload(event: FileUploadEvent): void {
    this.imageUploaded.emit((event.originalEvent as HttpResponse<string>).body!)
    this.toastService.showSuccess('Image uploaded successfully.')
  }

  onError(event: FileUploadErrorEvent): void {
    this.toastService.showError(`Image upload failed.`)
  }

  getUrl(): string {
    const params: string[] = [];

    if (this.filmId) {
      params.push(`filmId=${this.filmId}`);
    }

    if (this.showId) {
      params.push(`showId=${this.showId}`);
    }

    if (this.episodeId) {
      params.push(`episodeId=${this.episodeId}`);
    }

    if (this.personId) {
      params.push(`personId=${this.personId}`);
    }

    // Join the parameters with '&' and return the final URL
    return `image?${params.join('&')}`;
  }
}
