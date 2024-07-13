import { Component, inject, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts.reviews',
  templateUrl: './posts.reviews.component.html',
  styleUrl: './posts.reviews.component.scss'
})
export class PostsReviewsComponent implements OnInit{

  authService = inject(AuthenticationService);
  router = inject(Router);

  ngOnInit(){
    if(this.authService.roleSignal() !== 1){
      this.router.navigateByUrl('');
    }
  }
}