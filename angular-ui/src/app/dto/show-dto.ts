import {GenreDto} from './genre-dto';
import {ImageIdDto} from './image-dto';

export interface ShowDto extends ImageIdDto {
  showId: string; // Guid
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  endDate?: string; // DateOnly
  episodeCount: number;
  seasonCount: number;
  genres: GenreDto[];
}

export interface ShowCreate {
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  endDate?: string; // DateOnly
}

export interface ShowUpdate {
  showId: string; // Guid
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  endDate?: string; // DateOnly
}
