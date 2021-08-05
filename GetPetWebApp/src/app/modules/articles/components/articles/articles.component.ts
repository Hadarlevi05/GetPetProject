import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseFilter } from 'src/app/shared/models/base-filter';
import { IArticle } from '../../models/iarticle';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.sass']
})
export class ArticlesComponent implements OnInit {

  articleLoading = true;
  articles: IArticle[] = [];
  gridColumns = 3;

  constructor(
    private articleService: ArticleService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadArticles();
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
}