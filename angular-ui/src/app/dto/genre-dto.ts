export interface GenreDto {
  genreId: string; // Guid
  name: string;
}

export interface GenreCreate {
  name: string;
}

export interface GenreUpdate {
  genreId: string; // Guid
  name: string;
}