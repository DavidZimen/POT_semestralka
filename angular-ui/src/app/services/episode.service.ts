import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EpisodeDto, EpisodeCreate, EpisodeUpdate } from '../dto/episode-dto';

@Injectable({ providedIn: 'root' })
export class EpisodeService {

  constructor(private http: HttpClient) {}

  getEpisodes(seasonId?: string): Observable<EpisodeDto[]> {
    const params = seasonId ? new HttpParams().set('seasonId', seasonId) : undefined;
    return this.http.get<EpisodeDto[]>('/episode', { params });
  }

  getEpisode(episodeId: string): Observable<EpisodeDto> {
    return this.http.get<EpisodeDto>(`/episode/${episodeId}`);
  }

  createEpisode(episode: EpisodeCreate): Observable<EpisodeDto> {
    return this.http.post<EpisodeDto>('/episode', episode);
  }

  updateEpisode(episodeId: string, episode: EpisodeUpdate): Observable<EpisodeDto> {
    return this.http.put<EpisodeDto>(`/episode/${episodeId}`, episode);
  }

  deleteEpisode(episodeId: string): Observable<void> {
    return this.http.delete<void>(`/episode/${episodeId}`);
  }
}
