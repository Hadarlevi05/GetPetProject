import { AddpetComponent } from './modules/pets/components/addpet/addpet.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './modules/home/components/index/index.component';
import { LoginComponent } from './modules/home/components/login/login.component';
import { RegisterComponent } from './modules/home/components/register/register.component';
import { SearchPetsComponent } from './modules/pets/components/search-pets/search-pets.component';
import { AuthGuard } from './shared/guards/auth-guard';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'index', component: IndexComponent },
  { path: 'account/login', component: LoginComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'pets/add', component: AddpetComponent, canActivate: [AuthGuard] },
  { path: 'pets/search', component: SearchPetsComponent }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
