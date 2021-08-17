import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  OnChanges
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatChip, MatChipList, MatChipSelectionChange } from '@angular/material/chips';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { map } from 'rxjs/operators';
import { ITrait } from 'src/app/shared/models/itrait';
import { ITraitSelection } from 'src/app/shared/models/itrait-selection';

@UntilDestroy()
@Component({
  selector: 'app-chips-multi-select',
  templateUrl: './multi-select-chips.component.html',
  styleUrls: ['./multi-select-chips.component.sass'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: MultiSelectChipsComponent,
      multi: true,
    },
  ],
})
export class MultiSelectChipsComponent
  implements OnInit, AfterViewInit, ControlValueAccessor, OnChanges {
  @ViewChild(MatChipList)
  chipList!: MatChipList;

  traitChipSelection: ITraitSelection = {} as ITraitSelection;
  traitChipSelections: ITraitSelection[] = [];

  @Input() isMatChipsTraitsLoaded;

  @Input() options: ITrait[] = [];

  // @Output()
  // selectionEvent = new EventEmitter<ITraitSelection>();

  @Output() data: EventEmitter<ITraitSelection[]> = new EventEmitter<ITraitSelection[]>();

  value: string[] = [];

  onChange!: (value: string[]) => void;
  onTouch: any;

  disabled = false;

  constructor() {}

  writeValue(value: string[]): void {
    // When form value set when chips list initialized
    if (this.chipList && value) {
      this.selectChips(value);
    } else if (value) {
      // When chips not initialized
      this.value = value;
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnInit() {
    this.data.emit(this.traitChipSelections);
 }

  ngOnChanges() {

    console.log("in child: isMatChipsTraitsLoaded: " + this.isMatChipsTraitsLoaded);

    this.setAllMatchipsFalse();

    console.log('all matchips selections', this.traitChipSelections);
  }

  public setAllMatchipsFalse() {

    this.traitChipSelections = [] as ITraitSelection[];

    this.options.forEach (op => {
      console.log("current mat chip option: ", op);
      this.traitChipSelection = {} as ITraitSelection;      
      this.traitChipSelection.traitId = op.id;
      this.traitChipSelection.traitOptionId = 
      (op.traitOptions[0].option === "לא") ? op.traitOptions[0].id : op.traitOptions[1].id;
      this.traitChipSelections.push(this.traitChipSelection);
    })
  }

  ngAfterViewInit() {
    this.selectChips(this.value);

    this.chipList.chipSelectionChanges
      .pipe(
        untilDestroyed(this),
        map((event) => event.source)
      )
      .subscribe((chip) => {
        if (chip.selected) {
          this.value = [...this.value, chip.value];
        } else {
          this.value = this.value.filter((o) => o !== chip.value);
        }

        this.propagateChange(this.value);
      });
  }

  propagateChange(value: string[]) {
    if (this.onChange) {
      this.onChange(value);
    }
  }

  selectChips(value: string[]) {
    this.chipList.chips.forEach((chip) => chip.deselect());

    const chipsToSelect = this.chipList.chips.filter((c) =>
      value.includes(c.value)
    );

    chipsToSelect.forEach((chip) => chip.select());
  }

  //value is of type iTrait
  toggleSelection(chip: MatChip) {
    if (!this.disabled) chip.toggleSelected();
    console.log("chip changed! : Property no. " + chip.value.id + "name: " + chip.value.name + " changed to : " + chip.selected);
    //strategy: all mats are "no" and then update to change
    var optionId;
    const item = this.traitChipSelections.find(i => i.traitId === chip.value.id);
    if (item) {
      if (chip.selected) {
        optionId = (chip.value.traitOptions[0].option === "כן") ? chip.value.traitOptions[0].id : chip.value.traitOptions[1].id;
      } else {
        optionId = (chip.value.traitOptions[0].option === "לא") ? chip.value.traitOptions[0].id : chip.value.traitOptions[1].id;
      }

      item.traitOptionId = optionId;
    }
    console.log('all matchips selections', this.traitChipSelections);
  }
}