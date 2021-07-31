import { AddpetComponent } from './modules/pets/components/addpet/addpet.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './modules/home/components/index/index.component';
import { LoginComponent } from './modules/home/components/login/login.component';
import { RegisterComponent } from './modules/home/components/register/register.component';
import { SearchPetsComponent } from './modules/pets/components/search-pets/search-pets.component';
import { AuthGuard } from './shared/guards/auth-guard';
import { AboutUsComponent } from './modules/home/components/about-us/about-us.component';
import { DonationsComponent } from './modules/home/components/donations/donations.component';
import { PetViewComponent } from './modules/pets/components/pet-view/pet-view.component';
import { ArticleViewComponent } from './modules/articles/components/article-view/article-view.component';
import { ArticlesComponent } from './modules/articles/components/articles/articles.component';


const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'index', component: IndexComponent },
  { path: 'account/login', component: LoginComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'pets/add', component: AddpetComponent, canActivate: [AuthGuard] },
  { path: 'pets/search', component: SearchPetsComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'donations', component: DonationsComponent },
  { path: 'articles', component: ArticlesComponent },
  { path: 'pet/view', component: PetViewComponent },
  { path: 'article/view', component: ArticleViewComponent },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
