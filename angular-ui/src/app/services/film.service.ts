import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FilmDto, FilmCreate, FilmUpdate } from '../dto/film-dto';
import { CharacterMediaDto } from '../dto/character-dto';

@Injectable({ providedIn: 'root' })
export class FilmService {
  constructor(private http: HttpClient) {}

  getFilms(): Observable<FilmDto[]> {
    return this.http.get<FilmDto[]>('/film');
  }

  getFilm(filmId: string): Observable<FilmDto> {
    return this.http.get<FilmDto>(`/film/${filmId}`);
  }

  createFilm(film: FilmCreate): Observable<FilmDto> {
    return this.http.post<FilmDto>('/film', film);
  }

  updateFilm(filmId: string, film: FilmUpdate): Observable<FilmDto> {
    return this.http.put<FilmDto>(`/film/${filmId}`, film);
  }

  deleteFilm(filmId: string): Observable<void> {
    return this.http.delete<void>(`/film/${filmId}`);
  }

  getFilmCharacters(filmId: string): Observable<CharacterMediaDto[]> {
    return this.http.get<CharacterMediaDto[]>(`/film/${filmId}/character`);
  }

  addGenreToFilm(filmId: string, genreId: string): Observable<void> {
    return this.http.post<void>(`/film/${filmId}/genre/${genreId}`, null);
  }

  removeGenreFromFilm(filmId: string, genreId: string): Observable<void> {
    return this.http.delete<void>(`/film/${filmId}/genre/${genreId}`);
  }
}
