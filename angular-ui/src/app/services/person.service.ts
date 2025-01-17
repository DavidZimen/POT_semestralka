import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonDto, CreatePerson, UpdatePerson } from '../dto/person-dto';

@Injectable({ providedIn: 'root' })
export class PersonService {
  constructor(private http: HttpClient) {}

  getPersons(): Observable<PersonDto[]> {
    return this.http.get<PersonDto[]>('/person');
  }

  getPerson(personId: string): Observable<PersonDto> {
    return this.http.get<PersonDto>(`/person/${personId}`);
  }

  createPerson(person: CreatePerson): Observable<void> {
    return this.http.post<void>('/person', person);
  }

  updatePerson(personId: string, person: UpdatePerson): Observable<PersonDto> {
    return this.http.put<PersonDto>(`/person/${personId}`, person);
  }

  deletePerson(personId: string): Observable<void> {
    return this.http.delete<void>(`/person/${personId}`);
  }
}
