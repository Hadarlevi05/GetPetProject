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