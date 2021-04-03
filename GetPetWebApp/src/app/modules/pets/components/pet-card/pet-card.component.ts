import { Component, Input, OnInit } from '@angular/core';
import { IPet } from '../../models/ipet';

@Component({
  selector: 'app-pet-card',
  templateUrl: './pet-card.component.html',
  styleUrls: ['./pet-card.component.sass']
})
export class PetCardComponent implements OnInit {

  @Input()
  pet: IPet = {} as IPet;

  constructor() { }

  ngOnInit(): void {
  }

}
