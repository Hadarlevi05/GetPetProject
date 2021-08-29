import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { IArticle } from '../../models/iarticle';
import { IComment } from '../../models/icomment';
import { ArticleService } from '../../services/article.service';
import { environment } from '../../../../../environments/environment';

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

  imageUrl = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private articleService: ArticleService,
    private snackBar: MatSnackBar,
    private authenticationService: AuthenticationService) {
    this.article = data.article;
  }

  ngOnInit(): void {
    this.imageUrl = environment.baseImageUrl;
  }

  getAvatarImage(num: number) {
    return `url(${environment.baseImageUrl}avatars/${num % 50}.png)`;
  }

  addComment() {

    const user = this.authenticationService.currentUserValue;
    if (!user.id) {
      this.openSnackBar('אתה חייב להרשם לבצע פעולה זו', 'סגור');
      return;
    }

    const comment = {
      text: this.text
    } as IComment;

    this.text = '';

    this.articleService.addComment(this.article.id, comment).subscribe(comments => {

      this.article.comments = comments as IComment[];
    })
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, { duration: 3_000 });
  }

}
