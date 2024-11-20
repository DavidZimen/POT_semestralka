import {KeycloakBearerInterceptor, KeycloakService} from 'keycloak-angular';
import {environment} from '../../environments/environment';
import {provideAppInitializer, Provider} from '@angular/core';
import {HTTP_INTERCEPTORS} from '@angular/common/http';

export function initializeKeycloak(keycloak: KeycloakService) {
  return () => {
    keycloak.init({
      config: {
        url: environment.keycloak.url,
        realm: environment.keycloak.realm,
        clientId: environment.keycloak.clientId
      },
      initOptions: {
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html'
      },
      enableBearerInterceptor: true,
      bearerPrefix: 'Bearer'
    })
  }
}

// Provider for Keycloak Bearer Interceptor
export const KeycloakBearerInterceptorProvider: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: KeycloakBearerInterceptor,
  multi: true
}

// Provider for Keycloak Initialization
export const KeycloakInitializerProvider: Provider = {
  provide: provideAppInitializer,
  useFactory: initializeKeycloak,
  multi: true,
  deps: [KeycloakService]
}
