import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly path = 'products';

  private http = inject(HttpClient)

  constructor() { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.path)
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.path}/${id}`);
  }
}

/**
 * DTO for Product.
 */
export interface Product {
  id: string,
  name: string,
  description?: string,
  price: number
}
