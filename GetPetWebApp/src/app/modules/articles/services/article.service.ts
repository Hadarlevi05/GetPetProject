import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';
import { IArticle } from '../models/iarticle';
import { BaseFilter } from 'src/app/shared/models/base-filter';


@Injectable({
  providedIn: 'root'
})
export class ArticleService extends BaseService {

  entPointUrl = `${this.BASE_URL}articles`;

  constructor(http: HttpClient) {
    super(http);
  }

  search(filter: BaseFilter): Observable<IArticle[]> {
    let params: HttpParams = this.GetHttpParams(filter);

    return this.http.get<IArticle[]>(`${this.entPointUrl}`, { params });
  }
}
