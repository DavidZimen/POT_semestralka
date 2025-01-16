import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DirectorFilmDto, DirectorShowDto } from '../dto/director-dto';

@Injectable({ providedIn: 'root' })
export class DirectorService {
  constructor(private http: HttpClient) {}

  getDirectorFilms(directorId: string): Observable<DirectorFilmDto[]> {
    return this.http.get<DirectorFilmDto[]>(`/director/${directorId}/films`);
  }

  getDirectorShows(directorId: string): Observable<DirectorShowDto[]> {
    return this.http.get<DirectorShowDto[]>(`/director/${directorId}/shows`);
  }
}
