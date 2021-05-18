import { Component, Input, OnInit } from '@angular/core';
import { ITrait } from 'src/app/shared/models/itrait';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.sass']
})
export class SelectComponent implements OnInit {

  @Input() trait: ITrait = {} as ITrait;

  constructor() { }

  ngOnInit(): void {
  }

}
