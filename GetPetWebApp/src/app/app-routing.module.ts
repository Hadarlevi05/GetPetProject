import { AddpetComponent } from './modules/pets/components/addpet/addpet.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './modules/home/components/index/index.component';
import { LoginComponent } from './modules/home/components/login/login.component';
import { RegisterComponent } from './modules/home/components/register/register.component';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'index', component: IndexComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'addpet', component: AddpetComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
