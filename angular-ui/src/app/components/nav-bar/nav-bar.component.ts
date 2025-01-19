import {Component, inject} from '@angular/core';
import {MenubarModule} from 'primeng/menubar';
import {MenuItem} from 'primeng/api';
import {Ripple} from 'primeng/ripple';
import {BadgeModule} from 'primeng/badge';
import {NgClass, NgIf} from '@angular/common';
import {AvatarModule} from 'primeng/avatar';
import {UiRoutes} from '../../constants/UiRoutes';
import {KeycloakService} from 'keycloak-angular';
import {RouterLink} from '@angular/router';
import {InputText} from 'primeng/inputtext';
import {Button} from 'primeng/button';

@Component({
  selector: 'app-nav-bar',
  imports: [
    MenubarModule,
    Ripple,
    BadgeModule,
    NgClass,
    NgIf,
    AvatarModule,
    RouterLink,
    InputText,
    Button
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
      label: 'Film',
      icon: 'pi pi-star',
      routerLink: `${UiRoutes.Film}/aa1967ea-5b82-4eb2-9dd9-5864602aef6b`
    }
  ]

  keycloakService = inject(KeycloakService)

  protected readonly UiRoutes = UiRoutes;
}
