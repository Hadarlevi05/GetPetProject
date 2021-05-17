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
import { AnimalTraitsService } from 'src/app/shared/services/animal-traits.service';
import { IAnimalTrait } from 'src/app/shared/models/ianimal-trait';
import { CityFilter } from 'src/app/shared/models/city-filter';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { AnimalTraitFilter } from 'src/app/shared/models/animal-trait-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';
import { MatChip } from '@angular/material/chips';

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
    images: [''],
    description: '',
    animalTypeId: 0,
    userId: 1,
    creationTimestamp: new Date()
  }
  
  animaltypes_arr: IAnimalType[] = [];
  city_arr: ICity[] = [];
  traits_arr: IAnimalTrait[] = [];
  traitsWithBooleanValue: IAnimalTrait[] = [];
  optionsForTrait: ITraitOption[] = [];
  options_arr: ITraitOption[] = [];
  gender_arr: string[] = ['לא ידוע', 'זכר', 'נקבה'];
  
  constructor(private _formBuilder: FormBuilder,
              private _animalTypeService: AnimalTypeService, 
              private _cityService: CityService, 
              private _traitsService: AnimalTraitsService,
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
          description:['', [Validators.required,
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
    let animalTypeId = event.value;
    console.log("animaltype changed to: " + animalTypeId);
    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTraitFilter(1, 100, date, animalTypeId);
    this._traitsService.Post(filter).subscribe(traits => {
      this.traits_arr = traits;
      this.getOptionsForTrait();
    })
  }

  private getOptionsForTrait() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new TraitOptionFilter(1,100,date);
    
    for(const trait of this.traits_arr) {
      
      filter.traitId = trait.traitId;
      console.log("iteration for trait id " + trait.traitId + "and name: " + trait.traitName);
      this._traitOptionsService.Post(filter).subscribe(options => {
        this.options_arr = options;

        for (const option of this.options_arr) {
            console.log("trait name: " + trait.traitName + " and the option is: " + option.option);
            if (this.isBooleanValue(option)) {
              this.optionBooleanVal = true;
              this.traitsWithBooleanValue.push(trait);
              break;
            } else { //trait has several option values
              //create selection: traitname and all of its options (in options_arr right now) needed.
              this.optionsForTrait.push(option);
            }

        }
          
          if (this.optionBooleanVal) {  
            //create chip: traitName needed.
            console.log("trait: " + trait.traitName + " has a Yes/No value!");


          } else {
            //create selection: traitname and all of its options (in options_arr right now) needed.
            console.log("trait: " + trait.traitName + " has several values!");
            

          }
          this.optionBooleanVal = false;
      })
    }
  }

  private isBooleanValue(optionElem) : boolean {
    return (optionElem.option == "כן" || optionElem.option == 'לא')
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

}