
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { ITrait } from '../models/itrait';
import { TraitFilter } from '../models/trait-filter';


@Injectable({
  providedIn: 'root'
})
export class TraitsService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  post(filter: TraitFilter): Observable<ITrait[]> {
    return this.http.post<ITrait[]>(`${this.entPointUrl}traits`, filter);
  }
}
