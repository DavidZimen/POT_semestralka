import {ImageIdDto} from './image-dto';

export interface PersonDto extends ImageIdDto{
  personId: string; // Guid
  firstName: string;
  middleName?: string;
  lastName: string;
  bio?: string;
  birthDate: string; // Date
  country: string;
  actorId?: string; // Guid
  directorId?: string; // Guid
}

export interface PersonMinimalDto extends ImageIdDto {
  personId: string; // Guid
  firstName: string;
  lastName: string;
}

export interface CreatePerson {
  firstName: string;
  middleName?: string;
  lastName: string;
  bio?: string;
  birthDate: string; // Date
  country: string;
}

export interface UpdatePerson {
  personId: string; // Guid
  firstName: string;
  middleName?: string;
  lastName: string;
  bio?: string;
  birthDate: string; // Date
  country: string;
}
