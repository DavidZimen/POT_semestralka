import {ApplicationConfig, provideExperimentalZonelessChangeDetection} from '@angular/core';
import {provideRouter, withComponentInputBinding} from '@angular/router';

import {routes} from './app.routes';
import {provideHttpClient, withFetch, withInterceptors, withInterceptorsFromDi} from '@angular/common/http';
import {KeycloakBearerInterceptorProvider, KeycloakInitializerProvider} from './config/keycloak.config';
import {apiUrlInterceptor} from './interceptors/api.interceptor';
import {KeycloakService} from 'keycloak-angular';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {providePrimeNG} from 'primeng/config';
import Lara from '@primeng/themes/lara'
import {ConfirmationService, MessageService} from 'primeng/api';
import * as countries from 'i18n-iso-countries';
import enLocale from 'i18n-iso-countries/langs/en.json';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),

    // HTTP client for API calls
    provideHttpClient(
      withFetch(),
      withInterceptors([apiUrlInterceptor]),
      withInterceptorsFromDi()
    ),

    // router
    provideRouter(routes, withComponentInputBinding()),

    // keycloak services
    KeycloakService,
    KeycloakInitializerProvider,
    KeycloakBearerInterceptorProvider,

    // Primeng styling components
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Lara,
        options: {
          darkModeSelector: 'none'
        }
      }
    }),

    // primeng messaging
    MessageService,
    ConfirmationService
  ]
};

countries.registerLocale(enLocale)
