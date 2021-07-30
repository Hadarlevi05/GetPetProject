import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { HttpEventType, HttpErrorResponse, JsonpClientBackend, HttpResponse } from '@angular/common/http';
import { of } from 'rxjs';  
import { catchError, map } from 'rxjs/operators';  
import { UploadService } from '../../services/upload.service';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.sass']
})
export class FileUploaderComponent implements OnInit {

  @ViewChild("fileUpload", {static: false}) fileUpload!: ElementRef;
  
  file: any;
  formData: FormData = {} as FormData;
  
  constructor(private uploadService: UploadService) {}

  fileName: string = '';
  imgPath: string = '';

  uploadFile(file) {    //delete this method
    console.log("in uploadFile:", file);
    this.file = file;
    this.fileName = file.data.name;
    this.formData = new FormData();  
    this.formData.set('formFile', file.data);  
    this.file.inProgress = true;  
    // this.uploadService.upload(formData).pipe(       //todo: move the upload itseld to the send button.
    //   map(event => {  
    //     switch (event.type) {  
    //       case HttpEventType.UploadProgress:  
    //         if (event.total) {
    //           const total:number = event.total;
    //           file.progress = Math.round(event.loaded * 100 / total);  
    //         }
    //         else {
    //           //todo: throw error cannot read file
    //         }
    //         break;
    //       case HttpEventType.Response:
    //         console.log("RESPONSE FROM SERVER:", event);
    //         break;
    //       return event;
    //     }  
    //   }),  
    //   catchError((error: HttpErrorResponse) => {  
    //     file.inProgress = false;  
    //     return of(`${file.data.name} upload failed.`);  
    //   })).subscribe((event: any) => {  
    //     if (typeof (event) === 'object') {  
    //       console.log(event.body);  
    //     }  
    //   });  
  }

  // sendFile(file) {
  //   return this.uploadService.uploadData(this.formData).pipe(
  //     map(event => {  
  //       // switch (event.type) {  
  //       //   case HttpEventType.UploadProgress:  
  //       //     if (event.total) {
  //       //       const total:number = event.total;
  //       //       file.progress = Math.round(event.loaded * 100 / total);  
  //       //     }
  //       //     else {
  //       //       //todo: throw error cannot read file
  //       //     }
  //       //     break;
  //         if (event.type == HttpEventType.Response) {
  //           console.log("RESPONSE FROM SERVER:", event);
  //           this.imgPath = JSON.parse(JSON.stringify(event.body)).path;
  //           console.log("1.IMAGE_PATH_RETURNED_FROM_SERVER: " + this.imgPath);
  //           return this.imgPath;
  //         } else {
  //           return null;
  //         }
  //       } 
  //     ),  
  //     catchError((error: HttpErrorResponse) => {  
  //       file.inProgress = false;  
  //       return of(`${file.data.name} upload failed.`);  
  //     }));  
  // }

//   private uploadFiles() {  
//     this.fileUpload.nativeElement.value = '';  
//     this.files.forEach(file => {  
//       this.uploadFile(file);  
//     });  
// }

onClick() {  
  const fileUpload = this.fileUpload.nativeElement;
  fileUpload.onchange = () => {  
   console.log("fileUpload ref: ", fileUpload);
   const file = fileUpload.files[0];
   console.log("file is: ", file);
   this.file = { data: file, inProgress: false, progress: 0};
   //this.file = fileObj;
   //this.files.push({ data: file, inProgress: false, progress: 0});  
   this.fileUpload.nativeElement.value = '';
   this.uploadFile(this.file);  
  };  
  fileUpload.click();  
}

  ngOnInit(): void {
    this.file = { data: null, inProgress: false, progress: 0};
  }

}
