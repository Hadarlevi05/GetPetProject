import { AnimalTraitsService } from './services/animalTraits.service';
import { IAnimalTrait } from './models/iAnimalTrait';
import { CityService } from './services/city.service';
import { ICity } from './models/iCity';
import { CityFilter } from './models/cityFilter';
import { AnimalTypeFilter } from './models/animalTypeFilter';
import { IAnimalType } from './models/iAnimalType';
import { AnimalTypeService } from './services/animalType.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';
import { AnimalTraitFilter } from './models/AnimalTraitFilter';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})
export class AddpetComponent implements OnInit {

  isSubmitted = false;

  firstFormGroup : FormGroup = this._formBuilder.group({
    animalType: ['', [Validators.required]],
  });

  secondFormGroup : FormGroup = this._formBuilder.group({
  petName: new FormControl('', [Validators.required]),
  gender:['', [Validators.required]],
  dob:['', [Validators.required]]
  });

  thirdFormGroup : FormGroup = this._formBuilder.group({
    city:['', [Validators.required]],
    description:['', [Validators.required]],
    animalTraits: ['', [Validators.required]]
  })

  animaltypes_arr: IAnimalType[] = [];
  city_arr: ICity[] = [];
  traits_arr: IAnimalTrait[] = [];

  constructor(public _formBuilder: FormBuilder, private _animalTypeService: AnimalTypeService, private _cityService: CityService, private _traitsService: AnimalTraitsService) { }

  ngOnInit(): void {

    this.loadAnimalTypes();
    this.loadCities();
    this.loadUniqueTraits();
  
  }

  afuConfig = {
    multiple: false,
    formatsAllowed: ".jpg, .jpeg, .png",
    maxSize: 10,    //in MB
    uploadAPI: {
      url:"https://example-file-upload-api" //TODO: change this url
    },
    hideProgressBar: false,
    hideResetBtn: true,
    replaceTexts: {
      selectFileBtn: 'בחר קובץ',
      uploadBtn: 'שלח',
      afterUploadMsg_success: 'העלאה הצליחה',
      afterUploadMsg_error: 'העלאת הקובץ נכשלה',
      sizeLimit: 'גודל מירבי'
    }
};

  // get animalTypeName() {
  //   return this.addPetForm.get('animalType')
  // }

  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTypeFilter(1, 5, date);
    this._animalTypeService.Get(filter).subscribe(types => {
      this.animaltypes_arr = types;
    });
  }

  loadCities() {
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new CityFilter(1,5,date);
    this._cityService.Get(filter).subscribe(cities => {
      this.city_arr = cities;
    });
  }

  loadUniqueTraits(animalTypeId: number = 2) {
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTraitFilter(1,5,date, animalTypeId);
    this._traitsService.Post(filter).subscribe(traits => {
      this.traits_arr = traits;
    })

  }
  
  // saveForm() {
  //   this.isSubmitted = true;
  //   if(this.addPetForm.valid){
  //     console.log('Add pet form data :: ', this.addPetForm.value);
  //   }
  // }
  }