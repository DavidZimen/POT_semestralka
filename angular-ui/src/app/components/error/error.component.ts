import {Component, inject} from '@angular/core';
import {ErrorService} from '../../services/error.service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-error',
  imports: [
    AsyncPipe
  ],
  templateUrl: './error.component.html',
  styleUrl: './error.component.scss'
})
export class ErrorComponent {

  // services
  errorService = inject(ErrorService)
}
