import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { IPet } from '../models/ipet';
import { PetFilter } from '../models/pet-filter';


@Injectable({
  providedIn: 'root'
})
export class PetsService extends BaseService {

  postPetId: number = 0;

  entPointUrl = `${this.BASE_URL}pets`;

  constructor(http: HttpClient) {
    super(http);
  }

  Search(filter: PetFilter): Observable<IPet[]> {
    return this.http.post<IPet[]>(`${this.entPointUrl}/search`, filter);
  }

  public addPet (pet: IPet) {
    return this.http.post(`${this.entPointUrl}`, pet).subscribe(
      (response) => {
        console.log("pet post response is",response);
        this.postPetId = response['id'];
        console.log(this.postPetId);
      },
      (error) => console.log(error)
    );
  }
}