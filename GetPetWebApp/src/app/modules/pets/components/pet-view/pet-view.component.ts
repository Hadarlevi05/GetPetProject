import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IPet } from '../../models/ipet';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-pet-view',
  templateUrl: './pet-view.component.html',
  styleUrls: ['./pet-view.component.sass']
})
export class PetViewComponent implements OnInit {


  @Input()
  pet: IPet = {} as IPet;

  imageUrl = '';

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.pet = data.pet;
  }

  ngOnInit(): void {
    this.imageUrl = environment.baseImageUrl;
  }
}