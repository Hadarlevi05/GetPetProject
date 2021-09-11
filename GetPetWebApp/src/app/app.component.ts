import { Component, OnInit } from '@angular/core';
import { PetFilter } from './modules/pets/models/pet-filter';
import { PetsService } from './modules/pets/services/pets.service';
import { PetStatus } from './shared/enums/pet-status';
import { IUser } from './shared/models/iuser';
import { AuthenticationService } from './shared/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'גט פט!';

  user: IUser = <IUser>{};
  isLogin = false;

  articleLoading = true;
  waitingForAdoptionCount: number = 0;
  waitingForAdoptionAnimation: number = 0;
  adoptedCount: number = -1;

  constructor(
    private authenticationService: AuthenticationService,
    private petsService: PetsService) { }

  ngOnInit() {
    this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
      this.isLogin = user.id > 0;
    });

    this.setCounters();
  }

  setCounters() {

    const date = new Date();
    date.setDate(date.getDate() - 1000);


    const filter = new PetFilter(1, 100, date, [], undefined, undefined, PetStatus.WaitingForAdoption);
    this.petsService.searchCount(filter).subscribe(counter => {
      this.waitingForAdoptionCount = counter.count;

      console.log("WAITING FOR ADOPTION: ", this.waitingForAdoptionCount);

      var waitingForAdoptionStop: any = setInterval(() => {
        console.log("waitfor count", this.waitingForAdoptionCount);
        if (this.waitingForAdoptionCount <= 0) {
          clearInterval(waitingForAdoptionStop);
        } else {
          this.waitingForAdoptionAnimation++;
          console.log("counting:", this.waitingForAdoptionAnimation);
          if (this.waitingForAdoptionAnimation == this.waitingForAdoptionCount) {
            clearInterval(waitingForAdoptionStop);
          }
        }
      }, 10);


    });

    filter.petStatus = PetStatus.Adopted;
    this.petsService.searchCount(filter).subscribe(counter => {
      this.adoptedCount = counter.count;
    });
  }

  logout() {
    this.authenticationService.logout();

  }
}