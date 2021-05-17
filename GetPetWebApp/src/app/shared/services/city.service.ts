
import { HttpClient, HttpParams, HttpParamsOptions } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { CityFilter } from '../models/city-filter';
import { ICity } from '../models/icity';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  Get(filter: CityFilter): Observable<ICity[]> {

    let params: HttpParams = this.GetHttpParams(filter);

    return this.http.get<ICity[]>(`${this.entPointUrl}cities`, { params });
  }
}
