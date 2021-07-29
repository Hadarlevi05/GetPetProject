import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from  '@angular/common/http';  
import { map } from  'rxjs/operators';
import { BehaviorSubject, forkJoin, Observable } from 'rxjs';
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

  //todo - send http calls as formData array length.
  public uploadData(formData: FormData[]): Observable<any> {

    console.log("upload URL: " + `${this.entPointUrl}`);
    // return this.httpClient.post(`${this.entPointUrl}`, formData, {  
    //     reportProgress: true,  
    //     observe: 'response'  
    //   });  
    const response0 = this.httpClient.post(`${this.entPointUrl}`, formData[0], {  reportProgress: true});
    const response1 = this.httpClient.post(`${this.entPointUrl}`, formData[1], {  reportProgress: true});
    const response2 = this.httpClient.post(`${this.entPointUrl}`, formData[2], {  reportProgress: true});
    
    return forkJoin([response0, response1, response2]);
  }
}
