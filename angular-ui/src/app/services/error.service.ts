import {inject, Injectable, OnDestroy} from '@angular/core';
import {Router} from '@angular/router';
import {BehaviorSubject, ObservableInput} from 'rxjs';
import {UiRoutes} from '../constants/UiRoutes';

@Injectable({
  providedIn: 'root'
})
export class ErrorService implements OnDestroy {

  // observables
  private error = new BehaviorSubject<Error | undefined>(undefined)
  $error = this.error.asObservable()

  // services
  private router = inject(Router)

  emitError(message: string | undefined, status: number): ObservableInput<any> {
    this.error.next({ message, status })
    return this.router.navigate([UiRoutes.Error])
  }

  clearError(): void {
    this.error.next(undefined)
  }

  ngOnDestroy(): void {
    this.error.complete()
  }
}

export interface Error {
  message: string | undefined
  status: number
}
