import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from  '@angular/common/http';  
import { map } from  'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class UploadService extends BaseService{

  entPointUrl = `${this.BASE_URL}metafilelinks`;
  imgPath:string = '';

	constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  public upload(formData) {

    return this.httpClient.post(`${this.entPointUrl}`, formData, {  
        reportProgress: true,  
        observe: 'response'  
      });  
  }
}
