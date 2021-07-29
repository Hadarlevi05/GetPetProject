import { Component, OnInit, ViewChild, ViewChildren, AfterViewInit, QueryList } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators, ControlContainer, ControlValueAccessor } from '@angular/forms';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { ITraitOption } from 'src/app/shared/models/itraitoption';
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
import { Pet } from '../../models/pet';
import { UploadService } from '../../services/upload.service';

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
  optionBooleanVal = false;
  isMatChipsLoaded = false;
  addPetFormGroup!: FormGroup;
  res0: string = '';
  res1: string = '';
  res2: string = '';
  formDataFile: FormData = {} as FormData;
  filesToUpload: FormData[] = [];
  imagesURLs: string[] = [];
  

  ngAfterViewInit() {

  }


  pet: Pet = {
    name: '',
    animalTypeId: 0,
    userId: 1,
    birthday: new Date(),
    traits: {},
    description: '',
    images: [],
    creationTimestamp: new Date(),
    petSource: 1,
    sourceLink: '',
    gender: 1
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
    public datepipe: DatePipe,
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

  get traits(): FormArray {
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

    console.log("traits_arr: ",this.traits_arr);

    for (const trait of this.traits_arr) {
      this.optionsForTrait = trait.traitOptions;
      for (const option of this.optionsForTrait) {
        if (this.isBooleanValue(option)) {
          this.traitsWithBooleanValue.push(trait);
          this.isMatChipsLoaded = true;
          break;
        } else {
          this.traitsWithSetOfValues.push(trait);
          break;
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

  //Date picker allows users to select date of birth range from
  //20 years ago until today.
  setAllowedDatePickerRange() {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 20, 0, 1);
    this.maxDate = new Date();
  }

  getCurrentUserId(): number {
    var userString = localStorage.getItem('currentUser');
    var user = JSON.parse(userString?? '');
    return user.id;
  }

  collectDataAndAddPet() {

    this.pet.name = this.formArray?.get([1])?.get('petName')?.value;
    this.pet.description = this.formArray?.get([1])?.get('description')?.value;
    this.pet.birthday = this.formArray?.get([1])?.get('dob')?.value;
    this.pet.gender = this.formArray?.get([1])?.get('gender')?.value;
    this.pet.animalTypeId = this.formArray?.get([0])?.get('animalType')?.value;
    this.pet.userId = this.getCurrentUserId();
    this.pet.images = this.imagesURLs;
    this.allSelectedTraits = this.traitSelections.concat(this.multiSelectChipsChild.traitChipSelections);
    console.log("allSelectedTraits: ", this.allSelectedTraits);
    let traitsDict = this.allSelectedTraits.reduce((a,x) => ({...a, [x.traitId]: x.traitOptionId}), {})     //convert array to dictionary
    this.pet.traits = traitsDict;
    this.pet.petSource = 1; //Internal

    console.log("PET TO SEND INFO: ", this.pet);

    try {
      this._petsService.addPet(this.pet);
      this.success = true;
    } catch (err) {
      console.log("Error, can't add pet!", err);
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

    this.collectDataAndAddPet();
    }, err => {
      console.log(err);
    });




    //console.log('data from selections:', this.traitSelections)
    //console.log('data from child:',this.traitChipSelections);


    // let traitsMap = this.allSelectedTraits.reduce((mapAccumulator, obj) => {
    //   mapAccumulator.set(obj.traitId, obj.traitOptionId);
    //   return mapAccumulator;
    // }, new Map());
    // console.log("All traits: ",traitsMap);

    // const allTraitsDict = {};
    // this.allSelectedTraits.forEach(([traitId, traitOptionId]) => allTraitsDict[traitId] = traitOptionId);
    // const allTraitsDict = this.allSelectedTraits.reduce((dict, [traitId, traitOptionId]) => Object.assign(dict, {[traitId]: traitOptionId}), {});


    // //upload pictures to db (old)
    // this.components.forEach(uploader => {
    //   //console.log("%%%%%%",uploader);
    //   console.log("uploader.file.data is: ", uploader.file.data);
    //   if (uploader.file.data) {
    //     uploader.sendFile(uploader.file)
    //     .subscribe((pathResponse) => {  
    //       //console.log("SUBSCRIBE:response from server:" + pathResponse);
    //       if (pathResponse) {
    //         console.log("img path - " + pathResponse);
    //         this.pet.images.push(pathResponse);
    //       }
    //     });
    //   }
    // })



  }
}