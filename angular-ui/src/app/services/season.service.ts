import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SeasonDto, SeasonCreate, SeasonUpdate } from '../dto/season-dto';

@Injectable({ providedIn: 'root' })
export class SeasonService {

  constructor(private http: HttpClient) {}

  getSeasons(showId?: string): Observable<SeasonDto[]> {
    const params = showId ? new HttpParams().set('showId', showId) : undefined;
    return this.http.get<SeasonDto[]>('/season', { params });
  }

  getSeason(seasonId: string): Observable<SeasonDto> {
    return this.http.get<SeasonDto>(`/season/${seasonId}`);
  }

  createSeason(season: SeasonCreate): Observable<SeasonDto> {
    return this.http.post<SeasonDto>('/season', season);
  }

  updateSeason(seasonId: string, season: SeasonUpdate): Observable<SeasonDto> {
    return this.http.put<SeasonDto>(`/season/${seasonId}`, season);
  }

  deleteSeason(seasonId: string): Observable<void> {
    return this.http.delete<void>(`/season/${seasonId}`);
  }
}
