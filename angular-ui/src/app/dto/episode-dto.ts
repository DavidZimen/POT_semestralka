import {ImageIdDto} from './image-dto';

export interface EpisodeDto extends ImageIdDto {
  episodeId: string; // Guid
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  duration: number;
  directorId: string; // Guid
  seasonId: string; // Guid
}

export interface EpisodeCreate {
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  duration: number;
  directorPersonId: string; // Guid
  seasonId: string; // Guid
}

export interface EpisodeUpdate {
  episodeId: string; // Guid
  title: string;
  description: string;
  releaseDate: string; // DateOnly
  duration: number;
  directorPersonId: string; // Guid
}
