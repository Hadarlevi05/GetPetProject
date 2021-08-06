import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IArticle } from '../../models/iarticle';
import { IComment } from '../../models/icomment';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-article-view',
  templateUrl: './article-view.component.html',
  styleUrls: ['./article-view.component.sass']
})
export class ArticleViewComponent implements OnInit {

  loading = false;
  text = '';

  @Input()
  article: IArticle = {} as IArticle;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private articleService: ArticleService) {
    this.article = data.article;
  }

  ngOnInit(): void {
  }

  getAvatarImage(num: number) {
    return `url(https://localhost:44345/images/avatars/${num % 50}.png)`;
  }

  addComment() {

    const comment = {
      text: this.text
    } as IComment;

    this.text = '';

    this.articleService.addComment(this.article.id, comment).subscribe(comments => {

      this.article.comments = comments as IComment[];
    })
  }
}
