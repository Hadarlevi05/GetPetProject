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

  getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    return age;
  }
}