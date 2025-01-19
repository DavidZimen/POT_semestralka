import {ChangeDetectorRef, Component, inject, Input, OnChanges} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RatingService} from '../../../services/rating.service';
import {AverageRatingDto} from '../../../dto/rating-dto';
import {DecimalPipe} from '@angular/common';

@Component({
  selector: 'app-average-rating',
  imports: [
    FormsModule,
    DecimalPipe
  ],
  templateUrl: './average-rating.component.html',
  styleUrl: './average-rating.component.scss'
})
export class AverageRatingComponent implements OnChanges {
  @Input()
  filmId: string | undefined

  @Input()
  showId: string | undefined

  @Input()
  episodeId: string | undefined

  /**
   * Average rating for the show, episode or film.
   */
  rating: AverageRatingDto

  // services
  ratingService = inject(RatingService)
  private cdr = inject(ChangeDetectorRef)

  ngOnChanges(): void {
    this.init()
  }

  private init(): void {
    this.ratingService.getAverageRating(this.filmId, this.showId, this.episodeId).subscribe({
      next: average => {
        this.rating = average
        this.cdr.detectChanges()
      },
      error: () => this.rating = { average: 0, count: 0 }
    })
  }
}
