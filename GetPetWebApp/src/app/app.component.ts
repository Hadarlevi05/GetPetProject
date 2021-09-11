import { Component, OnInit } from '@angular/core';
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

  constructor(
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
      this.isLogin = user.id > 0;
    });
  }

  logout() {
    this.authenticationService.logout();
  }

}