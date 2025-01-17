import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CharacterDto, CharacterCreate } from '../dto/character-dto';

@Injectable({ providedIn: 'root' })
export class CharacterService {
  constructor(private http: HttpClient) {}

  getCharacters(filmId?: string, showId?: string): Observable<CharacterDto[]> {
    let params = new HttpParams();
    if (filmId) params = params.set('filmId', filmId);
    if (showId) params = params.set('showId', showId);

    return this.http.get<CharacterDto[]>('/character', { params });
  }

  createCharacter(character: CharacterCreate): Observable<CharacterDto> {
    return this.http.post<CharacterDto>('/character', character);
  }

  deleteCharacter(characterId: string): Observable<void> {
    return this.http.delete<void>(`/character/${characterId}`);
  }
}
