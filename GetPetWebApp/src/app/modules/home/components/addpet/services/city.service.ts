import { ICity } from './../models/iCity';
import { CityFilter } from './../models/cityFilter';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  Get(filter: CityFilter): Observable<ICity[]> {
    return this.http.get<ICity[]>(`${this.entPointUrl}cities`);
  }
}
