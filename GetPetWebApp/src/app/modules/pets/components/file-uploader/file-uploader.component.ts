import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { of } from 'rxjs';  
import { catchError, map } from 'rxjs/operators';  
import { UploadService } from '../../services/upload.service';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.sass']
})
export class FileUploaderComponent implements OnInit {

  @ViewChild("fileUpload", {static: false}) 
    fileUpload!: ElementRef;
    files: any[]  = [];  
  
  constructor(private uploadService: UploadService) { }

  fileName: string = '';
  clicked: boolean = false;

  uploadFile(file) { 
    this.fileName = file.data.name;
    const formData = new FormData();  
    formData.append('formFile', file.data);  
    file.inProgress = true;  
    this.uploadService.upload(formData).pipe(  
      map(event => {  
        switch (event.type) {  
          case HttpEventType.UploadProgress:  
            if (event.total) {
              const total:number = event.total;
              file.progress = Math.round(event.loaded * 100 / total);  
            }
            else {
              //todo: throw error cannot read file
            }
            break;
          case HttpEventType.Response:
            console.log("RESPONSE FROM SERVER:", event);
            break;
          return event;
        }  
      }),  
      catchError((error: HttpErrorResponse) => {  
        file.inProgress = false;  
        return of(`${file.data.name} upload failed.`);  
      })).subscribe((event: any) => {  
        if (typeof (event) === 'object') {  
          console.log(event.body);  
        }  
      });  
  }

  private uploadFiles() {  
    this.fileUpload.nativeElement.value = '';  
    this.files.forEach(file => {  
      this.uploadFile(file);  
    });  
}

onClick() {  
  this.clicked = true;
  const fileUpload = this.fileUpload.nativeElement;fileUpload.onchange = () => {  
  for (let index = 0; index < fileUpload.files.length; index++)  
  {  
   const file = fileUpload.files[index];  
   console.log("current file name: ", file.name);
   console.log("arr of files: ",fileUpload.files);
   this.files.push({ data: file, inProgress: false, progress: 0});  
  }  
    this.uploadFiles();  
  };  
  fileUpload.click();  
}

  ngOnInit(): void {
  }

}
