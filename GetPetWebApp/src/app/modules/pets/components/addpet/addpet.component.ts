import { CityService } from '../../../../shared/services/city.service';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { IPet } from 'src/app/modules/pets/models/ipet';
import { ICity } from 'src/app/shared/models/icity';
import { AnimalTypeService } from 'src/app/shared/services/animal-type.service';
import { AnimalTraitsService } from 'src/app/shared/services/animal-traits.service';
import { IAnimalTrait } from '../../../../shared/models/ianimal-trait';
import { CityFilter } from 'src/app/shared/models/city-filter';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { AnimalTraitFilter } from 'src/app/shared/models/animal-trait-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})

export class AddpetComponent implements OnInit {

  loading = false;
  success = false;
  addPetFormGroup!: FormGroup;

  pet: IPet = {
    name: '',
    images: [''],
    description: '',
    animalTypeId: 0,
    userId: 1,
    creationTimestamp: new Date()
  }

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
          gender: ['', [Validators.required]],
          dob: ['', [Validators.required]],
          //animalTraits: new FormArray([]),
          description: ['', [Validators.required,
          Validators.maxLength(500)]],
        }),
        this._formBuilder.group({
          //upload picture control?
        }),
        this._formBuilder.group({
          city: ['', [Validators.required]],
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
      url: "https://example-file-upload-api" //TODO: change this url
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
    let filter = new CityFilter(1, 5, date);
    this._cityService.Get(filter).subscribe(cities => {
      this.city_arr = cities;
    });
  }

  loadUniqueTraits(event) {

    let animalTypeId = event.value;
    // console.log(animalTypeId);
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTraitFilter(1, 5, date, animalTypeId);
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

    this.pet.name = this.formArray?.get([1])?.get('petName')?.value;
    this.pet.description = this.formArray?.get([1])?.get('description')?.value;
    this.pet.animalTypeId = this.formArray?.get([0])?.get('animalType')?.value;

    console.log("PET INFO IS:")
    console.log(this.pet);

    try {
      this._petsService.addPet(this.pet);
      this.success = true;
    } catch (err) {
      console.log(err);
    }
    this.loading = false;
  }

  // changeAnimalType(value : any) {
  //   this.loadUniqueTraits(value);
  // }
}