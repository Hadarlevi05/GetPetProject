import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { CityFilter } from 'src/app/shared/models/cityFilter';
import { ICity } from 'src/app/shared/models/iCity';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { CityService } from 'src/app/shared/services/city.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {

  @Input() error: string | undefined;
  cities: ICity[] = [];
  loading = false;

  form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    organization: new FormControl('', [Validators.required]),
    cityId: new FormControl('', [Validators.required]),
  });

  constructor(
    private authenticationService: AuthenticationService,
    private cityService: CityService,
    private route: Router
  ) { }

  ngOnInit(): void {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new CityFilter(1, 1000, date);

    this.cityService.Get(filter).subscribe(cities => {
      this.cities = cities;
    });

  }

  submit() {

    this.loading = true;

    this.authenticationService.login(this.form.value.email, this.form.value.password)
      .subscribe((res) => {
        this.route.navigate(['']);

        this.loading = false;
      });
  }
  onKey(event) {

  }

}
