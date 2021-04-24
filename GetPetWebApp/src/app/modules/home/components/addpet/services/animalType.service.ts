import { PetFilter } from './../../../../pets/models/pet-filter';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { IAnimalType } from '../models/iAnimalType';

@Injectable({
  providedIn: 'root'
})
export class AnimalTypeService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  Get(filter: PetFilter): Observable<IAnimalType[]> {
    return this.http.get<IAnimalType[]>(`${this.entPointUrl}animaltypes`);
  }
}
