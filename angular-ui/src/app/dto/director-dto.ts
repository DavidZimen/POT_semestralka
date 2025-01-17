export interface DirectorFilmDto {
  filmId: string; // Guid
  title: string;
}

export interface DirectorShowDto {
  showId: string; // Guid
  title: string;
  episodeCount: number;
}