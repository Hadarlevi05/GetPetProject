import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { IPet } from '../models/ipet';
import { Pet } from '../models/pet';
import { PetFilter } from '../models/pet-filter';


@Injectable({
  providedIn: 'root'
})
export class PetsService extends BaseService {

  entPointUrl = `${this.BASE_URL}pets`;

  constructor(http: HttpClient) {
    super(http);
  }

  search(filter: PetFilter): Observable<IPet[]> {
    return this.http.post<IPet[]>(`${this.entPointUrl}/search`, filter);
  }

  addPet(pet: Pet) {
    console.log("in add pet");
    return this.http.post(`${this.entPointUrl}`, pet).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }
}