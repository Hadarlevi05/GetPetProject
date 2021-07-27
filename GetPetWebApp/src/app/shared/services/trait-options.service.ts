import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { ITraitOption } from '../models/itrait-option';
import { TraitOptionFilter } from '../models/trait-option-filter';

@Injectable({
  providedIn: 'root'
})
export class TraitOptionsService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  post(filter: TraitOptionFilter): Observable<ITraitOption[]> {
    return this.http.post<ITraitOption[]>(`${this.entPointUrl}traitoptions`, filter);
  }
}
