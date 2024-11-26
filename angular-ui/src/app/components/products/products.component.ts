import {Component, inject, OnInit} from '@angular/core';
import {Product, ProductService} from '../../services/product.service';
import {BehaviorSubject, catchError, delay, Observable, of, tap} from 'rxjs';
import {TableModule} from 'primeng/table';
import {LoadingComponent} from '../abstract/loading.component';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-products',
  imports: [
    TableModule,
    AsyncPipe
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent extends LoadingComponent implements OnInit {

  products$: Observable<Product[]>

  // services
  private productService = inject(ProductService)

  ngOnInit(): void {
    this.loadProducts()
  }

  private loadProducts(): void {
    this.products$ = this.productService.getAllProducts()
      .pipe(
        delay(2000),
        tap(() => this.completeLoading()),
        catchError(err => {
          console.error(err)
          this.completeLoading()
          return of([])
        })
      )
  }
}
