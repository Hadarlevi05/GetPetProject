import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
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
  }

onClick() {  
  const fileUpload = this.fileUpload.nativeElement;
  fileUpload.onchange = () => {  
   console.log("fileUpload ref: ", fileUpload);
   const file = fileUpload.files[0];
   console.log("file is: ", file);
   this.file = { data: file, inProgress: false, progress: 0};
   this.fileUpload.nativeElement.value = '';
   this.uploadFile(this.file);  
  };  
  fileUpload.click();  
}

  ngOnInit(): void {
    this.file = { data: null, inProgress: false, progress: 0};
  }

}
