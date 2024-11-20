import { ApplicationConfig, provideExperimentalZonelessChangeDetection } from '@angular/core';
import {provideRouter, withComponentInputBinding, withViewTransitions} from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withFetch, withInterceptors, withInterceptorsFromDi} from '@angular/common/http';
import {KeycloakBearerInterceptorProvider, KeycloakInitializerProvider} from './config/keycloak.config';
import {KeycloakService} from 'keycloak-angular';
import {apiUrlInterceptor} from './interceptors/api.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    provideHttpClient(
      withFetch(),
      withInterceptors([apiUrlInterceptor]),
      withInterceptorsFromDi()
    ),
    provideRouter(routes, withComponentInputBinding(), withViewTransitions()),
    KeycloakInitializerProvider,
    KeycloakBearerInterceptorProvider,
    KeycloakService
  ]
};
