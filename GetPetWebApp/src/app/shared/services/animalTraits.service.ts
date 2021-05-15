
import { HttpClient } from '@angular/common/http';
import { HtmlParser } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAnimalTrait } from 'src/app/shared/models/iAnimalTrait';
import { BaseService } from 'src/app/shared/services/base-service';
import { AnimalTraitFilter } from '../models/AnimalTraitFilter';



@Injectable({
  providedIn: 'root'
})
export class AnimalTraitsService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  Post(filter: AnimalTraitFilter): Observable<IAnimalTrait[]> {
    return this.http.post<IAnimalTrait[]>(`${this.entPointUrl}animaltraits`, filter);
  }
}
