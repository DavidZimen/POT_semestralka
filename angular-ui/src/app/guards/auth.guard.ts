import { CanActivateFn } from '@angular/router';
import {inject} from '@angular/core';
import {KeycloakService} from 'keycloak-angular';

export const authGuard: CanActivateFn = async (route, state) => {
  const keycloak = inject(KeycloakService);

  const authenticated = keycloak.isLoggedIn();

  if (!authenticated) {
    try {
      await keycloak.login({
        redirectUri: window.location.origin + state.url
      })
    } catch (e) {
      return false
    }
  }

  return true;
};
