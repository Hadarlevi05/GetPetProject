import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { empty, Observable, of } from 'rxjs';
import { map, startWith, switchMap } from 'rxjs/operators';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { CityFilter } from 'src/app/shared/models/city-filter';
import { ICity } from 'src/app/shared/models/icity';
import { IUser } from 'src/app/shared/models/iuser';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { CityService } from 'src/app/shared/services/city.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { IOrganization } from 'src/app/shared/models/iorganization';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {

  @Input() error: string | undefined;
  cities: ICity[] = [];
  loading = false;
  hide = true;

  filteredCities: Observable<ICity[]> = of({} as ICity[]);

  user: IUser = <IUser>{
    organization: <IOrganization>{}
  };

  form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    organizationName: new FormControl('', []),
    cityId: new FormControl('', [Validators.required]),
    filterCity: new FormControl('', [Validators.required]),
  });

  private _filter(value: string): ICity[] {
    const filterValue = value.toLowerCase();

    return this.cities.filter(city => city.name.toLowerCase().indexOf(filterValue) === 0);
  }

  constructor(
    private authenticationService: AuthenticationService,
    private cityService: CityService,
    private route: Router
  ) { }

  ngOnInit(): void {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new CityFilter(1, 1000, date);

    this.cityService.get(filter).subscribe(cities => {
      this.cities = cities;
    });
    this.formOnChanges();
  }

  formOnChanges(): void {
    this.form.valueChanges.subscribe(formUser => {
      this.user.email = formUser.email;
      this.user.cityId = formUser.cityId;
      this.user.name = formUser.name;
      this.user.organization.name = formUser.organizationName;
      this.user.cityId = formUser.cityId;
      this.user.password = formUser.password;
    });

    this.filteredCities = this.form.controls.filterCity.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }

  selectedCity(cityName: string) {
    const cityId = this.cities.find(c => c.name === cityName)?.id;
    this.form.controls.cityId.setValue(cityId);
  }

  submit() {
    this.loading = true;

    this.authenticationService.register(this.user)
      .pipe(
        switchMap(() => this.authenticationService.login(this.user.email, this.user.password))
      )
      .subscribe((res) => {
        this.route.navigate(['']);
        this.loading = false;
      });
  }
}