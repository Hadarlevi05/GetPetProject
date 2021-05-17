import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from '../../models/iarticle';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.sass']
})
export class ArticleCardComponent implements OnInit {

  @Input()
  article: IArticle = {} as IArticle;


  constructor() { }

  ngOnInit(): void {
  }

}
