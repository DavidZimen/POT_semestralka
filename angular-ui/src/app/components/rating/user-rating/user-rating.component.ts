import {ChangeDetectorRef, Component, EventEmitter, inject, Input, OnChanges, Output} from '@angular/core';
import {RatingService} from '../../../services/rating.service';
import {KeycloakService} from 'keycloak-angular';
import {RatingDto} from '../../../dto/rating-dto';
import {Dialog} from 'primeng/dialog';
import {Button} from 'primeng/button';
import {Rating} from 'primeng/rating';
import {FormsModule} from '@angular/forms';
import {Textarea} from 'primeng/textarea';
import {ToastService} from '../../../services/toast.service';
import {FloatLabel} from 'primeng/floatlabel';

@Component({
  selector: 'app-user-rating',
  imports: [
    Dialog,
    Button,
    Rating,
    FormsModule,
    Textarea,
    FloatLabel
  ],
  templateUrl: './user-rating.component.html',
  styleUrl: './user-rating.component.scss'
})
export class UserRatingComponent implements OnChanges {

  @Input()
  filmId: string | undefined

  @Input()
  showId: string | undefined

  @Input()
  episodeId: string | undefined

  @Output()
  ratingChanged = new EventEmitter<boolean>

  userId: string | undefined
  userRating: RatingDto | undefined
  openDialog = false
  newValue: number
  newDescription: string | undefined

  // services
  ratingService = inject(RatingService)
  keycloakService = inject(KeycloakService)
  toastService = inject(ToastService)
  private cdr = inject(ChangeDetectorRef)

  ngOnChanges(): void {
    this.init()
  }

  private async init(): Promise<void> {
    if (this.keycloakService.isLoggedIn()) {
      try {
        const profile = await this.keycloakService.loadUserProfile(true)
        this.userId = profile.id

        if (this.userId == null) {
          return
        }

        this.getRating();
      } catch (e) {
        return
      }
    } else {
      this.keycloakService.login()
    }

    // this.ratingService.getUserRating()
  }

  removeRating(): void {
    this.ratingService.deleteRating(this.userRating!.id, { userId: this.userRating!.id, id: this.userRating!.id })
      .subscribe({
        next: () => {
          this.userRating = undefined
          this.toastService.showSuccess('Rating deleted successfully.')
          this.openDialog = false
          this.ratingChanged.emit(true)
        },
        error: () => this.toastService.showError('Error while deleting rating.')
      })
  }

  createOrUpdateRating() {
    if (this.userRating) {
      this.updateRating()
    } else {
      this.createRating()
    }
  }

  private createRating(): void {
    this.ratingService.createRating({
      value: this.newValue,
      description: this.newDescription,
      filmId: this.filmId,
      showId: this.showId,
      episodeId: this.episodeId
    }).subscribe({
      next: () => {
        this.toastService.showSuccess('Rating updated successfully.')
        this.openDialog = false
        this.ratingChanged.emit(true)
        this.getRating()
      },
      error: () => this.toastService.showError('Error while updating rating.')
    })
  }

  private updateRating(): void {
    this.ratingService.updateRating(this.userRating!.id, {
      id: this.userRating!.id,
      userId: this.userRating!.userId,
      value: this.newValue,
      description: this.newDescription,
    }).subscribe({
      next: rating => {
        this.userRating = rating;
        this.toastService.showSuccess('Rating updated successfully.')
        this.ratingChanged.emit(true)
        this.openDialog = false
      },
      error: () => this.toastService.showError('Error while updating rating.')
    })
  }

  private getRating() {
    this.ratingService.getUserRating(this.userId!, this.filmId, this.showId, this.episodeId).subscribe({
      next: userRating => {
        this.userRating = userRating
        this.newValue = userRating.value
        this.newDescription = userRating.description
        this.cdr.detectChanges()
      }
    })
  }
}
