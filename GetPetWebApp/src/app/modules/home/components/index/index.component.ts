import { Component, OnInit } from '@angular/core';
import { IPet } from 'src/app/modules/pets/models/ipet';
import { PetFilter } from 'src/app/modules/pets/models/pet-filter';
import { PetsService } from 'src/app/modules/pets/services/pets.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.sass']
})
export class IndexComponent implements OnInit {

  loading = true;

  pets: IPet[] = [];

  constructor(private petsService: PetsService) { }

  ngOnInit(): void {
    this.loadPets();
  }


  loadPets() {

    let date = new Date();
    date.setDate(date.getDate() - 20);

    let filter = new PetFilter(1, 5, date);

    this.petsService.Search(filter).subscribe(pets => {
      this.pets = pets;

      this.loading = false;
    });
  }
}
