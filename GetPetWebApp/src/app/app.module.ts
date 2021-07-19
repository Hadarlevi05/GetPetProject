import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './modules/home/components/index/index.component';
import { LoginComponent } from './modules/home/components/login/login.component';
import { RegisterComponent } from './modules/home/components/register/register.component';
import { AddpetComponent } from './modules/pets/components/addpet/addpet.component';
import { PetCardComponent } from './modules/pets/components/pet-card/pet-card.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTabsModule } from '@angular/material/tabs';
import { LoaderComponent } from './shared/components/loader/loader.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { AngularFileUploaderModule } from "angular-file-uploader";
import { MatStepperModule } from '@angular/material/stepper';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MultiSelectChipsComponent } from './modules/pets/components/multi-select-chips/multi-select-chips.component';
import { MatDividerModule } from '@angular/material/divider';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ArticleCardComponent } from './modules/articles/components/article-card/article-card.component';
import { SearchPetsComponent } from './modules/pets/components/search-pets/search-pets.component';
import { SearchCriteriaComponent } from './modules/pets/components/search-criteria/search-criteria.component';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    LoginComponent,
    RegisterComponent,
    PetCardComponent,
    LoaderComponent,
    AddpetComponent,
    MultiSelectChipsComponent,
    ArticleCardComponent,
    SearchPetsComponent,
    SearchCriteriaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTabsModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatListModule,
    MatAutocompleteModule,
    AngularFileUploaderModule,
    MatStepperModule,
    MatCheckboxModule,
    MatChipsModule,
    MatIconModule,
    MatToolbarModule,
    MatDividerModule,
    MatCheckboxModule,
    FlexLayoutModule,
    MatSidenavModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
