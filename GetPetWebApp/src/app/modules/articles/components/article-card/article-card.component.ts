import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from '../../models/iarticle';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.sass']
})
export class ArticleCardComponent implements OnInit {

  @Input()
  article: IArticle = {} as IArticle;

  imageUrl = '';

  constructor() { }

  ngOnInit(): void {
    this.imageUrl = environment.baseImageUrl;
  }

}
