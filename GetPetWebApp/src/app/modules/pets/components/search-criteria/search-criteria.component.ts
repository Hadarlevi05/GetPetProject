import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { ITrait } from 'src/app/shared/models/itrait';

@Component({
  selector: 'app-search-criteria',
  templateUrl: './search-criteria.component.html',
  styleUrls: ['./search-criteria.component.sass']
})
export class SearchCriteriaComponent implements OnInit {

  @Input()
  trait: ITrait = {} as ITrait;

  @Input() clearEvents: Observable<void> = {} as Observable<void>;

  @Output() changed = new EventEmitter<{ traitId: number, options: number[] | boolean }>();

  private clearSubscription: Subscription = {} as Subscription;

  optionsSelected: number[] = [];

  traitChecked = false;

  constructor() { }

  ngOnInit(): void {

    this.clearSubscription = this.clearEvents.subscribe(() => this.clear());

  }

  clear() {

    this.optionsSelected = [];
    this.traitChecked = false;

    console.log('clear!');
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