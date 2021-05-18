import { CityService } from '../../../../shared/services/city.service';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ControlContainer, ControlValueAccessor } from '@angular/forms';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { IPet } from 'src/app/modules/pets/models/ipet';
import { TraitOptionFilter } from 'src/app/shared/models/traitOptionFilter';
import { ITraitOption } from 'src/app/shared/models/iTraitOption';
import { TraitOptionsService } from 'src/app/shared/services/traitOptions.service';
import { compileNgModule } from '@angular/compiler';
import { ICity } from 'src/app/shared/models/icity';
import { AnimalTypeService } from 'src/app/shared/services/animal-type.service';
import { CityFilter } from 'src/app/shared/models/city-filter';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';
import { MatChip } from '@angular/material/chips';
import { TraitsService } from 'src/app/shared/services/traits.service';
import { ITrait } from 'src/app/shared/models/itrait';
import { TraitFilter } from 'src/app/shared/models/trait-filter';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})

export class AddpetComponent 
  implements OnInit {

  loading = false;
  success = false;
  optionBooleanVal = false;
  addPetFormGroup!: FormGroup;

  
  pet: IPet = {
    name: '',
    animalTypeId: 0,
    userId: 1,
    birthday: "",
    traits: new Map(),
    description: '',
    images: [''],
    creationTimestamp: new Date()
  }
  
  animaltypes_arr: IAnimalType[] = [];
  city_arr: ICity[] = [];
  traits_arr: ITrait[] = [];
  optionsForTrait: ITraitOption[] = [];
  traitsWithBooleanValue: ITrait[] = [];
  traitsWithSetOfValues: ITrait[] = [];
  gender_arr: string[] = ['לא ידוע', 'זכר', 'נקבה'];
  
  constructor(private _formBuilder: FormBuilder,
              private _animalTypeService: AnimalTypeService, 
              private _cityService: CityService, 
              private _traitsService: TraitsService,
              private _traitOptionsService: TraitOptionsService,
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
          chipsControl: new FormControl(['']),
          traits: new FormControl (['']),
          description:['', [Validators.required,
                            Validators.maxLength(500)]],
        }),
        this._formBuilder.group({
          //upload picture control?
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

  // get traitsFormArray() {
  //   return this.addPetFormGroup.controls.animalTraits as FormArray;
  // }

  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTypeFilter(1, 100, date);
    this._animalTypeService.Get(filter).subscribe(types => {
      this.animaltypes_arr = types;
    });
  }

  loadCities() {
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new CityFilter(1, 100, date);
    this._cityService.Get(filter).subscribe(cities => {
      this.city_arr = cities;
    });
  }

  loadUniqueTraits(event) {

    this.deleteTraitsArrays();
    let animalTypeId = event.value;
    console.log("animaltype changed to: " + animalTypeId);
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new TraitFilter(1, 100, date, animalTypeId);
    this._traitsService.Post(filter).subscribe(traits => {
      this.traits_arr = traits;
      this.classifyTraits();
    })
  }

  private classifyTraits() {

    console.log(this.traits_arr);

    for(const trait of this.traits_arr) {
      this.optionsForTrait = trait.traitOptions;
      for (const option of this.optionsForTrait) {
        if (this.isBooleanValue(option)) {
            this.traitsWithBooleanValue.push(trait);
            break;
          } else {
            this.traitsWithSetOfValues.push(trait);
            break;
          }
      }
    }

    // console.log("traits with boolean value:");
    // console.log(this.traitsWithBooleanValue);
    // console.log("trait with set of value:");
    // console.log(this.traitsWithSetOfValues);
  }

  private isBooleanValue(op: ITraitOption) : boolean {
    return (op.option == 'כן' || op.option == 'לא')
  }

  private deleteTraitsArrays() {
    this.traits_arr = [];
    this.traitsWithBooleanValue = [];
    this.traitsWithSetOfValues = [];
  }

  onSubmit(postData) {
    console.log(postData);

    // userId: 1,
    // birthday: "",
    // traits: new Map(),
    // images: [''],
    // creationTimestamp: new Date()

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

}