import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ITrait } from 'src/app/shared/models/itrait';

@Component({
  selector: 'app-search-criteria',
  templateUrl: './search-criteria.component.html',
  styleUrls: ['./search-criteria.component.sass']
})
export class SearchCriteriaComponent implements OnInit {

  @Input()
  trait: ITrait = {} as ITrait;

  @Output() changed = new EventEmitter<{ traitId: number, options: number[] | boolean }>();

  optionsSelected: number[] = [];

  traitChecked = false;

  constructor() { }

  ngOnInit(): void {
  }

  selectOption(value: number) {
    const index = this.optionsSelected.findIndex(i => i === value);

    if (index > -1) {
      this.optionsSelected.splice(index, 1);
    } else {
      this.optionsSelected.push(value);
    }
    this.changed.emit({ traitId: this.trait.id, options: [...this.optionsSelected] });
  }

  selectCheckbox() {
    this.changed.emit({ traitId: this.trait.id, options: this.traitChecked });
  }
}
