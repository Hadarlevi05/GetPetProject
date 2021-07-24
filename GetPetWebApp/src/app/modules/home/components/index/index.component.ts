import { Component, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { IArticle } from 'src/app/modules/articles/models/iarticle';
import { ArticleService } from 'src/app/modules/articles/services/article.service';
import { IPet } from 'src/app/modules/pets/models/ipet';
import { PetFilter } from 'src/app/modules/pets/models/pet-filter';
import { PetsService } from 'src/app/modules/pets/services/pets.service';
import { BaseFilter } from 'src/app/shared/models/base-filter';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.sass']
})
export class IndexComponent implements OnInit {

  petLoading = true;
  articleLoading = true;

  pets: IPet[] = [];
  articles: IArticle[] = [];

  gridColumns = 3;

  animalTypeId = 1;


  toggleGridColumns() {
    this.gridColumns = this.gridColumns === 3 ? 4 : 3;
  }


  constructor(
    private petsService: PetsService,
    private articleService: ArticleService) { }

  ngOnInit(): void {
    this.loadPets();
    this.loadArticles();
  }

  tabChanged(tabChangeEvent: MatTabChangeEvent): void {
    this.animalTypeId = tabChangeEvent.index + 1;

    this.pets = [];
    this.loadPets();
  }

  loadArticles() {

    this.articleLoading = true;

    let date = new Date();
    date.setDate(date.getDate() - 14);

    let filter = new BaseFilter(1, 10, date);

    this.articleService.search(filter).subscribe(articles => {
      this.articles = articles;

      this.articleLoading = false;
    });
  }

  loadPets() {

    this.petLoading = false;

    let date = new Date();
    date.setDate(date.getDate() - 14);

    let filter = new PetFilter(1, 100, date, [this.animalTypeId]);

    this.petsService.search(filter).subscribe(pets => {
      this.pets = pets;

      this.petLoading = false;
    });
  }
}
