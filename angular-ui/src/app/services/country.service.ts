import {Injectable} from '@angular/core';
import * as countries from 'i18n-iso-countries';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  getCountryName(countryCode: string): string {
    return countries.getName(countryCode, 'en') || 'Unknown Country';
  }
}
