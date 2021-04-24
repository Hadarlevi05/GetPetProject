import { CityService } from './services/city.service';
import { ICity } from './models/iCity';
import { CityFilter } from './models/cityFilter';
import { AnimalTypeFilter } from './models/animalTypeFilter';
import { IAnimalType } from './models/iAnimalType';
import { AnimalTypeService } from './services/animalType.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})
export class AddpetComponent implements OnInit {

  isSubmitted = false;
  animaltypes_arr: IAnimalType[] = [];
  city_arr: ICity[] = [];

  constructor(public fb: FormBuilder, private animalTypeService: AnimalTypeService, private cityService : CityService) {}

  ngOnInit(): void {
    this.loadAnimalTypes();
    this.loadCities();
    this.addPetForm.reset({gender: 'new value'});
  }

  addPetForm = this.fb.group({
    petName:['', [Validators.required]],
    animalType: ['', [Validators.required]],
    gender:['', [Validators.required]],
    dob:['', [Validators.required]],
    city:['', [Validators.required]],
    description:['', [Validators.required]],
  })

  get animalTypeName() {
    return this.addPetForm.get('animalType')
  }

  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTypeFilter(1, 5, date);
    this.animalTypeService.Get(filter).subscribe(types => {
      this.animaltypes_arr = types;
    });
  }

  loadCities() {
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new CityFilter(1,5,date);
    this.cityService.Get(filter).subscribe(cities => {
      this.city_arr = cities;
    });
  }

  deleteForm() {
    //delete all fields in form
  }

  saveForm() {
    this.isSubmitted = true;
    if(this.addPetForm.valid){
      console.log('Add pet form data :: ', this.addPetForm.value);
    }
  }
}