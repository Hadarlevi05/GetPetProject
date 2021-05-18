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

  constructor() { }

  ngOnInit(): void {
  }

}
