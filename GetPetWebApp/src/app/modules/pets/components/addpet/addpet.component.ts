import { Component, OnInit, ViewChild, ViewChildren, AfterViewInit, QueryList } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ControlContainer, ControlValueAccessor } from '@angular/forms';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { ITraitOption } from 'src/app/shared/models/itrait-option';
import { AnimalTypeService } from 'src/app/shared/services/animal-type.service';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';
import { TraitsService } from 'src/app/shared/services/traits.service';
import { ITrait } from 'src/app/shared/models/itrait';
import { TraitFilter } from 'src/app/shared/models/trait-filter';
import { ITraitSelection } from 'src/app/shared/models/itrait-selection';
import { FileUploaderComponent } from '../file-uploader/file-uploader.component';
import { DatePipe } from '@angular/common';
import { MultiSelectChipsComponent } from '../multi-select-chips/multi-select-chips.component';
import { UploadService } from '../../services/upload.service';
import { Pet } from '../../models/pet';
import { PetSource } from 'src/app/shared/enums/pet-source';
import { Gender } from 'src/app/shared/enums/gender';
import { PetStatus } from 'src/app/shared/enums/pet-status';

@Component({
  selector: 'app-addpet',
  templateUrl: './addpet.component.html',
  styleUrls: ['./addpet.component.sass']
})

export class AddpetComponent
  implements OnInit {

  @ViewChildren('fileuploader') components!: QueryList<FileUploaderComponent>
  @ViewChild('chips') multiSelectChipsChild!: MultiSelectChipsComponent;

  loading = false;
  success = false;
  formUploaded = false;
  optionBooleanVal = false;
  isMatChipsLoaded = false;
  addPetFormGroup!: FormGroup;
  formDataFile: FormData = {} as FormData;
  filesToUpload: FormData[] = [];
  imagesURLs: string[] = [];
  
  ngAfterViewInit() {
  }


  pet: Pet = {
    name: '',
    description: '',
    birthday: new Date(),
    gender: Gender.Unknown,
    animalTypeId: 0,
    status: PetStatus.WaitingForAdoption,
    userId: 0,
    traits: {},
    source: PetSource.Internal,
    images: [],
    creationTimestamp: new Date(),
  }


  animaltypes_arr: IAnimalType[] = [];
  traits_arr: ITrait[] = [];
  optionsForTrait: ITraitOption[] = [];
  traitsWithBooleanValue: ITrait[] = [];
  traitsWithSetOfValues: ITrait[] = [];
  gender_arr: string[] = ['לא ידוע', 'זכר', 'נקבה'];
  traitSelections: ITraitSelection[] = [];
  traitChipSelections: ITraitSelection[] = [];  //delete this
  allSelectedTraits: ITraitSelection[] = [];
  minDate!: Date;
  maxDate!: Date;


  constructor(private _formBuilder: FormBuilder,
    private _animalTypeService: AnimalTypeService,
    private _traitsService: TraitsService,
    private _petsService: PetsService,
    private _uploadService: UploadService) { }

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
          chipsControl: new FormControl(['']),
          traits: this._formBuilder.array([]),
          description: ['', [Validators.required,
          Validators.maxLength(1000)]],
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

  get traits(): FormArray {
    return this.addPetFormGroup.get('traits') as FormArray;
  }

  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    let filter = new AnimalTypeFilter(1, 100, date);
    this._animalTypeService.get(filter).subscribe(types => {
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
    this._traitsService.post(filter).subscribe(traits => {
      this.traits_arr = traits;
      this.classifyTraits();
    })
  }

  private classifyTraits() {

    console.log("traits_arr: ",this.traits_arr);

    for (const trait of this.traits_arr) {
      if (trait.isBoolean) {
        this.traitsWithBooleanValue.push(trait);
      } else {
        this.traitsWithSetOfValues.push(trait)
      }
    }
    this.isMatChipsLoaded = true;

    console.log("traits with boolean value:", this.traitsWithBooleanValue);
    console.log("trait with set of values:", this.traitsWithSetOfValues);
  }

  private deleteTraitsArrays() {
    
    this.multiSelectChipsChild.setAllMatchipsFalse();
    this.traits_arr = [];
    this.traitsWithBooleanValue = [];
    this.traitsWithSetOfValues = [];
    this.traitSelections = [];
    this.traitChipSelections = [];
    this.allSelectedTraits = [] as ITraitSelection[];
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

  eventHandler(event: ITraitSelection[]) {
    this.traitChipSelections = event;
  }

  //Date picker allows users to select date of birth in range of 15 years
  setAllowedDatePickerRange() {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 15, 0, 1);
    this.maxDate = new Date();
  }

  getCurrentUserId(): number {
    var userString = localStorage.getItem('currentUser');
    var user = JSON.parse(userString?? '');
    return user.id;
  }

  AddPet() {

    this.pet.name = this.formArray?.get([1])?.get('petName')?.value;
    this.pet.description = this.formArray?.get([1])?.get('description')?.value;
    this.pet.birthday = this.formArray?.get([1])?.get('dob')?.value;
    this.pet.gender = this.formArray?.get([1])?.get('gender')?.value;
    this.pet.animalTypeId = this.formArray?.get([0])?.get('animalType')?.value;
    this.pet.userId = this.getCurrentUserId();
    this.pet.images = this.imagesURLs;
    this.allSelectedTraits = this.traitSelections.concat(this.multiSelectChipsChild.traitChipSelections);
    console.log("allSelectedTraits: ", this.allSelectedTraits);
    this.pet.traits = this.allSelectedTraits.reduce((a,x) => ({...a, [x.traitId]: x.traitOptionId}), {})     //convert array to dictionary
    
    console.log("PET TO SEND INFO: ", this.pet);

    try {
      this._petsService.addPet(this.pet);
      this.formUploaded = true;
      this.success = true;
    } catch (err) {
      this.success = false;
      console.log("Error, can't add pet!, success changed to false", err);
    }
    this.loading = false;
  }


  async onSubmit(postData) {

    //collect all files to upload (of FormDate type)
    this.components.forEach(uploader => {
      console.log("uploader.file.data is: ", uploader.file.data);
      if (uploader.file.data) {  //check if there is a file in currentuploader
        this.formDataFile = new FormData();
        this.formDataFile.set('formFile', uploader.file.data); //prepare FormData object from file
        this.filesToUpload.push(this.formDataFile);
      }
    })

    console.log("array of formdata files ready to upload:");
    this.filesToUpload.forEach(f => {
      console.log(f.get('formFile'));
    })

    console.log(this.filesToUpload.length);

    //upload pictures to db
    this._uploadService.uploadData(this.filesToUpload)
    .subscribe(res => {
      console.log(res);
      for (const element of res) {
        this.imagesURLs.push(element['path']);
    }
    console.log("THE IMAGES URLS:",this.imagesURLs);

    this.AddPet();
    }, err => {
      console.log("pet upload failed!",err);
      this.formUploaded = true;
      this.success = false;
    });
  }
}