import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {AverageRatingDto, RatingCreate, RatingDelete, RatingDto, RatingUpdate, UserRatingRequest } from '../dto/rating-dto';

@Injectable({ providedIn: 'root' })
export class RatingService {
  constructor(private http: HttpClient) {}

  getRating(ratingId: string): Observable<RatingDto> {
    return this.http.get<RatingDto>(`/rating/${ratingId}`);
  }

  createRating(rating: RatingCreate): Observable<void> {
    return this.http.post<void>('/rating', rating);
  }

  updateRating(ratingId: string, rating: RatingUpdate): Observable<RatingDto> {
    return this.http.put<RatingDto>(`/rating/${ratingId}`, rating);
  }

  deleteRating(ratingId: string, rating: RatingDelete): Observable<void> {
    return this.http.request<void>('delete', `/rating/${ratingId}`, { body: rating });
  }

  getUserRating(userId: string, filmId?: string, showId?: string, episodeId?: string): Observable<UserRatingRequest> {
    let params = new HttpParams().set('userId', userId);
    if (filmId) params = params.set('filmId', filmId);
    if (showId) params = params.set('showId', showId);
    if (episodeId) params = params.set('episodeId', episodeId);

    return this.http.get<UserRatingRequest>('/rating/user', { params });
  }

  getAverageRating(filmId?: string, showId?: string, episodeId?: string): Observable<AverageRatingDto> {
    let params = new HttpParams();
    if (filmId) params = params.set('filmId', filmId);
    if (showId) params = params.set('showId', showId);
    if (episodeId) params = params.set('episodeId', episodeId);

    return this.http.get<AverageRatingDto>('/rating/average', { params });
  }
}
