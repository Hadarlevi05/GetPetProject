import { IAnimalTrait } from '../models/iAnimalTrait';
import { HttpClient } from '@angular/common/http';
import { HtmlParser } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { TraitOptionFilter } from '../models/traitOptionFilter';
import { ITraitOption } from '../models/iTraitOption';

@Injectable({
  providedIn: 'root'
})
export class TraitOptionsService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  Post(filter: TraitOptionFilter): Observable<ITraitOption[]> {
    return this.http.post<ITraitOption[]>(`${this.entPointUrl}traitoptions`, filter);
  }
}
