import { AnimalTraitsService } from './services/animalTraits.service';
import { IAnimalTrait } from './models/iAnimalTrait';
import { CityService } from './services/city.service';
import { ICity } from './models/iCity';
import { CityFilter } from './models/cityFilter';
import { AnimalTypeFilter } from './models/animalTypeFilter';
import { IAnimalType } from './models/iAnimalType';
import { AnimalTypeService } from './services/animalType.service';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AnimalTraitFilter } from './models/AnimalTraitFilter';
import { PetsService } from 'src/app/modules/pets/services/pets.service';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})

export class AddpetComponent implements OnInit {

  loading = false;
  success = false;
  addPetFormGroup!: FormGroup;

  animaltypes_arr: IAnimalType[] = [];
  city_arr: ICity[] = [];
  traits_arr: IAnimalTrait[] = [];
  gender_arr: string[] = ['לא ידוע', 'זכר', 'נקבה'];

  constructor(private _formBuilder: FormBuilder,
              private _animalTypeService: AnimalTypeService, 
              private _cityService: CityService, 
              private _traitsService: AnimalTraitsService,
              private _petsService: PetsService) { }

  ngOnInit(): void {

    this.loadAnimalTypes();
    this.loadCities();

    this.addPetFormGroup = this._formBuilder.group({
      formArray: this._formBuilder.array([
        this._formBuilder.group({
          animalType: ['', [Validators.required]]
        }),
        this._formBuilder.group({
          petName: new FormControl('', [Validators.required, 
            Validators.minLength(2),
            Validators.maxLength(10)]),
          gender:['', [Validators.required]],
          dob:['', [Validators.required]],
          //animalTraits: new FormArray([]),
          description:['', [Validators.required,
                            Validators.maxLength(500)]],
        }),
        this._formBuilder.group({
          //upload picture control?
        }),
        this._formBuilder.group({
          city:['', [Validators.required]],
        }),
        this._formBuilder.group({
          //preview and send
        }),
      ])
    });
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

get formArray(): AbstractControl | null {
  return this.addPetFormGroup.get('formArray');
}

  get traitsFormArray() {
    return this.addPetFormGroup.controls.animalTraits as FormArray;
  }

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

  loadUniqueTraits(event) {

    let animalTypeId = event.value;
    // console.log(animalTypeId);
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTraitFilter(1,5,date, animalTypeId);
    this._traitsService.Post(filter).subscribe(traits => {
      this.traits_arr = traits;
    })

    this.addTraitCheckboxes();
  }

  private addTraitCheckboxes() {
    this.traits_arr.forEach(() => this.traitsFormArray.push(new FormControl(false)));
  }

  onSubmit(postData) {
    console.log(postData);
  }

  // onSubmit() {
  //   console.log("in onSubmit!");
  //   var formData: any = new FormData();
  //   formData.append("name", this.formArray?.get([1])?.get('petName')?.value);
  //   formData.append("birthday", this.formArray?.get([1])?.get('dob')?.value);
  //   formData.append("gender", this.formArray?.get([1])?.get('gender')?.value);
  //   formData.append("animalTypeId", this.formArray?.get([0])?.get('animalType')?.value);
  //   formData.append("description", this.formArray?.get([1])?.get('description')?.value);
    
  //   this.loading = true;
  //   console.log("The form data that is going to be sent is:");
  //   console.log(formData);

  //   try {
  //      this._petsService.addPet(formData);
  //      this.success = true;
  //    } catch (err) {
  //      console.log(err);
  //    }
  //    this.loading = false;
  // }

  // changeAnimalType(value : any) {
  //   this.loadUniqueTraits(value);
  // }
  }