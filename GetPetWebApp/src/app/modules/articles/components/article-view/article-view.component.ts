import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IArticle } from '../../models/iarticle';

@Component({
  selector: 'app-article-view',
  templateUrl: './article-view.component.html',
  styleUrls: ['./article-view.component.sass']
})
export class ArticleViewComponent implements OnInit {

  @Input()
  article: IArticle = {} as IArticle;


  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.article = data.article;
  }

  ngOnInit(): void {
  }

  getAvatarImage(num: number) {
    return `url(https://localhost:44345/images/avatars/${num % 50}.png)`;
  }

}
