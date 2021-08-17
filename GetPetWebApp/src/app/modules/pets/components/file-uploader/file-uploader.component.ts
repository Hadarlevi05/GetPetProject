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
  
  constructor(private uploadService: UploadService) {}

  fileName: string = '';
  imgPath: string = '';

onClick() {  
  const fileUpload = this.fileUpload.nativeElement;
  fileUpload.onchange = () => {  
   console.log("fileUpload ref: ", fileUpload);
   this.file = fileUpload.files[0];
   this.fileName = this.file.name;
   console.log("in file-uploader.component, file is: ", this.file);
   console.log("Type of uploaded file is: " + typeof(this.file));
   this.fileUpload.nativeElement.value = '';
  };  
  fileUpload.click();  
}

  ngOnInit(): void {
  }

}
