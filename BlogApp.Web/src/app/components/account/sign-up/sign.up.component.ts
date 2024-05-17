import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserRegisterModel } from '../../../models/user.register.model';
import { UsersService } from '../../../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign.up.component.html',
  styleUrl: './sign.up.component.scss'
})
export class SignUpComponent implements OnInit{

  signupForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  error: boolean;
  success: boolean;
  loading: boolean;

  @ViewChild('fileUploader') fileUploader: ElementRef;

  constructor(private userService: UsersService, private router: Router) {
  }

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      username: new FormControl("", [Validators.required, Validators.min(2), Validators.max(100), Validators.pattern(/[\S]/)]),
      email: new FormControl("", [Validators.required, Validators.max(254), Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)]), 
      password: new FormControl("", [Validators.required, Validators.min(10), Validators.max(255)]),
      passwordConfirmation: new FormControl("", [Validators.required, Validators.min(10), Validators.max(255)])
    })
  }

  submit(){
    this.loading = true;
    this.error = false;
    this.errorMessage = '';

    const user: UserRegisterModel = this.signupForm.getRawValue();

    this.userService.add(user).subscribe({
      next: () => {
        this.error = false;
        this.success = true;
        this.successMessage = "You'll be redirected to the login page";   
        
        setTimeout(() => {
          this.router.navigateByUrl('/login');
        }, 2000)
      },
      error: (response) => {
        this.error = true;
        this.loading = false;

        if(response.status === 0)
          this.errorMessage = "Site unavailable, try again later";
        else
         this.errorMessage = response.error;
      }
    });
  }

  getFile(event: Event){
    let target = event.target as HTMLInputElement;
    let files = target.files as FileList;
    let selectedFile = files[0];

    if(this.validateFileExtension(selectedFile)){
      alert('Invalid file extension. Valid extesions are: .jpg, .jpeg or .png')
      target.value = '';
    }
    else{
      let reader = new FileReader();

      reader.readAsDataURL(selectedFile);

      reader.onload = () => {
          this.signupForm.patchValue({
            profileImageContent: reader.result
          });
      };
    }
  }

  validateFileExtension(file: File){
    let extension = file.name.split('.').pop()?.toLowerCase();

    if(extension !== "jpg" && extension !== "jpeg" && extension !== "png")
      return true;

    return false;
  }

}
