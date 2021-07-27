import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PetFilter } from 'src/app/modules/pets/models/pet-filter';
import { BaseService } from './base-service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService extends BaseService {

  entPointUrl = `${this.BASE_URL}`;

  constructor(http: HttpClient) {
    super(http);
  }

  upsert(filter: PetFilter): Observable<any> {
    return this.http.post<any>(`${this.entPointUrl}notifications`, filter);
  }
}
