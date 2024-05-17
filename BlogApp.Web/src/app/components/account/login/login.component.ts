import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { LoginModel } from '../../../models/login.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit{

  loginForm: FormGroup;
  errorMessage: string = '';

  loading: boolean;

  constructor(public authService: AuthenticationService, private router: Router) {      
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required, Validators.min(2), Validators.max(100), Validators.pattern(/[\S]/)]),
      password: new FormControl("", [Validators.required, Validators.min(10), Validators.max(255)]),
    })
  }

  submit(){
    this.loading = true;
    this.errorMessage = '';

    const loginInfo: LoginModel = this.loginForm.getRawValue();

    this.authService.login(loginInfo).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
      error: (response) => {
        this.loading = false;

        if(response.status === 0)
          this.errorMessage = "Site unavailable, try again later";
        else
         this.errorMessage = response.error;
      }
    }) 
  }
}