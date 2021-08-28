import { Component, Input, OnInit } from '@angular/core';
import { IPet } from '../../models/ipet';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-pet-card',
  templateUrl: './pet-card.component.html',
  styleUrls: ['./pet-card.component.sass']
})
export class PetCardComponent implements OnInit {

  @Input()
  pet: IPet = {} as IPet;

  imageUrl = '';
  taritsToDisplay = 3;

  topTraits(): Array<string> {
    if (!this.pet)
      return [];

    return [...Object.values(this.pet.traits), ...Object.keys(this.pet.booleanTraits)].slice(0, this.taritsToDisplay);
  }

  traitsHiddenCount(): number {
    return Object.keys(this.pet.traits).length + Object.keys(this.pet.booleanTraits).length - this.taritsToDisplay;
  }

  constructor() { }

  ngOnInit(): void {
    this.imageUrl = environment.baseImageUrl;
  }

  getAge(birthday, gender) {

    birthday = new Date(birthday);

    var age = this.calculateAge(new Date(birthday));
    const genderString = gender == 1 ? 'בן' : 'בת'
    if (age === 0) {
      const oneDay = 24 * 60 * 60 * 1000;
      const diffMonth = Math.round(Math.abs((Date.now() - birthday) / oneDay) / 30);
      if (diffMonth === 1) return `, ${genderString} חודש`;
      if (diffMonth === 2) return `, ${genderString} חודשיים`;
      if (diffMonth == 0)
        return;
      return `, ${genderString} ${diffMonth} חודשים`;
    }

    if (age === 1) return `, ${genderString} שנה`;
    if (age === 2) return `, ${genderString} שנתיים`;
    if (age == 0 || age > 18)
      return;
    return `, ${genderString} ${age} שנים`;
  }

  calculateAge(birthday) {
    var ageDifMs = Date.now() - birthday;
    var ageDate = new Date(ageDifMs); // miliseconds from epoch
    return Math.abs(ageDate.getUTCFullYear() - 1970);
  }
}