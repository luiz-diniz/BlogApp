import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserRegisterModel } from '../../../models/user.register.model';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign.up.component.html',
  styleUrl: './sign.up.component.scss'
})
export class SignUpComponent implements OnInit{

  signupForm: FormGroup;

  @ViewChild('fileUploader') fileUploader: ElementRef;

  constructor(private userService: UsersService) {
  }

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      username: new FormControl("", [Validators.required, Validators.max(100), Validators.pattern(/[\S]/)]),
      email: new FormControl("", [Validators.required, Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/)]), 
      password: new FormControl("", [Validators.required]),
      description: new FormControl(""), 
      profileImageContent: new FormControl("")
    })
  }

  submit(){
    let user = new UserRegisterModel();

    user.username = this.signupForm.value["username"];
    user.email = this.signupForm.value["email"];
    user.password = this.signupForm.value["password"];
    user.description = this.signupForm.value["description"];
    user.profileImageContent = this.signupForm.value["profileImageContent"];

    
    this.userService.add(user).subscribe({
      next: () => {
        // temporary
        alert(`User created!`);
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
