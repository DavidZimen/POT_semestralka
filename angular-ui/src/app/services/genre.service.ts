import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenreDto, GenreCreate, GenreUpdate } from '../dto/genre-dto';

@Injectable({ providedIn: 'root' })
export class GenreService {
  constructor(private http: HttpClient) {}

  getGenres(): Observable<GenreDto[]> {
    return this.http.get<GenreDto[]>('/genre');
  }

  getGenre(genreId: string): Observable<GenreDto> {
    return this.http.get<GenreDto>(`/genre/${genreId}`);
  }

  createGenre(genre: GenreCreate): Observable<void> {
    return this.http.post<void>('/genre', genre);
  }

  updateGenre(genreId: string, genre: GenreUpdate): Observable<GenreDto> {
    return this.http.put<GenreDto>(`/genre/${genreId}`, genre);
  }

  deleteGenre(genreId: string): Observable<void> {
    return this.http.delete<void>(`/genre/${genreId}`);
  }
}
