import { ApplicationConfig, provideExperimentalZonelessChangeDetection } from '@angular/core';
import {provideRouter, withComponentInputBinding} from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withFetch, withInterceptors, withInterceptorsFromDi} from '@angular/common/http';
import {KeycloakBearerInterceptorProvider, KeycloakInitializerProvider} from './config/keycloak.config';
import {apiUrlInterceptor} from './interceptors/api.interceptor';
import {KeycloakService} from 'keycloak-angular';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    provideHttpClient(
      withFetch(),
      withInterceptors([apiUrlInterceptor]),
      withInterceptorsFromDi()
    ),
    provideRouter(routes, withComponentInputBinding()),
    KeycloakService,
    KeycloakInitializerProvider,
    KeycloakBearerInterceptorProvider,
  ]
};
