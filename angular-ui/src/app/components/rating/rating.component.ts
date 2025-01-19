import {Component, Input} from '@angular/core';
import {AverageRatingComponent} from './averate-rating/average-rating.component';
import {UserRatingComponent} from './user-rating/user-rating.component';

@Component({
  selector: 'app-rating',
  imports: [
    AverageRatingComponent,
    UserRatingComponent
  ],
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.scss'
})
export class RatingComponent {
  @Input()
  filmId: string | undefined

  @Input()
  showId: string | undefined

  @Input()
  episodeId: string | undefined
}
