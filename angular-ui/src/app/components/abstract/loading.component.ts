import {Directive, OnDestroy, OnInit} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

@Directive()
export abstract class LoadingComponent implements OnDestroy {

  private loadingSubject = new BehaviorSubject<boolean>(true)

  loading$ = this.loadingSubject.asObservable()

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
