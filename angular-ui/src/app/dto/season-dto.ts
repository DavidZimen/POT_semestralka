export interface SeasonDto {
  seasonId: string; // Guid
  title: string;
  description: string;
  episodeCount: number;
  showId: string; // Guid
}

export interface SeasonCreate {
  title: string;
  description: string;
  showId: string; // Guid
}

export interface SeasonUpdate {
  seasonId: string; // Guid
  title: string;
  description: string;
}
