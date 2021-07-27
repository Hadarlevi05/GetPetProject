import { AnimalTypeFilter } from '../models/animal-type-filter';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { IAnimalType } from '../models/ianimal-type';

@Injectable({
  providedIn: 'root'
})
export class AnimalTypeService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  get(filter: AnimalTypeFilter): Observable<IAnimalType[]> {
    return this.http.get<IAnimalType[]>(`${this.entPointUrl}animaltypes`);
  }
}
