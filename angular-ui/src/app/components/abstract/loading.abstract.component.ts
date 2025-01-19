import {Directive, OnDestroy} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Directive()
export abstract class LoadingAbstractComponent implements OnDestroy {

  private loadingSubject = new BehaviorSubject<boolean>(false)
  $loading = this.loadingSubject.asObservable()

  protected completeLoading(): void {
    this.loadingSubject.next(false)
  }

  protected startLoading(): void {
    this.loadingSubject.next(true)
  }

  ngOnDestroy(): void {
    this.loadingSubject.complete()
  }
}
