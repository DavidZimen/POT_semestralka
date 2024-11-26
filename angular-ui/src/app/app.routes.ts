import { Routes } from '@angular/router';
import {UiRoutes} from './constants/UiRoutes';
import {authGuard} from './guards/auth.guard';


export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: UiRoutes.Home,
    title: 'Home Page',
    loadComponent: () => import('./components/home/home.component').then(x => x.HomeComponent)
  },
  {
    path: UiRoutes.Products,
    title: 'Products list',
    canActivate: [authGuard],
    loadComponent: () => import('./components/products/products.component').then(x => x.ProductsComponent)
  },
  {
    path: `${UiRoutes.Product}/:id`,
    title: 'Product Details',
    canActivate: [authGuard],
    loadComponent: () => import('./components/product/product.component').then(x => x.ProductComponent)
  }
]
