import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShowDto, ShowCreate, ShowUpdate } from '../dto/show-dto';
import {CharacterMediaDto} from '../dto/character-dto';

@Injectable({ providedIn: 'root' })
export class ShowService {
  constructor(private http: HttpClient) {}

  getShows(): Observable<ShowDto[]> {
    return this.http.get<ShowDto[]>('/show');
  }

  getShow(showId: string): Observable<ShowDto> {
    return this.http.get<ShowDto>(`/show/${showId}`);
  }

  createShow(show: ShowCreate): Observable<ShowDto> {
    return this.http.post<ShowDto>('/show', show);
  }

  updateShow(showId: string, show: ShowUpdate): Observable<ShowDto> {
    return this.http.put<ShowDto>(`/show/${showId}`, show);
  }

  deleteShow(showId: string): Observable<void> {
    return this.http.delete<void>(`/show/${showId}`);
  }

  getShowCharacters(showId: string): Observable<CharacterMediaDto[]> {
    return this.http.get<CharacterMediaDto[]>(`/show/${showId}/character`);
  }

  addGenreToShow(showId: string, genreId: string): Observable<void> {
    return this.http.post<void>(`/show/${showId}/genre/${genreId}`, null);
  }

  removeGenreFromShow(showId: string, genreId: string): Observable<void> {
    return this.http.delete<void>(`/show/${showId}/genre/${genreId}`);
  }
}
