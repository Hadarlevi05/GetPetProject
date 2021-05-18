import { Component, OnInit } from '@angular/core';
import { ITrait } from 'src/app/shared/models/itrait';
import { TraitFilter } from 'src/app/shared/models/trait-filter';
import { TraitsService } from 'src/app/shared/services/traits.service';

@Component({
  selector: 'app-search-pets',
  templateUrl: './search-pets.component.html',
  styleUrls: ['./search-pets.component.sass']
})
export class SearchPetsComponent implements OnInit {

  traits: ITrait[] = [];

  constructor(private traitsService: TraitsService) { }

  ngOnInit(): void {

    this.loadTraits();
  }

  loadTraits() {
    let animalTypeId = 3;

    let date = new Date();
    date.setDate(date.getDate() - 20);
    const filter = new TraitFilter(1, 100, date, animalTypeId);

    this.traitsService.Post(filter).subscribe(traits => {
      this.traits = traits;
    })
  }
}