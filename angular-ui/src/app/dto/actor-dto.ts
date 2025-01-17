import { PersonDto } from "./person-dto";

export interface ActorDto {
  id: string; // Guid
  person: PersonDto; // PersonDto from PersonDtos
  showCount: number;
  filmCount: number;
}
