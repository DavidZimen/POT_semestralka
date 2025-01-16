export interface RatingDto {
  id: string; // Guid
  userId: string;
  value: number; // 1-10
  description?: string;
}

export interface AverageRatingDto {
  average?: number;
  count: number;
}

export interface RatingCreate {
  value: number; // 1-10
  description?: string;
  filmId?: string; // Guid
  showId?: string; // Guid
  episodeId?: string; // Guid
}

export interface RatingUpdate {
  id: string; // Guid
  userId: string;
  value: number; // 1-10
  description?: string;
}

export interface RatingDelete {
  id: string; // Guid
  userId: string;
}

export interface UserRatingRequest {
  userId: string;
  filmId?: string; // Guid
  showId?: string; // Guid
  episodeId?: string; // Guid
}

export interface AverageRatingRequest {
  filmId?: string; // Guid
  showId?: string; // Guid
  episodeId?: string; // Guid
}