import { Component, Input, OnInit } from '@angular/core';
import { ITrait } from 'src/app/shared/models/itrait';

@Component({
  selector: 'app-search-criteria',
  templateUrl: './search-criteria.component.html',
  styleUrls: ['./search-criteria.component.sass']
})
export class SearchCriteriaComponent implements OnInit {

  @Input()
  trait: ITrait = {} as ITrait;

  optionsSelected: number[] = [];

  traitChecked = false;

  constructor() { }

  ngOnInit(): void {
  }

  selectOption(value) {

    const index = this.optionsSelected.findIndex(i => i === value);

    if (index > -1) {
      this.optionsSelected.splice(index, 1);
    } else {
      this.optionsSelected.push(value);
    }
  }
}
