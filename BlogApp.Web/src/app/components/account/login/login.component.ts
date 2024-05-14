import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { LoginModel } from '../../../models/login.model';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit{

  loginForm: FormGroup;

  constructor(public authService: AuthenticationService, private router: Router) {      
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required, Validators.max(100), Validators.pattern(/[\S]/)]),
      password: new FormControl("", [Validators.required])
    })
  }

  submit(){
    let loginInfo = new LoginModel;

    loginInfo.username = this.loginForm.value["username"];
    loginInfo.password = this.loginForm.value["password"];

    this.authService.login(loginInfo).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
      error: error => {
        console.log(error);
      }
    })
  }
}