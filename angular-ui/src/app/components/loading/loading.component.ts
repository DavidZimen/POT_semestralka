import {Component, Input} from '@angular/core';
import {ProgressSpinner} from 'primeng/progressspinner';

@Component({
  selector: 'app-loading',
  imports: [
    ProgressSpinner
  ],
  templateUrl: './loading.component.html',
})
export class LoadingComponent {

  /**
   * Message to be displayed under the spinner.
   */
  @Input()
  message: string
}
