import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActorDto } from '../dto/actor-dto';
import { CharacterActorDto } from '../dto/character-dto';

@Injectable({ providedIn: 'root' })
export class ActorService {
  constructor(private http: HttpClient) {}

  getActors(): Observable<ActorDto[]> {
    return this.http.get<ActorDto[]>('/actor');
  }

  getActorMedias(actorId: string): Observable<CharacterActorDto[]> {
    return this.http.get<CharacterActorDto[]>(`/actor/${actorId}/media`);
  }
}
