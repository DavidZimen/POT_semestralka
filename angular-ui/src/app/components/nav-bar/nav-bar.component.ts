import {Component, inject} from '@angular/core';
import {MenubarModule} from 'primeng/menubar';
import {MenuItem} from 'primeng/api';
import {Ripple} from 'primeng/ripple';
import {BadgeModule} from 'primeng/badge';
import {NgClass, NgIf} from '@angular/common';
import {ChipsModule} from 'primeng/chips';
import {AvatarModule} from 'primeng/avatar';
import {UiRoutes} from '../../constants/UiRoutes';
import {KeycloakService} from 'keycloak-angular';

@Component({
  selector: 'app-nav-bar',
  imports: [
    MenubarModule,
    Ripple,
    BadgeModule,
    NgClass,
    NgIf,
    ChipsModule,
    AvatarModule
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',

})
export class NavBarComponent {
  items: MenuItem[] = [
    {
      label: 'Home',
      icon: 'pi pi-home',
      routerLink: UiRoutes.Home
    },
    {
      label: 'Products',
      icon: 'pi pi-star',
      routerLink: UiRoutes.Products
    }
  ]

  keycloakService = inject(KeycloakService)

  protected readonly UiRoutes = UiRoutes;
}
