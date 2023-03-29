import { HttpClient, HttpRequest, HttpEventType, HttpResponse, HttpHeaders } from '@angular/common/http'
import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})

export class FileUploadComponent implements OnInit {
  baseUrl: string | undefined;
  progress: number | undefined;
  profileForm: FormGroup;
  @ViewChild('labelImport', { static: true })  labelImport: ElementRef | undefined;

  constructor(private http: HttpClient) { 
    this.profileForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(2)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      picture: new FormControl('', [Validators.required])
    })

  }

  get form() {
    return this.profileForm.controls;
  }

  onSubmit() {
    console.log(this.profileForm.value, this.profileForm.valid);
    const formData = new FormData();
    for (const key of Object.keys(this.profileForm.value)) {
      const value = this.profileForm.value[key];
      formData.append(key, value);
    }
    this.http.post(this.baseUrl + 'FileManagement/createprofile', formData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe((event:any) => {
      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round((100 * event.loaded) / event.total);
      }
      if (event.type === HttpEventType.Response) {
        console.log(event.body);
        this.profileForm.reset();
      }
    });
  }


  onFileChanged(event:any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      if(this.labelImport){
        this.labelImport.nativeElement.innerText = file.name;
      }
  
      this.profileForm.patchValue({
        picture: file,
      });
    }
  }

  upload(files:any) {
    if (files?.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', this.baseUrl + 'FileManagement/upload', formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe((event: any) => {
      if (event.type === HttpEventType.UploadProgress) {
        this.progress = Math.round(100 * event.loaded / event.total);
      };
    });
  }

  


  ngOnInit(): void {
  }

}
