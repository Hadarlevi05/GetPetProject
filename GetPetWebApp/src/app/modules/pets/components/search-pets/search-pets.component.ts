import { OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AnimalTypeFilter } from 'src/app/shared/models/animal-type-filter';
import { IAnimalType } from 'src/app/shared/models/ianimal-type';
import { ITrait } from 'src/app/shared/models/itrait';
import { TraitFilter } from 'src/app/shared/models/trait-filter';
import { AnimalTypeService } from 'src/app/shared/services/animal-type.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { TraitsService } from 'src/app/shared/services/traits.service';
import { IPet } from '../../models/ipet';
import { PetFilter } from '../../models/pet-filter';
import { PetsService } from '../../services/pets.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PetViewComponent } from '../pet-view/pet-view.component';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { PetStatus } from 'src/app/shared/enums/pet-status';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-search-pets',
  templateUrl: './search-pets.component.html',
  styleUrls: ['./search-pets.component.sass']
})
export class SearchPetsComponent implements OnInit, OnDestroy {

  showFiller = true;

  loading = true;

  animalTypeId = 1;
  animaltypes: IAnimalType[] = [];
  traits: ITrait[] = [];

  booleanTraits: ITrait[] = [];
  nonBooleanTraits: ITrait[] = [];

  pets: IPet[] = [];

  gridColumns = 3;

  searchTraitValues: {} = {};
  searchBooleanTraits: number[] = [];

  clearSubject: Subject<void> = new Subject<void>();


  get hasParameters(): boolean {
    if (Object.keys(this.searchTraitValues).length > 0)
      return true;

    if (this.searchBooleanTraits.length > 0)
      return true;

    return false;
  }

  clearParameters() {

    this.searchTraitValues = {};
    this.searchBooleanTraits = [];

    this.clearSubject.next();

    this.search();
  }

  form: FormGroup = new FormGroup({
    animalType: new FormControl('')
  });

  constructor(
    private traitsService: TraitsService,
    private animalTypeService: AnimalTypeService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private petsService: PetsService,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  openPetDialog(pet: IPet) {
    this.dialog.open(PetViewComponent, {
      data: { pet }
    });
  }

  ngOnDestroy(): void {

  }

  ngOnInit(): void {

    this.setFormSubscribers();
    this.getQueryString();
    this.loadAnimalTypes();
    this.loadPets();
  }

  setFormSubscribers() {
    this.form.controls['animalType'].valueChanges.subscribe(value => {

      const urlTree = this.router.parseUrl(this.router.url);
      urlTree.queryParams['animalType'] = value;
      this.router.navigateByUrl(urlTree);
    });
  }

  getQueryString() {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params['animalType']) {
        this.animalTypeId = +params['animalType'];
      } else {
        this.animalTypeId = 1;
      }

      if (this.form.controls['animalType'].value !== this.animalTypeId) {
        this.form.controls['animalType'].setValue(this.animalTypeId);
      }

      this.loadTraits(this.animalTypeId);
    });
  }


  loadAnimalTypes() {

    let date = new Date();
    date.setDate(date.getDate() - 20);

    const filter = new AnimalTypeFilter(1, 100, date);

    this.animalTypeService.get(filter).subscribe(animaltypes => {
      this.animaltypes = animaltypes;
    });
  }

  loadTraits(animalTypeId: number) {

    let date = new Date();
    date.setDate(date.getDate() - 20);
    const filter = new TraitFilter(1, 100, date, animalTypeId);

    this.traitsService.post(filter).subscribe(traits => {
      this.booleanTraits = traits.filter(t => t.isBoolean);
      this.nonBooleanTraits = traits.filter(t => !t.isBoolean);
    });
  }

  loadPets() {

    this.loading = true;

    let date = new Date();
    date.setDate(date.getDate() - 14);

    let filter = new PetFilter(1, 100, date, [this.animalTypeId]);

    this.petsService.search(filter).subscribe(pets => {
      this.pets = pets;
      this.loading = false;
    });
  }

  onOptionsChanged(values: { traitId: number, options: number[] | boolean }) {

    if (typeof values.options === 'boolean') {
      if (values.options) {
        this.searchBooleanTraits.push(values.traitId);
      } else {
        this.searchBooleanTraits.splice(this.searchBooleanTraits.indexOf(values.traitId), 1);
      }
    } else {
      this.searchTraitValues[values.traitId] = values.options;
    }

  }

  search() {

    this.loading = true;

    const date = new Date();
    date.setDate(date.getDate() - 14);

    const filter = new PetFilter(1, 100, date, [this.animalTypeId], this.searchTraitValues, this.searchBooleanTraits);

    this.petsService.search(filter).subscribe(pets => {
      this.pets = pets;
      this.loading = false;
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, { duration: 3_000 });
  }

  addSearchNotification() {
    const user = this.authenticationService.currentUserValue;

    if (!user.id) {
      this.openSnackBar('אתה חייב להרשם לבצע פעולה זו', 'סגור');
      return;
    }

    const date = new Date();
    date.setDate(date.getDate() - 14);

    const filter = new PetFilter(1, 100, date, [this.animalTypeId], this.searchTraitValues, this.searchBooleanTraits, PetStatus.WaitingForAdoption);
    this.notificationService.upsert(filter).subscribe(result => {
      this.openSnackBar('התראה נוספה בהצלחה!', 'סגור');
    });
  }
}