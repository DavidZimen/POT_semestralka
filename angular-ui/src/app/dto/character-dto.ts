import {PersonMinimalDto} from "./person-dto";

export interface CharacterDto {
  characterId: string; // Guid
  filmId?: string; // Guid
  showId?: string; // Guid
  actorId: string; // Guid
  characterName: string;
}

export interface CharacterActorDto {
  showId?: string;
  filmId?: string;
  mediaName: string;
  characterName: string;
  premiereDate: string; // Date
  endDate?: string; // Date
}

export interface CharacterMediaDto {
  characterId: string
  title: string;
  characterName: string;
  actor: PersonMinimalDto; // Minimal Person DTO
}

export interface CharacterCreate {
  characterName: string;
  filmId?: string; // Guid
  showId?: string; // Guid
  actorPersonId: string; // Guid
}
