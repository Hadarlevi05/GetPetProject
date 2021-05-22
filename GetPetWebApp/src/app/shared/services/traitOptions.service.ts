import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { ITraitOption } from '../models/itraitoption';
import { TraitOptionFilter } from '../models/traitOptionFilter';

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
