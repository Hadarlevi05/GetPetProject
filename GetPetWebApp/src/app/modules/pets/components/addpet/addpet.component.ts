import { Component, OnInit, ViewChildren, AfterViewInit, QueryList} from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ControlContainer, ControlValueAccessor } from '@angular/forms';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { IPet } from 'src/app/modules/pets/models/ipet';
import { ITraitOption } from 'src/app/shared/models/iTraitOption';
import { TraitOptionFilter } from 'src/app/shared/models/traitOptionFilter';
import { TraitOptionsService } from 'src/app/shared/services/traitOptions.service';
import { compileNgModule } from '@angular/compiler';
import { compileNgModule } from '@angular/compiler';
import { ICity } from 'src/app/shared/models/icity';
import { AnimalTypeService } from 'src/app/shared/services/animal-type.service';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';
import { TraitsService } from 'src/app/shared/services/traits.service';
import { ITrait } from 'src/app/shared/models/itrait';
import { ITraitSelection } from 'src/app/shared/models/itrait-selection';
import { FileUploaderComponent } from '../file-uploader/file-uploader.component';
import { TraitFilter } from 'src/app/shared/models/trait-filter';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})

export class AddpetComponent
  implements OnInit {

  @ViewChildren('fileuploader') components!: QueryList<FileUploaderComponent>

  loading = false;
  success = false;
  optionBooleanVal = false;
  isMatChipsLoaded = false;
  addPetFormGroup!: FormGroup;
  //petIdServer: number = 0; //to do- import its value from upload.service.ts
  imgPath: string = '';
  ngAfterViewInit() {
  
  }

  
  
  pet: IPet = {
    name: '',
    animalTypeId: 0,
    userId: 1,
    birthday: "",
    traits: new Map(),
    description: '',
    images: [],
    creationTimestamp: new Date()


  
  animaltypes_arr: IAnimalType[] = [];
  //city_arr: ICity[] = [];
  traits_arr: ITrait[] = [];
  optionsForTrait: ITraitOption[] = [];
  traitsWithBooleanValue: ITrait[] = [];
  traitsWithSetOfValues: ITrait[] = [];
  traitSelections: ITraitSelection[] = [];
  traitChipSelections: ITraitSelection[] = [];
  allSelectedTraits: ITraitSelection[] = [];
  minDate!: Date;
  maxDate!: Date;
  
  
              private _animalTypeService: AnimalTypeService, 
              private _traitsService: TraitsService,
              private _petsService: PetsService) { }
              private _petsService: PetsService) { }

  ngOnInit(): void {

    this.loadAnimalTypes();
    this.setAllowedDatePickerRange();

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
          traits: this._formBuilder.array([]),
          description:['', [Validators.required,
                            Validators.maxLength(500)]],
                            Validators.maxLength(500)]],
        }),
        this._formBuilder.group({
          //upload pictures
        }),
        this._formBuilder.group({
          //send button
        }),
      ])
    });
  }

  get formArray(): AbstractControl | null {
    return this.addPetFormGroup.get('formArray');
  }

  get traits() : FormArray {
    return this.addPetFormGroup.get('traits') as FormArray;
  }

  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTypeFilter(1, 100, date);
    this._animalTypeService.Get(filter).subscribe(types => {
      this.animaltypes_arr = types;
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

    for (const trait of this.traits_arr) {
      this.optionsForTrait = trait.traitOptions;
      for (const option of this.optionsForTrait) {
            this.traitsWithBooleanValue.push(trait);
            this.isMatChipsLoaded = true;
            break;
          } else {
            this.traitsWithSetOfValues.push(trait);
            break;
          }
          }
      }
    }

    console.log("traits with boolean value:");
    console.log(this.traitsWithBooleanValue);
    console.log("trait with set of value:");
    console.log(this.traitsWithSetOfValues);
  }

  private isBooleanValue(op: ITraitOption): boolean {
    return (op.option == 'כן' || op.option == 'לא')
  }
  private deleteTraitsArrays() {
    this.traits_arr = [];
    this.traitsWithBooleanValue = [];
    this.traitsWithSetOfValues = [];
    this.isMatChipsLoaded = false;
  }

  onTraitSelection(traitSelection: ITraitSelection) {
    console.log('traitSelections', this.traitSelections);
    const item = this.traitSelections.find(i => i.traitId === traitSelection.traitId);
    if (item) {
      item.traitOptionId = traitSelection.traitOptionId;
      item.description = traitSelection.description;
    } else {
      this.traitSelections.push(traitSelection);
    }
  }
  //           console.log("trait: " + trait.traitName + " has a Yes/No value!");

  eventHandler(event:ITraitSelection[]) {
    this.traitChipSelections = event;
  }

  //Date picker allows users to select date of birth range from
  //20 years ago until today.
  setAllowedDatePickerRange() {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 20, 0, 1);
    this.maxDate = new Date();
    }

  onSubmit(postData) {

  // id?: number;
  // birthday?: string;
  // gender?: string;
  // animalType?: string;
  // status?: string;
  // userId: number;
  // user?: IUser;
  // images: string[];

    //console.log('data from selections:', this.traitSelections)
    //console.log('data from child:',this.traitChipSelections);

    this.allSelectedTraits = this.traitSelections.concat(this.traitChipSelections);
    console.log("allSelectedTraits: ", this.allSelectedTraits);

    let traitsMap = this.allSelectedTraits.reduce((mapAccumulator, obj) => {
      mapAccumulator.set(obj.traitId, obj.traitOptionId);
      return mapAccumulator;
    }, new Map());
    console.log(traitsMap);
    
    //upload pictures to db
    this.components.forEach(uploader => {
      console.log("%%%%%%",uploader);
      console.log("555uploader.file.data is: " + uploader.file.data);
      if (uploader.file.data) {
        uploader.sendFile(uploader.file).subscribe((pathResponse) => {  
          console.log("SUBSCRIBE:response from server:" + pathResponse);
          if (pathResponse) {
            console.log("pathhhh - " + pathResponse);
            this.pet.images.push(pathResponse);
          }
        });
        //console.log("sending file :" + uploader.fileName + " to server.");
        //console.log("2.IN ADDPET FORM - image path: " + this.imgPath);
        //add path to images array
      }
    })

    this.pet.name = this.formArray?.get([1])?.get('petName')?.value;
    this.pet.description = this.formArray?.get([1])?.get('description')?.value;
    this.pet.animalTypeId = this.formArray?.get([0])?.get('animalType')?.value;
    this.pet.traits = traitsMap;

    console.log("PET INFO: ", this.pet);
    
    try {
      this._petsService.addPet(this.pet);
      this.success = true;
    } catch (err) {
      console.log(err);
    }
    this.loading = false;


  }
}