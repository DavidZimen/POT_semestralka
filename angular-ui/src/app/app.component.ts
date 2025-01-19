import {Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavBarComponent} from './components/nav-bar/nav-bar.component';
import {Toast} from 'primeng/toast';
import {ConfirmDialog} from 'primeng/confirmdialog';
import {KeycloakEventTypeLegacy, KeycloakService} from 'keycloak-angular';
import {UserService} from './services/user.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBarComponent, Toast, ConfirmDialog],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',

})
export class AppComponent implements OnInit {
  title = 'angular-ui';

  keycloakService = inject(KeycloakService)
  userService = inject(UserService)

  ngOnInit(): void {
    this.keycloakService.keycloakEvents$.subscribe({
      next: async event => {
        if (event.type == KeycloakEventTypeLegacy.OnAuthSuccess) {
          const profile = await this.keycloakService.loadUserProfile()
          console.log(profile)
          this.userService.registerUser({ id: profile.id! }).subscribe({next: value => console.log('Registered')})
        }
      }
    })
  }
}
