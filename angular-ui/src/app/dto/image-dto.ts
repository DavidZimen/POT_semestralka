export interface ImageIdDto {
  imageId?: string; // Guid
}

export interface ImageCreate {
  content: Uint8Array;
  name: string;
  type: string;
  personId?: string; // Guid
  filmId?: string; // Guid
  showId?: string; // Guid
  episodeId?: string; // Guid
}
