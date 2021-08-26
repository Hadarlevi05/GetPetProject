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
  responses: Observable<any>[] = new Array();

	constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  //todo - send http calls as formData array length.
  public uploadData(formData: FormData[]): Observable<any> {

    console.log("upload URL: " + `${this.entPointUrl}`);

    var numOfFiles = formData.length;
    console.log("in upload service - number Of Files:" + numOfFiles);

    for (var i = 0; i < numOfFiles; i++) {
      this.responses[i] = this.httpClient.post(`${this.entPointUrl}`, formData[i]);
    }

    return forkJoin(this.responses);
  }

  public AttachFilesToPet(imagesIds: number[], petDbId: number) {

      //??
  }
}
