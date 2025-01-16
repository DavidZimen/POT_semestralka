import { GenreDto } from "./genre-dto";

export interface FilmDto {
  filmId: string;
  title: string;
  description: string;
  releaseDate: string; // Date
  duration: number;
  directorId: string; // Guid
  genres: GenreDto[]; // GenreDto from GenreDtos
}

export interface FilmCreate {
  title: string;
  description: string;
  releaseDate: string; // Date
  duration: number;
  directorPersonId: string; // Guid
}

export interface FilmUpdate {
  filmId: string; // Guid
  title: string;
  description: string;
  releaseDate: string; // Date
  duration: number;
  directorPersonId: string; // Guid
}
