import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ITrait } from 'src/app/shared/models/itrait';
import { ITraitSelection } from 'src/app/shared/models/itrait-selection';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.sass']
})
export class SelectComponent implements OnInit {

  traitSelection: ITraitSelection = {} as ITraitSelection;

  @Input() 
  trait: ITrait = {} as ITrait;

  @Output()
  selectionEvent = new EventEmitter<ITraitSelection>();

  constructor() { }

  ngOnInit(): void {
  }

  selectionChange(value: string){

    this.traitSelection.traitId = this.trait.id;
    this.traitSelection.traitOptionId = +value;

    this.selectionEvent.emit(this.traitSelection);
  }
}
